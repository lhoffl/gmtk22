using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    private float _defaultMoveSpeed;
    
    LootBoxGun _defaultGun;
    Gun _gun;
    AimIndicator _aimIndicator;
    bool _movementEnabled = true;

    Camera _currentCamera;
    
    AudioSource _source;
    Health _health;

    public static PlayerController Instance { get; private set; }
    public event Action OnPlayerDied;

    void Start()
    {
        _source = GetComponent<AudioSource>();
        _health = GetComponent<Health>();
        _health.OnHealthChanged += HandleHealthChanged;
    }
    
    void HandleHealthChanged(object sender, HealthChangedEventArgs e) {
        if (e.Health <= 0) {
            _source.PlayOneShot(_deathClip);
            OnPlayerDied?.Invoke();
            _gun.ResetGun();
            _aimIndicator.UpdateSprite(_gun.Projectile.GetComponent<SpriteRenderer>().sprite);
        }
    }

    bool _hitByL, _hitByH, _hitByF, _hitByO;
    [SerializeField] Sprite _lhofflSprite;

    public void LogLetter(Projectile projectile) {
        if (projectile.name.Contains("1")) {
            _hitByL = true;
        }
        if (projectile.name.Contains("2")) {
            _hitByH = true;
        }
        if (projectile.name.Contains("3")) {
            _hitByO = true;
        }
        if (projectile.name.Contains("4")) {
            _hitByF = true;
        }

        if (_hitByF && _hitByH && _hitByL && _hitByO) {
            GetComponent<SpriteRenderer>().sprite = _lhofflSprite;
        }
    }
    
    void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _defaultMoveSpeed = moveSpeed;
       
        DontDestroyOnLoad(this);
        
        _gun = GetComponent<Gun>();
        _gun.OnShotFired += HandleShotFired;
        _aimIndicator = GetComponent<AimIndicator>();
        GameManager.Instance.OnNewLevel += ResetCamera;

        if (moveSpeed == 0) {
            moveSpeed = _defaultMoveSpeed;
        }
        
        if (GameObject.FindObjectOfType<Camera>() == null) return;
        ResetCamera(0);
    }

    void OnEnable() {
        if (moveSpeed == 0) {
            moveSpeed = _defaultMoveSpeed;
        }
        MovementEnabled(true);
    }

    void ResetCamera(int level) {
        MainCamera = FindObjectOfType<Camera>();
        
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = this.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = this.transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        
        //GetComponent<Damageable>().TakeDamage(0);
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

        if (!_gun) {
            _gun = GetComponent<Gun>();
        }

        if (!MainCamera) {
            MainCamera = Camera.current;
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

    public void UpdateGun(LootBoxGun newGun) {
        _gun.UpdateGun(newGun);
        _aimIndicator.UpdateSprite(_gun.Projectile.GetComponent<SpriteRenderer>().sprite);
    }

    public void PowerUp(int health, int speed)
    {
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
                }
            }
        }
    }
    
    Vector3 FixedScreenToWorldPoint() {
        if (!MainCamera) {
            MainCamera = Camera.current;
        }
        
        Vector3 worldPoint = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
    public void MovementEnabled(bool enabled) {
        _movementEnabled = enabled;
    }

    public void ResetStats()
    {
        moveSpeed = _defaultMoveSpeed;
        _gun.ResetGun();
        _aimIndicator.UpdateSprite(_gun.Projectile.GetComponent<SpriteRenderer>().sprite);
        Health playerHealth = GetComponent<Health>();
        if(playerHealth) playerHealth.Add(playerHealth.MaxHealth);
    }

    public IEnumerator IFrameOnNewLevel(float f) {
        enabled = true;
        GetComponent<Damageable>().Heal(0); 
        if (moveSpeed == 0) {
            moveSpeed = _defaultMoveSpeed;
        }
        MovementEnabled(true);
        MainCamera = Camera.current;
        ResetCamera(0);
        GetComponent<Damageable>().Invincible = true;
        yield return new WaitForSecondsRealtime(f);
        GetComponent<Damageable>().Invincible = false;
    }
}