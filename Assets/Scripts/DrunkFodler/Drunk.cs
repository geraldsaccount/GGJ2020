using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class Drunk : MonoBehaviour {
	private float timeStartedLerping;
	private float drunkMultiplicator;
	private float lastDrunkMultiplicator;
	[SerializeField] private float lerpTime;
	private float timeLerped;
	[SerializeField] private float baseDrunkMultiplicator;
	
	private bool shouldLerp;
	[SerializeField] private PostProcessVolume postProcessVolume;
	private LensDistortion distortion;
	public Material material;
	private bool canFocus = true;
	private bool isFocusing;

	[SerializeField] private float focusCooldown;
	[SerializeField] private float focusTime;
	[SerializeField] private float focusSpeed;
	private float focusCounter;
	private float cooldownCounter;
	private float focusMultiplicator;
	private float distortionValue;
	
	[SerializeField] private float minMoveAmplitude;
	[SerializeField] private float maxMoveAmplitude;
	
	private void Start() {
		postProcessVolume.profile.TryGetSettings(out distortion);
		
		lastDrunkMultiplicator = baseDrunkMultiplicator;
		StartSobering();
	}
	
	private void Update() {
		if (shouldLerp && !isFocusing) {
			drunkMultiplicator = Lerp(lastDrunkMultiplicator, 0, timeStartedLerping, lerpTime);
			timeLerped += Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && canFocus) {
			PauseSobering();
		}

		if (Input.GetKeyUp(KeyCode.Mouse0) && isFocusing) {
			EndFocus();
		}

		if (Input.GetKey(KeyCode.Mouse0) && canFocus) {
			isFocusing = true;
			drunkMultiplicator = Lerp(lastDrunkMultiplicator, 0, timeStartedLerping, focusSpeed);
			distortion.intensity.value = Lerp(0, 50, timeStartedLerping, focusSpeed);

			focusCounter += Time.deltaTime;
		}

		if (focusCounter >= focusTime && canFocus) {
			EndFocus();
		}
		
	}

	private void EndFocus() {
		canFocus = false;
		timeStartedLerping = Time.time;
		focusMultiplicator = drunkMultiplicator;
		distortionValue = distortion.intensity.value;
		StartCoroutine(ResetMultiplicator());
	}

	private IEnumerator ResetMultiplicator() {
		while (drunkMultiplicator < lastDrunkMultiplicator) {
			drunkMultiplicator = Lerp(focusMultiplicator, lastDrunkMultiplicator, timeStartedLerping, focusSpeed);
			distortion.intensity.value = Lerp(distortionValue, 0, timeStartedLerping, focusSpeed);
			yield return new WaitForEndOfFrame();
		}

		timeStartedLerping = Time.time;
		shouldLerp = true;
		isFocusing = false;
		lerpTime -= timeLerped;
		timeLerped = 0;
		focusCounter = 0;
		yield return new WaitForSeconds(focusCooldown);

		canFocus = true;
	}
	
	private void StartSobering() {
		timeStartedLerping = Time.time;
		shouldLerp = true;
	}

	private void PauseSobering() {
		timeStartedLerping = Time.time;
		lastDrunkMultiplicator = drunkMultiplicator;
		shouldLerp = false;
	}

	public Vector2 GetDrunkMove() {
		Vector2 drunkMove = new Vector2();
		float randX = Random.Range(minMoveAmplitude, maxMoveAmplitude);
		float randY = Random.Range(minMoveAmplitude, maxMoveAmplitude);

		drunkMove.x = Mathf.Sin(Time.time) * randX * drunkMultiplicator;
		drunkMove.y = Mathf.Sin(Time.time) * randY * drunkMultiplicator;
		
		return drunkMove;
	}
	
	
	private float Lerp(float start, float end, float startTime, float lerpDuration = 1) {
		float timeSinceStarted = Time.time - startTime;
		float percentageComplete = timeSinceStarted / lerpDuration;

		float result = Mathf.Lerp(start, end, percentageComplete);
		return result;
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		material.SetFloat("_DrunkMultiplicator", drunkMultiplicator);
		Graphics.Blit (source, destination, material);
	}
}