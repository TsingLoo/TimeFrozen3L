using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upStage2 : MonoBehaviour
{
    [SerializeField] FlowManager flw2;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            flw2.GameStage = 2;
        }
    }
    // Update is called once per frame
}
