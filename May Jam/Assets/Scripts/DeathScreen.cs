using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private Animator deathAnimator;
    [SerializeField] private float delayBeforeLoseScene = 3f;

    public void TriggerDeath()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        deathAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        StartCoroutine(PlayDeathAnimation());
    }

    System.Collections.IEnumerator PlayDeathAnimation()
    {
        yield return null;
        deathAnimator.Play("DeathAnimation");
        yield return new WaitForSecondsRealtime(delayBeforeLoseScene);
        LoadLoseScene();
    }


    void LoadLoseScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LoseScene");
    }
}