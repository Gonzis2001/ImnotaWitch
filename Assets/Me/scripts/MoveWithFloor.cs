using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{

    CharacterController player;
    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;
    Quaternion lastGroundRotation;
    Quaternion acutualRotation;
    // Start is called before the first frame update
    void Start()
    {
       player=this.GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        if(player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position,player.height/4.1f,-transform.up,out hit))
            {
                GameObject groundedIn=hit.collider.gameObject;
                groundName=groundedIn.name;
                groundPosition = groundedIn.transform.position;
                acutualRotation = groundedIn.transform.rotation;
                if(groundPosition!=lastGroundPosition&& groundName==lastGroundName)
                {
                    this.transform.position += groundPosition - lastGroundPosition;
                }
                if (acutualRotation!=lastGroundRotation && groundName == lastGroundName)
                {
                    var newRot=this.transform.rotation*(acutualRotation.eulerAngles-lastGroundRotation.eulerAngles);
                    this.transform.RotateAround(groundedIn.transform.position, Vector3.up, newRot.y);
                
                }
                lastGroundName = groundName;
                lastGroundPosition = groundPosition;
                lastGroundRotation = acutualRotation;
                
            }
        }
        else if(!player.isGrounded) 
        {
            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
            lastGroundRotation=Quaternion.Euler(0,0,0); 



        }
    }

    private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position-transform.up, player.height / 4.1f);
    }
}
