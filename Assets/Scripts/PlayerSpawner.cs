using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaveGame
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private int _spawnSearchRange;

        private GameObject _player;

        private Vector2 CalculatePlayerSpawnPos()
        {
            int mapWidth = _mapGenerator.Map.GetLength(0);
            int mapHeight = _mapGenerator.Map.GetLength(1);

            Debug.Log($"Map size: {mapWidth}, {mapHeight}");

            for (int x = 0; x < _spawnSearchRange; x++)
            {
                for (int y = 0; y < _spawnSearchRange; y++)
                {
                    if (_mapGenerator.Map[mapWidth - 1 - x, mapHeight - 1 - y] == 1) continue;
                    else if (_mapGenerator.Map[mapWidth - 1 - x, mapHeight - 1 - (y + 1)] == 0)
                    {
                        return new Vector2(mapWidth - 1 - x + 0.5f, mapHeight - 1 - y);
                    }
                }
            }
            return Vector2.zero;
        }

        private void SpawnPlayer()
        {
            if (_player != null) return;
            _player = Instantiate(_playerPrefab, CalculatePlayerSpawnPos(), Quaternion.identity);
            Debug.Log("Player spawned");
        }

        private void OnEnable()
        {
            EventManager.OnMapGenerated += SpawnPlayer;
        }
        private void OnDisable()
        {
            EventManager.OnMapGenerated -= SpawnPlayer;
        }
    }
}
