using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private Light2D playerLight;
    [SerializeField] private float startRadius = 10f;
    [SerializeField] private float minRadius = 0.5f;
    [SerializeField] private float shrinkDuration = 60f;
    private float extraDrain = 0f;
    [SerializeField] private float falloffExponent = 1.8f;

    [SerializeField] private float minLightIntensity = 0.8f;
    [SerializeField] private float maxLightIntensity = 1.6f;
    [SerializeField] private DeathScreen deathScreen;

    private float timeElapsed = 0f;
    private bool gameOver = false;

    void Start()
    {
        playerLight.pointLightOuterRadius = startRadius;
    }

    void Update()
    {
        if (gameOver) return;

        if (timeElapsed < shrinkDuration)
        {
            timeElapsed += Time.deltaTime + extraDrain;
            Debug.Log("extra drain" + extraDrain);
            float t = timeElapsed / shrinkDuration;
            extraDrain = 0f; 
            float currentRadius = Mathf.Lerp(startRadius, minRadius, t);
            playerLight.pointLightOuterRadius = currentRadius;
            playerLight.pointLightInnerRadius = Mathf.Max(0, currentRadius * 0.25f);
            playerLight.intensity = Mathf.Lerp(minLightIntensity, maxLightIntensity, t);
        }
        else
        {
            TriggerGameOver();
        }
        playerLight.falloffIntensity = Mathf.Clamp01(1f - (1f / falloffExponent));
    }

    void TriggerGameOver()
    {
        gameOver = true;
        deathScreen.TriggerDeath();
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
        extraDrain += amount;
    }

    public void RestoreRadius(float amount)
    {
        float currentRadius = playerLight.pointLightOuterRadius;
        float newRadius = Mathf.Min(startRadius, currentRadius + amount);
        float newT = 1f - ((newRadius - minRadius) / (startRadius - minRadius));
        timeElapsed = Mathf.Max(0f, newT * shrinkDuration);
    }
}