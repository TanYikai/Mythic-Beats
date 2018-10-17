using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public GameObject player;
    public GameObject rhythm;
    public GameObject drumRim;
    public GameObject altDrum;
    public bool isAttackDrum;
    public KeyCode keyCode;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private Player playerRef;
    private Rhythm rhythmRef;
    private Material drumRimMat;
    private UnityAction toggleDrumToAttackListener;
    private UnityAction toggleDrumToMovementListener;
    private UnityAction deactivateDrumListener;
    private UnityAction activateDrumListener;
    private UnityAction feedbackDrumListener;


    void Start () {
        startUpListeners();
        playerRef = player.GetComponent<Player>();
        rhythmRef = rhythm.GetComponent<Rhythm>();
        drumRimMat = drumRim.GetComponent<Renderer>().materials[4];
    }


    private void Awake() {
        toggleDrumToAttackListener = new UnityAction(ToggleDrumToAttack);
        toggleDrumToMovementListener = new UnityAction(ToggleDrumToMovement);
        deactivateDrumListener = new UnityAction(DeactivateDrum);
        activateDrumListener = new UnityAction(ActivateDrum);
        feedbackDrumListener = new UnityAction(FeedbackDrum);
    }

    private void Update() {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isAttackDrum) {
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

    private void ToggleDrumToAttack() {
        if (!isAttackDrum)
        {
            altDrum.SetActive(true);
            altDrum.GetComponent<ControllerDrum>().startUpListeners();
            EventManager.StopListening("ToggleDrumToAttack", toggleDrumToAttackListener);
            EventManager.StopListening("ToggleDrumToMovement", toggleDrumToMovementListener);
            EventManager.StopListening("DeactivateDrum", deactivateDrumListener);
            EventManager.StopListening("FeedbackDrum", feedbackDrumListener);
            EventManager.StopListening("ActivateDrum", activateDrumListener);
            this.gameObject.SetActive(false);
        }

    }

    private void ToggleDrumToMovement() {
        if (isAttackDrum)
        {
            altDrum.SetActive(true);
            altDrum.GetComponent<ControllerDrum>().startUpListeners();
            EventManager.StopListening("ToggleDrumToAttack", toggleDrumToAttackListener);
            EventManager.StopListening("ToggleDrumToMovement", toggleDrumToMovementListener);
            EventManager.StopListening("DeactivateDrum", deactivateDrumListener);
            EventManager.StopListening("FeedbackDrum", feedbackDrumListener);
            EventManager.StopListening("ActivateDrum", activateDrumListener);
            this.gameObject.SetActive(false);
        }
    }


    private void DeactivateDrum() {
        StartCoroutine(colorTransition(drumRimMat.color, Color.red));
    }


    private void ActivateDrum() {
        StartCoroutine(colorTransition(drumRimMat.color, Color.grey));
    }

    private void FeedbackDrum(){
        StartCoroutine(colorTransition(drumRimMat.color, Color.green));
    }

    public void startUpListeners() {
        EventManager.StartListening("ToggleDrumToAttack", toggleDrumToAttackListener);
        EventManager.StartListening("ToggleDrumToMovement", toggleDrumToMovementListener);
        EventManager.StartListening("DeactivateDrum", deactivateDrumListener);
        EventManager.StartListening("FeedbackDrum", feedbackDrumListener);
        EventManager.StartListening("ActivateDrum", activateDrumListener);
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
