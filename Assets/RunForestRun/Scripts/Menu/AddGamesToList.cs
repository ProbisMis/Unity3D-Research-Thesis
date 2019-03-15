using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddGamesToList : MonoBehaviour
{


    public static List<string[]> games = new List<string[]>();

    public static string choosenPatient;

    public static string choosenGameId ;
    public GameObject itemTemplate;

    public GameObject content;

    private bool isListed = false;

    public void AddButton_Click()
    {
        if (!isListed)
        {
            foreach (var game in games)
            {

                isListed = true;
                //id
                itemTemplate.GetComponentInChildren<TMP_Text>().text = game[0].ToString();
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);
                copy.transform.localPosition = Vector3.zero;


                copy.GetComponentInChildren<Button>().onClick.AddListener(

                    () =>
                    {
                        //TODO
                        Debug.Log("Choosen :" + game[0].ToString());
                        choosenGameId = game[0];
                        StartCoroutine(AttachGameToPatient());
                        
                    }
                );
            }
        }





    }

    IEnumerator AttachGameToPatient()
    {

        string targetURL = "http://localhost:8080/RunForestRun/savegametopatient.php";
        Debug.Log("Getting patients from  " + targetURL);


        WWWForm form = new WWWForm();

        form.AddField("name", choosenPatient);
        form.AddField("gameid",choosenGameId);


        WWW www = new WWW(targetURL, form);
        yield return www;

        if (www.text == "0")
        {
            Debug.Log("Succesfully saved game to patients!!");
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }
    }
}
