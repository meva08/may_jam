using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float fadeInDuration = 2f;
    [SerializeField] private float targetVolume = 0.5f;

    private float currentTime = 0f;
    private bool fadingIn = true;

    void Start()
    {
        audioSource.volume = 0f;
        audioSource.Play();
    }

    void Update()
    {
        if (fadingIn)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, currentTime / fadeInDuration);

            if (currentTime >= fadeInDuration)
            {
                audioSource.volume = targetVolume;
                fadingIn = false;
            }
        }
    }
}