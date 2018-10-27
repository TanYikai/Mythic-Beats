using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator animator;
    private string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
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
}
