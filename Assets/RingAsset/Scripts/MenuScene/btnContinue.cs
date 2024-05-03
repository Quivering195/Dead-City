using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnContinue : MonoBehaviour
{
    public Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
        string filePath = Path.Combine(Application.persistentDataPath, "DataGame");
        if (!File.Exists(filePath))
        {
            _button.interactable = false;
        }
        else
        {
            _button.interactable = true;
        }
    }

    private void ActionClick()
    {
        UiManager.Instance._uiController._loadScene.gameObject.SetActive(true);
        UiManager.Instance._uiController._fillAmountController.isCheckLoadMap = true;
        StartCoroutine(LoadSceneCoroutine(GameManager.Instance.LoadLevel())); //=> load lại scene cũ
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        // Load scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Chờ cho scene load xong
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Khi scene đã load xong, thực hiện các hành động tiếp theo tại đây
        // Ví dụ: tắt UI load scene
    }
}
