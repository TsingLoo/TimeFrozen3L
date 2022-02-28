using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class DashUI : MonoBehaviour
{
    [SerializeField] PlayerMovement plm;
    [SerializeField] Image img;
    float rotateZdeg = 0;
    // Start is called before the first frame update
    void Start()
    {
        img.DOColor(Color.white, 1);
    }

    // Update is called once per frame
    void Update()
    {
        img.transform.localEulerAngles = new Vector3(0, 0, rotateZdeg);
       // Debug.Log(img.transform.localEulerAngles);
        if(!plm.ableTodash)
        {
            rotateZdeg = 0;
            img.DOColor(Color.red, 0.1f);
            // Tween tween = DOTween.To(() => rotateZdeg, x => rotateZdeg = x, 360, 1);
           // Debug.Log(rotateZdeg);
        }
        else 
        {
            img.DOColor(Color.white, 0.5f);
            Tween tween = DOTween.To(() => rotateZdeg, x => rotateZdeg = x, -360, 0.5f);
            


        }
    }
}
