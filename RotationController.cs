using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    
  
    void FixedUpdate()
    {
        transform.RotateAround((transform.position+Vector3.up*5), Vector3.up, 5);
    }
}
