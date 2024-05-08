using System;
using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : RingSingleton<UIGameManager>
{
    public UiGameController _uiGameController;
    public int _killUpdateInGame;
    public bool isCursorVisible = false;

    public Text _missionKill;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMoneyInGame(GameManager.Instance._dataGame.money);
        HideCursor();
        //bắt đầu thời gian đếm
        _uiGameController._timer.StartTimer();
        _killUpdateInGame = 0;
        _missionKill.text = "Mission: " + ListZombieController.Instance._listZombies.Count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCursorVisible = !isCursorVisible;

            if (!isCursorVisible)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            Cursor.visible = isCursorVisible;
        }
    }

    public void UpdateMoneyInGame(int value)
    {
        _uiGameController._money.text = "Money: " + value;
    }

    public void UpdateMoneyInGameWhenKillZombie(int value)
    {
        GameManager.Instance._dataGame.money += value;
        _uiGameController._money.text = "Money: " + GameManager.Instance._dataGame.money;
        GameManager.Instance.SaveMoney(GameManager.Instance._dataGame.money);
    }

    #region Mouse

    void HideCursor()
    {
        // Ẩn con trỏ chuột
        Cursor.visible = false;
    }

    // Đây chỉ là ví dụ, bạn có thể gọi hàm này trong bất kỳ sự kiện nào phù hợp
    void ShowCursor()
    {
        // Hiện con trỏ chuột
        Cursor.visible = true;
    }

    #endregion
}
