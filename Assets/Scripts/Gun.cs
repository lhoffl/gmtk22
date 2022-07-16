using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour {
    [SerializeField] float _rateOfFire = 0.5f;
    [SerializeField] float _bulletSpeed = 100f;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] int _numberOfProjectiles = 1;
    [SerializeField] float _spread = 0f;
    [SerializeField] Transform _aimIndicator;

    public Projectile Projectile => _projectilePrefab;
    
    float _timeSinceLastShot = -1;
    Queue<Projectile> _pool = new Queue<Projectile>();
    Vector3 _direction = Vector3.down;

    void Update() => _timeSinceLastShot -= Time.deltaTime;
    
    public void FireProjectile() {
        if (_timeSinceLastShot > 0) return;
        Projectile projectile = GetProjectile();
        if (projectile == null) return;
        projectile.Launch(_aimIndicator.position, _direction * _bulletSpeed);
        _timeSinceLastShot = _rateOfFire;
    }

    Projectile GetProjectile() {
        if (_pool.Count > 0) {
            Projectile projectile = _pool.Dequeue();
            projectile.gameObject.SetActive(true);
            return projectile;
        } 
        else {
            Projectile projectile = Instantiate(_projectilePrefab, _aimIndicator.position, Quaternion.identity);
            projectile.gameObject.SetActive(true);
            projectile.SetGun(this);
            return projectile;
        }
    }

    public void AddToPool(Projectile projectile) {
        projectile.gameObject.SetActive(false);
        _pool.Enqueue(projectile);
    }

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
