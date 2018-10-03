using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Combo : MonoBehaviour {

    private GameObject spells;
    private GameObject user;
    private Queue<string> queue;
    public Animator anim;

    void Start() {
        spells = Resources.Load<GameObject>("Prefabs/Spells");
        user = this.gameObject;
        queue = new Queue<string>();
    }

    public bool addToStack(string command) {
        string[] arrayOfCommand = queue.ToArray();

        switch (queue.Count) {
            case 0:
                if (command == "Left") {
                    queue.Enqueue("Left");
                }
                break;
            case 1:
                if (command != "Left") {
                    queue.Enqueue(command);
                }
                break;
            case 2:
                if ((arrayOfCommand[1] == "Up" && command == "Down") ||
                    (arrayOfCommand[1] == "Right" && command == "Up") ||
                    (arrayOfCommand[1] == "Right" && command == "Left") ||
                    (arrayOfCommand[1] == "Down" && command == "Up")) {
                    queue.Enqueue(command);
                }
                else {
                    queue.Clear();
                    if (command == "Left") {
                        queue.Enqueue("Left");
                    }
                }
                break;
            case 3:
                if ((arrayOfCommand[2] == "Down" && command == "Right") ||
                    (arrayOfCommand[2] == "Up" && command == "Down") ||
                    (arrayOfCommand[2] == "Left" && command == "Right") ||
                    (arrayOfCommand[2] == "Up" && command == "Right")) {
                    queue.Enqueue(command);
                    determineCombo(buildComboCommand());
                    return true;
                }
                else {
                    queue.Clear();
                    if (command == "Left") {
                        queue.Enqueue("Left");
                    }
                }
                break;
            default:
                Debug.LogError("Error encountered: Hit default in Combo");
                return false;
        }

        determineCombo(command);

        return false;
    }

    private string buildComboCommand() {
        StringBuilder sb = new StringBuilder();

        while (queue.Count > 0) {
            sb.Append(queue.Dequeue());
        }

        return sb.ToString();
    }

    private void determineCombo(string command) {

            switch (command) {
                case "Up":
                    attackFront();
                    break;
                case "Down":
                    attackBack();
                    break;
                case "Left":
                    attackLeft();
                    break;
                case "Right":
                    attackRight();
                    break;
                case "LeftUpDownRight":
                anim.SetBool("SpcAtk", true);
                Debug.Log("LeftUpDownRight combo happened");
                    break;
                case "LeftRightUpDown":
                    Debug.Log("LeftRightUpDown combo happened");
                    break;
                case "LeftRightLeftRight":
                    Debug.Log("LeftRightLeftRight combo happened");
                    break;
                case "LeftDownUpRight":
                    Debug.Log("LeftDownUpRight combo happened");
                    break;
                default:
                    Debug.LogError("Combo makes no sense");
                    break;
            }
    }

    private void attackFront() {
        Debug.Log("front");
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        anim.SetBool("UpAtk", true);
    }

    private void attackBack() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, -1);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        anim.SetBool("DownAtk", true);
    }

    private void attackLeft() {
        Vector3 targetPosition = user.transform.position + new Vector3(-1, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        anim.SetBool("LeftAtk", true);
    }

    private void attackRight() {
        Vector3 targetPosition = user.transform.position + new Vector3(1, 0, 0);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition);
        anim.SetBool("RightAtk", true);
    }



}
