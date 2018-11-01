using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

    private float timeTillExpire;
    private GameObject user;
    private GameObject player;
    private GameObject enemy;
    private string type;
    private Vector3 position;
    public GameObject[] effects;
    private AudioSource[] soundEffects;
    private bool spellDestroyed = false;

    // Use this for initialization
    void Start() { 
        // only for projectile. others is just a dummy
        timeTillExpire = 2f;
        Destroy(this.transform.parent.gameObject, timeTillExpire);

        player = GameObject.Find("Player").gameObject;
        enemy = GameObject.Find("Enemy").gameObject;
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
        Vector3 playerPos = user.transform.position;
        bool dmgIsToBeDone = false;

        switch (type) {
            case "bFront":
                // attack effects
                spell = Instantiate(effects[0], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                // sound
                sound = soundEffects[0];
                sound.Play();
                // damage
                if (DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 1, Mathf.RoundToInt(playerPos.x))) {
                    DamageController.instance.doDamageToEnemy(1);
                };
                break;
            case "bBack":
                // attack effects
                Vector3 frontPosition = position + new Vector3(0, 0, 1f);
                spell = Instantiate(effects[1], position, Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[1], frontPosition, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                // sound
                sound = soundEffects[1];
                sound.Play();
                // damage
                if (DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 1, Mathf.RoundToInt(playerPos.x)) ||
                    DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 2, Mathf.RoundToInt(playerPos.x))) {
                    DamageController.instance.doDamageToEnemy(2);
                }
                break;
            case "bLeft":
                // attack effects
                frontPosition = position + new Vector3(0, 0, 1f);
                spell = Instantiate(effects[2], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                spell = Instantiate(effects[3], frontPosition, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                // sound
                sound = soundEffects[2];
                sound.Play();
                // damaage
                if (DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 1, Mathf.RoundToInt(playerPos.x) - 1) ||
                   DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 2, Mathf.RoundToInt(playerPos.x) - 1)) {
                    DamageController.instance.doDamageToEnemy(1);
                }
                break;
            case "bRight":
                // attack effects
                frontPosition = position + new Vector3(0, 0, 1f);
                spell = Instantiate(effects[2], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                spell = Instantiate(effects[3], frontPosition, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 0.5f);
                // sound
                sound = soundEffects[3];
                sound.Play();
                // damage
                if (DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 1, Mathf.RoundToInt(playerPos.x) + 1) ||
                    DamageController.instance.checkIfEnemyIsInRange(Mathf.RoundToInt(playerPos.z) + 2, Mathf.RoundToInt(playerPos.x) + 1)) {
                    DamageController.instance.doDamageToEnemy(1);
                }
                break;
            case "combo1":
                // attack effects
                // center
                spell = Instantiate(effects[4], position, Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(0, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(0, 0, 2f), Quaternion.identity, transform) as GameObject;
                // left
                spell = Instantiate(effects[4], position + new Vector3(-1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(-1f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(-1f, 0, 2f), Quaternion.identity, transform) as GameObject;
                // right
                spell = Instantiate(effects[4], position + new Vector3(1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(1f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[4], position + new Vector3(1f, 0, 2f), Quaternion.identity, transform) as GameObject;

                Destroy(spell, 2f);
                // sound
                sound = soundEffects[4];
                sound.Play();
                // damage
                int rowIndex = Mathf.RoundToInt(playerPos.z);
                int colIndex = Mathf.RoundToInt(playerPos.x);

                for (int i = rowIndex + 1; i <= rowIndex + 3; i++) {
                    if (DamageController.instance.checkIfEnemyIsInRange(i, colIndex - 1) ||
                    DamageController.instance.checkIfEnemyIsInRange(i, colIndex) ||
                    DamageController.instance.checkIfEnemyIsInRange(i, colIndex + 1)) {
                        dmgIsToBeDone = true;
                    }
                }
                if (dmgIsToBeDone) {
                    DamageController.instance.doDamageToEnemy(15);
                }
                break;
            case "combo2":
                // attack effects
                spell = Instantiate(effects[5], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                //sound
                sound = soundEffects[5];
                sound.Play();
                // damage checks using collision box
                break;
            case "combo3":
                // attack effects
                // first row
                spell = Instantiate(effects[6], position + new Vector3(-1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-2f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-3f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-4f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position, Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(2f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(3f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(4f, 0, 0), Quaternion.identity, transform) as GameObject;

                // second row
                spell = Instantiate(effects[6], position + new Vector3(-1f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-2f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-3f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(-4f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(0, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(1f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(2f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(3f, 0, 1f), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[6], position + new Vector3(4f, 0, 1f), Quaternion.identity, transform) as GameObject;

                Destroy(spell, 2f);
                //sound
                sound = soundEffects[6];
                sound.Play();
                // damage
                rowIndex = Mathf.RoundToInt(playerPos.z);
                colIndex = Mathf.RoundToInt(playerPos.x);
                int colStartIndex = colIndex - 4;
                int colEndIndex = colIndex + 4;

                for (int i = colStartIndex; i <= colEndIndex; i++) {
                    if (DamageController.instance.checkIfEnemyIsInRange(rowIndex + 1, i) ||
                    DamageController.instance.checkIfEnemyIsInRange(rowIndex + 2, i)) {
                        dmgIsToBeDone = true;
                    }
                }
                if (dmgIsToBeDone) {
                    DamageController.instance.doDamageToEnemy(15);
                }
                break;
            case "combo4":
                // attack effects
                spell = Instantiate(effects[7], position, Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[7], position + new Vector3(1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[7], position + new Vector3(-1f, 0, 0), Quaternion.identity, transform) as GameObject;
                spell = Instantiate(effects[8], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                // sound
                sound = soundEffects[7];
                sound.Play();
                // damage
                rowIndex = Mathf.RoundToInt(playerPos.z);
                if (DamageController.instance.checkIfEnemyIsInRange(rowIndex + 1, Mathf.RoundToInt(playerPos.x) - 1) ||
                DamageController.instance.checkIfEnemyIsInRange(rowIndex + 1, Mathf.RoundToInt(playerPos.x)) ||
                DamageController.instance.checkIfEnemyIsInRange(rowIndex + 1, Mathf.RoundToInt(playerPos.x) + 1)) {
                    DamageController.instance.doDamageToEnemy(15);
                }
                break;
            case "enemyColumnProjectile":
                // attack effects
                spell = Instantiate(effects[10], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                // damage checks using collision box
                break;
            case "enemyColumnAttack":
                // attack effects
                int columnStartIndex = -4;
                int columnEndIndex = 0;

                for (int i = columnStartIndex; i < columnEndIndex + 1; i++) {
                    spell = Instantiate(effects[9], position + new Vector3(1f, -0.3f, i - 0.1f), Quaternion.identity, transform) as GameObject;
                    spell = Instantiate(effects[9], position + new Vector3(-1f, -0.3f, i - 0.1f), Quaternion.identity, transform) as GameObject;
                    spell = Instantiate(effects[9], position + new Vector3(0, -0.3f, i - 0.1f), Quaternion.identity, transform) as GameObject;
                    Destroy(spell, 2f);
                }
                // damage checks in ColumnAttack.cs
                break;
            case "enemyRowAttack":
                // attack effects
                int rowStartIndex = -4;
                int rowEndIndex = 4;

                for (int i = rowStartIndex; i < rowEndIndex + 1; i++) {
                    spell = Instantiate(effects[9], position + new Vector3(i, -0.3f, -0.1f), Quaternion.identity, transform) as GameObject;
                    Destroy(spell, 2f);
                }
                // damage checks in RowAttack.cs
                break;
            case "enemyRandomAttack":
                // attack effects
                spell = Instantiate(effects[9], position, Quaternion.identity, transform) as GameObject;
                Destroy(spell, 2f);
                // damage checks in RandomAttack.cs
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
        if (this.type.Equals("combo2"))
        {
            if (!spellDestroyed) {
                this.transform.Translate(Vector3.forward * 4f * Time.deltaTime);
            }
        }
        else if (this.type.Equals("enemyColumnProjectile")) {
            if (!spellDestroyed)
            {
                this.transform.Translate(Vector3.back * 4f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        // enemy and player should fall under the same base class
        // only if its projectile will check for collision
        if (type.Equals("combo2") && other.gameObject.tag == "Enemy" && other.gameObject != user) {
            //other.gameObject.GetComponent<Enemy>().takeDamage(20);
            DamageController.instance.doDamageToEnemy(10);
            Destroy(gameObject, 0.15f);
            spellDestroyed = true;

            GameObject explode = Instantiate(effects[4], enemy.transform.position, Quaternion.identity, transform) as GameObject;
            Destroy(explode, 0.5f);
        }

        if (type.Equals("enemyColumnProjectile") && other.gameObject.tag == "Player" && other.gameObject != user) {
            other.gameObject.GetComponent<Player>().takeDamage();

            Destroy(gameObject, 0.15f);
            spellDestroyed = true;

            GameObject explode = Instantiate(effects[4], player.transform.position, Quaternion.identity, transform) as GameObject;
            Destroy(explode, 0.5f);
        }
    }
}
