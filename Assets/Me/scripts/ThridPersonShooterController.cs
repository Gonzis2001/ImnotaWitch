using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class ThridPersonShooterController : MonoBehaviour
{
    /*[SerializeField]private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitibity;
    [SerializeField] private float AimSensitibity;
    [SerializeField] private LayerMask aimcolliderMask= new LayerMask();
    [SerializeField] private Transform debugTransform;

    [SerializeField] private Transform pfBullet;
    [SerializeField] private Transform spawnBullet;


   
    public NewBehaviourScript thirdPersonController;

    private void Start()
    {
        thirdPersonController = GetComponent<NewBehaviourScript>();
        
    }
    private void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector3 mouseWorldPosition =  Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimcolliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        }
        if (starterAssetsInputs.aim) 
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensibilty(AimSensitibity);
            thirdPersonController.SetRotateOnMove(false);
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y=transform.position.y;
            Vector3 aimDirection=(worldAimTarget-transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward,aimDirection,Time.deltaTime*20f);
            

        }
        else
        {
           
            thirdPersonController.SetSensibilty(normalSensitibity);
            thirdPersonController.SetRotateOnMove(true);
            aimVirtualCamera.gameObject.SetActive(false);

        }
        if(starterAssetsInputs.shoot)
        {
            Vector3 aimDirectionBullet = (mouseWorldPosition - spawnBullet.position).normalized;
            Instantiate(pfBullet, spawnBullet.position,Quaternion.LookRotation(aimDirectionBullet,Vector3.up));
            starterAssetsInputs.shoot = false;
        }
      
       
       
        
    }*/
}
