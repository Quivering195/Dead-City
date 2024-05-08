using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangStateBoss : MonoBehaviour
{
    [SerializeField] private ZombieHealth _zombieHealth;
    [SerializeField] private ZombieSlow _zombieSlow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_zombieHealth._health <= _zombieHealth._dataZombies.Health*.3f)
        {
            _zombieSlow._attackState.damage *= 2;
        }
    }
}
