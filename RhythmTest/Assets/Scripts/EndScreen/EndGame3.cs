using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour {

    private SceneChanger sceneChanger;

    // Use this for initialization
    void Start () {
        sceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChanger>();
        sceneChanger.waitAndFadeToScene("StartScreen", 10.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
