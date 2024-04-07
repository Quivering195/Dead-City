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
    private float _speed = 10f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletTarget>() != null)
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
