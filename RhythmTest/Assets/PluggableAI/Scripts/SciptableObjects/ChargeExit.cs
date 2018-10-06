using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Exits/Charge")]
public class ChargeExit : Exit {

    public override void OnExit(StateController controller) {
        exit(controller);
    }

    private void exit(StateController controller) {
        controller.resetCharge();
    }
}
