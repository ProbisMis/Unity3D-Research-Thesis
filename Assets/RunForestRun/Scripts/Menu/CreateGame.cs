using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CreateGame : MonoBehaviour
{


    public static  int lastId;
    private GameSettings myGame;


    //CreateGame-1
    private GameObject LRD;
    private GameObject TDD;

    private GameObject totalDown;
    private GameObject totalUp;

    //CreateGame-2


    private GameObject totalTime;
    private GameObject playerSpeed;
    private GameObject distanceBetweenMovements;

    //CreateGame-3
    private GameObject trainingMode;
    private GameObject slowMotionMode;
    



    void Start()
    {

        //CREATE GAME Settings
        //bools
        GameObject createGameMenu = transform.Find("CreateGame").gameObject;
        LRD = createGameMenu.transform.Find("LeftRightDist").gameObject; //transorm.find finds child
        totalDown = createGameMenu.transform.Find("TotalMovement").gameObject; //transorm.find finds child
        TDD = createGameMenu.transform.Find("TopDownDist").gameObject; //transorm.find finds child
        totalUp = createGameMenu.transform.Find("TotalMovementUst").gameObject;

        //CREATE GAME3 Settings
        //bools
        GameObject createGameMenu3 = transform.Find("CreateGame3").gameObject;
        totalTime = createGameMenu3.transform.Find("TotalTime").gameObject;
        playerSpeed = createGameMenu3.transform.Find("PlayerSpeed").gameObject;
        distanceBetweenMovements = createGameMenu3.transform.Find("DistanceBetweenEvents").gameObject;

        //CreateGame2 Settings  TODO implement
        GameObject createGameMenu2 = transform.Find("CreateGame2").gameObject;
        slowMotionMode = createGameMenu2.transform.Find("SlowMotionMode").gameObject;
        trainingMode = createGameMenu2.transform.Find("TrainingMode").gameObject;




    }


    public void CallSave()
    {
        //CreateGame-1
        float LeftRightRatio = LRD.GetComponent<Slider>().value;
        float TopDownRatio = TDD.GetComponent<Slider>().value;  //Access toggle ON property
        string TotalNumberOfDowns = totalDown.GetComponent<TMP_InputField>().text;
        string TotalNumberOfUps = totalUp.GetComponent<TMP_InputField>().text; //Access toggle ON property
        //CreateGame-3
        string speed = playerSpeed.GetComponent<TMP_InputField>().text; //Access toggle ON property
        string time = totalTime.GetComponent<TMP_InputField>().text; //Access toggle ON property
        string distance = distanceBetweenMovements.GetComponent<TMP_InputField>().text; //Access toggle ON property
        
        //CreateGame-2
        bool isSlowMo = slowMotionMode.GetComponent<Toggle>().isOn; //Access toggle ON property
        bool isTrainingMode = trainingMode.GetComponent<Toggle>().isOn; //Access toggle ON property
        

        

        //no need for obj here
        myGame = new GameSettings(LeftRightRatio, TopDownRatio, TotalNumberOfDowns, TotalNumberOfUps, isTrainingMode, isSlowMo, speed, time, distance);
        
        //TODO JSON RAW FORMATTING FOR PHP AND UNITY IF NEEDED
        string myGameToJSON = JsonUtility.ToJson(myGame);
        Debug.Log(myGameToJSON);
        StartCoroutine(SaveGameSettings());

    }

    IEnumerator SaveGameSettings()
    {

        string targetURL = "http://localhost:8080/RunForestRun/savegame.php";
        Debug.Log("Logining " + targetURL);


        WWWForm form = new WWWForm();
        if (DBManager.LoggedIn == true)
        {
            form.AddField("name", DBManager.username);
        }
        form.AddField("LeftDownRatio", myGame.LDR.ToString());
        form.AddField("TopDownRatio", myGame.TDR.ToString());
        form.AddField("TotalDown", myGame.totalD.ToString());
        form.AddField("TotalUp", myGame.totalU.ToString());
        form.AddField("TrainingMode", myGame.trainingMode.ToString());
        form.AddField("SlowMotionMode", myGame.slowMotionMode.ToString());
        form.AddField("Speed", myGame.speed.ToString());
        form.AddField("Time", myGame.time.ToString());
        form.AddField("Distance", myGame.distance.ToString());

        WWW www = new WWW(targetURL, form);
        yield return www;

        if (www.text != "0")
        {
            Debug.Log("Succesfully saved game!!");
            Debug.Log(www.text);
            lastId = Int32.Parse(www.text);
            //DBManager.score = int.Parse(www.text.Split('\t')[1]); //pass score in second chuck
            //UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }
}
