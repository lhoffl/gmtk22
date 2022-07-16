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

    void Start()
    {
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
        }
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
}
