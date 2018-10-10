using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm : MonoBehaviour {

    public float beatsPerMinute;
    public float timeBeforeBeatStarts;
    public float timeDurationForAction;
    public int beatsDenied;

    private AudioSource[] clips;
    private AudioSource introClip;
    private AudioSource loopClip;
    private bool isTimeForPlayerAction;
    private float actionTimeErrorMargin; // half the error margin time
    private float secondsPerBeat;
    private bool isActionWindowOpenedBeforeThisBeat;
    public bool isSpecialOccurring;
    private bool isTimeForEnemyAction;

    public delegate void OnBeat();

    public event OnBeat onPlayerBeat;
    public event OnBeat onEnemyBeat;

    void OnEnable () {
        onPlayerBeat += openWindowForPlayerActionHandler;
        onEnemyBeat += openWindowForEnemyActionHandler;
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
        isSpecialOccurring = false;
        isTimeForEnemyAction = true;
        beatsDenied = 0;

        //StartCoroutine(waitForNextBeat());
        //StartCoroutine(playDrumBeatLoop());
        introClip.Play();
        loopClip.PlayDelayed(introClip.clip.length);

        StartCoroutine(checkForBeat());
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

    public bool IsSpecialOccurring {
        get {
            return isSpecialOccurring;
        }
        set {
            isSpecialOccurring = value;
        }
    }

    void openWindowForPlayerActionHandler() {
        StartCoroutine(openWindowForPlayerAction());
    }

    void openWindowForEnemyActionHandler() {
        StartCoroutine(openWindowForEnemyAction());
    }

    // Not Used. Initially want to wait for 8 beats before able to start moving
    IEnumerator waitForIntro() {
        yield return new WaitForSeconds(introClip.clip.length - 0.05f); // minus 0.05 because of optimization in checkForBeat()
        StartCoroutine(checkForBeat());
    }

    IEnumerator checkForBeat() {
        yield return new WaitForSeconds(0.05f); // optimization to only do this check every 0.05s

        float moduloCurrSongTime = loopClip.time % secondsPerBeat;
        if (moduloCurrSongTime <= actionTimeErrorMargin || moduloCurrSongTime >= (secondsPerBeat - actionTimeErrorMargin)) {
            if (beatsDenied == 0 && !isSpecialOccurring && !isActionWindowOpenedBeforeThisBeat && onPlayerBeat != null) {
                onPlayerBeat();
            }
            else {
                if (beatsDenied > 0) {
                    beatsDenied -= 1;
                }
            }
        }

        if (isTimeForEnemyAction && onEnemyBeat != null)
        {
            onEnemyBeat();
        }

        StartCoroutine(checkForBeat());
    }

    //IEnumerator playDrumBeatLoop() {
    //    Debug.Log(Time.time);
    //    loopClip.PlayOneShot(loopClip.clip, 1.0f);
    //    Debug.Log(Time.time);
    //    yield return new WaitForSeconds(secondsPerBeat);
    //    StartCoroutine(waitForNextBeat());
    //    StartCoroutine(playDrumBeatLoop());
    //}

    //IEnumerator waitForNextBeat() {
    //    float nextReadyTime = secondsPerBeat - actionTimeErrorMargin;
    //    yield return new WaitForSeconds(nextReadyTime);
    //    StartCoroutine(openWindowForAction());
    //}

    IEnumerator openWindowForPlayerAction() {
        //Debug.Log("now");
        isTimeForPlayerAction = true;
        isActionWindowOpenedBeforeThisBeat = true;
        yield return new WaitForSeconds(timeDurationForAction);
        isTimeForPlayerAction = false;
        isActionWindowOpenedBeforeThisBeat = false;

    }

    IEnumerator openWindowForEnemyAction() {
        isTimeForEnemyAction = false;
        yield return new WaitForSeconds(timeDurationForAction);
        isTimeForEnemyAction = true;
    }

    public void denyPlayerBeatWindow(int window) {
        if (beatsDenied <= beatsPerMinute / 5) {
            beatsDenied += (window * (int)beatsPerMinute / 15);
        }
    }
}
