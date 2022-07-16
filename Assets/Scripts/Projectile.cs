using System;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] float _duration;
    [SerializeField] int _damageAmount;
    [SerializeField] bool _rotate = true;
    [SerializeField] float _rotationSpeed = 0.01f;

    private DamageNumberController _damageNumberController;
    
    Rigidbody2D _rigidbody;
    Collider2D _collider;
    float _count;
    Gun _gun;

    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _damageNumberController = FindObjectOfType<DamageNumberController>();
    }

    void Update() {
        _count -= Time.deltaTime;
        if (_rotate) {
            transform.Rotate(0f, 0f,  Time.deltaTime * _rotationSpeed);
        }
        
        if (_count <= 0) {
            _gun.AddToPool(this);
        }
    }

    void OnEnable() {
        _count = _duration;
        transform.localRotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other == null) return;
        
        if (other.GetComponent<Projectile>() != null) {
            _gun.AddToPool(this);
            return;
        }

        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        
        if (damageable != null) {
             int damage = Random.Range(1, _damageAmount + 1);
            damageable.TakeDamage(_damageAmount);
            if (_damageNumberController) _damageNumberController.SpawnDamageNumber(damage, transform.position);
            _gun.AddToPool(this);
            return;
        }

        _gun.AddToPool(this);
    }

    void OnCollisionEnter2D(Collision2D other) {
        _gun.AddToPool(this);
    }

    public void Launch(Vector3 position, Vector3 velocity) {
        transform.position = position;
        _rigidbody.velocity = velocity;
    }

    public void SetGun(Gun gun) => _gun = gun;

    public int GetDamage() => _damageAmount;
}