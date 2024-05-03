using System;
using System.Collections;
using Ring;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : RingSingleton<PlayerController>
{
    public PlayerComponent playerComponent;
    public PlayerSkins _playerSkins;
    public PlayerWeapon _playerWeapon;
    public PlayerHealth _playerHealth;
    public StarterAssetsInputs _starterAssetsInputs;
    public ThirdPersonController _ThirdPersonController;
    public ThirdPersonShooterController _ThirdPersonShooterController;

    public void ResetFire()
    {
        playerComponent._thirdPersonShooterController._shooterController._isCheckReload = false;
        playerComponent._thirdPersonShooterController._shooterController._animator.SetLayerWeight(2, 0);
    }

    private void Start()
    {
        ChangeSkins();
        ChangeWeapon();
    }

    private void ChangeSkins()
    {
        _playerSkins._listSkin.ForEach(a => a.gameObject.SetActive(false));
        _playerSkins._listSkin[GameManager.Instance._dataGame.currentSkin].gameObject.SetActive(true);
        _playerHealth._health = GameManager.Instance._dataGame.health;
        _playerHealth._giap = 100;
    }

    private void ChangeWeapon()
    {
        _playerWeapon._listWeapons.ForEach(a => a.gameObject.SetActive(false));
        _playerWeapon._listWeapons[GameManager.Instance._dataGame.currentWeapon].gameObject.SetActive(true);
    }

    public void GetAttack(int damage)
    {
        playerComponent._animator.SetBool("Hit", true);
        if (UIGameManager.Instance._uiGameController._giapInGUI.fillAmount > 0)
        {
            _playerHealth._giap += damage;
            UIGameManager.Instance._uiGameController._giapInGUI.fillAmount =
                ((float)_playerHealth._giap / 100);
        }
        else
        {
            _playerHealth._health += damage;
            UIGameManager.Instance._uiGameController._healthInGUI.fillAmount =
                ((float)_playerHealth._health / (float)GameManager.Instance._dataGame.health);
            if (UIGameManager.Instance._uiGameController._healthInGUI.fillAmount <= 0)
            {
                Debug.Log("Lose");
                UIGameManager.Instance._uiGameController._lose.gameObject.SetActive(true);
            }
        }
    }

    public void OutHit()
    {
        _starterAssetsInputs.enabled = true;
        _ThirdPersonController.enabled = true;
        playerComponent._animator.SetBool("Hit", false);
    }

    public void StartHit()
    {
        _starterAssetsInputs.enabled = false;
        _ThirdPersonController.enabled = false;
    }
}