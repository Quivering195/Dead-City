using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : RingSingleton<MusicManager>
{
    public MusicController _musicController;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("MenuScene"))
        {
            PlayerFire();
        }
        else
        {
            PlayerBackGround();
        }

        SetUpVolume();
    }

    public void SetUpVolume()
    {
        _musicController.audioSource_Fire.ForEach(a => a.volume = GameManager.Instance.LoadSound());
        _musicController.audioSource_ShootGun.volume = GameManager.Instance.LoadSound();
        _musicController.audioSource_BackGround.volume = GameManager.Instance.LoadMusic();
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

    public void PlayerFire()
    {
        _musicController.audioSource_Fire[0].Play();
        _musicController.audioSource_Fire[1].Play();
    }
}
