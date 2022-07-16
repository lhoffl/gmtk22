using UnityEngine;
public class AimIndicator : MonoBehaviour {
    [SerializeField] Transform _aimIndicator;

    void Update() {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0).normalized;
        
        float angle = Vector2.SignedAngle(Vector2.up, mousePosition);
        
        _aimIndicator.eulerAngles = new Vector3(0, 0, angle);
        _aimIndicator.position = transform.position + mousePosition / 2;
    }
    public void UpdateSprite(Sprite sprite) {
        _aimIndicator.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}