using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxObject : MonoBehaviour
{
    public LootBoxGun lootBoxGun;
    public LootBoxItem lootBoxItem;
    public bool isItem = false;
    public bool isSelected = false;
    public string objectType;

    void OnMouseDown()
    {
        isSelected = true;
    }

    public void SetLootBox(string newType, int roll)
    {
        objectType = newType;
        switch (newType)
        {
            case "gun":
                isItem = false;
                lootBoxGun.SetGun(roll);
                SetToGun();
                break;
            case "health":
                isItem = true;
                lootBoxItem.SetPowerUp((roll == 6 ? 5 : roll), (roll == 6 ? 1 : 0));
                SetToItem();
                break;
            case "speed":
                isItem = true;
                lootBoxItem.SetPowerUp((roll == 6 ? 1 : 0), (roll == 6 ? 5 : roll));
                SetToItem();
                break;
        }
    }

   private void SetToItem()
   {
        lootBoxItem = Instantiate(lootBoxItem, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f), Quaternion.identity);
   }

   private void SetToGun()
   {
        lootBoxGun = Instantiate(lootBoxGun, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f), Quaternion.identity);
   }
}
