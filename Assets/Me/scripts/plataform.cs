using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataform : MonoBehaviour
{
    public Rigidbody rbPlatform;
    public Transform[] positions;
    public float speed;
    private int actualPosition = 0;
    private int nextPosotion = 1;

    public bool moveToTheNext;
    public float waitTime;
  
    void Update()
    {
        MovePlatform();
    }
    void MovePlatform()
    {
        if (moveToTheNext)
        {
            StopCoroutine(WaitForMove(0));
            rbPlatform.MovePosition(Vector3.MoveTowards(rbPlatform.position, positions[nextPosotion].position, speed * Time.deltaTime));
        }
        if (Vector3.Distance(rbPlatform.position, positions[nextPosotion].position) <=0) 
        {
            StartCoroutine(WaitForMove(waitTime));
            actualPosition = nextPosotion;
            nextPosotion++;
            if (nextPosotion > positions.Length-1)
            {
                nextPosotion = 0;
            
            
            
            }
        
        
        }
    }

    IEnumerator WaitForMove(float time)
    {
        moveToTheNext = false;
        yield return new WaitForSeconds(time);
        moveToTheNext=true; 
    }
}
