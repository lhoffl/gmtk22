using UnityEngine;
public class Damageable : MonoBehaviour {
    [SerializeField] Health _health;

    public void TakeDamage(int amount) {

        int remainder = amount;
        
        if (remainder > 0 && _health != null)
            _health.Remove(Mathf.Abs(remainder));
    }

    public void Heal(int amount) {
        int remainder = amount;
        
        if(remainder > 0 && _health != null)
            _health.Add(remainder);
    }
}
