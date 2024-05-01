using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShop : MonoBehaviour
{
    public CinemachineVirtualCamera _cameraShop;

    private void OnEnable()
    {
        _cameraShop.enabled = true;
    }

    private void OnDisable()
    {
        _cameraShop.enabled = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}