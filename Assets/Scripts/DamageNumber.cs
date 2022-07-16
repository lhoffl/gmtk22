using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] List<Sprite> _sprites;
    private SpriteRenderer _spriteRenderer;
    private int framesToLive = 420;
    private int frameCount = 0;
    private float moveSpeed = 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        frameCount++;
        transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
        if (_spriteRenderer) 
        {
            _spriteRenderer.sprite = _sprites[value];

            // float alpha = (255 * (1f - ((float)frameCount / (float)framesToLive)));
            // Color tmp = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, alpha);
            // _spriteRenderer.color = tmp;
            // Debug.Log("New Alpha: " + alpha + ", Color Alpha: " + _spriteRenderer.color.a);
        }
        if (frameCount >= framesToLive) Destroy(gameObject);
    }

    // private float SmoothLerp (float time, float duration)
    // {
    //     float t = time / duration;
    //     return t * t * (3f - 2f * t);
    // }

    public void SetValue(int newValue) {
        value = newValue; 
    }

}
