using UnityEngine;
using UnityEngine.Tilemaps;

public class QuadrantSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private int collectiblesPerQuadrant = 2;
    [SerializeField] private float collectibleRespawnTime = 10f;

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

    private Vector2[] quadrantCenters;

    void Start()
    {
        quadrantCenters = new Vector2[]
        {
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY + quadrantHeight / 2),
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY + quadrantHeight / 2),
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY - quadrantHeight / 2),
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY - quadrantHeight / 2),
        };

        SpawnCollectibles();
        SpawnEnemies();
    }

    void SpawnCollectibles()
    {
        for (int i = 0; i < quadrantCenters.Length; i++)
        {
            for (int j = 0; j < collectiblesPerQuadrant; j++)
            {
                SpawnCollectibleInQuadrant(i);
            }
        }
    }

    public void RespawnCollectibleInQuadrant(int quadrantIndex)
    {
        Invoke(nameof(SpawnCollectibleInQuadrant) + quadrantIndex, collectibleRespawnTime);
        StartCoroutine(RespawnAfterDelay(quadrantIndex, collectibleRespawnTime));
    }

    private System.Collections.IEnumerator RespawnAfterDelay(int quadrantIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnCollectibleInQuadrant(quadrantIndex);
    }

    void SpawnCollectibleInQuadrant(int quadrantIndex)
    {
        Vector2 center = quadrantCenters[quadrantIndex];
        int maxAttempts = 50;

        while (maxAttempts > 0)
        {
            maxAttempts--;
            float randomX = Random.Range(center.x - quadrantWidth / 2 + 1f, center.x + quadrantWidth / 2 - 1f);
            float randomY = Random.Range(center.y - quadrantHeight / 2 + 1f, center.y + quadrantHeight / 2 - 1f);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            if (IsInWall(spawnPos)) continue;
            if (IsInCenter(spawnPos)) continue;

            GameObject obj = Instantiate(collectiblePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            Collectible collectible = obj.GetComponent<Collectible>();
            collectible.quadrantIndex = quadrantIndex;
            collectible.spawner = this;
            return;
        }
    }

    void SpawnEnemies()
    {
        foreach (Vector2 center in quadrantCenters)
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