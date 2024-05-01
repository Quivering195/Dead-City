using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FillAmountController : MonoBehaviour
{
    public Image fillImage;
    public float duration = 4f;
    public Transform imageCurrent;
    public bool isCheckLoadMap;
    public List<Transform> listObjectOffInPlayGame;

    void OnEnable()
    {
        StartCoroutine(FillImageCoroutine());
    }

    IEnumerator FillImageCoroutine()
    {
        float timer = 0f;
        while (timer < duration)
        {
            float fillAmount = Mathf.Lerp(0f, 1f, timer / duration);
            fillImage.fillAmount = fillAmount;
            timer += Time.deltaTime;
            yield return null;
        }

        // Đã đạt fill amount là 1, tắt game object
        imageCurrent.gameObject.SetActive(false);
        if (isCheckLoadMap)
        {
            isCheckLoadMap = false;
            listObjectOffInPlayGame.ForEach(a => a.gameObject.SetActive(false));
        }
    }
}
