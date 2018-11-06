using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour {

    private enum TutorialStage {
        MovementWithoutBeat,
        MovementWithBeat,
        TogglingToAttack,
        NormalAttack,
        ComboAttack,
        TransitionToMainGame
    };

    private TutorialStage currentStage;
    [HideInInspector] public bool isTextShowing;
    [HideInInspector] public bool isMovementEnabled;
    [HideInInspector] public bool isBeatEnabled;
    [HideInInspector] public bool isAttackTogglingEnabled;
    [HideInInspector] public bool isComboEnabled;
    [HideInInspector] public bool isAttackTogglingObjectiveCompleted;
    [HideInInspector] public bool isComboObjectiveCompleted;
    [HideInInspector] public bool isSpecial;    // to handle special case where text is showing but action is still enabled
    private bool[] directionChecklist; // in order of up, down, left, right
    private bool[] attackDirectionChecklist; // in order of up, down, left, right

    private GameObject enemy;
    private EnemyTutorial enemyTutorialScript;
    private TextController textController;

    private SceneChanger sceneChanger;

    void Awake() {
        enemy = GameObject.Find("Enemy");
        enemyTutorialScript = enemy.GetComponentInChildren<EnemyTutorial>();
        textController = GameObject.Find("DisplayText").GetComponent<TextController>();

        setupMovementWithoutBeatStage();
    }

    void Start() {
        StartCoroutine(LateStart());
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
    }

    IEnumerator LateStart() {
        yield return new WaitForEndOfFrame();
        enemy.SetActive(false);
    }

    void Update() {
        switch (currentStage) {
            case TutorialStage.MovementWithoutBeat:
                if (checkDirectionChecklist()) {
                    setupMovementWithBeatStage();
                    displayTextAfterStage();
                }
                break;
            case TutorialStage.MovementWithBeat:
                if (checkDirectionChecklist()) {
                    setupTogglingToAttackStage();
                    displayTextAfterStage();
                }
                break;
            case TutorialStage.TogglingToAttack:
                if (isAttackTogglingObjectiveCompleted) {
                    setupNormalAttackStage();
                    displayTextAfterStage();
                }
                break;
            case TutorialStage.NormalAttack:
                if (checkAttackDirectionChecklist()) {
                    setupComboAttackStage();
                    displayTextAfterStage();
                }
                break;
            case TutorialStage.ComboAttack:
                if (isComboObjectiveCompleted && enemyTutorialScript.getIsDead()) {
                    setupTransitionToMainGame();
                    displayTextAfterStage();
                }
                break;
            case TutorialStage.TransitionToMainGame:
                if (!isTextShowing) {
                    transitToMainGame();
                }
                break;
        }    
    }

    private void displayTextAfterStage() {
        textController.transform.parent.gameObject.SetActive(true);
        progressText();
    }

    public void progressText() {
        if (textController.getIsTextProceedPossible()) {
            bool isTextRemainVisible = textController.proceedAndDisplayNextText();
            isSpecial = textController.isTextSpecial();
            if (!isTextRemainVisible) {
                isTextShowing = false;
            }
        }
    }

    private bool checkDirectionChecklist() {
        foreach (bool val in directionChecklist) {
            if (val == false) {
                return false;
            }
        }
        return true;
    }

    private bool checkAttackDirectionChecklist() {
        foreach (bool val in attackDirectionChecklist) {
            if (val == false) {
                return false;
            }
        }
        return true;
    }

    private void transitToMainGame() {
        // do nothing for now
        Debug.Log("Transiting to main game");
        sceneChanger.fadeToScene("SampleScene");
    }

    public void checkDirectionChecklist(int direction) {
        directionChecklist[direction] = true;
    }

    public void checkAttackDirectionChecklist(int direction) {
        attackDirectionChecklist[direction] = true;
    }

    private void setupMovementWithoutBeatStage() {
        currentStage = TutorialStage.MovementWithoutBeat;
        isTextShowing = true;
        isMovementEnabled = true;
        isBeatEnabled = false;
        isAttackTogglingEnabled = false;
        isComboEnabled = false;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;
        Debug.Log("Entering movement without beat stage");
    }

    private void setupMovementWithBeatStage() {
        currentStage = TutorialStage.MovementWithBeat;
        isTextShowing = true;
        isMovementEnabled = true;
        isBeatEnabled = true;
        isAttackTogglingEnabled = false;
        isComboEnabled = false;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;
        Debug.Log("Entering movement with beat stage");
    }

    private void setupTogglingToAttackStage() {
        currentStage = TutorialStage.TogglingToAttack;
        isTextShowing = true;
        isMovementEnabled = false;
        isBeatEnabled = false;
        isAttackTogglingEnabled = true;
        isComboEnabled = false;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;
        Debug.Log("Entering toggling to attack stage");
    }

    private void setupNormalAttackStage() {
        currentStage = TutorialStage.NormalAttack;
        isTextShowing = true;
        isMovementEnabled = true;
        isBeatEnabled = true;
        isAttackTogglingEnabled = true;
        isComboEnabled = false;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;

        enemy.SetActive(true);

        Debug.Log("Entering normal attack stage");
    }

    private void setupComboAttackStage() {
        currentStage = TutorialStage.ComboAttack;
        isTextShowing = true;
        isMovementEnabled = true;
        isBeatEnabled = true;
        isAttackTogglingEnabled = true;
        isComboEnabled = true;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;

        enemyTutorialScript.resetEnemyHealth();

        Debug.Log("Entering combo attack stage");
    }

    private void setupTransitionToMainGame() {
        currentStage = TutorialStage.TransitionToMainGame;
        isTextShowing = true;
        isMovementEnabled = true;
        isBeatEnabled = true;
        isAttackTogglingEnabled = true;
        isComboEnabled = true;
        directionChecklist = new bool[4];
        attackDirectionChecklist = new bool[4];
        isAttackTogglingObjectiveCompleted = false;
        isComboObjectiveCompleted = false;
        isSpecial = false;

        Debug.Log("Entering transition to main game stage");
    }
}
