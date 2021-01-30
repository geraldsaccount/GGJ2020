using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRandomizer : MonoBehaviour {
    [SerializeField] private License_Plate_Randomizer[] cars;
    [SerializeField] private List<Transform> hintPositions;
    [SerializeField] private Object_Behaviour hints;
    
    // Start is called before the first frame update
    void Start() {
        foreach (License_Plate_Randomizer car in cars) {
            car.CreateLicensePlate();
        }
        
        int rand = Random.Range(0, cars.Length);
        cars[rand].GetComponent<CarOpen>().isRightCar = true;

        int hintNo = 0;
        foreach (char character in cars[rand].GetComponent<License_Plate_Randomizer>().LicensePlate) {
            Transform randTransform = hintPositions[Random.Range(0, hintPositions.Count)];
            hintPositions.Remove(randTransform);
            Object_Behaviour hint = Instantiate(hints, randTransform.position, Quaternion.identity);
            hint.Hint = character.ToString();
            hint.HintNo = hintNo;
            hintNo++;
        }
    }

}
