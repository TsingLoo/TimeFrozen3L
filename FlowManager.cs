using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlowManager : MonoBehaviour
{
    [SerializeField] winScript ws;
    [SerializeField] AwakeScreenEffect ase;
    float currentTime = 0f;

    public bool needRespawn = false;
    public float TimerTime;
    float anifinishrealtime = 0f;
    [SerializeField] GameObject textUI;
    [SerializeField] float waitTime = 0.2f;
    FallController fcl;
    [SerializeField] GameObject escObject;
    GameObject[] GameStage0;
    GameObject[] GameStage1;
    GameObject[] GameStage2;
    GameObject[] GameStage3;

    [SerializeField] KeyCode RestartKey = KeyCode.R;
    [SerializeField] public KeyCode NextPlayer = KeyCode.P;
    public bool isInEsc = false;
    [SerializeField] KeyCode EscCode = KeyCode.Escape;
    [SerializeField] Text timerText;
    float startTime;


    public static FlowManager flowManager;
    public int GameStage;
    public int respawnTime;


    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        FlowManager flowManager = this;
        GameStage = -1;
        respawnTime = 0;

        GameStage0 = GameObject.FindGameObjectsWithTag("GameStage0");
        GameStage1 = GameObject.FindGameObjectsWithTag("GameStage1");
        GameStage2 = GameObject.FindGameObjectsWithTag("GameStage2");
        GameStage3 = GameObject.FindGameObjectsWithTag("GameStage3");
    }






    private void Update()
    {

        TimerTime = Time.realtimeSinceStartup - anifinishrealtime  - currentTime;

        string minutes = ((int)TimerTime / 60).ToString();
        string seconds = (TimerTime % 60).ToString("f2");

        if (ase.progress == 1)
        {
            if (!ws.thisPlayerHasWon)
            {
                textUI.SetActive(true);
                timerText.text = minutes + ":" + seconds;
            }
            else
            {
                textUI.SetActive(false);
            }
        }
            
       
            if (ase.progress < 1)
            {
                anifinishrealtime = Time.realtimeSinceStartup;


            }

            if (Input.GetKeyDown(KeyCode.M) && isInEsc)
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(EscCode))
            {
                if (!ws.thisPlayerHasWon)
                {
                    if (!isInEsc)
                    {
                        escObject.SetActive(true);
                        Time.timeScale = 0;
                        isInEsc = true;

                    }
                    else
                    {
                        Time.timeScale = 1;
                        escObject.SetActive(false);
                        isInEsc = false;

                    }
                }
            }

            if (Input.GetKeyDown(NextPlayer))
            {
                
                ws.thisPlayerHasWon = false;
                currentTime = Time.realtimeSinceStartup - anifinishrealtime - startTime;
                needRespawn = true;
                respawnTime = 0;
                GameStage = 0;
                respawn0();
                respawn1();
                respawn2();
                respawn3();

            }

            if (Input.GetKeyDown(RestartKey))
            {

                StartCoroutine(wairSeconds(waitTime));
                respawnTime = respawnTime + 1;
                if (GameStage == 0)
                {
                    needRespawn = true;
                    respawn0();
                }

                if (GameStage == 1)
                {
                    needRespawn = true;
                    respawn1();
                }

                if (GameStage == 2)
                {
                    needRespawn = true;
                    respawn2();
                }

                if (GameStage == 3)
                {
                    needRespawn = true;
                    respawn3();
                }


            }

            //  Debug.Log(GameStage);


        }

        void respawn0()
        {
            for (int i = 0; i < GameStage0.Length; i++)
            {
                fcl = GameStage0[i].GetComponent<FallController>();
                fcl.BackToWork();

                //GameStage0[i].SetActive(false);
                //GameStage0[i].SetActive(true);

            }
        }

        void respawn1()
        {
            for (int i = 0; i < GameStage1.Length; i++)
            {
                fcl = GameStage1[i].GetComponent<FallController>();
                fcl.BackToWork();

            }
        }
        void respawn2()
        {
            for (int i = 0; i < GameStage2.Length; i++)
            {
                fcl = GameStage2[i].GetComponent<FallController>();
                fcl.BackToWork();

            }
        }

        void respawn3()
        {
            for (int i = 0; i < GameStage3.Length; i++)
            {
                fcl = GameStage3[i].GetComponent<FallController>();
                fcl.BackToWork();
            }
        }




        IEnumerator wairSeconds(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
        }
    }
