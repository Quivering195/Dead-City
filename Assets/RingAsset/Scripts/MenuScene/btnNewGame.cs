using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class btnNewGame : MonoBehaviour
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
        // Xóa dữ liệu game cũ nếu có
        string filePath = Path.Combine(Application.persistentDataPath, "DataGame");
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        GameManager.Instance._dataGame = new DataGame(new List<int>() { 0 }, new List<int>() { 0 },
            0, 0, 10f,
            50f, 100,
            5000, 1, new List<string>());
        GameManager.SaveDataGame(GameManager.Instance._dataGame);
        StartCoroutine(LoadSceneCoroutine(Settings.SceneName1));
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