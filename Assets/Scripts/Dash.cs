using System;
using UnityEngine;
public class Dash : MonoBehaviour {

    [SerializeField] KeyCode _inputKey;
    [SerializeField] float _speed = 3;
    [SerializeField] float _dashLength = 3;
    [SerializeField] float _cooldown = 3;
    [SerializeField] Sprite _invSprite;
    
    float _timeSinceLastDash = 0;
    Rigidbody2D _rigidbody;
    PlayerController _player;
    SpriteRenderer _renderer;
    
    Sprite _originalSprite;
    
    public float Cooldown => _cooldown;
    
    Vector3 _dashDirection;
    Vector3 _startPosition;

    public event Action OnDashStarted;
    
    void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
        _originalSprite = _player.GetComponent<SpriteRenderer>().sprite;
        _renderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (Vector3.Distance(_startPosition, transform.position) >= _dashLength) {
            _player.MovementEnabled(true);
            _player.GetComponent<Damageable>().Invincible = false;
            _renderer.sprite = _originalSprite;
            return;
        }
        
        if (_timeSinceLastDash < _cooldown && _timeSinceLastDash > 0) {
            _rigidbody.AddForce(_dashDirection * _speed * Time.deltaTime);
        }
    }
    
    void Update() {
        _timeSinceLastDash -= Time.deltaTime;
        if (_timeSinceLastDash > 0) return;

        _player.MovementEnabled(true);
        _player.GetComponent<Damageable>().Invincible = false;
        _renderer.sprite = _originalSprite;
        
        if (Input.GetKeyDown(_inputKey)) {
            OnDashStarted?.Invoke();
            DashTowardsMouse();
            _timeSinceLastDash = _cooldown;
            _player.MovementEnabled(false);
            _player.GetComponent<Damageable>().Invincible = true;
            _renderer.sprite = _invSprite;
        }
    }
    
    void DashTowardsMouse() {
        _startPosition = transform.position;
        var direction = (FixedScreenToWorldPoint() - transform.position) * _dashLength;
        _dashDirection = direction;
    }

    Vector3 FixedScreenToWorldPoint() {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
}
