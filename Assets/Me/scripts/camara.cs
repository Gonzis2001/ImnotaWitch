using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Vector3 offset;
    private Transform target;
    [Range(0,1)]public float lerpValeu;
    public float sensibilidad;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }   

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, lerpValeu) ;

        offset = Quaternion.AngleAxis(Input.GetAxisRaw  ("Mouse X") * sensibilidad, Vector3.up)*offset;




        transform.LookAt(target.position);
    }
}
