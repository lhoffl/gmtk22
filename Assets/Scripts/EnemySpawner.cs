using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject _cover;
    [SerializeField] Enemy[] _enemyTypes;
    [SerializeField] List<Transform> _spawnPointsAndPathLocations;
    public int _totalNumberOfEnemies;
    public int _maxPathLength = 1;

    [SerializeField] bool _shouldShoot = true;

    [FormerlySerializedAs("Debug")] public bool DebugMode;
    
    public bool SpawnCover = true;
    int _remainingEnemies;

    public event Action OnAllEnemiesDead;
    
    public void SpawnEnemies() {
        _remainingEnemies = _totalNumberOfEnemies;

        if (SpawnCover) {
            int randomSpawnPoint = Random.Range(0, _spawnPointsAndPathLocations.Count);
            Instantiate(_cover, _spawnPointsAndPathLocations [randomSpawnPoint]);
            _spawnPointsAndPathLocations.Remove(_spawnPointsAndPathLocations[randomSpawnPoint]);
        }
        
        for (int i = 0; i < _totalNumberOfEnemies; i++) {
            int randomEnemyType = Random.Range(0, _enemyTypes.Length);

            int randomSpawnPoint = Random.Range(0, _spawnPointsAndPathLocations.Count);
            Enemy enemy = Instantiate(_enemyTypes [randomEnemyType], _spawnPointsAndPathLocations [randomSpawnPoint]);

            enemy.OnDeath += HandleEnemyDeath;
            
            enemy.ShouldShoot = _shouldShoot;
            enemy.AddPointToPath(_spawnPointsAndPathLocations[randomSpawnPoint]);
            _spawnPointsAndPathLocations.Remove(_spawnPointsAndPathLocations[randomSpawnPoint]);
            
            randomSpawnPoint = Random.Range(0, _spawnPointsAndPathLocations.Count);
            enemy.AddPointToPath(_spawnPointsAndPathLocations[randomSpawnPoint]);
            
            for (int j = 0; j < _maxPathLength; j++) {
                int r = Random.Range(0, _spawnPointsAndPathLocations.Count);
                if (r % 2 == 0) {
                    if(!enemy._positions.Contains(_spawnPointsAndPathLocations[r])) 
                        enemy.AddPointToPath(_spawnPointsAndPathLocations[r]);
                }
            }
        }
    }
    void HandleEnemyDeath() {
        _remainingEnemies--;
        if(_remainingEnemies <= 0)
            OnAllEnemiesDead?.Invoke();
    }

    void Awake() {
        for (int i = 0; i < _spawnPointsAndPathLocations.Count; i++) {
            _spawnPointsAndPathLocations [i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
        
        if(DebugMode)
            SpawnEnemies();
        else 
            GameManager.Instance.RegisterSpawner(this);
    }
}