using UnityEngine;
using UnityEngine.Tilemaps;

public class QuadrantSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private int collectiblesPerQuadrant = 2;

    [Header("Enemies")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemiesPerQuadrant = 2;

    [Header("Map Settings")]
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private float mapCenterX = 10f;
    [SerializeField] private float mapCenterY = -10f;
    [SerializeField] private float quadrantWidth = 10f;
    [SerializeField] private float quadrantHeight = 10f;
    [SerializeField] private float centerExclusionSize = 8f;

    void Start()
    {
        SpawnCollectibles();
        SpawnEnemies();
    }

    Vector2[] GetQuadrantCenters()
    {
        return new Vector2[]
        {
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY + quadrantHeight / 2),
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY + quadrantHeight / 2),
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY - quadrantHeight / 2),
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY - quadrantHeight / 2),
        };
    }

    void SpawnCollectibles()
    {
        foreach (Vector2 center in GetQuadrantCenters())
        {
            SpawnInQuadrant(collectiblePrefab, collectiblesPerQuadrant, center);
        }
    }

    void SpawnEnemies()
    {
        foreach (Vector2 center in GetQuadrantCenters())
        {
            SpawnInQuadrant(enemyPrefab, enemiesPerQuadrant, center);
        }
    }

    void SpawnInQuadrant(GameObject prefab, int count, Vector2 center)
    {
        int spawned = 0;
        int maxAttempts = 50;

        while (spawned < count && maxAttempts > 0)
        {
            maxAttempts--;

            float randomX = Random.Range(center.x - quadrantWidth / 2 + 1f, center.x + quadrantWidth / 2 - 1f);
            float randomY = Random.Range(center.y - quadrantHeight / 2 + 1f, center.y + quadrantHeight / 2 - 1f);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            if (IsInWall(spawnPos)) continue;
            if (IsInCenter(spawnPos)) continue;

            Instantiate(prefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            spawned++;
        }
    }

    bool IsInWall(Vector2 position)
    {
        Vector3Int cellPos = wallTilemap.WorldToCell(new Vector3(position.x, position.y, 0));
        return wallTilemap.HasTile(cellPos);
    }

    bool IsInCenter(Vector2 position)
    {
        return Mathf.Abs(position.x - mapCenterX) < centerExclusionSize &&
               Mathf.Abs(position.y - mapCenterY) < centerExclusionSize;
    }
}