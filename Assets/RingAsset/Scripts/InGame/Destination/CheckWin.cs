using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance._isCheckWin = true;
            if (ListZombieController.Instance._listZombies.Count <= 0 &&
                !UIGameManager.Instance._uiGameController._win.gameObject.activeSelf &&
                GameManager.Instance._isCheckWin)
            {
                UIGameManager.Instance._uiGameController._win.gameObject.SetActive(true);
                Debug.Log("Win");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance._isCheckWin = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance._isCheckWin = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
