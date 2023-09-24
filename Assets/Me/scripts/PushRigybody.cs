using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushRigybody : MonoBehaviour
{

    public float pushPower = 2f;

    private float masa;
  
    

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body= hit.collider.attachedRigidbody;
        

        if (body == null|| body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        masa = body.mass;

        Vector3 pushDirection=new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);
        body.velocity = pushDirection* pushPower/masa;
    }
}
