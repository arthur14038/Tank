using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    float Health;

    public void DealDamage(float damage)
    {
        if (Health > 0)
            Health -= damage;
        if (Health <= 0)
            Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
        DeadEffect();
    }

    protected virtual void DeadEffect()
    {

    }
}
