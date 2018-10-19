using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControlController : MonoBehaviour {

    public Player playerRef;
    public Rhythm rhythmRef;

    public ControllerStick[] drumSticks;
    public ControllerDrum[] movementDrums;
    public ControllerDrum[] attackDrums;

    public bool isAttackMode = false;

    private UnityAction toggleDrumToAttackListener;
    private UnityAction toggleDrumToMovementListener;
    private UnityAction deactivateDrumListener;
    private UnityAction activateDrumListener;
    private UnityAction feedbackDrumListener;

    // Use this for initialization
    void Start () {
        EventManager.StartListening("ToggleDrumToAttack", toggleDrumToAttackListener);
        EventManager.StartListening("ToggleDrumToMovement", toggleDrumToMovementListener);
        EventManager.StartListening("DeactivateDrum", deactivateDrumListener);
        EventManager.StartListening("FeedbackDrum", feedbackDrumListener);
        EventManager.StartListening("ActivateDrum", activateDrumListener);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void drumHit(KeyCode key, int id) {
        drumSticks[id].triggerVibration();
        playerRef.ExecuteKey(key, isAttackMode);
    }

    private void Awake()
    {
        toggleDrumToAttackListener = new UnityAction(ToggleDrumToAttack);
        toggleDrumToMovementListener = new UnityAction(ToggleDrumToMovement);
        deactivateDrumListener = new UnityAction(DeactivateDrum);
        activateDrumListener = new UnityAction(ActivateDrum);
        feedbackDrumListener = new UnityAction(FeedbackDrum);
    }

    private void ToggleDrumToAttack()
    {
        isAttackMode = true;
        foreach(ControllerDrum drum in movementDrums) {
            drum.gameObject.SetActive(false);
        }
        foreach (ControllerDrum drum in attackDrums)
        {
            drum.gameObject.SetActive(true);
        }
    }

    private void ToggleDrumToMovement()
    {
        isAttackMode = false;
        foreach (ControllerDrum drum in attackDrums)
        {
            drum.gameObject.SetActive(false);
        }
        foreach (ControllerDrum drum in movementDrums)
        {
            drum.gameObject.SetActive(true);
        }
    }

    private void DeactivateDrum()
    {
        if (isAttackMode)
        {
            foreach (ControllerDrum drum in attackDrums)
            {
                drum.flashDrumColor(Color.red);
            }
        }
        else {
            foreach (ControllerDrum drum in movementDrums)
            {
                drum.flashDrumColor(Color.red);
            }
        }
    }


    private void ActivateDrum()
    {
        if (isAttackMode)
        {
            foreach (ControllerDrum drum in attackDrums)
            {
                drum.flashDrumColor(Color.grey);
            }
        }
        else
        {
            foreach (ControllerDrum drum in movementDrums)
            {
                drum.flashDrumColor(Color.grey);
            }
        }
    }

    private void FeedbackDrum()
    {
        if (isAttackMode)
        {
            foreach (ControllerDrum drum in attackDrums)
            {
                drum.flashDrumColor(Color.green);
            }
        }
        else
        {
            foreach (ControllerDrum drum in movementDrums)
            {
                drum.flashDrumColor(Color.green);
            }
        }
    }
}
