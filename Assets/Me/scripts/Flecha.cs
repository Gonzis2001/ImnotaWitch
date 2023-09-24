using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    Rigidbody rb;
    Vector3 direccion;
    public float vel;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //time-= Time.deltaTime;
     
        //transform.position +=transform.forward*vel*Time.deltaTime ;
       // if (time <= 0)
        //{
            //Destroy(gameObject);
       // }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}   
