using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking_Up_Items : MonoBehaviour
{
    [Header ("Object_Check")]
    public Transform InteracterPosition;
    public float InteractRadius;
    public LayerMask ObjectLayer;

    [Header("Object_Storage")]
    public Collider[] StoredColliders;
    public Transform[] ColliderTransforms;
    public GameObject StoredObject;
    public Object_Behaviour ObjectBehaviour;
    public bool IsPickedUp;

    public Transform ClosestTransform;

    private void Start()
    {

    }

    void Update()
    {
        

        if (ObjectsInRange() > 0)
        {
            StoredObject = ClosestObject(Transforms()).gameObject;
        }
        else
        {
            StoredObject = null;
        }

        if (ClosestObject(Transforms()) != null && Input.GetButtonDown("Interact"))
        {
            PickUpObject();
        }

        if (Input.GetButtonDown("Close"))
        {
            ObjectBehaviour.DeactivateImage();
        }
    }

    public int ObjectsInRange()
    {
        return StoredColliders.Length;
    }

    public Transform[] Transforms()
    {
        StoredColliders = Physics.OverlapSphere(InteracterPosition.position, InteractRadius, ObjectLayer);

        ColliderTransforms = new Transform[StoredColliders.Length];

        for (int i = 0; i < StoredColliders.Length; i++)
        {
            ColliderTransforms[i] = StoredColliders[i].gameObject.transform;
        }
        return ColliderTransforms;
    }

    public Transform ClosestObject(Transform[] Transforms)
    {
        Transform closestTransform = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (Transform t in Transforms)
        {
            float dist = Vector3.Distance(t.position, currentPos);

            if (dist < minDist)
            {
                closestTransform = t;
                minDist = dist;
            }
        }

        ClosestTransform = closestTransform;

        if (ClosestTransform != null)
        {
            ObjectBehaviour = ClosestTransform.GetComponent<Object_Behaviour>(); 
        }

        return closestTransform;
    }

    public void PickUpObject()
    {
        IsPickedUp = true;
        StoredObject.SetActive(false);
        ObjectBehaviour.ActivateImage();

        Debug.Log(StoredObject.name + " was picked up");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteracterPosition.position, InteractRadius);
    }
}
