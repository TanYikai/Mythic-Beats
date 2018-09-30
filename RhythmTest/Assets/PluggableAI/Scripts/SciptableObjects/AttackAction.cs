using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action {

    public override void Act(StateController controller) {
        attack(controller);
    }

    private void attack(StateController controller) {
        controller.currentSkill.doSkill(controller.getParent().transform.position);
        controller.attackIsDone = true;
    }
}
