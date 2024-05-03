using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShop : MonoBehaviour
{
    public CinemachineVirtualCamera _cameraShop;

    public void BuySkin()
    {
        _cameraShop.enabled = true;
    }

    public void OutBuySkin()
    {
        _cameraShop.enabled = false;
    }
}
