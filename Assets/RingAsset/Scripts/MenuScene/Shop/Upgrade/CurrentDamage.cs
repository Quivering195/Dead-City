using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDamage : MonoBehaviour
{
    public Text _curretnValue;

    private void OnEnable()
    {
        _curretnValue.text = GameManager.Instance._dataGame.damage.ToString();
    }
}
