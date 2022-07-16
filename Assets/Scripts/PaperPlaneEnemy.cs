using UnityEngine;
public class PaperPlaneEnemy : Enemy {
    [SerializeField] int _damageAmountOnImpact = 3;
    
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.GetComponent<PlayerController>() != null) {
            other.gameObject.GetComponent<Damageable>().TakeDamage(_damageAmountOnImpact);
            GetComponent<Damageable>().TakeDamage(int.MaxValue);
        }
    }
}