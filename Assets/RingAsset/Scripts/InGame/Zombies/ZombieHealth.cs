using System;
using System.Collections;
using System.Collections.Generic;
using Ring;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public DataZombies _dataZombies;
    public Animator _animator;
    public int _health;
    public int _speed;
    public ZombieSlow _zombieSlow;
    public Rigidbody _rigidbody;
    public List<Rigidbody> _ListRigidbody;
    public Collider _collider;

    private void Start()
    {
        _health = _dataZombies.Health;
        _speed = _dataZombies.Speed;
    }

    private void Update()
    {
        if (_health <= 0)
        {
            _ListRigidbody.ForEach(a => a.isKinematic = true);
            _ListRigidbody.ForEach(a => a.isKinematic = false);
            _animator.enabled = false;
            _zombieSlow.enabled = false;
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
            ListZombieController.Instance._listZombies.Remove(this);
            UIGameManager.Instance._killUpdateInGame++;
            UIGameManager.Instance._uiGameController._killZombies.text =
                UIGameManager.Instance._killUpdateInGame.ToString();
            GameManager.Instance.SaveMoney(100);
            UIGameManager.Instance._uiGameController._money.text = "Money: " + GameManager.Instance._dataGame.money;
            if (ListZombieController.Instance._listZombies.Count <= 0 &&
                !UIGameManager.Instance._uiGameController._win.gameObject.activeSelf &&
                GameManager.Instance._isCheckWin)
            {
                UIGameManager.Instance._uiGameController._win.gameObject.SetActive(true);
                Debug.Log("Win");
            }

            this.enabled = false;
        }
    }
}
