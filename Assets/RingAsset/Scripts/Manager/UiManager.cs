using System;
using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class UiManager : RingSingleton<UiManager>
{
    public UiController _uiController;
    public List<Transform> _listGunInShop;

    public void UpdateMoney(int value)
    {
        _uiController._money.text = "Money: " + value;
    }

    private void OnEnable()
    {
        _listGunInShop[GameManager.Instance._dataGame.currentWeapon].gameObject.SetActive(true);
    }
}