using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ring;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : RingSingleton<GameManager>
{
    public GameController _gameController;
    public DataGame _dataGame;
    public bool _isCheckWin;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        _dataGame = LoadDataGame();
        CreateData();
    }

    void CreateData()
    {
        PlayerMenuScene.Instance.playerSkins._listSkin.ForEach(a => a.gameObject.SetActive(false));
        PlayerMenuScene.Instance.playerSkins._listSkin[_dataGame.currentSkin].gameObject.SetActive(true);

        ChangeSkin(_dataGame.currentSkin); //lấy trang phục trước đó lưu
    }

    #region Save and Load

    #region Level

    public void SaveLevel(string name)
    {
        _dataGame.level = Convert.ToInt32(name);
        SaveDataGame(_dataGame);
    }

    public string LoadLevel()
    {
        return _dataGame.level.ToString();
    }

    #endregion

    #region Audio

    public void SaveMusic(float value)
    {
        PlayerPrefs.SetFloat("Music", value);
    }

    public float LoadMusic()
    {
        return PlayerPrefs.GetFloat("Music", 1);
    }

    public void SaveSound(float value)
    {
        PlayerPrefs.SetFloat("Sound", value);
    }

    public float LoadSound()
    {
        return PlayerPrefs.GetFloat("Sound", 1);
    }

    #endregion

    #region FPS

    public void SaveFPS(int value)
    {
        PlayerPrefs.SetInt("FPS", value);
    }

    public int LoadFPS()
    {
        return PlayerPrefs.GetInt("FPS", 1);
    }

    #endregion

    #region Skin Player

    public void SaveSkin(int value)
    {
        if (!_dataGame._listSkin.Contains(value))
        {
            _dataGame._listSkin.Add(value);
            _dataGame.currentSkin = value;
            SaveDataGame(_dataGame);
            _gameController._currentBuy = 0;
        }
    }


    public void ChangeSkin(int value)
    {
        PlayerMenuScene.Instance.playerSkins._listSkin.ForEach(a => a.gameObject.SetActive(false));
        PlayerMenuScene.Instance.playerSkins._listSkin[value].gameObject.SetActive(true);
    }

    #endregion

    #region Weapon Player

    public void SaveWeapon(int value)
    {
        if (!_dataGame._listWeapon.Contains(value))
        {
            _dataGame._listWeapon.Add(value);
            _dataGame.currentWeapon = value;
            SaveDataGame(_dataGame);
            _gameController._currentBuy = 0;
        }
    }

    public void ChangeWeapon(int value)
    {
        PlayerMenuScene.Instance.playerSkins._listWeapon.ForEach(a => a.gameObject.SetActive(false));
        PlayerMenuScene.Instance.playerSkins._listWeapon[value].gameObject.SetActive(true);
    }

    #endregion

    #region Money

    public void SaveMoney(int value)
    {
        _dataGame.money += value;
        if (UiManager.Instance != null)
        {
            UiManager.Instance.UpdateMoney(_dataGame.money);
        }

        SaveDataGame(_dataGame);
    }

    #endregion

    #endregion

    #region Json

    public static void SaveDataGame(DataGame data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "DataGame");
        string jsonData = JsonUtility.ToJson(data);

        // Tách mỗi biến xuống dòng
        string[] variables = jsonData.Split(',');
        jsonData = string.Join(",\n", variables);

        File.WriteAllText(filePath, jsonData);
    }


    public static DataGame LoadDataGame()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "DataGame");
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<DataGame>(jsonData);
        }
        else
        {
            DataGame _dataGame = new DataGame(new List<int>() { 0 }, new List<int>() { 0 }, 0, 0, 10f,
                50f, 100,
                5000, 0, new List<string>());
            SaveDataGame(_dataGame);
            string jsonData = File.ReadAllText(filePath);
            return JsonUtility.FromJson<DataGame>(jsonData);
        }
    }

    #endregion

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

[Serializable]
public class DataGame
{
    public List<int> _listSkin;
    public List<int> _listWeapon;
    public int currentSkin;
    public int currentWeapon;
    public float damage;
    public float speed;
    public int health;
    public int money;
    public int level;
    public List<string> timePlay;

    public DataGame(List<int> listSkin, List<int> listWeapon, int currentSkin,
        int currentWeapon, float damage,
        float speed, int health,
        int money, int level, List<string> timePlay)
    {
        _listSkin = listSkin;
        _listWeapon = listWeapon;
        this.currentSkin = currentSkin;
        this.currentWeapon = currentSkin;
        this.damage = damage;
        this.speed = speed;
        this.health = health;
        this.money = money;
        this.level = level;
        this.timePlay = timePlay;
    }
}
