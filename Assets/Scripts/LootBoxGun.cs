using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxGun : MonoBehaviour
{
    [SerializeField] float _rateOfFire = 0.5f;
    [SerializeField] float _bulletSpeed = 100f;
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] int _numberOfProjectiles = 1;
    [SerializeField] float _spread = 0f;
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
        _rateOfFire = Random.Range(0.1f, 1.0f);
        _bulletSpeed = Random.Range(10f, 300f);
        _projectilePrefab = _potentialProjectilePrefabs[Random.Range(0, (_potentialProjectilePrefabs.Count - 1))];
        _numberOfProjectiles = Random.Range(0, 100) < 75 ? 1 : Random.Range(1,3); 
        _spread = _numberOfProjectiles == 1 ? 0f : Random.Range(45, 90);
        sprite.sprite = _projectilePrefab.GetComponent<SpriteRenderer>().sprite;
        DrawText();
    }

    public string GunStatsString()
    {
        return 
        "Projectile: " + sprite.sprite.name + System.Environment.NewLine +
        "Rate of Fire: " + _rateOfFire + System.Environment.NewLine + 
        "Bullet Speed: " + _bulletSpeed + System.Environment.NewLine + 
        "Number of Projectiles: " + _numberOfProjectiles +System.Environment.NewLine + 
        "Spread: " + _spread;
    }

    private void DrawText()
    {
        text.text = GunStatsString();
    }
}
