using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float elapsedTime = 0f;
    private bool isRunning = false;
    public Text _time;

    private void Update()
    {
        _time.text = GetElapsedTime();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public string GetElapsedTime()
    {
        // Nếu đếm đang chạy, tăng thời gian trôi qua
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }

        // Tính toán phút và giây
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Trả về kết quả dưới dạng chuỗi "phút:giây"
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
