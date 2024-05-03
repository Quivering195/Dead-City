using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnHeaderInShop : MonoBehaviour
{
    public Button _btnUpgrade;
    public List<Transform> _listDisableBackgroundButton;

    public List<Transform> _enableBackgroundButton;
    public int count;
    public List<Transform> _popupShopBuy;
    public CameraShop _cameraShop;
    public Transform _rawImage;

    // Start is called before the first frame update
    void Start()
    {
        _btnUpgrade.onClick.AddListener(ActionClick);
    }

    private void ActionClick()
    {
        _listDisableBackgroundButton.ForEach(a => a.gameObject.SetActive(true));
        _enableBackgroundButton.ForEach(a => a.gameObject.SetActive(false));
        _enableBackgroundButton[count].gameObject.SetActive(true);
        _popupShopBuy.ForEach(a => a.gameObject.SetActive(false));
        _popupShopBuy[count].gameObject.SetActive(true);
        if (count == 1)
        {
            _cameraShop.BuySkin();
        }
        else
        {
            _cameraShop.OutBuySkin();
        }

        if (count == 0)
        {
            _rawImage.gameObject.SetActive(true);
        }
        else
        {
            _rawImage.gameObject.SetActive(false);
        }
    }
}
