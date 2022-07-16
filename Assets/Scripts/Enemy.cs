using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public List<Transform> _positions;
    [SerializeField] float _speed = 3;

    protected int _currentPosition = 0;
    
    protected Gun _gun;

    void Awake() {
        _gun = GetComponent<Gun>();
        if (_positions == null || _positions.Count == 0)
            _positions = new List<Transform>();
    }

    protected virtual void Update() {
        TryShoot();
        Move();
        ChooseNextPosition();
    }

    protected virtual void TryShoot() {
        if (_gun == null) return;
       _gun.FireProjectile(); 
    }

    protected void Move() {
        transform.position = Vector3.Lerp(transform.position, _positions [_currentPosition].position, Time.deltaTime * _speed);
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
    public void AddPointToPath(Transform point) {
        _positions.Add(point);
    }
}