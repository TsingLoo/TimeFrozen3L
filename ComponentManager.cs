using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    FlowManager flowManager;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (flowManager.GameStage >= 0) 
        {
            gameObject.GetComponent<PlayerMovement>().enabled = true;
        }    
    }
}
