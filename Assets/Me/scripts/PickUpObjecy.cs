using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjecy : MonoBehaviour
{
    public GameObject ObjectToPickUp;
    public GameObject PickedObject;
    public Transform interactionZone;

    void Update()
    {
        if (ObjectToPickUp != null && ObjectToPickUp.GetComponent<PickakableoObject>().isPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
            {
                PickedObject = ObjectToPickUp;
                PickedObject.GetComponent<PickakableoObject>().isPickable = false;
                PickedObject.transform.SetParent(interactionZone);
                PickedObject.transform.position = interactionZone.position;
                PickedObject.GetComponent<Rigidbody>().useGravity = false;
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;

            }
        }
        else if (PickedObject != null)
        {
            if (Input.GetKeyDown(KeyCode.F) || (Input.GetKeyDown(KeyCode.Joystick1Button1)))
            {
                PickedObject.GetComponent<PickakableoObject>().isPickable = true;
                PickedObject.transform.SetParent(null);
                PickedObject.GetComponent<Rigidbody>().useGravity = true;
                PickedObject.GetComponent<Rigidbody>().isKinematic = false;
                PickedObject = null;
            }
        }
    }
    
}
