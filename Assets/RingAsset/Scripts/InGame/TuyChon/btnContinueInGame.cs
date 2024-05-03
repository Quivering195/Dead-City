using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class btnContinueInGame : MonoBehaviour
{
    public Button _button;
    public Transform _transformOff;
    public StarterAssetsInputs _starterAssetsInputs;
    public ThirdPersonController _ThirdPersonController;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _transformOff.gameObject.SetActive(false);
        _starterAssetsInputs.enabled = true;
        _ThirdPersonController.enabled = true;
    }
}