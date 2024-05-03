using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PopupWin : MonoBehaviour
{
    public Text _kill;
    public Text _money;
    public Text _time;


    void OnEnable()
    {
        _kill.text = UIGameManager.Instance._killUpdateInGame.ToString();
        _money.text = "+" + (UIGameManager.Instance._killUpdateInGame * 100);
        UIGameManager.Instance._uiGameController._timer.StopTimer();
        _time.text = UIGameManager.Instance._uiGameController._timer.GetElapsedTime();
        DisableObject();
    }

    void DisableObject()
    {
        PlayerController.Instance._ThirdPersonShooterController.enabled = false;
        PlayerController.Instance._ThirdPersonController.enabled = false;
        PlayerController.Instance.enabled = false;
        PlayerController.Instance._starterAssetsInputs.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
