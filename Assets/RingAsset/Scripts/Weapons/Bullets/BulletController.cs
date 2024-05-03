using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Transform vfxHitDamage;
    [SerializeField] private Transform vfxNoDamage;
    private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 40f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Start()
    {
        MusicManager.Instance.PlayAudio_Grenade();
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BulletTarget>() != null)
        {
            LeanPool.Spawn(vfxHitDamage, transform.position, Quaternion.identity);
        }
        else
        {
            LeanPool.Spawn(vfxNoDamage, transform.position, Quaternion.identity);
        }

        if (other.gameObject.CompareTag("ZombieBody"))
        {
            other.gameObject.GetComponent<ZombieHealth>()._health +=
                -(int)GameManager.Instance._dataGame.damage * (GameManager.Instance._dataGame.currentWeapon==2 ? 5:2)*5;
        }

        Destroy(gameObject);
    }
}
