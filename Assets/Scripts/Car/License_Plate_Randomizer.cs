using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class License_Plate_Randomizer : MonoBehaviour
{
    private int LPFirstNumber;
    private int LPSecondNumber;
    private string LPCode;
    public string LicensePlate;
    public TMP_Text LicensePlateText;

    private void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            CreateLicensePlate();
            Debug.Log(LicensePlate);
        }
    }

    public void CreateLicensePlate()
    {
        LPFirstNumber = Random.Range(10, 100);
        LPSecondNumber = Random.Range(10, 100);
        CreateRandomLPCode();
        LicensePlate = LPFirstNumber.ToString() + LPCode + LPSecondNumber.ToString();
        LicensePlateText.text = LicensePlate;
    }

    public void CreateRandomLPCode()
    {
        int LPCodeLength = 3;
        LPCode = "";
        string[] Characters = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        for (int i = 0; i <= LPCodeLength; i++)
        {
            LPCode = LPCode + Characters[Random.Range(0, Characters.Length)];
            LPCode = LPCode.ToUpper();
        }
    }
}
