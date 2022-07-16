using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxObject : MonoBehaviour
{
    public LootBoxGun lootBoxGun;
    public LootBoxItem lootBoxItem;
    public bool isItem = false;
    public bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        isItem = (Random.Range(1, 100) > 50);
        if (isItem) SetToItem(); 
        else SetToGun();
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked!");
        isSelected = true;
    }

   private void SetToItem()
   {
        lootBoxItem = Instantiate(lootBoxItem, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f), Quaternion.identity);
   }

   private void SetToGun()
   {

        lootBoxGun = Instantiate(lootBoxGun, this.transform.position, Quaternion.identity);
   }
}
