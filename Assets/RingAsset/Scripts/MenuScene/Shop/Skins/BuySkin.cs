using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySkin : MonoBehaviour
{
    public Button _button;
    public List<Transform> _listHeader;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        Debug.Log(_listHeader[0].transform.GetChild(1).name);
        if (_listHeader[0].transform.GetChild(1).gameObject.activeSelf)
        {
            //weapon
            if (GameManager.Instance._dataGame.money >= Settings.PriceWeapon)
            {
                GameManager.Instance.SaveMoney(-Settings.PriceWeapon);
                GameManager.Instance.SaveWeapon(GameManager.Instance._gameController._currentBuy);
                UiManager.Instance._uiController._buyItem.interactable = false;
                UiManager.Instance._uiController._priceItem.gameObject.SetActive(false);
                UiManager.Instance._uiController._doneItem.gameObject.SetActive(true);
            }
        }
        else if (_listHeader[1].transform.GetChild(1).gameObject.activeSelf)
        {
            //skin
            if (GameManager.Instance._dataGame.money >= Settings.PriceSkin)
            {
                GameManager.Instance.SaveMoney(-Settings.PriceSkin);
                GameManager.Instance.SaveSkin(GameManager.Instance._gameController._currentBuy);
                UiManager.Instance._uiController._buyItem.interactable = false;
                UiManager.Instance._uiController._priceItem.gameObject.SetActive(false);
                UiManager.Instance._uiController._doneItem.gameObject.SetActive(true);
            }
        }
        else
        {
            //upgrade
        }
    }
}
