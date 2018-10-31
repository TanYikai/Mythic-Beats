using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartScreenController : MonoBehaviour {
    public ControllerStick[] drumSticks;
    public ControllerDrum[] movementDrums;

    private UnityAction deactivateDrumListener;
    private UnityAction activateDrumListener;
    private UnityAction feedbackDrumListener;

    private SceneChanger sceneChanger;

    // Use this for initialization
    void Start() {
        EventManager.StartListening("DeactivateDrum", deactivateDrumListener);
        EventManager.StartListening("FeedbackDrum", feedbackDrumListener);
        EventManager.StartListening("ActivateDrum", activateDrumListener);
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    private void Awake() {
        deactivateDrumListener = new UnityAction(DeactivateDrum);
        activateDrumListener = new UnityAction(ActivateDrum);
        feedbackDrumListener = new UnityAction(FeedbackDrum);
    }

    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            sceneChanger.fadeToScene("Tutorial");
        }

        if (Input.GetKey(KeyCode.S)) {
            sceneChanger.fadeToScene("SampleScene");
        }
    }


    public void drumHit(KeyCode key, int id) {
        drumSticks[id].triggerVibration();
        ExecuteKey(key);
    }

    private void DeactivateDrum() {
        foreach (ControllerDrum drum in movementDrums) {
            drum.flashDrumColor(Color.red);
        }
    }

    private void ActivateDrum() {
        foreach (ControllerDrum drum in movementDrums) {
            drum.flashDrumColor(Color.grey);
        }
    }

    private void FeedbackDrum() {
        foreach (ControllerDrum drum in movementDrums) {
            drum.flashDrumColor(Color.green);
        }
    }

    private void ExecuteKey(KeyCode key) {
        EventManager.TriggerEvent("FeedbackDrum");
        if (key == KeyCode.W) {
            sceneChanger.fadeToScene("Tutorial");
        }
        if (key == KeyCode.S) {
            sceneChanger.fadeToScene("SampleScene");
        }
    }
}
