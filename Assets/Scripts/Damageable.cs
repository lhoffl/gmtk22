using UnityEngine;
public class Damageable : MonoBehaviour {
    [SerializeField] Health _health;

    public bool Invincible;
    
    public void TakeDamage(int amount) {
        if (Invincible) return;
        
        if (_health != null) 
            _health.Remove(Mathf.Abs(amount));
    }

    public void Heal(int amount) {
        if(_health != null)
            _health.Add(Mathf.Abs(amount));
    }
}
