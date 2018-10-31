using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour {

    enum ComboDirection {
        up,
        down,
        left,
        right
    };

    public int lengthOfCombo;
    public int playerHealth;

    private TutorialController tutorialController;
    private RhythmTutorial rhythmController;
    private PlayerMovementRestrictorTutorial playerMovementRestrictor;
    public Animator anim;

    private ComboTutorial combo;

    // To allow debug without VR
    GameObject mainCamera;

    // movement sound effects
    private AudioSource[] movementSounds;

    // Use this for initialization
    void Start() {
        tutorialController = GameObject.Find("TutorialController").GetComponent<TutorialController>();
        rhythmController = GameObject.Find("Rhythm").GetComponent<RhythmTutorial>();
        playerMovementRestrictor = new PlayerMovementRestrictorTutorial();
        combo = this.gameObject.GetComponent<ComboTutorial>();
        mainCamera = GameObject.Find("Main Camera");

        movementSounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);


        if (mainCamera) {
            if (tutorialController.isTextShowing) {
                if (Input.GetMouseButtonDown(0)) {
                    tutorialController.progressText();
                }
                return;
            }

            if (tutorialController.isAttackTogglingEnabled) {
                if (Input.GetKey(KeyCode.Q)) {
                    EventManager.TriggerEvent("ToggleDrumToAttack");
                }

                if (Input.GetKey(KeyCode.E)) {
                    EventManager.TriggerEvent("ToggleDrumToMovement");
                }
            }
            if (Input.GetButton("Fire1")) {
                if (tutorialController.isAttackTogglingEnabled) {
                    doAttack();
                }
            }
            else {
                if (tutorialController.isMovementEnabled) {
                    doMovement();
                }
            }
        }
    }

    private void doMovement() {
        Vector3 newPosition;
        AudioSource sound;

        if (tutorialController.isBeatEnabled) {
            if (!rhythmController.IsTimeForPlayerAction) {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                    failBeat();
                }
                return;
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
            EventManager.TriggerEvent("FeedbackDrum");
            if (tutorialController.isComboEnabled) {
                rhythmController.playerComboCount += 1;
                EventManager.TriggerEvent("UpdateCounter");
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, 1);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                sound = movementSounds[0];
                sound.Play();

                anim.SetBool("Up", true);
                StartCoroutine(locationTransition(transform.position, newPosition));
                tutorialController.checkDirectionChecklist((int)ComboDirection.up);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(-1, 0, 0);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                sound = movementSounds[1];
                sound.Play();

                anim.SetBool("Left", true);
                StartCoroutine(locationTransition(transform.position, newPosition));
                tutorialController.checkDirectionChecklist((int)ComboDirection.left);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, -1);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                sound = movementSounds[2];
                sound.Play();

                anim.SetBool("Down", true);
                StartCoroutine(locationTransition(transform.position, newPosition));
                tutorialController.checkDirectionChecklist((int)ComboDirection.down);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(1, 0, 0);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                sound = movementSounds[3];
                sound.Play();

                anim.SetBool("Right", true);
                StartCoroutine(locationTransition(transform.position, newPosition));
                tutorialController.checkDirectionChecklist((int)ComboDirection.right);
            }
            return;
        }
    }

    public void ExecuteKey(KeyCode keyCode, bool attackMode) {
        if (tutorialController.isTextShowing) {
            tutorialController.progressText();
            return;
        }

        Vector3 newPosition;
        AudioSource sound;

        if (tutorialController.isBeatEnabled) {
            if (!rhythmController.IsTimeForPlayerAction) {
                failBeat();
                return;
            }
        }

        EventManager.TriggerEvent("FeedbackDrum");

        if (tutorialController.isComboEnabled) {
            rhythmController.playerComboCount += 1;
            EventManager.TriggerEvent("UpdateCounter");
        }

        if (attackMode) {
            if (tutorialController.isAttackTogglingEnabled) {
                if (keyCode == (KeyCode.W)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    if (combo.addToStack("Up", tutorialController.isComboEnabled)) {
                        StartCoroutine(specialTransition());
                        tutorialController.isComboObjectiveCompleted = true;
                    }
                    else {
                        StartCoroutine(attackTransition());
                        tutorialController.checkAttackDirectionChecklist((int)ComboDirection.up);
                    }
                    return;
                }
                if (keyCode == (KeyCode.A)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    if (combo.addToStack("Left", tutorialController.isComboEnabled)) {
                        StartCoroutine(specialTransition());
                        tutorialController.isComboObjectiveCompleted = true;
                    }
                    else {
                        StartCoroutine(attackTransition());
                        tutorialController.checkAttackDirectionChecklist((int)ComboDirection.left);
                    }
                    return;
                }
                if (keyCode == (KeyCode.S)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    if (combo.addToStack("Down", tutorialController.isComboEnabled)) {
                        StartCoroutine(specialTransition());
                        tutorialController.isComboObjectiveCompleted = true;
                    }
                    else {
                        StartCoroutine(attackTransition());
                        tutorialController.checkAttackDirectionChecklist((int)ComboDirection.down);
                    }
                    return;
                }
                if (keyCode == (KeyCode.D)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    if (combo.addToStack("Right", tutorialController.isComboEnabled)) {
                        StartCoroutine(specialTransition());
                        tutorialController.isComboObjectiveCompleted = true;
                    }
                    else {
                        StartCoroutine(attackTransition());
                        tutorialController.checkAttackDirectionChecklist((int)ComboDirection.right);
                    }
                    return;
                }
            }
        }
        else {
            if (tutorialController.isMovementEnabled) {
                if (keyCode == (KeyCode.W)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    newPosition = transform.position + new Vector3(0, 0, 1);
                    if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                        sound = movementSounds[0];
                        sound.Play();

                        anim.SetBool("Up", true);
                        StartCoroutine(locationTransition(transform.position, newPosition));
                        tutorialController.checkDirectionChecklist((int)ComboDirection.up);
                    }
                    return;
                }
                if (keyCode == (KeyCode.A)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    newPosition = transform.position + new Vector3(-1, 0, 0);
                    if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                        sound = movementSounds[1];
                        sound.Play();

                        anim.SetBool("Left", true);
                        StartCoroutine(locationTransition(transform.position, newPosition));
                        tutorialController.checkDirectionChecklist((int)ComboDirection.left);
                    }
                    return;
                }
                if (keyCode == (KeyCode.S)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    newPosition = transform.position + new Vector3(0, 0, -1);
                    if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                        sound = movementSounds[2];
                        sound.Play();

                        anim.SetBool("Down", true);
                        StartCoroutine(locationTransition(transform.position, newPosition));
                        tutorialController.checkDirectionChecklist((int)ComboDirection.down);
                    }
                    return;
                }
                if (keyCode == (KeyCode.D)) {
                    rhythmController.IsTimeForPlayerAction = false;
                    newPosition = transform.position + new Vector3(1, 0, 0);
                    if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                        sound = movementSounds[3];
                        sound.Play();

                        anim.SetBool("Right", true);
                        StartCoroutine(locationTransition(transform.position, newPosition));
                        tutorialController.checkDirectionChecklist((int)ComboDirection.right);
                    }
                    return;
                }
            }
        }

    }

    private void doAttack() {
        if (tutorialController.isBeatEnabled) {
            if (!rhythmController.IsTimeForPlayerAction) {
                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                    failBeat();
                }
                return;
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
            EventManager.TriggerEvent("FeedbackDrum");
            if (tutorialController.isComboEnabled) {
                rhythmController.playerComboCount += 1;
                EventManager.TriggerEvent("UpdateCounter");
            }
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Up", tutorialController.isComboEnabled)) {
                StartCoroutine(specialTransition());
                tutorialController.isComboObjectiveCompleted = true;
            }
            else {
                StartCoroutine(attackTransition());
                tutorialController.checkAttackDirectionChecklist((int)ComboDirection.up);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Left", tutorialController.isComboEnabled)) {
                StartCoroutine(specialTransition());
                tutorialController.isComboObjectiveCompleted = true;
            }
            else {
                StartCoroutine(attackTransition());
                tutorialController.checkAttackDirectionChecklist((int)ComboDirection.left);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Down", tutorialController.isComboEnabled)) {
                StartCoroutine(specialTransition());
                tutorialController.isComboObjectiveCompleted = true;
            }
            else {
                StartCoroutine(attackTransition());
                tutorialController.checkAttackDirectionChecklist((int)ComboDirection.down);
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Right", tutorialController.isComboEnabled)) {
                StartCoroutine(specialTransition());
                tutorialController.isComboObjectiveCompleted = true;
            }
            else {
                StartCoroutine(attackTransition());
                tutorialController.checkAttackDirectionChecklist((int)ComboDirection.right);
            }
            return;
        }

    }

    IEnumerator locationTransition(Vector3 startPosition, Vector3 endPosition) {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.05f;
        while (currentAnimationTime < totalAnimationTime) {
            currentAnimationTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator attackTransition() {

        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.3f;
        while (currentAnimationTime < totalAnimationTime) {
            currentAnimationTime += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
        resetAttackAnimation();
    }

    IEnumerator specialTransition() {

        //float currentAnimationTime = 0.0f;
        //float totalAnimationTime = 0.3f;
        rhythmController.IsSpecialOccurring = true;
        EventManager.TriggerEvent("DeactivateDrum");
        //while (currentAnimationTime < totalAnimationTime) {
        //currentAnimationTime += Time.deltaTime;
        yield return new WaitForSeconds(1.5f);
        //}
        resetAttackAnimation();
        Debug.Log("Special Transition");
        anim.SetBool("C1Atk", false);
        anim.SetBool("C2Atk", false);
        anim.SetBool("C3Atk", false);
        anim.SetBool("C4Atk", false);
        rhythmController.IsSpecialOccurring = false;
    }


    private void resetAttackAnimation() {
        anim.SetBool("UpAtk", false);
        anim.SetBool("DownAtk", false);
        anim.SetBool("LeftAtk", false);
        anim.SetBool("RightAtk", false);
    }



    public void takeDamage() {
        Debug.Log("player took damage");
        playerHealth--;
        EventManager.TriggerEvent("ReducePlayerHP");
        checkAndDestroyIfDead();
    }

    private void checkAndDestroyIfDead() {
        if (playerHealth <= 0) {
            Debug.Log("player dead");
            Destroy(this.gameObject);
        }
    }

    private void failBeat() {
        Debug.Log("Fail Happened");
        EventManager.TriggerEvent("DeactivateDrum");
        rhythmController.playerComboCount = 0;
        EventManager.TriggerEvent("UpdateCounter");
        //combo.clearCombo();
    }
}
