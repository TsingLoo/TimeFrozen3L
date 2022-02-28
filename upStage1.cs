using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upStage1 : MonoBehaviour
{
    [SerializeField]FlowManager flw;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.CompareTag("Player"));

        if (other.CompareTag("Player")) 
        {
          
            flw.GameStage = 2;
        }
    }
    // Update is called once per frame
}
