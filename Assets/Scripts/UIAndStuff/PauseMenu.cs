using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	[SerializeField] private GameObject[] lines;
	[SerializeField] private Transform startPos;
	[SerializeField] private Transform endPos;

	[SerializeField] private float[] lerpSpeeds;
	[SerializeField] private float lineAmplitude;

	private Vector3[] lastPos;
	private float timeStartedPaused;
	public bool paused;
	private bool isLerped;
	private bool isWavy;

	private void Start() {
		lastPos = new Vector3[lines.Length];
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape) && !paused) {
			Pause();
		}
		else if (Input.GetKeyDown(KeyCode.Escape) && paused) {
			Unpause();
		}
	}

	private void Pause() {
		Cursor.lockState = CursorLockMode.Confined;

		paused = true;
		for (int i = 0; i < lines.Length; i++) {
			lastPos[i] = lines[i].transform.position;
		}

		Time.timeScale = 0;
		timeStartedPaused = Time.realtimeSinceStartup;
		StartCoroutine(LerpLines());
		StartCoroutine(WavyLines());
	}
	
	public void Unpause() {
		Cursor.lockState = CursorLockMode.Locked;
		paused = false;
		isWavy = false;
		isLerped = false;
		
		for (int i = 0; i < lines.Length; i++) {
			lastPos[i] = lines[i].transform.position;
		}

		Time.timeScale = 1;
		timeStartedPaused = Time.realtimeSinceStartup;
		StartCoroutine(LerpLinesOut());
	}

	public void MainMenu() {
		Time.timeScale = 1;
		GameManager.instance.LoadMainMenu();
	}
	
	public void QuitGame() {
		Application.Quit();
	}
	
	private IEnumerator LerpLines() {
		while (lines[0].transform.position != endPos.position && paused) {
			for (int i = 0; i < lines.Length; i++) {
				lines[i].transform.position =
					Lerp(lastPos[i], endPos.transform.position, timeStartedPaused, lerpSpeeds[i]);
			}
			
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
		}

		isLerped = true;
	}

	private IEnumerator WavyLines() {
		if (isWavy) {
			yield break;
		}
		while (paused) {
			isWavy = true;
			if (isLerped) {
				for (int i = 0; i < lines.Length; i++) {
					lines[i].transform.position = new Vector3(lines[i].transform.position.x, lines[i].transform.position.y + (Mathf.Sin(Time.realtimeSinceStartup + (i * 0.0625f)) * 0.25f), lines[i].transform.position.z);

				}
			}
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
		}
	}

	private IEnumerator LerpLinesOut() {
		print("i'm in");
		while (lines[lines.Length - 1].transform.position != startPos.position && !paused) {
			for (int i = 0; i < lines.Length; i++) {
				lines[i].transform.position =
					Lerp(lastPos[i], startPos.transform.position, timeStartedPaused, lerpSpeeds[lines.Length - 1 - i]);

			}
			
			yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
		}

		isLerped = false;
	}

	private Vector3 Lerp(Vector3 start, Vector3 end, float startTime, float lerpDuration = 1) {
		float timeSinceStarted = Time.realtimeSinceStartup - startTime;
		float percentageComplete = timeSinceStarted / lerpDuration;

		Vector3 result = Vector3.Lerp(start, end, percentageComplete);
		return result;
	}
}
