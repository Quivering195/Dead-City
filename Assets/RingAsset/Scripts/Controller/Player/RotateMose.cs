using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMose : MonoBehaviour
{
    // Tốc độ quay của object
    public float rotationSpeed = 5.0f;
    private float xRotation = 0f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        // Lấy di chuyển của chuột theo trục X và Y
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Tính góc quay dựa trên di chuyển của chuột
        float rotationX = mouseY * rotationSpeed * Time.deltaTime;
        float rotationY = mouseX * rotationSpeed * Time.deltaTime;
        xRotation += rotationY;
        transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
        // Quay object theo hướng quay của chuột

        transform.Rotate(Vector3.up * rotationX);
    }
}
