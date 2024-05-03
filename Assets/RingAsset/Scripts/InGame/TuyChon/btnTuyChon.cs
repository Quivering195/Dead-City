using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class btnTuyChon : MonoBehaviour
{
    public Button _button;
    public Transform _popupTuyChon;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _popupTuyChon.gameObject.SetActive(true);
        PlayerController.Instance._starterAssetsInputs.enabled = false;
        PlayerController.Instance._ThirdPersonController.enabled = false;
    }
}
