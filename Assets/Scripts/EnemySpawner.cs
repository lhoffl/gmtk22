using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour {
    [SerializeField] Enemy[] _enemyTypes;
    [SerializeField] List<Transform> _spawnPointsAndPathLocations;
    [SerializeField] int _totalNumberOfEnemies;
    [SerializeField] int _maxPathLength = 1;
    
    void SpawnEnemies() {
        for (int i = 0; i < _totalNumberOfEnemies; i++) {
            int randomEnemyType = Random.Range(0, _enemyTypes.Length);

            int randomSpawnPoint = Random.Range(0, _spawnPointsAndPathLocations.Count);
            Enemy enemy = Instantiate(_enemyTypes [randomEnemyType], _spawnPointsAndPathLocations [randomSpawnPoint]);
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

    void Awake() => SpawnEnemies();
}
