using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesForTherapist : MonoBehaviour {


	public void CallGetGames()
    {
        StartCoroutine(GetGamesList());
    }

    IEnumerator GetGamesList()
    {

        string targetURL = "http://localhost:8080/RunForestRun/getgamelist.php";
        Debug.Log("Getting games from  " + targetURL);


        WWWForm form = new WWWForm();
        if (DBManager.LoggedIn == true)
        {
            form.AddField("name", DBManager.username);
        }  

        WWW www = new WWW(targetURL, form);
        yield return www;
        if (www.text != "0")
        {
            Debug.Log("Succesfully retriving games!!");
            Debug.Log("games retrieved are : " + www.text);
            string[] tGames = www.text.Split('&');  //split by delimiter specified in getgame.php
			
			for (int i= 0; i < tGames.Length -1 ; i++)
			{
			Debug.Log(tGames[i]);
            string[] items = tGames[i].Split('*');  //split by delimiter specified in getgame.php	
			
			// for (int j= 0; j < items.Length -1 ; j++)
			// {
			// 	Debug.Log(items[j]);
				
			// }
			AddGamesToList.games.Add(items);
			
			
			}
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }
}
