using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GetGame : MonoBehaviour {


	
	public void CallPlayTest()
    {
      StartCoroutine(SaveGameSettings());
    }

    IEnumerator SaveGameSettings()
    {

        string targetURL = "http://localhost:8080/RunForestRun/getgame.php";
        Debug.Log("Getting game from  " + targetURL);


        WWWForm form = new WWWForm();
        if (DBManager.LoggedIn == true)
        {
            form.AddField("name", DBManager.username);
        }
        Debug.Log("Last Id is:  " + CreateGame.lastId);
        form.AddField("lastId", CreateGame.lastId.ToString());
   

        WWW www = new WWW(targetURL, form);
        yield return www;
        if (www.text != "0")
        {
            Debug.Log("Succesfully retriving game!!");
            Debug.Log("Settings retrieved are : " + www.text);
			string[] settings = www.text.Split('*');  //split by delimiter specified in getgame.php
			GM.currentGameSetting = settings; //pass to GM  not good practive
			SceneManager.LoadScene(1); //load game 
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }
}
