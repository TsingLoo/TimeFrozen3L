using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    PlayerMovement pm;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask Grappleable;
    public Transform gunTip;
    public Transform camera;
    public Transform player;
    private float maxDistance;
    private SpringJoint joint;

    [SerializeField] float jointSpring = 5f;
    [SerializeField] float jointDamper = 7f;
    [SerializeField] float jointMassale = 4.5f;
    [SerializeField] KeyCode GrapKey = KeyCode.Q;
 

    

    // Start is called before the first frame update
    void Awake()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        lr = GetComponent<LineRenderer>();
        maxDistance = 30f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(GrapKey))
        {
            StartGrapple();
        }
        else if (Input.GetKeyUp(GrapKey))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope() 
    {
        if (!joint) return;

        lr.SetPosition(0,gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StartGrapple() 
    {
       

        RaycastHit hit;
      //  Debug.Log("This is camera.position: " + camera.position);
      //  Debug.Log("This is camera.ratation: " + camera.forward);

        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, Grappleable)) ;
        grapplePoint = hit.point;

        if (hit.point != new Vector3 (0,0,0)) 
        {

            StopCoroutine(TimeScaleControl.instance.ActionE(0.4f));
            StartCoroutine(TimeScaleControl.instance.ActionE(0.4f));
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = jointSpring;
            joint.damper = jointDamper;
            joint.massScale = jointMassale;
            lr.positionCount = 2;
        }



    }

    void StopGrapple() 
    {
        lr.positionCount = 0;
        Destroy(joint);

    }


}
