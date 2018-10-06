using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Exits/Movement")]
public class MovementExit : Exit {
    public override void OnExit(StateController controller) {
        exit(controller);
    }

    private void exit(StateController controller) {
        // does nothing
    }
}
