using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }

    void TakeDamage(float _damage);
    void Die();

}
