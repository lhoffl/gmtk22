using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] AudioClip _deathClip;
    [SerializeField] AudioClip _shootClip;

    public float moveSpeed;
    public float speedUpgradeIncrement;
    public float maxSpeed;
    public Rigidbody2D rig;
    public GameObject projectile;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    Gun _gun;
    AimIndicator _aimIndicator;
    bool _movementEnabled = true;

    AudioSource _source;
    Health _health;

    public static PlayerController Instance { get; private set; }
    public event Action OnPlayerDied;

    void Start()
    {
        DontDestroyOnLoad(this);
        _source = GetComponent<AudioSource>();
        _health = GetComponent<Health>();
        _health.OnHealthChanged += HandleHealthChanged;
    }
    
    void HandleHealthChanged(object sender, HealthChangedEventArgs e) {
        if (e.Health <= 0) {
            _source.PlayOneShot(_deathClip);
            OnPlayerDied?.Invoke();
        }
    }

    void Awake() {
        if (Instance == null)
            Instance = this;

        if (!MainCamera) MainCamera = FindObjectOfType<Camera>();
        
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = this.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = this.transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        _gun = GetComponent<Gun>();
        _gun.OnShotFired += HandleShotFired;
        _aimIndicator = GetComponent<AimIndicator>();
    }
    void HandleShotFired() {
        _source.PlayOneShot(_shootClip);
    }

    void Update()
    {
        if(_movementEnabled)
            movePlayer();
        CheckInputs();
    }

    void LateUpdate()
    {
        Vector3 viewPos = this.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        this.transform.position = viewPos;
    }

    private void CheckInputs()
    {
        if (Input.GetMouseButton(0))
        {
            _gun.FireProjectile();
        }
        
        _gun.SetDirection((FixedScreenToWorldPoint() - transform.position).normalized);
    }

    private Vector2 GetMovementVectorFromInputs()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        return new Vector2(x * moveSpeed, y * moveSpeed);
    }

    private void movePlayer()
    {
        rig.velocity = GetMovementVectorFromInputs();
    }

    public void UpdateGun(LootBoxGun newGun)
    {
        _gun.UpdateGun(newGun);
        _aimIndicator.UpdateSprite(_gun.Projectile.GetComponent<SpriteRenderer>().sprite);
    }

    public void PowerUp(int health, int speed)
    {
        Debug.Log("Player Power Up: H+" + health + " S+" + speed);
        if (health > 0)
        {
            Health playerHealth = GetComponent<Health>();
            if (playerHealth)
            {
                if (playerHealth.MaxHealth - playerHealth.CurrentHealth < health) playerHealth.Add(playerHealth.MaxHealth - playerHealth.CurrentHealth);
                else playerHealth.Add(health);
            }
        }

        if (speed > 0)
        {
            for(int i = 0; i < speed; i++)
            {
                if (moveSpeed < maxSpeed)
                {
                    if (moveSpeed + speedUpgradeIncrement > maxSpeed) moveSpeed = maxSpeed;
                    else moveSpeed += speedUpgradeIncrement;
                    Debug.Log("newmovespeed: " + moveSpeed);
                }
            }
        }
    }
    
    Vector3 FixedScreenToWorldPoint() {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
    public void MovementEnabled(bool enabled) {
        _movementEnabled = enabled;
    }
}