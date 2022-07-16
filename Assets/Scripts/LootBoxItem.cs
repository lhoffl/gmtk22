using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxItem : MonoBehaviour
{
    public int healthUp = 0;

    public int speedUp = 0;

    public SpriteRenderer sprite;

    public List<Sprite> sprites;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //decide if speed or health
        if(Random.Range(0,100) > 50)
        {
            healthUp++;
            sprite.sprite = sprites[0];
        }
        else 
        {
            speedUp++;
            sprite.sprite = sprites[1];
        }
    }
}