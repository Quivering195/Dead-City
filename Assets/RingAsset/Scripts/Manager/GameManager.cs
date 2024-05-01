using System.Collections;
using System.Collections.Generic;
using System.IO;
using Ring;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : RingSingleton<GameManager>
{
    public GameController _gameController;
    public DataGame _dataGame;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        CreateData();
        _dataGame = LoadDataGame();
    }

    void CreateData()
    {
        PlayerController.Instance.playerSkins._listSkin.ForEach(a => a.gameObject.SetActive(false));
        PlayerController.Instance.playerSkins._listSkin[LoadSkin()].gameObject.SetActive(true);
        SaveDataGame(new DataGame(new List<int>() { 1, 2, 3 }, new List<int>() { 1, 2, 3 }, 0, 10f, 50f, 100, 5000));
    }

    #region Save and Load

    #region Level

    public void SaveLevel(string name)
    {
        PlayerPrefs.SetString("Level", name);
    }

    public string LoadLevel()
    {
        return PlayerPrefs.GetString("Level", null);
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
        PlayerPrefs.SetInt("SkinPlayer", value);
        _gameController._currentBuy = 0;
    }

    public void ChangeSkin(int value)
    {
        PlayerController.Instance.playerSkins._listSkin.ForEach(a => a.gameObject.SetActive(false));
        PlayerController.Instance.playerSkins._listSkin[value].gameObject.SetActive(true);
    }

    public int LoadSkin()
    {
        return PlayerPrefs.GetInt("SkinPlayer", 0);
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
            Debug.LogWarning("File not found: " + filePath);
            return null;
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

public class DataGame
{
    public List<int> _listSkin;
    public List<int> _listWeapon;
    public int currentSkin;
    public float damage;
    public float speed;
    public int health;
    public int money;

    public DataGame(List<int> listSkin, List<int> listWeapon, int currentSkin, float damage, float speed, int health,
        int money)
    {
        _listSkin = listSkin;
        _listWeapon = listWeapon;
        this.currentSkin = currentSkin;
        this.damage = damage;
        this.speed = speed;
        this.health = health;
        this.money = money;
    }
}
