using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intoLava : MonoBehaviour
{
    [SerializeField] FlowManager flw;

    private void OnCollisionEnter(Collision collision)
    {
        flw.respawnTime = flw.respawnTime + 1;
    }
}
