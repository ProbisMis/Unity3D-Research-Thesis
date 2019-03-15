using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class GameSettings
{
    public float LDR;
    public float TDR;
    public string totalD;
    public string totalU;
    public bool trainingMode;
    public bool slowMotionMode;
    public string speed;
    public string time;
    public string distance;

    
    public GameSettings(float left, float right, string td, string tu, bool training, bool slowmo, string sp, string t, string dist)
    {
        LDR = (left);
        TDR = right;
        totalD = td;
        totalU = tu;
        trainingMode = training;
        slowMotionMode = slowmo;
        speed = sp;
        time = t;
		distance = dist;
    }


}
