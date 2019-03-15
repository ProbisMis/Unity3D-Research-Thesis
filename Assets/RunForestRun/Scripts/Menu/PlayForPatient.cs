using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayForPatient : MonoBehaviour {

	// Use this for initialization
	public void CallPlayTest()
    {
      StartCoroutine(GetGameSettings());
    }

    IEnumerator GetGameSettings()
    {

        string targetURL = "http://localhost:8080/RunForestRun/getgamepatient.php";
        Debug.Log("Getting game from   " + targetURL);


        WWWForm form = new WWWForm();
        if (DBManager.LoggedIn == true)
        {
            form.AddField("name", DBManager.username);
        }  
        WWW www = new WWW(targetURL, form);
        yield return www;
        
		if (www.text != "0")
        {
            Debug.Log("Succesfully retriving game!!");
            Debug.Log("Settings retrieved are : " + www.text);
            //split by delimiter specified in getgame.php
            GM.currentGameSetting = www.text.Split('*'); //pass to GM  not good practive
			SceneManager.LoadScene(1); //load game 
        }
        else
        {
            Debug.Log("There is no game avaliable, if you want to play go to mainmenu. Error #" + www.text);
        }

    }
}
