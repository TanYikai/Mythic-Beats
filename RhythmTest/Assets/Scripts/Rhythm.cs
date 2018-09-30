using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour {

    public float beatsPerMinute;
    public float timeBeforeBeatStarts;
    public float timeDurationForAction;

    private AudioSource[] clips;
    private AudioSource introClip;
    private AudioSource loopClip;
    private bool isTimeForPlayerAction;
    private float actionTimeErrorMargin; // half the error margin time
    private float secondsPerBeat;
    private bool isActionWindowOpenedBeforeThisBeat;

    public delegate void OnBeat();

    public event OnBeat onBeat;

    void OnEnable () {
        onBeat += openWindowForActionHandler;
    }

    // Use this for initialization
    void Start() {
        isTimeForPlayerAction = false;
        actionTimeErrorMargin = timeDurationForAction / 2.0f;
        secondsPerBeat = 60 / beatsPerMinute;
        clips = GetComponents<AudioSource>();
        introClip = clips[0];
        loopClip = clips[1];
        isActionWindowOpenedBeforeThisBeat = false;

        //StartCoroutine(waitForNextBeat());
        //StartCoroutine(playDrumBeatLoop());
        introClip.Play();
        loopClip.PlayDelayed(introClip.clip.length);

        StartCoroutine(waitForIntro());
    }

    // Update is called once per frame
    void Update() {

    }

    public bool IsTimeForPlayerAction {
        get {
            return isTimeForPlayerAction;
        }
        set {
            isTimeForPlayerAction = value;
        }
    }

    void openWindowForActionHandler() {
        StartCoroutine(openWindowForAction());
    }

    IEnumerator waitForIntro() {
        yield return new WaitForSeconds(introClip.clip.length - 0.05f); // minus 0.05 because of optimization in checkForBeat()
        StartCoroutine(checkForBeat());
    }

    IEnumerator checkForBeat() {
        yield return new WaitForSeconds(0.05f); // optimization to only do this check every 0.05s

        float moduloCurrSongTime = loopClip.time % secondsPerBeat;
        if (!isActionWindowOpenedBeforeThisBeat && (moduloCurrSongTime <= actionTimeErrorMargin || moduloCurrSongTime >= (secondsPerBeat - actionTimeErrorMargin))) {
            if (onBeat != null) {
                onBeat();
            }
        }   
        StartCoroutine(checkForBeat());
    }

    IEnumerator playDrumBeatLoop() {
        Debug.Log(Time.time);
        loopClip.PlayOneShot(loopClip.clip, 1.0f);
        Debug.Log(Time.time);
        yield return new WaitForSeconds(secondsPerBeat);
        StartCoroutine(waitForNextBeat());
        StartCoroutine(playDrumBeatLoop());
    }

    IEnumerator waitForNextBeat() {
        float nextReadyTime = secondsPerBeat - actionTimeErrorMargin;
        yield return new WaitForSeconds(nextReadyTime);
        StartCoroutine(openWindowForAction());
    }

    IEnumerator openWindowForAction() {
        //Debug.Log("now");
        isTimeForPlayerAction = true;
        isActionWindowOpenedBeforeThisBeat = true;
        yield return new WaitForSeconds(timeDurationForAction);
        isTimeForPlayerAction = false;
        isActionWindowOpenedBeforeThisBeat = false;
    }   
}
