using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour {

    private GameObject spells;
    private GameObject user;

    void Start() {
        spells = Resources.Load<GameObject>("Prefabs/Spells");
        user = this.gameObject;
    }

    public void determineCombo(string combo) {
        switch (combo) {
            case "0":
                attackFront();
                break;
            case "1":
                attackBack();
                break;
            case "2":
                attackLeft();
                break;
            case "3":
                attackRight();
                break;
        }
    }

    private void attackFront() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 2);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackBack() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, -2);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackLeft() {
        Vector3 targetPosition = user.transform.position + new Vector3(-2, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackRight() {
        Vector3 targetPosition = user.transform.position + new Vector3(2, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }
}
