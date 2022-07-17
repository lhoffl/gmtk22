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
    void Start()
    {
        if (!_spriteRenderer) _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        frameCount++;
        transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
        if (_spriteRenderer) 
        {
            _spriteRenderer.sprite = _sprites[value];
        }
        if (frameCount >= framesToLive) Destroy(gameObject);
    }

    public void SetValue(int newValue) {
        value = newValue; 
    }

}
