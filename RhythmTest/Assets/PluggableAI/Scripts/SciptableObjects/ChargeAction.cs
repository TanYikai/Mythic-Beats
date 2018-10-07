using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Charge")]
public class ChargeAction : Action {

    public override void Act(StateController controller) {
        charging(controller);
    }

    private void charging(StateController controller) {
        Debug.Log("charging");
        if (controller.currentSkill == null) {
            controller.decideSkill();
        }

        controller.incrementCharge();

        showTelegraphAttack(controller);
    }

    private void showTelegraphAttack(StateController controller) {
        int chargeLeft = controller.currentSkill.getCharge() - controller.chargeCount;

        controller.currentSkill.handleTelegraphAttack(controller.getParent().transform.position, chargeLeft);
    }
}
