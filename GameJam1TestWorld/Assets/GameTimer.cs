using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    Canvas VictoryScreen;
    Canvas GUI;

    public UnityEngine.UI.Text timeRemainingText;

    public UnityEngine.UI.Text victoryText;

    public float gameTimerStart = 60;

    public float gameTimerEnd = 0;

    public float gameTimerIterator = 60;

    public bool RunnerOneisAlive = true;
    public bool RunnerTwoisAlive = true;
    public bool RunnerThreeisAlive = true;

    public bool isGameRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        timeRemainingText.text = "Timer: " + gameTimerIterator;

        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen").GetComponent<Canvas>();
        GUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<Canvas>();
        RunnerOneisAlive = GameObject.FindGameObjectWithTag("RunnerOne").GetComponent<RunnerMovement>().alive;
        RunnerTwoisAlive = GameObject.FindGameObjectWithTag("RunnerTwo").GetComponent<RunnerMovement>().alive;
        RunnerThreeisAlive = GameObject.FindGameObjectWithTag("RunnerThree").GetComponent<RunnerMovement>().alive;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {


            //add to iterator
            gameTimerIterator -= 1 * Time.deltaTime;

            timeRemainingText.text = "Timer: " + gameTimerIterator;

            //if all players are dead
            if (!RunnerOneisAlive && !RunnerTwoisAlive && !RunnerThreeisAlive)
            {
                GUI.enabled = false;
                //bull wins
                victoryText.text = "BULL WINS!";
                VictoryScreen.enabled = true;
                isGameRunning = false;

            }

            //if timer reaches the end timer value
            if (gameTimerIterator < gameTimerEnd)
            {
                //if all players are dead
                if (!RunnerOneisAlive && !RunnerTwoisAlive && !RunnerThreeisAlive)
                {
                    GUI.enabled = false;
                    //bull wins
                    victoryText.text = "BULL WINS!";
                    VictoryScreen.enabled = true;
                    isGameRunning = false;
                }

                //if ANY of the players are alive they win
                if (RunnerOneisAlive || RunnerTwoisAlive || RunnerThreeisAlive)
                {
                    GUI.enabled = false;
                    //runners win
                    victoryText.text = "RUNNERS WIN!";
                    VictoryScreen.enabled = true;
                    isGameRunning = false;
                }



            }

        }
    }
}
