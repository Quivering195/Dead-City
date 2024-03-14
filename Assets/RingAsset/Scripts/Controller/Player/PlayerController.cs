using System.Collections;
using Ring;
using UnityEngine;

public class PlayerController : RingSingleton<PlayerController>
{
    public PlayerComponent playerComponent;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        Movement();
    }

    #region Move
    private void Movement()
    {
        playerMovement._moveX = Input.GetAxis("Horizontal");
        playerMovement._moveY = Input.GetAxis("Vertical");
        playerMovement._direction = transform.forward * playerMovement._moveY + transform.right * playerMovement._moveX;
        playerComponent._rigidbody.velocity = playerMovement._direction * playerMovement._speed * Time.deltaTime;
    }
    #endregion
}
