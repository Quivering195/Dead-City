using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnContinue : MonoBehaviour
{
    public Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
        if (GameManager.Instance.LoadLevel() == null || GameManager.Instance.LoadLevel() == "")
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    private void ActionClick()
    {
    }
}
