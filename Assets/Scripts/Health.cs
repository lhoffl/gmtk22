using System;
using UnityEngine;
public class Health : MonoBehaviour {
    [SerializeField] protected int _maxHealth = 100;

    protected int _health;

    public event EventHandler<HealthChangedEventArgs> OnHealthChanged;

    public bool IsDead => _health <= 0;
    public int CurrentHealth => _health;

    public void Add(int value) {
        value = Mathf.Max(value, 0);
        _health = Mathf.Min(_health + value, _maxHealth);
    }

    public void Remove(int value) {
        value = Mathf.Max(value, 0);
        _health = Mathf.Max(_health - value, 0);

        if (_health <= 0) {
            HandleDeath();
        }
    }

    void HandleHealthChanged(int oldValue, int newValue) {
        OnHealthChanged?.Invoke(this, new HealthChangedEventArgs {
            Health = _health,
            MaxHealth = _maxHealth
        });
    }

    void HandleDeath() => gameObject.SetActive(false);
}
