using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Exits/Attack")]
public class AttackExit : Exit {

    public override void OnExit(StateController controller) {
        exit(controller);
    }

    private void exit(StateController controller) {
        controller.resetSkill();
    }
}
