using System.Collections.Generic;
using UnityEngine;
public class Gun : MonoBehaviour {
    [SerializeField] float _rateOfFire = 0.5f;
    [SerializeField] float _bulletSpeed = 100f;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _aimIndicator;
    
    float _timeSinceLastShot = -1;
    Queue<Projectile> _pool = new Queue<Projectile>();

    void Update() => _timeSinceLastShot -= Time.deltaTime;
    
    public void FireProjectile() {
        if (_timeSinceLastShot > 0) return;
        Projectile projectile = GetProjectile();
        if (projectile == null) return;
        projectile.Launch(_aimIndicator.position, (FixedScreenToWorldPoint() - transform.position).normalized * _bulletSpeed);
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
    
    Vector3 FixedScreenToWorldPoint() {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
}
