using UnityEngine;
using UnityEngine.UI;
public class BossHealthUI : MonoBehaviour {
    [SerializeField] Image _fillImage;
    [SerializeField] Health _bossHealth;
    
    void Start() {
        _bossHealth.OnHealthChanged += UpdateUI;
    }
    
    void UpdateUI(object sender, HealthChangedEventArgs e) {
        float amount = (float) e.Health / e.MaxHealth;
        _fillImage.fillAmount = amount;
    }

}
