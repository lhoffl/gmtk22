using UnityEngine;
public class MouseEnemy : Enemy {
    
    [SerializeField] Rigidbody2D _rig;
    
    void Awake() {
        _rig = GetComponent<Rigidbody2D>();
    }

    protected override void Update() {

        transform.right = -(_positions[_currentPosition].position - transform.position);
        base.Update();
    }
}