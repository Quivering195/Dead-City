using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnSetting : MonoBehaviour
{
    public Button _button;
    public Transform _transformOff;
    public Transform _popupSetting;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void Start()
    {
    }

    private void ActionClick()
    {
        _popupSetting.gameObject.SetActive(true);
        _transformOff.gameObject.SetActive(false);
    }
}
