using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class btnContinueInGame : MonoBehaviour
{
    public Button _button;
    public Transform _transformOff;


    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _transformOff.gameObject.SetActive(false);
        PlayerController.Instance._starterAssetsInputs.enabled = true;
        PlayerController.Instance._ThirdPersonController.enabled = true;
    }
}
