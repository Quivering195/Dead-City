using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
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
        int levelCount = Convert.ToInt32(SceneManager.GetActiveScene().name);
        levelCount++;
        if (levelCount >= 4)
        {
            levelCount = 1;
        }

        GameManager.Instance.SaveLevel(levelCount.ToString());

        SaveRank();


        StartCoroutine(LoadSceneCoroutine(levelCount.ToString()));
    }

    private static void SaveRank()
    {
        string newTime = UIGameManager.Instance._uiGameController._timer.GetElapsedTime();
        int newKill = UIGameManager.Instance._killUpdateInGame;
        Debug.Log(GameManager.Instance._dataGame.timePlay.Count);
        if (GameManager.Instance._dataGame.timePlay.Count <= 0)
        {
            //add vào luôn
            GameManager.Instance._dataGame.timePlay.Add(newTime);
            Debug.LogError(newKill);
        }
        else
        {
            /////
            DateTime timeA = DateTime.ParseExact(newTime, "HH:mm", null);
            List<DateTime> timeBList = GameManager.Instance._dataGame.timePlay
                .Select(time => DateTime.ParseExact(time, "HH:mm", null)).ToList();
            timeBList.Add(timeA);
            List<DateTime> sortedTimeList = timeBList.OrderBy(time => time).ToList();
            List<string> sortedTimeStringList = sortedTimeList.Select(time => time.ToString("HH:mm")).ToList();
            GameManager.Instance._dataGame.timePlay = sortedTimeStringList;
            Debug.LogError("Sort");
            /////
        }

        GameManager.SaveDataGame(GameManager.Instance._dataGame);
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
