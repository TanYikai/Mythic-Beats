using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

    private float timeTillExpire;
    private GameObject user;

    // Use this for initialization
    void Start() {
        timeTillExpire = 0.5f;
        Destroy(this.gameObject, timeTillExpire);
    }

    public void setup(GameObject user, Vector3 position) {
        this.user = user;
        this.transform.position = position;
    }

    private void OnTriggerEnter(Collider other) {
        // enemy and player should fall under the same base class
        if (other.gameObject.tag == "Enemy" && other.gameObject != user) {
            other.gameObject.GetComponent<Enemy>().takeDamage();
        }

        if (other.gameObject.tag == "Player" && other.gameObject != user) {
            other.gameObject.GetComponent<Player>().takeDamage();
        }
    }
}
