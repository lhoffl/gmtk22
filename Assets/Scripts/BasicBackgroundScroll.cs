using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 0.01f;

    public List<Sprite> potentialBackgrounds;

    public int selectedBackground = -1;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (selectedBackground == -1) selectedBackground = Random.Range(0, potentialBackgrounds.Count);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - Random.Range(scrollSpeed, (scrollSpeed + (0.01f * scrollSpeed))), this.transform.position.z);
        _spriteRenderer.sprite = potentialBackgrounds[selectedBackground];
    }
}
