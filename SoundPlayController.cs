using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayController : MonoBehaviour
{
    [SerializeField] PlayerMovement plm;
    public AudioClip jump0;
    public AudioClip jump1;
    public bool flag = true;

    AudioSource ads;
    // Start is called before the first frame update
    void Start()
    {
        
        ads = gameObject.GetComponent<AudioSource>();
        ads.playOnAwake = false;
        ads.volume = 1f;
        ads.spatialBlend = 1f;
        ads.maxDistance = 70f;
    }

    public void JumpSound() 
    {
        if (flag == true)
        {
            ads.clip = jump1;
            flag = false;
        }
        else 
        {
            ads.clip = jump0;
            flag = true;
        }
        ads.Play();

    }


    

}
