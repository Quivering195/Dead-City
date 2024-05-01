using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyGame : MonoBehaviour
{
    public Text _money;

    // Start is called before the first frame update
    void OnEnable()
    {
        _money.text = "Money: " + GameManager.Instance._dataGame.money;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
