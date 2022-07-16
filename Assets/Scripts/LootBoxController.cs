using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    [SerializeField] LootBoxObject lootBoxObjectPrefab;
    [SerializeField] RollD6 rollD6Prefab;
    private RollD6 _rollD6;
    private List<LootBoxObject> _lootBoxOptions;

    private GameManager _gameManager;

    private bool _isLootBoxSpawned = false;

    public int _roll = 3;

    // Start is called before the first frame update
    void Start()
    {
        //_roll = Random.Range(1,7);
        _lootBoxOptions = new List<LootBoxObject>();
        _gameManager = FindObjectOfType<GameManager>();
        _rollD6 = Instantiate(rollD6Prefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
    }

    void LateUpdate()
    {
        if(!_isLootBoxSpawned && _rollD6.DoneRolling()) {
            _roll = _rollD6.currentFace;
            SpawnLootBox();
        }

        if(_lootBoxOptions.Count > 0)
        {
            foreach(var item in _lootBoxOptions)
            {
                if(item.isSelected)
                {
                    if (_gameManager) _gameManager.RewardLootBox(item);
                }
            }
        }
    }

    private void SpawnLootBox()
    {
        Debug.Log("spawning lootbox");
        _isLootBoxSpawned = true;
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        foreach(var item in FindObjectsOfType<LootBoxObject>())
        {
            _lootBoxOptions.Add(item);
        }
        _lootBoxOptions[0].SetLootBox("gun", _roll);
        _lootBoxOptions[1].SetLootBox("health", _roll);
        _lootBoxOptions[2].SetLootBox("speed", _roll);
    }

    public void SetRoll(int roll) => _roll = roll;
}
