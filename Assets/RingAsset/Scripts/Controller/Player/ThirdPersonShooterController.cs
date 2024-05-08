using System.Collections;
using Lean.Pool;
using Ring;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ThirdPersonShooterController : RingSingleton<ThirdPersonShooterController>
{
    public ShooterController _shooterController;
    public bool isCheck;
    public Transform _transform;

    [FormerlySerializedAs("_transformButlletSpawn")]
    public Transform _prefabButlletSpawn;

    public Transform _positionSpawnBullet;


    // Giảm giá trị của _bullet.text
    string[] bulletInfo;
    int currentBullets;
    int totalBullets;

    private void Start()
    {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (Input.GetJoystickNames()[i].ToString().Equals("Controller (XBOX 360 For Windows)"))
            {
                isCheck = true;
                break;
            }
        } // Giảm giá trị của _bullet.text

        _shooterController._starterAssetsInputs.shoot = true;
        _shooterController._isCheckReload = false;
        bulletInfo = UIGameManager.Instance._uiGameController._bulletCount.text.Split('/');
        currentBullets = int.Parse(bulletInfo[0]);
        totalBullets = int.Parse(bulletInfo[1]);
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
        if (_shooterController._starterAssetsInputs.reload && !_shooterController._isCheckReload)
        {
            _shooterController._isCheckReload = true;
            _shooterController._animator.SetLayerWeight(2, 1);
            _shooterController._animator.SetTrigger("Reload");
        }
    }

    public void StartReload()
    {
        _shooterController._isCheckReload = true;
        if (totalBullets > 0)
        {
            currentBullets = 0;
            if (GameManager.Instance._dataGame.currentWeapon == 2)
            {
                currentBullets += 45;
                totalBullets -= 45;
            }
            else
            {
                currentBullets += 60;
                totalBullets -= 60;
            }

            UIGameManager.Instance._uiGameController._bulletCount.text = currentBullets + "/" + totalBullets;
        }
    }

    public void EndReload()
    {
        _shooterController._isCheckReload = false;
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
            if (_shooterController._listMultiAim[0].data.sourceObjects.GetTransform(0) !=
                _shooterController._targetAim)
            {
                for (int i = 0; i < _shooterController._listMultiAim.Count; i++)
                {
                    var data = _shooterController._listMultiAim[i].data.sourceObjects;
                    data.SetTransform(0, _shooterController._targetAim);
                    _shooterController._listMultiAim[i].data.sourceObjects = data;
                    _shooterController._thirdPersonController._animator.enabled = false;
                    _shooterController._listRigAim.Build();
                    _shooterController._thirdPersonController._animator.enabled = true;
                }
            }
        }
        else
        {
            if (_shooterController._listMultiAim[0].data.sourceObjects.GetTransform(0) !=
                _shooterController._targetDontAim)
            {
                for (int i = 0; i < _shooterController._listMultiAim.Count; i++)
                {
                    var data = _shooterController._listMultiAim[i].data.sourceObjects;
                    data.SetTransform(0, _shooterController._targetDontAim);
                    _shooterController._listMultiAim[i].data.sourceObjects = data;
                    _shooterController._thirdPersonController._animator.enabled = false;
                    _shooterController._listRigAim.Build();
                    _shooterController._thirdPersonController._animator.enabled = true;
                }
            }

            _shooterController._thirdPersonController.SetRotationOnMove(true);
            _shooterController._aimVirtualCamera.gameObject.SetActive(false);
            if (isCheck)
            {
                SentivityConsole();
                Debug.LogWarning("Xbox");
            }
            else
            {
                SentivityKeyBoard();
                Debug.LogWarning("Key Board");
            }

            //reset shoot
            _shooterController._starterAssetsInputs.shoot = false;
        }

        if (_shooterController._starterAssetsInputs.shoot && _shooterController._starterAssetsInputs.aim &&
            !_shooterController._isCheckReload)
        {
            Debug.Log(1);
            //MusicManager.Instance.PlayAudio_Grenade();

            //_shooterController._starterAssetsInputs.shoot = false;

            if (currentBullets > 0)
            {
                Vector3 aimDir = (mouseWorltPosition - _positionSpawnBullet.position).normalized;
                Instantiate(_prefabButlletSpawn, _positionSpawnBullet.position,
                    Quaternion.LookRotation(aimDir, Vector3.up));
                currentBullets--;
                UIGameManager.Instance._uiGameController._bulletCount.text =
                    currentBullets.ToString() + "/" + totalBullets.ToString();
            }
            else
            {
                // Khi hết đạn, kích hoạt trigger reload
                //_shooterController._animator.SetTrigger("Reload");
            }
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
