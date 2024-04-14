using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class MusicManager : RingSingleton<MusicManager>
{
    public MusicController _musicController;

    private void Start()
    {
        PlayerBackGround();
    }

    public void PlayAudio_Grenade()
    {
        _musicController.audioSource_ShootGun.PlayOneShot(
            _musicController.listAudioClip_ShootGun[Random.Range(0, _musicController.listAudioClip_ShootGun.Count)]);
    }

    public void PlayerBackGround()
    {
        _musicController.audioSource_BackGround.Play();
    }
}
