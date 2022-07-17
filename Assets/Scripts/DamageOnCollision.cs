using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] int _damageAmountOnImpact = 1;
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            Debug.Log("Dealing damage to " + other.gameObject.name);
            other.gameObject.GetComponent<Damageable>().TakeDamage(_damageAmountOnImpact);
        }
    }
}
