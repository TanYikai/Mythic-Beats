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
                    EventManager.TriggerEvent("UpdateComboStatus");
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
        EventManager.TriggerEvent("UpdateComboStatus");
        Debug.Log("In Combo");

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
                    anim.SetBool("C1Atk", true);
                    Debug.Log("LeftUpDownRight combo happened");
                    combo1();
                    break;
                case "LeftRightUpDown":
                    anim.SetBool("C2Atk", true);
                    Debug.Log("LeftRightUpDown combo happened");
                    combo2();
                    break;
                case "LeftRightLeftRight":
                    anim.SetBool("C3Atk", true);
                    Debug.Log("LeftRightLeftRight combo happened");
                    combo3();
                    break;
                case "LeftDownUpRight":
                    anim.SetBool("C4Atk", true);
                    Debug.Log("LeftDownUpRight combo happened");
                    combo4();
                    break;
                default:
                    Debug.LogError("Combo makes no sense");
                    break;
            }

    }

    private void attackFront() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "bFront");
        anim.SetBool("UpAtk", true);
    }

    private void attackBack() {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "bBack");
        anim.SetBool("DownAtk", true);
    }

    private void attackLeft() {
        Vector3 targetPosition = user.transform.position + new Vector3(-1f, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "bLeft");
        anim.SetBool("LeftAtk", true);
    }

    private void attackRight() {
        Vector3 targetPosition = user.transform.position + new Vector3(1f, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "bRight");
        anim.SetBool("RightAtk", true);
    }

    private void combo1()
    {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "combo1");
    }

    private void combo2()
    {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "combo2");
    }

    private void combo3()
    {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 1f, 1f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "combo3");
    }

    private void combo4()
    {
        Vector3 targetPosition = user.transform.position + new Vector3(0, 0, 0.5f);
        GameObject spell = Instantiate(spells);
        spell.GetComponentInChildren<Spells>().setup(user, targetPosition, "combo4");
    }

    public void clearCombo() {
        queue.Clear();
        EventManager.TriggerEvent("ClearComboText");
    }

    public Queue<string> getQueue() {
        return queue;
    }
}
