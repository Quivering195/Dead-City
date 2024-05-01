using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackScene : MonoBehaviour
{
    public Button _button;
    public Transform _transformOn;
    public Transform _popupOff;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _transformOn.gameObject.SetActive(true);
        _popupOff.gameObject.SetActive(false);
    }
}
