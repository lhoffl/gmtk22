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
        if (!player)
        {
            PlayerController temp = FindObjectOfType<PlayerController>();
            if (temp) player = temp.gameObject;
        }
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
            sceneManager.ChangeScene("Debug_Scene");
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            //go to lootbox scene
            sceneManager.ChangeScene("Debug_Lootbox_Scene");
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            //spawn player
            if(!player) player = playerPrefab.GetComponent<PlayerController>().spawnPlayer().gameObject;
        }

    }

    public void RewardLootBox(LootBoxObject reward)
    {
        //update player values
        if(reward.isItem)
        {
            Debug.Log("REWARD: " + (reward.lootBoxItem.healthUp > 0 ? "HEALTH++" : "SPEED++"));
            sceneManager.ChangeScene("Andy Test");
        }
        else
        {
            Debug.Log("REWARD: " + "beeg new gun");
            sceneManager.ChangeScene("Andy Test");
        }
        //go to next level
    }
}
