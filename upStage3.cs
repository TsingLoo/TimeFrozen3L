using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upStage3 : MonoBehaviour
{
    [SerializeField] FlowManager flw3;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("qwe");
            flw3.GameStage = 3;
        }
    }
    // Update is called once per frame
}
