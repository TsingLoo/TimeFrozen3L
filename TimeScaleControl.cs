using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeScaleControl : MonoBehaviour
{

    public static TimeScaleControl instance;

    public Color AFade;
    public float FadeTime = 1f;
    float currentNum = 0f;
    public bool action;
    

    public FlowManager flowManager; 



    // Start is called before the first frame update
    void Start()
    {
        FlowManager flowManager;
        AFade = Color.red;
        instance = this;
       // gameObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        if (flowManager.GameStage >= 0)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            bool z = Input.GetKeyDown(KeyCode.Space);

            float time = (x != 0 || y != 0 || z is true) ? 1f : .03f;//为真，动起来。
            float lerpTime = (x != 0 || y != 0 || z is true) ? .05f : .5f;

            time = action ? 1 : time;
            lerpTime = action ? .1f : lerpTime;

            Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
            FadeTime = Mathf.Lerp(FadeTime, time, .5f);

            //AFade.a = 0f;
            //float _x = 0;
            //DOTween.To(() => _x, x => _x = x, 1f, 20f).OnUpdate(()=> {
            //    AFade.a = _x;
            //});
            AFade.a = (1 - Time.timeScale);


            Time.fixedDeltaTime = 0.02f * Time.timeScale;


            if (!(x != 0 || y != 0 || z is true))
            {
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
        
      
    }
    

    public IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(time);
        action = false;
    }


  


   

}
