using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Movement")]
public class MovementDecision : Decision {

    public override bool Decide(StateController controller) {
        bool isDoneMoving = checkOnMovement(controller);
        return isDoneMoving;
    }

    private bool checkOnMovement(StateController controller) {
        return controller.decideMoveOrCharge();
    }
}
