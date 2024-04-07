using System.Collections;
using Lean.Pool;
using Ring;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class ThirdPersonShooterController : MonoBehaviour
{
    public ShooterController _shooterController;
    public bool isCheck;
    public Transform _transform;

    [FormerlySerializedAs("_transformButlletSpawn")]
    public Transform _prefabButlletSpawn;

    public Transform _positionSpawnBullet;

    private void Start()
    {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i].ToString().Equals("Controller (XBOX 360 For Windows)"))
            {
                isCheck = true;
                break;
            }
        }
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _shooterController._aimColliderLayerMask))
        {
            _transform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }

        MouseSentivity(mouseWorldPosition);
    }

    private void MouseSentivity(Vector3 mouseWorltPosition)
    {
        if (_shooterController._starterAssetsInputs.aim)
        {
            _shooterController._aimVirtualCamera.gameObject.SetActive(true);
            _shooterController._thirdPersonController.SetSentivity(_shooterController._aimSentivity);
            _shooterController._thirdPersonController.SetRotationOnMove(false);
            _shooterController._animator.SetLayerWeight(1,
                Mathf.Lerp(_shooterController._animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
            Vector3 worldAimTarget = mouseWorltPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _shooterController._thirdPersonController.SetRotationOnMove(true);
            _shooterController._aimVirtualCamera.gameObject.SetActive(false);
            _shooterController._animator.SetLayerWeight(1,
                Mathf.Lerp(_shooterController._animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            if (isCheck)
            {
                SentivityConsole();
                Debug.Log("Xbox");
            }
            else
            {
                SentivityKeyBoard();
                Debug.Log("Key Board");
            }
        }

        if (_shooterController._starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorltPosition - _positionSpawnBullet.position).normalized;

            LeanPool.Spawn(_prefabButlletSpawn, _positionSpawnBullet.position,
                Quaternion.LookRotation(aimDir, Vector3.up));
            _shooterController._starterAssetsInputs.shoot = false;
        }
    }

    private void SentivityKeyBoard()
    {
        _shooterController._thirdPersonController.SetSentivity(_shooterController._normalSentivity);
    }

    private void SentivityConsole()
    {
        _shooterController._thirdPersonController.SetSentivity(_shooterController._consoleSentivity);
    }
}
