using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

    private float timeTillExpire;
    private GameObject user;
    private string type;
    private Vector3 position;
    public GameObject[] effects;
    private AudioSource[] soundEffects;

    // Use this for initialization
    void Start() { 
        // only for projectile. others is just a dummy
        timeTillExpire = 2f;
        Destroy(this.transform.parent.gameObject, timeTillExpire);

        soundEffects = GetComponents<AudioSource>();

        StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation() {
        yield return new WaitForSeconds(0.05f); // minus 0.05 because of optimization in checkForBeat()
        StartEffect();
    }

    void StartEffect() {
        GameObject spell;
        AudioSource sound;
        
        switch (type) {
            case "bFront":
                Debug.Log("attack front");
                spell = Instantiate(effects[0], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                sound = soundEffects[0];
                sound.Play();
                break;
            case "bBack":
                spell = Instantiate(effects[1], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                sound = soundEffects[1];
                sound.Play();
                break;
            case "bLeft":
                spell = Instantiate(effects[2], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                spell = Instantiate(effects[3], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                sound = soundEffects[2];
                sound.Play();
                break;
            case "bRight":
                spell = Instantiate(effects[2], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                spell = Instantiate(effects[3], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                sound = soundEffects[3];
                sound.Play();
                break;
            case "combo1":
                spell = Instantiate(effects[4], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                sound = soundEffects[4];
                sound.Play();
                break;
            case "combo2":
                spell = Instantiate(effects[5], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                sound = soundEffects[5];
                sound.Play();
                break;
            case "combo3":
                spell = Instantiate(effects[6], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                sound = soundEffects[6];
                sound.Play();
                break;
            case "combo4":
                Vector3 leftTonardo = position + new Vector3(1f, 0, 0);
                Vector3 righttTonardo = position + new Vector3(-1f, 0, 0);
                spell = Instantiate(effects[7], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                spell = Instantiate(effects[7], leftTonardo, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                spell = Instantiate(effects[7], righttTonardo, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                spell = Instantiate(effects[8], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                sound = soundEffects[7];
                sound.Play();
                break;
        }
    }

    public void setup(GameObject user, Vector3 position, string type) {
        this.user = user;
        this.transform.position = position;
        this.position = position;
        this.type = type;
    }

    void Update() {
            if (this.type.Equals("bBack") || this.type.Equals("bLeft") || this.type.Equals("bRight"))
            { // all basic attacks except front
                this.transform.Translate(Vector3.forward * 3f * Time.deltaTime);
            }
            else if (this.type.Equals("combo2"))
            {
                this.transform.Translate(Vector3.forward * 4f * Time.deltaTime);
            }
    }

    private void OnTriggerEnter(Collider other) {
        // enemy and player should fall under the same base class
        // only if its projectile will check for collision
        if (type.Equals("combo2") && other.gameObject.tag == "Enemy" && other.gameObject != user) {
            Debug.Log("avatar projectile");
            other.gameObject.GetComponent<Enemy>().takeDamage();
        }

        if (other.gameObject.tag == "Player" && other.gameObject != user) {
            other.gameObject.GetComponent<Player>().takeDamage();
        }
    }
}
