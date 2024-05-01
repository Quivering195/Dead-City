using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnRank : MonoBehaviour
{
    public Button _button;
    public Transform _popupRank;
    public Transform _transformOff;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _popupRank.gameObject.SetActive(true);
        _transformOff.gameObject.SetActive(false);
    }
}
