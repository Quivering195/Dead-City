using System.Collections;
using Ring;
using UnityEngine;

public class ChangeOptions : MonoBehaviour
{
    public ChangeSkins changeSkins;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Editor

    #region Skin

#if UNITY_EDITOR
    private void OnValidate()
    {
        ChangeSkin();
    }
#endif

    public void ChangeSkin()
    {
        // Tắt tất cả các skin trước khi kích hoạt skin mới
        foreach (GameObject skinObject in changeSkins.listObjectSkins)
        {
            skinObject.SetActive(false);
        }
        switch (changeSkins.selectedSkin)
        {
            case PlayerSkin.Skin1:
                changeSkins.listObjectSkins[0].SetActive(true);
                Debug.Log("Skin1");
                break;
            case PlayerSkin.Skin2:
                changeSkins.listObjectSkins[1].SetActive(true);
                Debug.Log("Skin2");
                break;
            case PlayerSkin.Skin3:
                changeSkins.listObjectSkins[2].SetActive(true);
                Debug.Log("Skin3");
                break;
            case PlayerSkin.Skin4:
                changeSkins.listObjectSkins[3].SetActive(true);
                Debug.Log("Skin4");
                break;
            case PlayerSkin.Skin5:
                changeSkins.listObjectSkins[4].SetActive(true);
                Debug.Log("Skin5");
                break;
            case PlayerSkin.Skin6:
                changeSkins.listObjectSkins[5].SetActive(true);
                Debug.Log("Skin6");
                break;
            case PlayerSkin.Skin7:
                changeSkins.listObjectSkins[6].SetActive(true);
                Debug.Log("Skin7");
                break;
            case PlayerSkin.Skin8:
                changeSkins.listObjectSkins[7].SetActive(true);
                Debug.Log("Skin8");
                break;
            case PlayerSkin.Skin9:
                changeSkins.listObjectSkins[8].SetActive(true);
                Debug.Log("Skin9");
                break;
            case PlayerSkin.Skin10:
                changeSkins.listObjectSkins[9].SetActive(true);
                Debug.Log("Skin10");
                break;
            case PlayerSkin.Skin11:
                changeSkins.listObjectSkins[10].SetActive(true);
                Debug.Log("Skin11");
                break;
            case PlayerSkin.Skin12:
                changeSkins.listObjectSkins[11].SetActive(true);
                Debug.Log("Skin12");
                break;
            case PlayerSkin.Skin13:
                changeSkins.listObjectSkins[12].SetActive(true);
                Debug.Log("Skin13");
                break;
            case PlayerSkin.Skin14:
                changeSkins.listObjectSkins[13].SetActive(true);
                Debug.Log("Skin14");
                break;
            case PlayerSkin.Skin15:
                changeSkins.listObjectSkins[14].SetActive(true);
                Debug.Log("Skin15");
                break;
            // Và tiếp tục cho các skin khác
            default:
                break;
        }
    }

    #endregion

    #endregion
}
