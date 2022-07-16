using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] Cloud cloudPrefab;

    private float cooldown = 1.0f;

    // Update is called once per frame
    void Update()
    {
        cooldown -= 0.1f;
        if(cooldown < 0)
        {
            cooldown = Random.Range(1.0f, 30.0f);
            Instantiate(cloudPrefab, new Vector3(Random.Range(this.transform.position.x - 4, this.transform.position.x + 4), this.transform.position.y, this.transform.position.z), Quaternion.identity);
        }
    }
}
