using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public GameObject rhythm;
    public GameObject drumRim;
    public KeyCode keyCode;
    private bool attackMode = false;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private Player playerRef;
    private Rhythm rhythmRef;
    private Material drumRimMat;
    private UnityAction toggleOnListener;
    private UnityAction toggleOffListener;
    private UnityAction deactivateDrumListener;
    private UnityAction activateDrumListener;
    private UnityAction feedbackDrumListener;

    void Start () {
        EventManager.StartListening("ToggleOn", toggleOnListener);
        EventManager.StartListening("ToggleOff", toggleOffListener);
        EventManager.StartListening("DeactivateDrum", deactivateDrumListener);
        EventManager.StartListening("FeedbackDrum", feedbackDrumListener);
        EventManager.StartListening("ActivateDrum", activateDrumListener);
        playerRef = player.GetComponent<Player>();
        rhythmRef = rhythm.GetComponent<Rhythm>();
        drumRimMat = drumRim.GetComponent<Renderer>().material;
    }


    private void Awake() {
        toggleOnListener = new UnityAction(ToggleOn);
        toggleOffListener = new UnityAction(ToggleOff);
        deactivateDrumListener = new UnityAction(DeactivateDrum);
        activateDrumListener = new UnityAction(ActivateDrum);
        feedbackDrumListener = new UnityAction(FeedbackDrum);
    }

    private void Update() {
        


    }

    private void OnTriggerEnter(Collider collider)
    {
        if (attackMode) {
            if (collider.CompareTag("DrumStick")) {
                Debug.Log("Sending " + keyCode + " (combo)");
                playerRef.ExecuteKey(keyCode, true);
            }
        }
        else {
            if (collider.CompareTag("DrumStick")) {
                Debug.Log("Sending " + keyCode + " (movement)");
                playerRef.ExecuteKey(keyCode, false);
            }
        }
    }

    private void ToggleOn() {
        attackMode = true;
        Debug.Log("attackMode ON");
    }

    private void ToggleOff() {
        attackMode = false;
        Debug.Log("attackMode OFF");
    }

    private void DeactivateDrum() {
        StartCoroutine(colorTransition(drumRimMat.color, Color.red));
    }


    private void ActivateDrum() {
        StartCoroutine(colorTransition(drumRimMat.color, Color.blue));
    }

    private void FeedbackDrum(){
        StartCoroutine(colorTransition(drumRimMat.color, Color.green));
    }

    IEnumerator colorTransition(Color initialColor, Color finalColor)
    {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.1f;
        while (currentAnimationTime < totalAnimationTime)
        {
            currentAnimationTime += Time.deltaTime;
            drumRimMat.color = Color.Lerp(initialColor, finalColor, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
