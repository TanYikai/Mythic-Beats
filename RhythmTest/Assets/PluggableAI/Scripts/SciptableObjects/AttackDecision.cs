using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Attack")]
public class AttackDecision : Decision {

    public override bool Decide(StateController controller) {
        bool castSkill = checkOnAttack(controller);
        return castSkill;
    }

    private bool checkOnAttack(StateController controller) {
        if (controller.attackIsDone == true) {
            return true;
        }

        return false;
    }
}
