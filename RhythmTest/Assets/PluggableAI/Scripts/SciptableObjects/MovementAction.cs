using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Movement")]
public class MovementAction : Action {

    public override void Act(StateController controller) {
        move(controller);
    }

    private void move(StateController controller) {
        controller.doMovement();
    }
}
