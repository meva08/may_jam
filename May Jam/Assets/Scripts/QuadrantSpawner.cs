using UnityEngine;

public class QuadrantSpawner : MonoBehaviour
{
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private int collectiblesPerQuadrant = 2;
    [SerializeField] private float mapCenterX = 10f;
    [SerializeField] private float mapCenterY = -10f;
    [SerializeField] private float quadrantWidth = 10f;   // half width of each quadrant
    [SerializeField] private float quadrantHeight = 10f;  // half height of each quadrant

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCollectiblesInQuadrants();
    }

    void SpawnCollectiblesInQuadrants()
    {
        // define the 4 quadrant centers offset from map center
        Vector2[] quadrantCenters = new Vector2[]
        {
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY + quadrantHeight / 2), // top left
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY + quadrantHeight / 2), // top right
            new Vector2(mapCenterX - quadrantWidth / 2, mapCenterY - quadrantHeight / 2), // bottom left
            new Vector2(mapCenterX + quadrantWidth / 2, mapCenterY - quadrantHeight / 2), // bottom right
        };

        foreach (Vector2 center in quadrantCenters)
        {
            for (int i = 0; i < collectiblesPerQuadrant; i++)
            {
                float randomX = Random.Range(center.x - quadrantWidth / 2 + 1f, center.x + quadrantWidth / 2 - 1f);
                float randomY = Random.Range(center.y - quadrantHeight / 2 + 1f, center.y + quadrantHeight / 2 - 1f);
                Instantiate(collectiblePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
            }
        }



    }
}
