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

    //private string comboStack;

    private Combo combo;

    // To allow debug without VR
    GameObject mainCamera;

    // Use this for initialization
    void Start () {
        rhythmController = GameObject.Find("Rhythm").GetComponent<Rhythm>();
        enemy = GameObject.Find("Enemy");
        playerMovementRestrictor = new PlayerMovementRestrictor(enemy);
        combo = this.gameObject.GetComponent<Combo>();
        mainCamera = GameObject.Find("Main Camera");
    }
	
	// Update is called once per frame
	void Update () {
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

        if (!rhythmController.IsTimeForPlayerAction)
        {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
                failBeat();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, 1);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                StartCoroutine(locationTransition(transform.position, newPosition));
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(-1, 0, 0);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                StartCoroutine(locationTransition(transform.position, newPosition));
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(0, 0, -1);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                StartCoroutine(locationTransition(transform.position, newPosition));
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            newPosition = transform.position + new Vector3(1, 0, 0);
            if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                StartCoroutine(locationTransition(transform.position, newPosition));
            }
            return;
        }
    }

    public void ExecuteKey(KeyCode keyCode, bool comboMode) {
        Vector3 newPosition;

        if (!rhythmController.IsTimeForPlayerAction) {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                failBeat();
            }
            return;
        }

        if (comboMode) {
            if (Input.GetKeyDown(KeyCode.W)) {
                rhythmController.IsTimeForPlayerAction = false;
                combo.addToStack("Up");
                return;
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                rhythmController.IsTimeForPlayerAction = false;
                combo.addToStack("Left");
                return;
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                rhythmController.IsTimeForPlayerAction = false;
                combo.addToStack("Down");
                return;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                rhythmController.IsTimeForPlayerAction = false;
                combo.addToStack("Right");
                return;
            }
        }
        else {
            if (keyCode == (KeyCode.W)) {
                rhythmController.IsTimeForPlayerAction = false;
                newPosition = transform.position + new Vector3(0, 0, 2);
                if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                return;
            }
            if (keyCode == (KeyCode.A)) {
                rhythmController.IsTimeForPlayerAction = false;
                newPosition = transform.position + new Vector3(-2, 0, 0);
                if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                return;
            }
            if (keyCode == (KeyCode.S)) {
                rhythmController.IsTimeForPlayerAction = false;
                newPosition = transform.position + new Vector3(0, 0, -2);
                if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                return;
            }
            if (keyCode == (KeyCode.D)) {
                rhythmController.IsTimeForPlayerAction = false;
                newPosition = transform.position + new Vector3(2, 0, 0);
                if (playerMovementRestrictor.checkValidPosition(newPosition)) {
                    transform.position = newPosition;
                }
                return;
            }
        }

    }

    private void doAttack() {
        //if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) { // for easier testing


        if (!rhythmController.IsTimeForPlayerAction) {
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))) {
                //failBeat();
            }
            return;
        }


        if (Input.GetKeyDown(KeyCode.W)) {
            rhythmController.IsTimeForPlayerAction = false;
            combo.addToStack("Up");
            return;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            rhythmController.IsTimeForPlayerAction = false;
            combo.addToStack("Left");
            return;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            rhythmController.IsTimeForPlayerAction = false;
            combo.addToStack("Down");
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            rhythmController.IsTimeForPlayerAction = false;
            combo.addToStack("Right");
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
