public class PaperPlaneEnemy : Enemy {
    protected override void Update() {
        if (transform.position == _positions [_currentPosition].position) {
            Destroy(this);
        }
        
        base.Update();
    }
    
    
}
