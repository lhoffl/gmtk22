using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HealthTextUI : MonoBehaviour {
    [SerializeField] Image[] _diceFaceUI;
    [SerializeField] Color _damagedColor;
    [SerializeField] Color _healthyColor;

    Health _health;
    
    void Start() {
        if (PlayerController.Instance == null) return;
        _health = PlayerController.Instance.GetComponent<Health>();
        if (_health == null) return;
        _health.OnHealthChanged += UpdateUI;
        Reset();
        UpdateUI(null, new HealthChangedEventArgs {Health = _health.CurrentHealth, MaxHealth = _health.MaxHealth});
    }
    
    void UpdateUI(object sender, HealthChangedEventArgs e) {
        if (_diceFaceUI == null) return;
        for(int i = 0; i < _diceFaceUI.Length; i++) {
            if (_diceFaceUI[i] == null) return;
            _diceFaceUI [i] .color = e.Health> i ? _healthyColor : _damagedColor;
        }
    }

    void Reset() {
        for(int i = 0; i < _diceFaceUI.Length; i++) {
            _diceFaceUI [i] .color = _healthyColor;
            if (i > _health.MaxHealth - 1) {
                _diceFaceUI [i].enabled = false;
            }
        }
    }
}