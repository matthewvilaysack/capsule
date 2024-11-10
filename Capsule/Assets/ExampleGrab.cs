using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExampleGrab : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // Drag a UI Canvas with CanvasGroup attached to it in the Inspector.
    public float fadeDuration = 1.0f; // Duration of the fade-in and fade-out effects.
    
    public void OnGrab()
    {
        Debug.Log("Grabbed");
        StartCoroutine(LoadNextSceneWithFade());
    }

    private IEnumerator LoadNextSceneWithFade()
    {
        // Fade out
        yield return StartCoroutine(Fade(1, fadeDuration));

        // Delay for 1 second
        yield return new WaitForSeconds(1f);

        // Load next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Fade in
        yield return StartCoroutine(Fade(0, fadeDuration));
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float time = 0;

        while (time < duration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
