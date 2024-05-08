using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusHealth : MonoBehaviour
{
    public Text _curretnValue;

    private void OnEnable()
    {
        _curretnValue.text = (GameManager.Instance._dataGame.health + 10).ToString();
    }
}