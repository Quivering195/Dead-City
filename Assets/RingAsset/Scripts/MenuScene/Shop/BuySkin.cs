using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
    public Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        //chưa lưu thành công
        GameManager.Instance.SaveSkin(GameManager.Instance._gameController._currentBuy);
        UiManager.Instance._uiController._buySkin.interactable = false;
        UiManager.Instance._uiController._priceSkin.gameObject.SetActive(false);
        UiManager.Instance._uiController._doneSkin.gameObject.SetActive(true);
    }
}
