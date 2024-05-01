using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupShop : MonoBehaviour
{
    private void OnDisable()
    {
        GameManager.Instance.ChangeSkin(GameManager.Instance._dataGame.currentSkin);
    }
}
