using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnShop : MonoBehaviour
{
    public Button _button;
    public Transform _popupShop;
    public Transform _transformOff;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _popupShop.gameObject.SetActive(true);
        _transformOff.gameObject.SetActive(false);
    }
}
