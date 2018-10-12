using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public State currentState;

    [HideInInspector] public bool attackIsDone;
    [HideInInspector] public int chargeCount;
    [HideInInspector] public EnemySkills currentSkill;

    private EnemySkillDecider skillDecider;
    private GameObject parent;
    private Enemy enemyScript;

    public Animator anim;

	// Use this for initialization
	void Start () {
        attackIsDone = false;
        chargeCount = 0;
        currentSkill = null;

        skillDecider = new EnemySkillDecider();

        parent = this.gameObject;
        enemyScript = gameObject.GetComponent<Enemy>();
	}

    public void UpdateState() {
        //anim.SetBool("B_Left", false);
        //anim.SetBool("B_Right", false);
        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState) {
        if (nextState != currentState) {
            currentState.OnExitState(this);
            currentState = nextState;
        }
    }

    public void decideSkill() {
        anim.SetBool("B_Atk", true);
        currentSkill = skillDecider.decideSkill(parent);
    }

    public void incrementCharge() {
        chargeCount += 1;
    }

    public void resetSkill() {
        currentSkill = null;
    }

    public void resetCharge() {
        chargeCount = 0;
    }

    public GameObject getParent() {
        return parent;
    }

    public bool checkValidGeneralPosition(Vector3 pos) {
        return enemyScript.checkValidGeneralPosition(pos);
    }

    public void doMovement() {
        enemyScript.doMovement();
    }

    public bool decideMoveOrCharge() {
        int value = Random.Range(0, 10);
        if (value < 4) {
            return false;
        }
        return true;
    }
}
