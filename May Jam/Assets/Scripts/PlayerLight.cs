using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private Light2D playerLight;
    [SerializeField] private float startRadius = 10f;
    [SerializeField] private float minRadius = 0.5f;
    [SerializeField] private float shrinkDuration = 60f;

    private float timeElapsed = 0f;
    private bool gameOver = false;

    void Start()
    {
        playerLight.pointLightOuterRadius = startRadius;
    }

    [SerializeField] private float radiusEdgeWidth = 0.5f;
    void Update()
    {
        if (gameOver) return;

        if (timeElapsed < shrinkDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / shrinkDuration;
            float currentRadius = Mathf.Lerp(startRadius, minRadius, t);
            playerLight.pointLightOuterRadius = currentRadius;
            playerLight.pointLightInnerRadius = Mathf.Max(0, currentRadius - radiusEdgeWidth);
        }
        else
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        gameOver = true;
        // reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // TODO: load a game over screen
        // SceneManager.LoadScene("GameOver");
    }

    public float GetCurrentRadius()
    {
        return playerLight.pointLightOuterRadius;
    }

    public float GetMinRadius()
    {
        return minRadius;
    }

    public void ReduceRadius(float amount)
    {
        float currentRadius = playerLight.pointLightOuterRadius;
        float newRadius = Mathf.Max(minRadius, currentRadius - amount);
        float newT = 1f - ((newRadius - minRadius) / (startRadius - minRadius));
        timeElapsed = newT * shrinkDuration;
    }

    public void RestoreRadius(float amount)
    {
        float currentRadius = playerLight.pointLightOuterRadius;
        float newRadius = Mathf.Min(startRadius, currentRadius + amount);
        float newT = 1f - ((newRadius - minRadius) / (startRadius - minRadius));
        timeElapsed = Mathf.Max(0f, newT * shrinkDuration);
    }
}