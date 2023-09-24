using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;


public class Disaro : MonoBehaviour
{


    public GameObject flecha;
    public float shotforce = 1500;
    public float shotRate = 0.5f;
    public float shotRateTime = 0;
    public float disparoTrigger;
    [SerializeField] private float AimTrigger;
    public float desparicon = 8;
     public float state = 0;

    public GameObject granade;

    [SerializeField] private Rigidbody granadarb;
    public LineRenderer lineRenderer;
   [SerializeField] private Transform ReleasedPosition;
    [SerializeField] int LinePoints = 25;
    [SerializeField]
    [Range(0.01f, 0.25f)] 
    private float TimeBetweenPoints = 0.01f;

    [SerializeField]
    [Range(1, 100)]
    private float ThrowStrength = 10f;

    [SerializeField] VisualEffect magicShpere;
    private float intensity = 17f;

   
    // Start is called before the first frame update

   
    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        disparoTrigger = Input.GetAxis("GatilloD");
        if (disparoTrigger > 0)
        {
            if (state == 0)
            {

                Lanzamientoflecha();
            }
            if (state == 1)
            {

                LanzamientoGranada();
            }
        }

        if (state == 1)
        {
            DrawPorjection();
            magicShpere.SetVector4("color", new Vector4(0.74f*intensity, 0.34f * intensity, 0.14f * intensity, 0.9f * intensity));
        }
        if (state == 0)
        {
            magicShpere.SetVector4("color", new Vector4(0.13f * intensity, 0.44f * intensity, 0.74f * intensity, 0.9f * intensity));

        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {

                if (state == 0)
                {
                
              
                
                    state = 1;
                }
                else if (state == 1)
                {
                    lineRenderer.enabled = false;

                    state = 0;
                }

            }
        AimTrigger = Input.GetAxis("Gatillo");
        if (AimTrigger < 0.09f)
        {


            lineRenderer.enabled = false;
        }
        

    }
    private void Lanzamientoflecha()
    {
        if (Time.time > shotRateTime)
        {

            GameObject newFlecha;
            newFlecha = Instantiate(flecha, transform.position, transform.rotation);

            newFlecha.GetComponent<Rigidbody>().AddForce(transform.forward * shotforce);
            shotRateTime = Time.time + shotRate;
            Destroy(newFlecha, desparicon);
            
        }
    }

    private void DrawPorjection()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(LinePoints / TimeBetweenPoints) + 1;
        Vector3 startPosition = ReleasedPosition.position;
        Vector3 startVelocity=ThrowStrength*transform.forward/granadarb.mass;
        int i = 0;
        lineRenderer.SetPosition(i, startPosition);
        for(float time1 = 0; time1 < LinePoints; time1 += TimeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time1 * startVelocity;
            point.y = startPosition.y + startVelocity.y * time1 + (Physics.gravity.y / 2f * time1 * time1);
            lineRenderer.SetPosition(i, point);
        }

    }
    private void LanzamientoGranada()
    {
        if (Time.time > shotRateTime)
        {

            GameObject newGranade;
            newGranade = Instantiate(granade, transform.position, transform.rotation);
            newGranade.GetComponent<Rigidbody>().isKinematic = false;

            newGranade.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowStrength,ForceMode.Impulse);
            shotRateTime = Time.time + shotRate;
           

        }
    }
    public void SetEstado(float newEstado)
    {
        state = newEstado;
    }

}
