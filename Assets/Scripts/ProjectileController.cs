using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Rigidbody2D rig;
    public float projectileSpeed;

    void OnCreate()
    {
        rig = this.GetComponent<Rigidbody2D>();
        rig.AddForce(Vector2.up * projectileSpeed, ForceMode2D.Force);
    }

    void OnCollision()
    {
        //Destroy(this);
    }
}
