using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarOpen : MonoBehaviour {
    public bool isRightCar;

    private Explode explosive;
    private GameObject player;
    private CarDriving driving;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform cameraPosition;
    private Vector3 lastCameraPosition;
    [SerializeField] private float lerpTime;
    private float carOpenTime;

    private void Start() {
        explosive = gameObject.GetComponent<Explode>();
        driving = gameObject.GetComponent<CarDriving>();
        driving.enabled = false;
    }

    public void OpenCar() {
        if (isRightCar) {
            player = camera.GetComponentInParent<GameObject>();
            camera.transform.parent = gameObject.transform;
            Destroy(player);

            carOpenTime = Time.time;
            lastCameraPosition = camera.transform.position;
            StartCoroutine(MoveCamera());
        }
        else {
            explosive.StartExploding(false);
        }
    }

    private IEnumerator MoveCamera() {
        while (camera.transform.position != cameraPosition.position) {
            camera.transform.position = Lerp(lastCameraPosition, cameraPosition.position, lerpTime, carOpenTime);
            yield return new WaitForEndOfFrame();
        }

        driving.enabled = true;
    }
    
    
    private Vector3 Lerp(Vector3 start, Vector3 end, float startTime, float lerpDuration = 1) {
        float timeSinceStarted = Time.realtimeSinceStartup - startTime;
        float percentageComplete = timeSinceStarted / lerpDuration;

        Vector3 result = Vector3.Lerp(start, end, percentageComplete);
        return result;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
