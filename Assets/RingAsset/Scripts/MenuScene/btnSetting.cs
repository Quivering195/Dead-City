using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (!SceneManager.GetActiveScene().name.Equals("MenuScene"))
        {
            if (!UIGameManager.Instance.isCursorVisible)
            {
                return;
            }
        }

        _popupSetting.gameObject.SetActive(true);
        _transformOff.gameObject.SetActive(false);
    }
}
