using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour {

    public State currentState;

    [HideInInspector] public bool attackIsDone;
    [HideInInspector] public int chargeCount;
    [HideInInspector] public EnemySkills currentSkill;

    private EnemySkillDecider skillDecider;
    private GameObject parentObj;
    private Enemy enemyScript;

    public Animator anim;

	// Use this for initialization
	void Start () {
        attackIsDone = false;
        chargeCount = 0;
        currentSkill = null;

        parentObj = this.gameObject;
        enemyScript = gameObject.GetComponent<Enemy>();

        skillDecider = new EnemySkillDecider(parentObj.transform.parent.gameObject);
	}

    public void UpdateState() {
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
        currentSkill = skillDecider.decideSkill();
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
        return parentObj;
    }

    public bool checkValidGeneralPosition(Vector3 pos) {
        return enemyScript.checkValidGeneralPosition(pos);
    }

    public void doMovement() {
        enemyScript.doMovementORStayIdle();
    }

    public bool decideMoveOrCharge() {
        return enemyScript.decideMoveOrCharge();
    }
}
