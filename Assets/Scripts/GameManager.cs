using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] SceneManager sceneManager;

    void Awake()
    {
        DontDestroyOnLoad(this);
        GetPlayer();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(player) Debug.Log(player.name + " - " + player.transform.position.ToString());
        //else Debug.Log("No player");
        testInputs();
    }

    void testInputs()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Go to debug scene
            GoToScene(true, "Debug_Scene");
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            //go to lootbox scene
            GoToScene(false, "Debug_Lootbox_Scene");
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            //spawn player
            // if(!player) player = playerPrefab.GetComponent<PlayerController>().spawnPlayer().gameObject;
        }

    }

    public void RewardLootBox(LootBoxObject reward)
    {
        //update player values
        if(reward.isItem)
        {
            Debug.Log("REWARD: " + "HEALTH+" + reward.lootBoxItem.healthUp + ", SPEED+" + reward.lootBoxItem.speedUp);
            GoToScene(true, "Debug_Scene");
            player.GetComponent<PlayerController>().PowerUp(reward.lootBoxItem.healthUp, reward.lootBoxItem.speedUp);
            Debug.Log("NEW SPEED: " + player.GetComponent<PlayerController>().moveSpeed);
        }
        else
        {
            Debug.Log("REWARD: " + "beeg new gun");
            GoToScene(true, "Debug_Scene");
            player.GetComponent<PlayerController>().UpdateGun(reward.lootBoxGun);
        }
        //go to next level
    }

    private void GoToScene(bool spawnPlayer, string scene){
        player.SetActive(spawnPlayer);
        player.GetComponent<PlayerController>().GetComponent<Gun>().ClearPool();
        sceneManager.ChangeScene(scene);
    }

    private void GetPlayer()
    {
        if (!player)
        {
            Debug.Log("Spawning player!");
            player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}
