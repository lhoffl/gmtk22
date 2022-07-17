using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] PlayerController playerPrefab;
    [SerializeField] SceneManager sceneManager;
    [SerializeField] int _lootboxLevelMod = 5;
     
    EnemySpawner _spawner;
    public static GameManager Instance;
    
    public int _level = 1;
    public int _maxPath = 5;

    public event Action<int> OnNewLevel;
    
    void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
        
        DontDestroyOnLoad(this);
        GetPlayer();
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Start() {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame() {
        yield return new WaitForSecondsRealtime(0.2f);
        if(player != null)
            MainMenu();
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
    }

    public void RewardLootBox(LootBoxObject reward)
    {
        if(reward.isItem)
        {
            GoToScene(true, "Level1");
            player.GetComponent<PlayerController>().PowerUp(reward.lootBoxItem.healthUp, reward.lootBoxItem.speedUp);
        }
        else
        {
            GoToScene(true, "Level1");
            player.GetComponent<PlayerController>().UpdateGun(reward.lootBoxGun);
        }
    }

    public void GoToScene(bool spawnPlayer, string scene){
        player.GetComponent<PlayerController>().GetComponent<Gun>().ClearPool();
        sceneManager.ChangeScene(scene);
        StartCoroutine(EnablePlayer(spawnPlayer));
    }

    IEnumerator EnablePlayer(bool spawnPlayer) {
        yield return new WaitForEndOfFrame();
        player.gameObject.SetActive(spawnPlayer);
    }

    void HandleNewLevel() {
        _level++;
        _maxPath++;

        if (_level % 5 == 0) {
            GoToScene(false, "Debug_Lootbox_scene");
        } else {
            GoToScene(true, "Level1");
        }
    }

    public void Test() {
       GoToScene(true, "Level1"); 
    }

    private void GetPlayer()
    {
        if (!player)
        {
            player = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
            player.enabled = false;
            player.OnPlayerDied += HandlePlayerDeath;
        }
    }
    public void HandlePlayerDeath() {
        GameOver();
    }

    public void RegisterSpawner(EnemySpawner spawner) {
        OnNewLevel?.Invoke(_level);
        _spawner = spawner;
        ModifySpawner(Mathf.Clamp(_level, 1, 69), true, _maxPath);
        player.enabled = true;
        player.GetComponent<Damageable>().Heal(0);
        player.IFrameOnNewLevel(2f);
    }

    public void ModifySpawner(int numEnemies, bool spawnCover, int maxPathLength = 5, float delay = 2f) {
        _spawner.SpawnCover = spawnCover;
        _spawner._totalNumberOfEnemies = numEnemies;
        _spawner._maxPathLength = maxPathLength;
        _spawner.SpawnEnemies();

        _spawner.OnAllEnemiesDead += HandleNewLevel;
    }

    private void ResetPlayerStats()
    {
        player.GetComponent<PlayerController>().ResetStats();
    }

    public void GameOver()
    {
        GoToScene(false, "GameOver");
        _level = 1;
        _maxPath = 5;
    }

    public void MainMenu()
    {
        ResetPlayerStats();
        //FindObjectOfType<MusicManager>().EndMusic();
        GoToScene(false, "MainMenu");
    }
}