using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTutorial : MonoBehaviour {

    public float beatsPerMinute;
    public float timeBeforeBeatStarts;
    public float timeDurationForAction;
    public int playerComboCount;

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

    void OnEnable() {
        onPlayerBeat += openWindowForPlayerActionHandler;
        onEnemyBeat += openWindowForEnemyActionHandler;
    }

    // Use this for initialization
    void Start() {
        isTimeForPlayerAction = false;
        actionTimeErrorMargin = timeDurationForAction / 2.0f;
        secondsPerBeat = 60 / beatsPerMinute;
        playerComboCount = 0;
        clips = GetComponents<AudioSource>();
        introClip = clips[0];
        loopClip = clips[1];
        isActionWindowOpenedBeforeThisBeat = false;
        isSpecialOccurring = false;
        isTimeForEnemyAction = true;

        startRhythm();
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

    public void startRhythm() {
        introClip.Play();
        loopClip.PlayDelayed(introClip.clip.length);

        StartCoroutine(checkForBeatIntro());
        StartCoroutine(waitForIntro());
    }

    void openWindowForPlayerActionHandler() {
        StartCoroutine(openWindowForPlayerAction());
    }

    void openWindowForEnemyActionHandler() {
        StartCoroutine(openWindowForEnemyAction());
    }

    // Wait for 8 beats before enemy starts moving
    IEnumerator waitForIntro() {
        yield return new WaitForSeconds(introClip.clip.length - 0.05f); // minus 0.05 because of optimization in checkForBeat()
        StartCoroutine(checkForBeat());
    }

    IEnumerator checkForBeatIntro() {
        yield return new WaitForSeconds(0.05f); // optimization to only do this check every 0.05s

        float moduloCurrSongTime = introClip.time % secondsPerBeat;
        if (moduloCurrSongTime <= actionTimeErrorMargin || moduloCurrSongTime >= (secondsPerBeat - actionTimeErrorMargin)) {
            if (!isSpecialOccurring && !isActionWindowOpenedBeforeThisBeat && onPlayerBeat != null) {
                onPlayerBeat();
            }
        }
        if (introClip.isPlaying) {
            StartCoroutine(checkForBeatIntro());
        }
    }

    IEnumerator checkForBeat() {
        yield return new WaitForSeconds(0.05f); // optimization to only do this check every 0.05s

        float moduloCurrSongTime = loopClip.time % secondsPerBeat;
        if (moduloCurrSongTime <= actionTimeErrorMargin || moduloCurrSongTime >= (secondsPerBeat - actionTimeErrorMargin)) {
            if (!isSpecialOccurring && !isActionWindowOpenedBeforeThisBeat && onPlayerBeat != null) {
                onPlayerBeat();
            }
        }

        if (isTimeForEnemyAction && onEnemyBeat != null) {
            onEnemyBeat();
        }

        StartCoroutine(checkForBeat());
    }

    IEnumerator openWindowForPlayerAction() {
        //Debug.Log("now");
        isTimeForPlayerAction = true;
        isActionWindowOpenedBeforeThisBeat = true;
        EventManager.TriggerEvent("ActivateDrum");
        yield return new WaitForSeconds(timeDurationForAction);
        isTimeForPlayerAction = false;
        isActionWindowOpenedBeforeThisBeat = false;

    }

    IEnumerator openWindowForEnemyAction() {
        isTimeForEnemyAction = false;
        yield return new WaitForSeconds(timeDurationForAction);
        isTimeForEnemyAction = true;
    }

}
