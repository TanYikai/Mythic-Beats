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

	// Use this for initialization
	void Start () {
        attackIsDone = false;
        chargeCount = 0;
        currentSkill = null;

        skillDecider = new EnemySkillDecider();

        parent = this.gameObject;
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
}
