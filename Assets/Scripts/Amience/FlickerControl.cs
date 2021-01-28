using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlickerControl : MonoBehaviour {
    [SerializeField] private bool isFlickering = false;
    private Light lightSource;
    [SerializeField] private int maxFlickeringAmount;
    [SerializeField] private float minChillTime;
    [SerializeField] private float maxChillTime;
    private void Start() {
        lightSource = gameObject.GetComponent<Light>();
    }

    private void Update() {
        if (isFlickering == false) {
            StartCoroutine(FlickeringLight());
        }
    }

    private IEnumerator FlickeringLight() {
        isFlickering = true;
        int flickeringAmount = Random.Range(1, maxFlickeringAmount);
        for (int i = 0; i < flickeringAmount; i++) {
            lightSource.enabled = !lightSource.enabled;
            
            float delay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(delay);

            lightSource.enabled = !lightSource.enabled;

            delay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(delay);
        }

        float chillTime = Random.Range(minChillTime, this.maxChillTime);
        yield return new WaitForSeconds(chillTime);
        isFlickering = false;
    }
}
