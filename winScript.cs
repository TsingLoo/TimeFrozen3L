using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScript : MonoBehaviour
{
    public bool thisPlayerHasWon = false;
    [SerializeField] FlowManager flw;
    [SerializeField] PlayerMovement plm;
    [SerializeField] GameObject WinUI;
    [SerializeField] Text winScoreText;
    float score;
    private void OnTriggerEnter(Collider other)
    {
        thisPlayerHasWon = true;
        score = flw.TimerTime;
        string minutes = ((int)score / 60).ToString();
        string seconds = (score % 60).ToString("f2");
        winScoreText.text = minutes + ":" + seconds;
        WinUI.SetActive(true);
    }

    private void Update()
    {
        if (thisPlayerHasWon) 
        {
            if (Input.GetKeyDown(flw.NextPlayer))
            {
                WinUI.SetActive(false);
            }
        }
    }

}
