using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayScene : MonoBehaviour
{
    public Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        UiManager.Instance._uiController._loadScene.gameObject.SetActive(true);
        UiManager.Instance._uiController._fillAmountController.isCheckLoadMap = true;
        GameManager.Instance.SaveLevel(SceneManager.GetActiveScene().name);
        StartCoroutine(LoadSceneCoroutine(SceneManager.GetActiveScene().name));
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
