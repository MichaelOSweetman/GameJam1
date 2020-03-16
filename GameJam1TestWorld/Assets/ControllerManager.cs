using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class ControllerManager : MonoBehaviour
{
    Canvas TitleScreen;
    Canvas VictoryScreen;
    Canvas GUI;

    GameTimer Timer;

    const int MaxControllerCount = 4;

    int ControllerCount = 0;

    int BullRiderControllerID = 0;
    
    // 1 = BullRider, 2 = Runner_1, etc
    int[] playerIDs;

    RunnerMovement[] Runners = new RunnerMovement[MaxControllerCount - 1];
    BullController BullRider;

    void Start()
    {
        TitleScreen = GameObject.FindGameObjectWithTag("TitleScreen").GetComponent<Canvas>();
        VictoryScreen = GameObject.FindGameObjectWithTag("VictoryScreen").GetComponent<Canvas>();
        GUI = GameObject.FindGameObjectWithTag("GUI").GetComponent<Canvas>();

        Timer = GetComponent<GameTimer>();

        Runners[0] = GameObject.FindGameObjectWithTag("RunnerOne").GetComponent<RunnerMovement>();
        Runners[1] = GameObject.FindGameObjectWithTag("RunnerTwo").GetComponent<RunnerMovement>();
        Runners[2] = GameObject.FindGameObjectWithTag("RunnerThree").GetComponent<RunnerMovement>();
        BullRider = GameObject.FindGameObjectWithTag("BullRider").GetComponent<BullController>();

        ControllerCount = XCI.GetNumPluggedCtrlrs();
        playerIDs = new int[ControllerCount];

        for (int PlayerIndex = 0; PlayerIndex < ControllerCount; ++PlayerIndex)
        {
            playerIDs[PlayerIndex] = PlayerIndex + 1;
        }
    }
    
    public void StartGame()
    {
        TitleScreen.enabled = false;
        VictoryScreen.enabled = false;
        GUI.enabled = true;

        int BullRiderID = Random.Range(1, ControllerCount);
        playerIDs[BullRiderID - 1] = 1;
        playerIDs[0] = BullRiderID;

        for (int RunnerIndex = 0; RunnerIndex < MaxControllerCount - 1; ++RunnerIndex)
        {
            if (RunnerIndex > ControllerCount - 1)
            {
                Runners[RunnerIndex].SetAlive(false);
                Runners[RunnerIndex].playerNumber = XboxController.First;
                continue;
            }
            Runners[RunnerIndex].playerNumber = (XboxController)playerIDs[RunnerIndex];
        }
        BullRider.controllerNumber = BullRiderID;

        Timer.isGameRunning = true;
    }
}
