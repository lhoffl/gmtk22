using UnityEngine;
public class Damageable : MonoBehaviour {
    [SerializeField] Health _health;

    public void TakeDamage(int amount) {
        Debug.Log(amount);
        if (_health != null) 
            _health.Remove(Mathf.Abs(amount));
    }

    public void Heal(int amount) {
        if(_health != null)
            _health.Add(Mathf.Abs(amount));
    }
}
