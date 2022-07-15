using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] float _duration;
    [SerializeField] int _damageAmount;
    
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    float _count;
    Gun _gun;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _gun = GetComponent<Gun>();
    }

    void Update() {
        _count -= Time.deltaTime;
        if (_count <= 0) {
            _gun.AddToPool(this);
        }
    }

    void OnEnable() => _count = _duration;

    void OnTriggerEnter2D(Collider2D other) {
        if (other == null) return;

        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable == null) return;

        damageable.TakeDamage(_damageAmount);
        _gun.AddToPool(this);
    }

    public void Launch(Vector3 position, Vector3 velocity) {
        transform.position = position;
        _rigidbody.velocity = velocity;
    }
}