using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CreateSkin : MonoBehaviour
{
    public GameObject _prefabsItemSkin;
    public List<ItemSkin> _listItemSkins;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        if (_listItemSkins.Count <= 0)
        {
            for (int i = 0; i < 12; i++)
            {
                GameObject item = Instantiate(_prefabsItemSkin, transform);
                item.GetComponent<ItemSkin>()._text.text = (i + 1).ToString();
                _listItemSkins.Add(item.GetComponent<ItemSkin>());
            }
        }

        _listItemSkins[GameManager.Instance._dataGame.currentSkin].ActionClick();
    }

    public void DisableClick()
    {
        _listItemSkins.ForEach(a => a._isCheckClick.gameObject.SetActive(false));
    }
}
