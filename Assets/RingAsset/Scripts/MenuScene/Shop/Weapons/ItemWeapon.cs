using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWeapon : MonoBehaviour
{
    public Button _button;
    public Text _text;
    public Transform _isCheckClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    public void ActionClick()
    {
        transform.parent.GetComponent<CreateWeapon>().DisableClick();
        _isCheckClick.gameObject.SetActive(true);
        int value = Convert.ToInt32(_text.text) - 1;
        //GameManager.Instance.ChangeWeapon(value);=> in game
        GameManager.Instance._gameController._currentBuy = value;
        if (value == 1 || value == 2)
        {
            UiManager.Instance._uiController._priceItem.GetComponent<Text>().text = 240.ToString();
        }

        //check đã mua hay chưa , nếu mua rồi thì không cho mua nữa
        if (GameManager.Instance._dataGame._listWeapon.Contains(value)) //đã có trong danh sách mua
        {
            Debug.Log(value);
            UiManager.Instance._uiController._buyItem.interactable = false;
            UiManager.Instance._uiController._priceItem.gameObject.SetActive(false);
            UiManager.Instance._uiController._doneItem.gameObject.SetActive(true);
            //chọn luôn current skins
            GameManager.Instance.SaveWeapon(GameManager.Instance._gameController._currentBuy);
            GameManager.Instance._dataGame.currentWeapon = value;
        }
        else
        {
            UiManager.Instance._uiController._buyItem.interactable = true;
            UiManager.Instance._uiController._priceItem.gameObject.SetActive(true);
            UiManager.Instance._uiController._doneItem.gameObject.SetActive(false);
        }
    }
}
