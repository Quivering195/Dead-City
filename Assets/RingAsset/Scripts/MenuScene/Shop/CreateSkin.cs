using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSkin : MonoBehaviour
{
    public GameObject _prefabsItemSkin;
    public List<ItemSkin> _listItemWeapon;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject item = Instantiate(_prefabsItemSkin, transform);
            item.GetComponent<ItemSkin>()._text.text = (i + 1).ToString();
            _listItemWeapon.Add(item.GetComponent<ItemSkin>());
        }
    }

    public void DisableClick()
    {
        _listItemWeapon.ForEach(a => a._isCheckClick.gameObject.SetActive(false));
    }
}
