using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator animator;
    private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && Input.GetKeyDown(KeyCode.UpArrow)) {
            fadeToScene("StartScreen");
        }

        if (Input.GetKeyDown(KeyCode.T) && Input.GetKeyDown(KeyCode.UpArrow)) {
            fadeToScene("Tutorial");
        }

        if (Input.GetKeyDown(KeyCode.N) && Input.GetKeyDown(KeyCode.UpArrow)) {
            fadeToScene("SampleScene");
        }
    }

    public void fadeToScene (string sceneName) {
        sceneToLoad = sceneName;
        animator.SetTrigger("FadeOut");
    }

    public void onFadeComplete() {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void waitAndFadeToScene(string sceneName, float duration) {
        StartCoroutine(PreSceneChangeDelay(sceneName, duration));
    }

    private IEnumerator PreSceneChangeDelay(string sceneName, float duration) {
        yield return new WaitForSeconds(duration);
        fadeToScene(sceneName);
    }
}
