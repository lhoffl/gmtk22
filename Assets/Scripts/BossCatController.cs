using System;
using System.Collections.Generic;
using UnityEngine;

public class BossCatController : MonoBehaviour
{
    [SerializeField] float _speed = 0.01f;
    public List<Transform> _positions = new List<Transform>();
    public int _currentPosition = 0;
    public Gun _gun;
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> _sprites;
    public int _hissingTimer = 0;

    public event Action OnShotFired;  

    // Start is called before the first frame update
    void Start()
    {
        _gun = GetComponent<Gun>();
        _gun.OnShotFired += HandleShotFired;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        TryShoot();
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
