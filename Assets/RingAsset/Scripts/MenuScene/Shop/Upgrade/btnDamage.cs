using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnDamage : MonoBehaviour
{
    public Text _curretnValue;
    public Text _plusValue;

    public Button _button;

    // Start is called before the first frame update
    void Start()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        if (GameManager.Instance._dataGame.money < 200)
        {
            return;
        }

        GameManager.Instance._dataGame.money -= 200;
        UiManager.Instance._uiController._money.text = "Money: " + GameManager.Instance._dataGame.money.ToString();
        GameManager.Instance._dataGame.damage += 10;
        _curretnValue.text = GameManager.Instance._dataGame.damage.ToString();
        _plusValue.text = (GameManager.Instance._dataGame.damage + 10).ToString();
        GameManager.SaveDataGame(GameManager.Instance._dataGame);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
