using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFPS : MonoBehaviour
{
    public Toggle _toggle30FPS;
    public Toggle _toggle60FPS;


    private void OnEnable()
    {
        bool is30FPS = GameManager.Instance.LoadFPS() == 0;
        _toggle30FPS.isOn = is30FPS;
        _toggle60FPS.isOn = !is30FPS;
    }

    public void SetFPSGame30()
    {
        if (_toggle30FPS.isOn)
        {
            GameManager.Instance.SaveFPS(0);
        }
        else
        {
            GameManager.Instance.SaveFPS(1);
        }
    }
}
