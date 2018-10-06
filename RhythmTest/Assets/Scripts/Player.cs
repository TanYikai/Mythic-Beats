using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    enum ComboDirection {
        up,
        down,
        left,
        right
    };

    public int lengthOfCombo;
    public int playerHealth;

    private Rhythm rhythmController;
    private PlayerMovementRestrictor playerMovementRestrictor;
    private GameObject enemy;
    public Animator anim;

    //private string comboStack;

    private Combo combo;

    // To allow debug without VR
    GameObject mainCamera;

    // movement sound effects
    private AudioSource[] movementSounds;

    // Use this for initialization
    void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        enemy = GameObject.Find("Enemy");
        playerMovementRestrictor = new PlayerMovementRestrictor(enemy);
        combo = this.gameObject.GetComponent<Combo>();
        mainCamera = GameObject.Find("Main Camera");

        movementSounds = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Up", false);
        anim.SetBool("Down", false);
        anim.SetBool("Left", false);
        anim.SetBool("Right", false);


        if (mainCamera) {
            if (Input.GetButton("Fire1")) {
                doAttack();
            }
            else {
                doMovement();
            }
        }
	}

    private void doMovement() {
        Vector3 newPosition;
        AudioSource sound;

        if (!rhythmController.IsTimeForPlayerAction) {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                failBeat();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, 1);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                sound = movementSounds[0];
                sound.Play();

                anim.SetBool("Up", true);
                StartCoroutine(locationTransition(transform.position, newPosition));
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
            }
            return;
        }
    }

    public void ExecuteKey(KeyCode keyCode, bool attackMode) {
        Vector3 newPosition;
        AudioSource sound;

        if (!rhythmController.IsTimeForPlayerAction) {
            failBeat();
            return;
        }

        if (attackMode) {
            if (keyCode == (KeyCode.W)) {
                rhythmController.IsTimeForPlayerAction = false;
                if (combo.addToStack("Up")){
                    StartCoroutine(specialTransition());
                } else {
                    StartCoroutine(attackTransition());
                }
                return;
            }
            if (keyCode == (KeyCode.A)) {
                rhythmController.IsTimeForPlayerAction = false;
                if (combo.addToStack("Left")) {
                    StartCoroutine(specialTransition());
                }
                else {
                    StartCoroutine(attackTransition());
                }
                return;
            }
            if (keyCode == (KeyCode.S)) {
                rhythmController.IsTimeForPlayerAction = false;
                if (combo.addToStack("Down")) {
                    StartCoroutine(specialTransition());
                }
                else {
                    StartCoroutine(attackTransition());
                }
                return;
            }
            if (keyCode == (KeyCode.D)) {
                rhythmController.IsTimeForPlayerAction = false;
                if (combo.addToStack("Right")) {
                    StartCoroutine(specialTransition());
                }
                else {
                    StartCoroutine(attackTransition());
                }
                return;
            }
        }
        else {
            if (keyCode == (KeyCode.W)) {
                rhythmController.IsTimeForPlayerAction = false;
                newPosition = transform.position + new Vector3(0, 0, 1);
                if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                    sound = movementSounds[0];
                    sound.Play();

                    anim.SetBool("Up", true);
                    StartCoroutine(locationTransition(transform.position, newPosition));
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
                }
                return;
            }
        }

    }

    private void doAttack() {
        //if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) { // for easier testing


        if (!rhythmController.IsTimeForPlayerAction) {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                failBeat();
            }
            return;
        }


        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Up")) {
                StartCoroutine(specialTransition());
            }
            else {
                StartCoroutine(attackTransition());
            }       
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Left")) {
                StartCoroutine(specialTransition());
            }
            else {
                StartCoroutine(attackTransition());
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Down")) {
                StartCoroutine(specialTransition());
            }
            else {
                StartCoroutine(attackTransition());
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            if (combo.addToStack("Right")) {
                StartCoroutine(specialTransition());
            }
            else {
                StartCoroutine(attackTransition());
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
        //while (currentAnimationTime < totalAnimationTime) {
            //currentAnimationTime += Time.deltaTime;
            yield return new WaitForSeconds(2f);
        //}
        resetAttackAnimation();
        Debug.Log("Special Transition");
        anim.SetBool("SpcAtk", false);
        rhythmController.IsSpecialOccurring = false;
    }


    private void resetAttackAnimation() {
        anim.SetBool("UpAtk", false);
        anim.SetBool("DownAtk", false);
        anim.SetBool("LeftAtk", false);
        anim.SetBool("RightAtk", false);
    }

    

    public void takeDamage() {
        playerHealth--;
        checkAndDestroyIfDead();
    }

    private void checkAndDestroyIfDead() {
        if (playerHealth <= 0) {
            Debug.Log("player dead");
            Destroy(this.gameObject);
        }
    }

    private void failBeat() {
        rhythmController.IsNextWindowDisabled = true;
    }

}
