using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Exit : ScriptableObject {
    public abstract void OnExit(StateController controller);
}
