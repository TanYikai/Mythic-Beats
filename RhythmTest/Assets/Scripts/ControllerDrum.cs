using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDrum : MonoBehaviour {

    public KeyCode keyCode;

    private SteamVR_TrackedObject trackedObj;
    private PlayerControlController playerControl;
    private Material drumRimMat;


    void Start () {
        drumRimMat = this.GetComponentInChildren<Renderer>().materials[4];
    }


    private void Awake() {

    }

    private void Update() {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("DrumStick")) {
            playerControl.drumHit(keyCode, collider.gameObject.GetComponent<ControllerStick>().id);
        }
    }


    public void flashDrumColor(Color color) {
        StartCoroutine(colorTransition(drumRimMat.color, color));
    }

    IEnumerator colorTransition(Color initialColor, Color finalColor)
    {
        float currentAnimationTime = 0.0f;
        float totalAnimationTime = 0.1f;
        while (currentAnimationTime < totalAnimationTime)
        {
            currentAnimationTime += Time.deltaTime;
            drumRimMat.color = Color.Lerp(initialColor, finalColor, currentAnimationTime / totalAnimationTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
