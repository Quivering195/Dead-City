using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateWeapon : MonoBehaviour
{
    public GameObject _prefabsItemWeapon;
    public List<ItemWeapon> _listItemWeapon;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        if (_listItemWeapon.Count <= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject item = Instantiate(_prefabsItemWeapon, transform);
                item.GetComponent<ItemWeapon>()._text.text = (i + 1).ToString();
                _listItemWeapon.Add(item.GetComponent<ItemWeapon>());
            }
        }

        _listItemWeapon[GameManager.Instance._dataGame.currentWeapon].ActionClick();
    }

    public void DisableClick()
    {
        _listItemWeapon.ForEach(a => a._isCheckClick.gameObject.SetActive(false));
    }
}
