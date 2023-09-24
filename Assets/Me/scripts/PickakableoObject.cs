using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickakableoObject : MonoBehaviour
{
    public bool isPickable = true;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteraction")
        {
            other.GetComponentInParent<PickUpObjecy>().ObjectToPickUp=this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteraction")
        {
            other.GetComponentInParent<PickUpObjecy>().ObjectToPickUp = null;
        }
    }
}
