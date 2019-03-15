using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DynamicGameSettings
{

    //Game Settings from getgame.php

    public static string trainingMode;

    public static string slowMotionMode;

    public static float maxSpeed = 0.15f; //default speed

    public static int maxTimeInSeconds = -1; //if no time , there is no time :)

    public static int maxDistance = 7;

    public static int numberOfLeft = 15;
    public static int numberOfRight = 15;

    public static int numberOfTopLeft = 15;

    public static int numberOfTopRight = 15;

    public static int numberOfDefaultTiles = 15;

    public static void SetGameSettings(string[] currentGameSetting)
    {
        //SETTINGS TEST
        if (currentGameSetting != null && currentGameSetting.Length != 1)
        {
            Debug.Log("Lenght of settings : " + currentGameSetting.Length);
  
            int LeftRatio = int.Parse(currentGameSetting[0]);
            int RightRatio = 100 - LeftRatio;
            int TopRatio = int.Parse(currentGameSetting[1]); //t
            int DownRatio = 100 - TopRatio;
            int TotalNumberOfLRD = Convert.ToInt32(currentGameSetting[2]); //d
            int TotalNumberOfTDD = Convert.ToInt32(currentGameSetting[3]); //u

            if (LeftRatio != 0)
            {
                numberOfLeft = (LeftRatio * TotalNumberOfLRD) / 100;
            }
            else
            {
                numberOfLeft = -1;
            }

            if (RightRatio != 0)
            {
                numberOfRight = (RightRatio * TotalNumberOfLRD) / 100;
            }
            else
            {
                numberOfRight = -1;
            }
            if (TopRatio != 0)
            {
                numberOfTopLeft = (TopRatio * TotalNumberOfTDD) / 100;
            }
            else
            {
                numberOfTopLeft = -1;
            }
            if (DownRatio != 0)
            {
                numberOfTopRight = (DownRatio * TotalNumberOfTDD) / 100;
            }
            else
            {
                numberOfTopRight = -1;
            }


            trainingMode = (currentGameSetting[4]); //jump
            slowMotionMode = (currentGameSetting[5]); //health

            maxSpeed = float.Parse(currentGameSetting[6]); //training
            maxSpeed = maxSpeed / 10;

            
			int maxTime = Convert.ToInt32(currentGameSetting[7]); //time in minutes
            maxTimeInSeconds = maxTime * 60;

            maxDistance = Convert.ToInt32(currentGameSetting[8]);

        }
    }

}
