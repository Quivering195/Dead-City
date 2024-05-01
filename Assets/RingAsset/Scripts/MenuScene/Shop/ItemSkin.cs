using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkin : MonoBehaviour
{
    public Button _button;
    public Text _text;
    public Transform _isCheckClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        Debug.Log(_text.text);
        transform.parent.GetComponent<CreateSkin>().DisableClick();
        _isCheckClick.gameObject.SetActive(true);
        int value = Convert.ToInt32(_text.text);
        GameManager.Instance.ChangeSkin(value);
        GameManager.Instance._gameController._currentBuy = value;
        //check đã mua hay chưa , nếu mua rồi thì không cho mua nữa
        if (GameManager.Instance._dataGame._listSkin.Contains(value))
        {
            UiManager.Instance._uiController._buySkin.interactable = false;
            UiManager.Instance._uiController._priceSkin.gameObject.SetActive(false);
            UiManager.Instance._uiController._doneSkin.gameObject.SetActive(true);
        }
        else
        {
            UiManager.Instance._uiController._buySkin.interactable = true;
            UiManager.Instance._uiController._priceSkin.gameObject.SetActive(true);
            UiManager.Instance._uiController._doneSkin.gameObject.SetActive(false);
        }
    }
}
