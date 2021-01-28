using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Object_Behaviour : MonoBehaviour
{
    [Header ("Color_Change")]
    public Renderer Renderer;
    public bool IsSelected;
    public Picking_Up_Items PickingUpItems;

    [Header("Hints")]
    public Sprite Hint;
    public Image CanvasImage;
    public GameObject Panel;

    private void Start()
    {
        Renderer = gameObject.GetComponent<Renderer>();
        PickingUpItems = GameObject.Find("Player_Proto").GetComponent<Picking_Up_Items>();
    }

    void Update()
    {
        ColorChange();

        if (PickingUpItems.IsPickedUp)
        {
            ActivateImage();
        }

        if (Input.GetButtonDown("Close"))
        {
            DeactivateImage();
        }
    }

    public void ColorChange()
    {
        if (PickingUpItems.StoredObject == gameObject)
        {
            Renderer.material.color = Color.green;
        }
        else
        {
            Renderer.material.color = Color.white;
        }
    }

    public void ActivateImage()
    {
        CanvasImage.sprite = Hint;
        Panel.SetActive(true);
        Time.timeScale = 0;
        PickingUpItems.IsPickedUp = false;
    }

    public void DeactivateImage()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
    }
}
