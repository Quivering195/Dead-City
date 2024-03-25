using System.Collections;
using Ring;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    public ShooterController _shooterController;
    public bool isCheck;

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
        if (_shooterController._starterAssetsInputs.aim)
        {
            _shooterController._aimVirtualCamera.gameObject.SetActive(true);
            _shooterController._thirdPersonController.SetSentivity(_shooterController._aimSentivity);
        }
        else
        {
            _shooterController._aimVirtualCamera.gameObject.SetActive(false);
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