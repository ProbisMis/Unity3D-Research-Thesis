using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text scoreFieldText;

    public TMP_Text scoreMultiplier;

    public TMP_Text health;


    public int actualScoreMultipler;

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
    }



}
