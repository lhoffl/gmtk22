using UnityEngine;
public class MouseEnemy : Enemy {
    
    [SerializeField] Rigidbody2D _rig;
    
    void Awake() {
        _rig = GetComponent<Rigidbody2D>();
    }

    protected override void Update() {
        transform.rotation = Quaternion.LookRotation(_rig.velocity);
        base.Update();
    }
}