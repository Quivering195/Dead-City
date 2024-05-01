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

    // Update is called once per frame
    void Start()
    {
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

        Destroy(gameObject);
    }
}
