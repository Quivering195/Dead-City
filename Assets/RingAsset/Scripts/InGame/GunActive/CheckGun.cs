using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckGun : MonoBehaviour
{
    void OnEnable()
    {
        if (GameManager.Instance._dataGame.currentWeapon == 0)
        {
            UIGameManager.Instance._uiGameController._bulletCount.text = "60/240";
        }
        else if (GameManager.Instance._dataGame.currentWeapon == 1)
        {
            UIGameManager.Instance._uiGameController._bulletCount.text = "60/240";
        }
        else if (GameManager.Instance._dataGame.currentWeapon == 2)
        {
            UIGameManager.Instance._uiGameController._bulletCount.text = "45/300";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
