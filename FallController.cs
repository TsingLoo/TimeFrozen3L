using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallController : MonoBehaviour
{
    [SerializeField] FlowManager flowManager;
  
    bool haveTouched = false;
    public AudioClip adc;
    AudioSource ads;
    [SerializeField]
    int mass = 2;
    [SerializeField]
    bool useGravity = false;
    [SerializeField]
    float waitTime = 2;
    [SerializeField]
    bool MoveNow = false;

    Vector3 start_Pos;
    Quaternion start_Qua;

  


    float checkTime = 0.05f;
    // 有质量的话，就有反作用力，立即有效果，过两秒后再下坠\
    private void Awake()
    {
        ads = gameObject.GetComponent<AudioSource>();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().mass = mass;
    }

    private void Start()
    {
        
        ads.playOnAwake = false;
        start_Pos = transform.position;
        start_Qua = transform.rotation;

     
    }

    private void OnCollisionEnter(Collision collision)
    {
     

        if (!haveTouched)
        {
            ads.clip = adc;
            ads.volume = 0.3f;
            ads.spatialBlend = 1f;
            ads.maxDistance = 70f;
            ads.Play();
            haveTouched = true;        
        }
        StartCoroutine(waitSconds(waitTime));
    }

 

    public void BackToWork()
    {
        
        if (flowManager.needRespawn)
        {
            StopAllCoroutines();
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            //print("I am back");
            gameObject.transform.position = start_Pos;
            gameObject.transform.rotation = start_Qua;
           
        }
      
    }


    IEnumerator waitSconds(float waitTime) 
    {



        if (!MoveNow)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else 
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(waitTime);
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
       
    }
}
