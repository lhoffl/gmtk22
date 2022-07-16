using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public int health;
    public Rigidbody2D rig;
    public GameObject projectile;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    Gun _gun;
    
    public static PlayerController Instance { get; private set; }


    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Awake() {
        if (Instance == null)
            Instance = this;

        if (!MainCamera) MainCamera = FindObjectOfType<Camera>();
        
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = this.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = this.transform.GetComponent<SpriteRenderer>().bounds.extents.y;

        _gun = GetComponent<Gun>();
    }

    void Update()
    {
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
        if (Input.GetKey(KeyCode.Space))
        {
            _gun.FireProjectile();
            Debug.Log("Gun? " + _gun.GunStatsString());
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
    }
    
    Vector3 FixedScreenToWorldPoint() {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(worldPoint.x, worldPoint.y, 0f);
    }
}
