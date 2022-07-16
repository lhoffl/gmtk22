using UnityEngine;
using UnityEngine.UI;
public class HealthTextUI : MonoBehaviour {
    [SerializeField] Image[] _diceFaceUI;
    [SerializeField] Color _damagedColor;
    [SerializeField] Color _healthyColor;

    Health _health;
    
    void Start() {
        _health = PlayerController.Instance.GetComponent<Health>();
        if (_health == null) return;
        _health.OnHealthChanged += UpdateUI;
        Reset();
    }
    
    void UpdateUI(object sender, HealthChangedEventArgs e) {
        for(int i = 0; i < _diceFaceUI.Length; i++) {
            _diceFaceUI [i] .color = e.Health> i ? _healthyColor : _damagedColor;
        }
    }

    void Reset() {
        for(int i = 0; i < _diceFaceUI.Length; i++) {
            _diceFaceUI [i] .color = _healthyColor;
        }
    }
}
