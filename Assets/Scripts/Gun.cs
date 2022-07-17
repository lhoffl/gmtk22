using System;
using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour {
    [SerializeField] float _rateOfFire = 0.5f;
    [SerializeField] float _bulletSpeed = 2;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] int _numberOfProjectiles = 3;
    [SerializeField] float _spread = 15f;
    [SerializeField] Transform _aimIndicator;

    public Projectile Projectile => _projectilePrefab;
    
    float _timeSinceLastShot = -1;
    Queue<Projectile> _pool = new Queue<Projectile>();
    Vector3 _direction = Vector3.down;
    float _oRof;
    float _oSpeed;
    Projectile _oProj;
    int _oNum;
    float _oSpread;

    public event Action OnShotFired;

    void Awake() {
        _oRof = _rateOfFire;
        _oSpeed = _bulletSpeed;
        _oProj = _projectilePrefab;
        _oNum = _numberOfProjectiles;
        _oSpread = _spread;
    }

    void Update() => _timeSinceLastShot -= Time.deltaTime;
    
    public void FireProjectile() {
        if (_timeSinceLastShot > 0) return;
        float spreadStep = _spread / _numberOfProjectiles;
        for(int i = 0; i < _numberOfProjectiles; i++) {
            Projectile projectile = GetProjectile();
            if (projectile == null) return;
            Vector3 rotatedDirection = Quaternion.Euler(0, 0, (spreadStep * i)) * _direction;
            projectile.Launch(_aimIndicator.position, (rotatedDirection * _bulletSpeed));
        }
        
        OnShotFired?.Invoke();
        _timeSinceLastShot = _rateOfFire;

    }

    Projectile GetProjectile() {
        if (_pool.Count > 0) {
            Projectile projectile = _pool.Dequeue();
            if (projectile == null) {
                Destroy(projectile);
                return GetNewProjectile();
            }
            projectile.gameObject.SetActive(true);
            return projectile;
        } 
        else {
            return GetNewProjectile();
        }
    }

    Projectile GetNewProjectile() {
        Projectile projectile = Instantiate(_projectilePrefab, _aimIndicator.position, Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.SetGun(this);
        return projectile;
    }

    public void AddToPool(Projectile projectile) {
        projectile.gameObject.SetActive(false);
        _pool.Enqueue(projectile);
    }

    public void ClearPool() => _pool.Clear();

    public void SetDirection(Vector3 direction) => _direction = direction;

    public void UpdateProjectile(Projectile projectile) {
        _projectilePrefab = projectile;
    }

    public void UpdateGun(LootBoxGun newGun)
    {
        _rateOfFire = newGun.rateOfFire;
        _bulletSpeed = newGun.bulletSpeed;
        _projectilePrefab = newGun.projectilePrefab;
        _numberOfProjectiles = newGun.numberOfProjectiles;
        _spread = newGun.spread;
    }

    public void ResetGun() {
        _rateOfFire = _oRof;
        _bulletSpeed = _oSpeed;
        _projectilePrefab = _oProj;
        _numberOfProjectiles = _oNum;
        _spread = _oSpread;
    }

    public string GunStatsString()
    {
        return 
        "Projectile: " + _projectilePrefab.GetComponent<SpriteRenderer>().sprite.name + System.Environment.NewLine +
        "Rate of Fire: " + _rateOfFire + System.Environment.NewLine + 
        "Bullet Speed: " + _bulletSpeed + System.Environment.NewLine + 
        "Number of Projectiles: " + _numberOfProjectiles +System.Environment.NewLine + 
        "Spread: " + _spread;
    }

}
