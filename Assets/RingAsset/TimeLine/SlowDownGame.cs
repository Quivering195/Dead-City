using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlowDownGame : MonoBehaviour
{
    public string sceneToLoad; // Tên của scene cần chuyển đến

    private void OnEnable()
    {
        ChangeScene();
    }

    // Phương thức này được gọi khi muốn chuyển đến scene khác
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
