using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : RingSingleton<GameManager>
{
    public GameController _gameController;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        PlayerPrefs.SetFloat("A", 1);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #region Method Game

    public bool CheckUIReturn()
    {
        #region Kiểm tra xem có nhấn va UI nào không , nếu không thì return

#if UNITY_EDITOR || UNITY_STANDALONE
        if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
            if (selectedObj != null)
            {
                return true;
            }
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                GameObject selectedObj = EventSystem.current.currentSelectedGameObject;
                if (selectedObj != null)
                {
                    return true;
                }
            }
        }
#endif

        #endregion Kiểm tra xem có nhấn va UI nào không , nếu không thì return

        return false;
    }

    #endregion Method Game
}