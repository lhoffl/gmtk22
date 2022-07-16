using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxItem : MonoBehaviour
{
    public int healthUp = 0;

    public int speedUp = 0;

    public SpriteRenderer sprite;

    public List<Sprite> sprites;
    [SerializeField] TextMesh text;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!text) text = this.GetComponent<TextMesh>();
        //decide if speed or health
        // if(Random.Range(0,100) > 50)
        // {
        //     healthUp++;
        //     sprite.sprite = sprites[0];
        // }
        // else 
        // {
        //     speedUp++;
        //     sprite.sprite = sprites[1];
        // }
    }

    void Update()
    {
        DrawText();
    }

    public string PowerUpStatsString()
    {
        string stats = "";

        if (healthUp > 0) stats += "+" + healthUp.ToString() + " Health Restored (up to max)" + System.Environment.NewLine;
        if (speedUp > 0) stats += "+" + speedUp.ToString() + " Speed" + System.Environment.NewLine;

        return stats; 
    }

    public void SetPowerUp(int health, int speed)
    {
        healthUp = health;
        speedUp = speed;
        if (speed > 0 && health <= 1) sprite.sprite = sprites[0];
        else 
        {
            sprite.sprite = sprites[health];
            text.transform.position = new Vector3(transform.position.x - 0.27f, transform.position.y - 0.19f, transform.position.z);
        }
    }

    private void DrawText()
    {
        text.text = PowerUpStatsString();
    }
}