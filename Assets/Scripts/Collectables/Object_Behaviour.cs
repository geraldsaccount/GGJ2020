using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Object_Behaviour : MonoBehaviour
{
    [Header ("Color_Change")]
    public Renderer Renderer;
    public bool IsSelected;
    public Picking_Up_Items PickingUpItems;

    [Header("Hints")] 
    public int HintNo;
    public string Hint;
    public TMP_Text CanvasText;
    public GameObject Panel;
    public Drunk drunk;
    
    private void Start() {
        drunk = FindObjectOfType<Drunk>();
        Renderer = gameObject.GetComponent<Renderer>();
        PickingUpItems = GameObject.Find("Player_Proto").GetComponent<Picking_Up_Items>();
    }

    void Update()
    {
        ColorChange();
    }

    public void ColorChange()
    {
        if (PickingUpItems.StoredObject == gameObject)
        {
            Renderer.material.color = Color.grey;
        }
        else
        {
            Renderer.material.color = Color.white;
        }
    }

    public void ActivateImage() {
        drunk.hints[HintNo].text = Hint;
        CanvasText.text = Hint;
        Panel.SetActive(true);
        PickingUpItems.IsPickedUp = false;
        Time.timeScale = 0;
    }

    public void DeactivateImage()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
}
