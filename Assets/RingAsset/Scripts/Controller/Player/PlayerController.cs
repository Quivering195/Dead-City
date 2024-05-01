using System.Collections;
using Ring;
using UnityEngine;

public class PlayerController : RingSingleton<PlayerController>
{
    public PlayerComponent playerComponent;
    public PlayerSkins playerSkins;

    public void ResetFire()
    {
        playerComponent._thirdPersonShooterController._shooterController._isCheckReload = false;
        playerComponent._thirdPersonShooterController._shooterController._animator.SetLayerWeight(2, 0);
    }
}
