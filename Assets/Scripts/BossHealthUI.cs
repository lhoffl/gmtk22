using UnityEngine;
using UnityEngine.UI;
public class BossHealthUI : MonoBehaviour {
    [SerializeField] Image _fillImage;
    [SerializeField] Health _bossHealth;
    
    void Start() {
        _bossHealth.OnHealthChanged += UpdateUI;
    }
    
    void UpdateUI(object sender, HealthChangedEventArgs e) {
        _fillImage.fillAmount = (e.Health / e.MaxHealth);
    }

}
