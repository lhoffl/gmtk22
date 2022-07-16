
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    [SerializeField] DamageNumber _damageNumberPrefab;
    [SerializeField] float fadeSpeed;

    public void Update()
    {
        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     SpawnDamageNumber(Random.Range(0,21), new Vector3(0,0,0));
        // }
    }

    public void SpawnDamageNumber(int damageValue, Vector3 spawnLocation)
    {
        DamageNumber temp = Instantiate(_damageNumberPrefab, spawnLocation, Quaternion.identity);
        temp.SetValue(damageValue);
    }

}