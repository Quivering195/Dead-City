using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnQuitMenuScene : MonoBehaviour
{
    public Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        SceneManager.LoadScene("MenuScene");
        UiManager.Instance._uiController._allButtons.gameObject.SetActive(true);
        UiManager.Instance._uiController._loadScene.gameObject.SetActive(true);
    }
}