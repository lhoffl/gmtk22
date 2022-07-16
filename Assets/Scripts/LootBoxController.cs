using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    [SerializeField] LootBoxObject lootBoxObjectPrefab;
    private List<LootBoxObject> _lootBoxOptions;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _lootBoxOptions = new List<LootBoxObject>();
        _gameManager = FindObjectOfType<GameManager>();
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        Instantiate(lootBoxObjectPrefab, new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        foreach(var item in FindObjectsOfType<LootBoxObject>())
        {
            _lootBoxOptions.Add(item);
        }
    }

    void LateUpdate()
    {
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
}
