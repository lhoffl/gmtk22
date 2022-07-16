using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float moveSpeed = 0.01f;

    public SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;

    private int selectedIndex = 0;

    private int timeToLive = 2500;

    // Start is called before the first frame update
    void Start()
    {
        selectedIndex = Random.Range(0, sprites.Count);
        moveSpeed = Random.Range(moveSpeed, (moveSpeed * 1.5f));
        float scaleFactor = Random.Range(1.0f, 3.0f);
        this.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - moveSpeed, this.transform.position.z);
        spriteRenderer.sprite = sprites[selectedIndex];
        timeToLive--;
        if(timeToLive < 0) Destroy(gameObject);
    }
}
