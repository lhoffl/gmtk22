using UnityEngine;
public class Damageable : MonoBehaviour {
    [SerializeField] Health _health;

    public bool Invincible;

    private DamageNumberController _damageNumberController;
    
    public void Awake()
    {
        _damageNumberController = FindObjectOfType<DamageNumberController>();
    }

    public void TakeDamage(int amount) {
        if (Invincible) return;
        
        if (_health != null) 
        {
            _health.Remove(Mathf.Abs(amount));
            if (_damageNumberController && amount != int.MaxValue) _damageNumberController.SpawnDamageNumber(Mathf.Abs(amount), transform.position);
        }
            
    }

    public void Heal(int amount) {
        if(_health != null)
            _health.Add(Mathf.Abs(amount));
    }
}
