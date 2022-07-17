using UnityEngine;
public class PencilEnemy : Enemy {
    [SerializeField] Projectile[] _projectiles;
    [SerializeField] float _spread = 3f;
    
    protected override void Update() {
        UpdateShootDirection();
        base.Update();
    }
    
    void UpdateShootDirection() {
        float r = Random.Range(-_spread, _spread);

        if (PlayerController.Instance == null) return;
        _gun.SetDirection((PlayerController.Instance.transform.position * r - transform.position).normalized);
    }
    
    protected override void TryShoot() {
        if(_projectiles.Length > 0)
            _gun.UpdateProjectile(GetRandomProjectile());
        base.TryShoot();
    }

    Projectile GetRandomProjectile() {
        int r = Random.Range(0, _projectiles.Length);
        return _projectiles [r];
    }
}