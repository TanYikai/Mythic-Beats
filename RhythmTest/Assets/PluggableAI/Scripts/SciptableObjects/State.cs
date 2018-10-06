using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject {

    public Action[] actions;
    public Transition[] transitions;
    public Exit[] exits;
    public bool toSelectOnlyOneTransition;
    public int[] selectionWeights;

    public void UpdateState(StateController controller) {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller) {
        if (toSelectOnlyOneTransition) {
            int choice = selectTransition();
            bool decisionSucceeded = transitions[choice].decision.Decide(controller);

            if (decisionSucceeded) {
                controller.TransitionToState(transitions[choice].trueState);
            }
            else {
                controller.TransitionToState(transitions[choice].falseState);
            }
        }
        else {
            for (int i = 0; i < transitions.Length; i++) {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);

                if (decisionSucceeded) {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }

    private int selectTransition() {
        if (selectionWeights.Length == 0 || selectionWeights.Length != transitions.Length) {
            Debug.LogError("Selection weights must be specified for each transition");
        }
        else {
            int totalWeightValue = 0;
            int[] cumulativeSum = new int[selectionWeights.Length];

            for (int i = 0; i < selectionWeights.Length; i++) {
                totalWeightValue += selectionWeights[i];
                cumulativeSum[i] = totalWeightValue;
            }

            int randomValue = Random.Range(0, totalWeightValue);

            for (int i = 0; i < cumulativeSum.Length; i++) {
                if (randomValue < cumulativeSum[i]) {
                    return i;
                }
            }
        }

        return -1;
    }

    public void OnExitState(StateController controller) {
        for (int i = 0; i < exits.Length; i++) {
            exits[i].OnExit(controller);
        }
    }
}
