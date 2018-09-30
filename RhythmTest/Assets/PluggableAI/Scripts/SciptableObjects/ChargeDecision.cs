using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Charge")]
public class ChargeDecision : Decision {

    public override bool Decide(StateController controller) {
        bool isDoneCharging = checkOnCharging(controller);
        return isDoneCharging;
    }

    private bool checkOnCharging(StateController controller) {
        if (controller.chargeCount >= controller.currentSkill.getCharge()) {
            return true;
        }

        return false;
    }
}
