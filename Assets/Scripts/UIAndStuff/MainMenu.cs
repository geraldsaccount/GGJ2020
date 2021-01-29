
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject quitButton;

    [SerializeField] private float rotationSpeed;
    
    [SerializeField] private Transform startPosStart;
    [SerializeField] private Transform startPosCredits;
    [SerializeField] private Transform startPosQuit;
    
    [SerializeField] private Transform endPosStart;
    [SerializeField] private Transform endPosCredits;
    [SerializeField] private Transform endPosQuit;

    [SerializeField] private Transform outPosStart;
    [SerializeField] private Transform outPosCredits;
    [SerializeField] private Transform outPosQuit;
    
    [SerializeField] private float lerpSpeedOutStart;
    [SerializeField] private float lerpSpeedOutCredits;
    [SerializeField] private float lerpSpeedOutQuit;
    
    [SerializeField] private float lerpSpeed;
    
    private Vector3 lastStartPos;
    private Vector3 lastCreditsPos;
    private Vector3 lastQuitPos;
    private float timeStartedLerpingStart;
    private float timeStartedLerpingCredits;
    private float timeStartedLerpingQuit;
    private bool startSelected;
    private bool creditsSelected;
    private bool quitSelected;
    private bool isLoading;
    private Vector3 backgroundSize;
    private Vector3 lastBackgroundPos;
    private void Start() {
        lastStartPos = startButton.transform.position;
        lastCreditsPos = creditsButton.transform.position;
        lastQuitPos = quitButton.transform.position;
    }

    void Update()
    {
        background.transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
        if (!isLoading) {
            HandleButtonMovement(startSelected, startButton, startPosStart, endPosStart, lastStartPos, timeStartedLerpingStart);
            HandleButtonMovement(creditsSelected, creditsButton, startPosCredits, endPosCredits, lastCreditsPos, timeStartedLerpingCredits);
            HandleButtonMovement(quitSelected, quitButton, startPosQuit, endPosQuit, lastQuitPos,
                timeStartedLerpingQuit);
        }
        
    }

    private void HandleButtonMovement(bool isSelected, GameObject button, Transform startPos, Transform endPos,
        Vector3 lastPos, float timeStarted) {
        if (isSelected) {
            button.transform.position = Lerp(lastPos, endPos.transform.position, timeStarted, lerpSpeed);
        }
        else {
            button.transform.position = Lerp(lastPos, startPos.transform.position, timeStarted, lerpSpeed);
        }
    }

    public void StartSelected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingStart = Time.time;
        if (!startSelected) {
            lastStartPos = startButton.transform.position;
        }
        startSelected = true;
        
    }

    public void StartDeselected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingStart = Time.time;
        if (startSelected) {
            lastStartPos = startButton.transform.position;
        }
        startSelected = false;
    }    
    
    public void CreditsSelected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingCredits = Time.time;
        if (!creditsSelected) {
            lastCreditsPos = creditsButton.transform.position;
        }
        creditsSelected = true;
        
    }

    public void CreditsDeselected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingCredits = Time.time;
        if (creditsSelected) {
            lastCreditsPos = creditsButton.transform.position;
        }
        creditsSelected = false;
    }
    
    public void QuitSelected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingQuit = Time.time;
        if (!quitSelected) {
            lastQuitPos = quitButton.transform.position;
        }
        quitSelected = true;
        
    }

    public void QuitDeselected() {
        if (isLoading) {
            return;
        }
        timeStartedLerpingQuit = Time.time;
        if (quitSelected) {
            lastQuitPos = quitButton.transform.position;
        }
        quitSelected = false;
    }

    public void StartGame() {
        isLoading = true;
        lastStartPos = startButton.transform.position;
        lastCreditsPos = creditsButton.transform.position;
        lastQuitPos = quitButton.transform.position;
        timeStartedLerpingStart = Time.time;
        timeStartedLerpingCredits = Time.time;
        timeStartedLerpingQuit = Time.time;
        
        
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading() {
        
        yield return StartCoroutine(MoveStuff());
        GameManager.instance.LoadGame();
        //StartCoroutine(LoadAsynchronously());
    }
    
    private IEnumerator MoveStuff() {
        while (startButton.transform.position != outPosStart.transform.position) {
            startButton.transform.position =
                Lerp(lastStartPos, outPosStart.transform.position, timeStartedLerpingStart, lerpSpeedOutStart);
            creditsButton.transform.position = Lerp(lastCreditsPos, outPosCredits.transform.position,
                timeStartedLerpingCredits, lerpSpeedOutCredits);
            quitButton.transform.position = Lerp(lastQuitPos, outPosQuit.transform.position, timeStartedLerpingQuit,
                lerpSpeedOutQuit);
            yield return null;
        }
    }
    
    // private IEnumerator LoadAsynchronously() {
    //     SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    //     
    // }
    
    public void Credits() {
        
    }

    public void Quit() {
        Application.Quit();
    }
    
    private Vector3 Lerp(Vector3 start, Vector3 end, float startTime, float lerpDuration = 1) {
        float timeSinceStarted = Time.time - startTime;
        float percentageComplete = timeSinceStarted / lerpDuration;

        Vector3 result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }
}
