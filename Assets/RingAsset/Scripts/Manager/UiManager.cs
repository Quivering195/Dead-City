using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class UiManager : RingSingleton<UiManager>
{
    public UiController _uiController;

    public void UpdateMoney(int value)
    {
        _uiController._money.text = "Money: " + value;
    }
}
