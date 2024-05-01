using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnTutorial : MonoBehaviour
{
    public Button _button;
    public Transform _popupTutorial;
    public Transform _transformOff;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _popupTutorial.gameObject.SetActive(true);
        _transformOff.gameObject.SetActive(false);
    }
}
