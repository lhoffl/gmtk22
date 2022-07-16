using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] SceneManager sceneManager;

    EnemySpawner _spawner;
    public static GameManager Instance;

    public int _numOfEnemies = 2;
    public int _maxPath = 5;

    void Awake() {
        if(Instance == null)
            Instance = this;
        
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

    public void GoToScene(bool spawnPlayer, string scene){
        player.SetActive(spawnPlayer);
        player.GetComponent<PlayerController>().GetComponent<Gun>().ClearPool();
        sceneManager.ChangeScene(scene);
    }

    void HandleNewLevel() {
        _numOfEnemies++;
        _maxPath++;
        GoToScene(true, "Level1");
    }

    public void Test() {
       GoToScene(true, "Level1"); 
    }

    private void GetPlayer()
    {
        if (!player)
        {
            Debug.Log("Spawning player!");
            player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
        }
    }

    public void RegisterSpawner(EnemySpawner spawner) {
        _spawner = spawner;
        ModifySpawner(_numOfEnemies / 2, true, _maxPath);
    }
    
    public void ModifySpawner(int numEnemies, bool spawnCover, int maxPathLength = 5, float delay = 2f) {
        _spawner.SpawnCover = spawnCover;
        _spawner._totalNumberOfEnemies = numEnemies;
        _spawner._maxPathLength = maxPathLength;
        _spawner.SpawnEnemies();

        _spawner.OnAllEnemiesDead += HandleNewLevel;
    }
}
