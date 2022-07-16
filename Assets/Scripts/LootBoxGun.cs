using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxGun : MonoBehaviour
{
    public float rateOfFire = 0.5f;
    public  float bulletSpeed = 100f;
    public  Projectile projectilePrefab;
    public  int numberOfProjectiles = 1;
    public  float spread = 0f;
    [SerializeField] List<Projectile> _potentialProjectilePrefabs;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] TextMesh text;

    void Start()
    {
        if (!text) text = this.GetComponent<TextMesh>();
        RandomizeProperties();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RandomizeProperties();
    }

    private void RandomizeProperties()
    {
        rateOfFire = Random.Range(0.1f, 1.0f);
        bulletSpeed = Random.Range(1f, 10f);
        projectilePrefab = _potentialProjectilePrefabs[Random.Range(0, (_potentialProjectilePrefabs.Count - 1))];
        numberOfProjectiles = Random.Range(0, 100) < 75 ? 1 : Random.Range(1,3); 
        spread = numberOfProjectiles == 1 ? 0f : Random.Range(45, 90);
        sprite.sprite = projectilePrefab.GetComponent<SpriteRenderer>().sprite;
        DrawText();
    }

    public string GunStatsString()
    {
        return 
        "Projectile: " + sprite.sprite.name + System.Environment.NewLine +
        "Rate of Fire: " + rateOfFire + System.Environment.NewLine + 
        "Bullet Speed: " + bulletSpeed + System.Environment.NewLine + 
        "Number of Projectiles: " + numberOfProjectiles +System.Environment.NewLine + 
        "Spread: " + spread;
    }

    private void DrawText()
    {
        text.text = GunStatsString();
    }
}
