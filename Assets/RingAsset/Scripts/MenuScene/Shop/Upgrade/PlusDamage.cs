using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusDamage : MonoBehaviour
{
    public Text _curretnValue;

    private void OnEnable()
    {
        _curretnValue.text = (GameManager.Instance._dataGame.speed + 10).ToString();
    }
}
