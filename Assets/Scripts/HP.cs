using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField]
    float _health = 100f;

    public float Health { get => _health; set => _health = value; }

    public void TakeDamage(float amount)
    {
        if (_health > 0f) _health -= amount;
        if (_health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
