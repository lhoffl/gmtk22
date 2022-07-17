using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxGun : MonoBehaviour
{
    public float rateOfFire = 0.5f;
    public  float bulletSpeed = 8f;
    public  Projectile projectilePrefab;
    public  int numberOfProjectiles = 1;
    public  float spread = 0f;
    [SerializeField] List<Projectile> _potentialProjectilePrefabs;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] TextMesh text;
    [SerializeField] List<Sprite> _sprites;

    public string gunName;

    void Start()
    {
        if (!text) text = this.GetComponent<TextMesh>();
        //RandomizeProperties();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) RandomizeProperties();
        DrawText();
    }

    private void RandomizeProperties()
    {
        rateOfFire = Random.Range(0.1f, 1.0f);
        bulletSpeed = Random.Range(1f, 10f);
        projectilePrefab = _potentialProjectilePrefabs[Random.Range(0, _potentialProjectilePrefabs.Count)];
        numberOfProjectiles = Random.Range(0, 100) < 75 ? 1 : Random.Range(1,3); 
        spread = numberOfProjectiles == 1 ? 0f : Random.Range(45, 90);
    }

    public string GunStatsString()
    {
        if(numberOfProjectiles > 1)
        {
            return
            "Projectiles: " + gunName + System.Environment.NewLine +
            "Damage: 1 - " + projectilePrefab.GetDamage().ToString() + System.Environment.NewLine +
            "Number of Projectiles: " + numberOfProjectiles + System.Environment.NewLine;
        }

        return
        "Projectile: " + gunName + System.Environment.NewLine +
        "Damage: 1 - " + projectilePrefab.GetDamage().ToString();
    }

    private void DrawText()
    {
        text.text = GunStatsString();
    }

    public void SetGun(int roll)
    {
        
        if (roll != 6) {
            sprite.sprite = _sprites[roll - 1];
            projectilePrefab = _potentialProjectilePrefabs[roll - 1];
        }
            
        switch (roll)
        {
            case 6:
                int newRoll = Random.Range(0,5);
                gunName = "Multishot";
                projectilePrefab = _potentialProjectilePrefabs[newRoll];
                sprite.sprite = _sprites[newRoll];
                numberOfProjectiles = 3;
                spread = 15f;
                break;
            case 5:
                gunName = "d20";
                break;
            case 4:
                gunName = "d12";
                break;
            case 3:
                gunName = "d8";
                break;
            case 2:
                gunName = "d6";
                break;
            case 1:
                gunName = "d4";
                break;
        }

    }
}
