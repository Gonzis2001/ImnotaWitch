using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicsphere : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);

    }
}
