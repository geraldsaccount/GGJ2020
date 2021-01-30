using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeathPanel : MonoBehaviour {
    [SerializeField] private GameObject[] Images;
    [SerializeField] private Transform[] endPos;
    private Vector3[] lastPos;
    [SerializeField] private float[] lerpSpeed;
    [SerializeField] private Vector2 minMaxAmplitude;
    private float lerpStartTime;
    private bool alive = true;
    private bool inPosition;

    public bool hasExploded = true;

	public bool hasDrivenDrunk;

	private void Start() {
		lastPos = new Vector3[Images.Length];
		StartCoroutine(StartExplosion());
	}

	IEnumerator StartExplosion() {
		yield return new WaitForSeconds(3);
		yield return StartCoroutine(LerpLines());
		StartCoroutine(WavyImages());
	}
	
	public void DieExplosion() {
	    if (hasExploded && alive) {
		    StartCoroutine(LerpLines());
		    alive = false;
	    }
    }

    public void DieCarCrash() {
	    
    }
    
    private IEnumerator LerpLines() {
	    print("I'm in");
	    lerpStartTime = Time.realtimeSinceStartup;
	    for (int i = 0; i < Images.Length; i++) {
		    lastPos[i] = Images[i].transform.position;
	    }
	    
	    while (Images[0].transform.position != endPos[0].position) {
		    for (int i = 0; i < Images.Length; i++) {
				print(Images[i].transform.position);
			    Images[i].transform.position =
				    Lerp(lastPos[i], endPos[i].transform.position, lerpStartTime, lerpSpeed[i]);
		    }
			
		    yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
	    }

	    inPosition = true;
    }

    private IEnumerator WavyImages() {
	    while (true) {
		    if (inPosition) {
			    for (int i = 0; i < Images.Length; i++) {
				    float randY = Random.Range(minMaxAmplitude.x, minMaxAmplitude.y);
				    float randX = Random.Range(minMaxAmplitude.x, minMaxAmplitude.y);
				    Images[i].transform.position += new Vector3(
					    Mathf.Sin(Time.realtimeSinceStartup) * randX,
					    Mathf.Sin(Time.realtimeSinceStartup) * randY,
					    0);
			    }
		    }
		    yield return new WaitForSeconds(Time.unscaledDeltaTime);
	    }
	    
    }
    
    private Vector3 Lerp(Vector3 start, Vector3 end, float startTime, float lerpDuration = 1) {
	    float timeSinceStarted = Time.realtimeSinceStartup - startTime;
	    float percentageComplete = timeSinceStarted / lerpDuration;

	    Vector3 result = Vector3.Lerp(start, end, percentageComplete);
	    return result;
    }
}
