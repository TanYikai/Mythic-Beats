using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Combo : MonoBehaviour {

    private GameObject spells;
    private GameObject user;

    void Start() {
        spells = Resources.Load<GameObject>("Prefabs/Spells");
        user = this.gameObject;
    }

    public void determineCombo(Stack<string> combo) {


        if (combo.Count >= 4)
        {
            StringBuilder sb = new StringBuilder();
            while (combo.Count > 0)
            {
                string word = combo.Pop();
                sb.Append(word);
            }
            switch (sb.ToString())
            {
                case "LeftUpDownRight":
                    Debug.Log("Something cool is supposed to happen");
                    break;
                case "LeftRightUpDown":
                    Debug.Log("Something cool is supposed to happen");
                    break;
                case "LeftRightLeftRight":
                    Debug.Log("Something cool is supposed to happen");
                    break;
                case "LeftDownUpDown":
                    Debug.Log("Something cool is supposed to happen");
                    break;
                default:
                    Debug.Log("Combo makes no sense");
                    break;
            }


        }
        else
        {
            string word = combo.Peek();
            switch (word)
            {
                case "Up":
                    attackFront();
                    break;
                case "Left":
                    attackBack();
                    break;
                case "Right":
                    attackLeft();
                    break;
                case "Down":
                    attackRight();
                    break;
            }
        }
    }

    private void attackFront() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackBack() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, -1);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackLeft() {
        Vector3 targetPosition = user.transform.position + new Vector3(-1, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }

    private void attackRight() {
        Vector3 targetPosition = user.transform.position + new Vector3(1, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
    }
}
