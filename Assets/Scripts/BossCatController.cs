using System;
using System.Collections.Generic;
using UnityEngine;

public class BossCatController : MonoBehaviour
{
    [SerializeField] float _speed = 0.5f;
    public List<Transform> _positions = new List<Transform>();
    public int _currentPosition = 0;
    public Gun _gun;
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> _sprites;
    public int _hissingTimer = 0;
    Health _health;

    private int _startingHealth; 

    private int _graceFrames = 3000;

    // Start is called before the first frame update
    void Start()
    {
        _gun = GetComponent<Gun>();
        _gun.OnShotFired += HandleShotFired;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _health = GetComponent<Health>();
        _health.OnHealthChanged += HandleHealthChanged;
        _startingHealth = _health.CurrentHealth;
    }

    void HandleHealthChanged(object sender, HealthChangedEventArgs e) {
        float healthPercent = (float)e.Health / (float)_startingHealth;

        if (healthPercent > 0.50f && healthPercent < 0.75f && _gun) 
        {
            _gun.UpdateNumberOfProjectiles(5);
            _gun.UpdateSpread(40);
            _gun.UpdateRateOfFire(4.5f);
            _speed = 0.55f;
        }
        if (healthPercent > 0.25f && healthPercent < 0.50f && _gun) 
        {
            _gun.UpdateNumberOfProjectiles(7);
            _gun.UpdateSpread(50);
            _gun.UpdateRateOfFire(4.0f);
            _speed = 0.6f;
        }
        if (healthPercent > 0.0f && healthPercent < 0.25f && _gun) 
        {
            _gun.UpdateNumberOfProjectiles(10);
            _gun.UpdateSpread(60);
            _gun.UpdateRateOfFire(3.5f);
            _speed = 0.7f;
        }
        
        if (e.Health <= 0) {
            Debug.Log("Game over! You're winner!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (_graceFrames > 0) _graceFrames--;
        else TryShoot();
        TryHiss();
    }

    protected void Move() {
        transform.position = Vector3.Lerp(transform.position, _positions [_currentPosition].position, Time.deltaTime * _speed);
        ChooseNextPosition();
    }

    protected void NextPosition() {
        _currentPosition++;
        if (_currentPosition >= _positions.Count)
            _currentPosition = 0;
    }

    protected void ChooseNextPosition() {
        if (Vector3.Distance(transform.position, _positions [_currentPosition].position) <= 0.1) {
            transform.position = _positions [_currentPosition].position;
            NextPosition();
        }
    }

    protected virtual void TryShoot() {
        if (_gun == null) return;
       _gun.FireProjectile(); 
    }

    void TryHiss()
    {
        if (_hissingTimer > 0)
        {
            _hissingTimer--;
            if (_sprites.Count >= 2) _spriteRenderer.sprite = _sprites[1];
        }
        else
        {
            _hissingTimer = 0;
            if (_sprites.Count >= 2) _spriteRenderer.sprite = _sprites[0];
        }
    }

    void HandleShotFired() {
        _hissingTimer = 180;
    }


}
