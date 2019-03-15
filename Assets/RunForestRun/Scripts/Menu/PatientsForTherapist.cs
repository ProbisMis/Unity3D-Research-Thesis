using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientsForTherapist : MonoBehaviour
{

    public void CallGetPatients()
    {
        StartCoroutine(GetPatientsToList());
    }

    IEnumerator GetPatientsToList()
    {

        string targetURL = "http://localhost:8080/RunForestRun/getpatients.php";
        Debug.Log("Getting patients from  " + targetURL);


        WWWForm form = new WWWForm();
        if (DBManager.LoggedIn == true)
        {
            form.AddField("name", DBManager.username);
        }  

        WWW www = new WWW(targetURL, form);
        yield return www;
        if (www.text != "0")
        {
            Debug.Log("Succesfully retriving Patients!!");
            Debug.Log("Patients retrieved are :" + www.text);
            string[] tPatients = www.text.Split('*');  //split by delimiter specified in getgame.php
            foreach (var item in tPatients)
            {
                AddObjectToList.patients.Add(item);     
            }
			
            //pass to GM  not good practive
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }

    }
}
