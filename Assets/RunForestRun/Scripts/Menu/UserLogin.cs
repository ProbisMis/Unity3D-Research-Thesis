using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserLogin : MonoBehaviour
{

    public TMP_InputField nameField;
    public TMP_InputField passwordField;
    public Toggle toggleTherapist;

    private bool isTherapist;

    private void CheckToggleStatus()
    {
        if (toggleTherapist.isOn)
        {
            isTherapist = true;
        }
        else
        {
            isTherapist = false;
        }
    }


    // Use this for initializationpublic void CallLogin()
    public void CallLogin()
    {
        StartCoroutine(LoginTherapist());
    }

    IEnumerator LoginTherapist()
    {
        string targetURL = "http://localhost:8080/RunForestRun/login.php";
        Debug.Log("Logining " + targetURL);


        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        //Therapist toggle box
        CheckToggleStatus();
        if (isTherapist == true)
        {
            form.AddField("therapist", "true");
        }
        else
        {
            form.AddField("therapist", "false");
        }

        WWW www = new WWW(targetURL, form);
        yield return www;

        //string[] wwwText = www.text.Split(' ');
        if (www.text[2] == '0')
        {
            Debug.Log("Logged In");
            DBManager.username = nameField.text;
            gameObject.SetActive(false); //Disable LoginMenu
            LoadUserScene();
        }
        else
        {
            Debug.Log("User login failed. Error #" + www.text);
        }


    }

    private void LoadUserScene()
    {
        if (isTherapist == true)
        {
            //TODO set active therapist menu
            GameObject menu = transform.parent.Find("TherapistMenu").gameObject;
            Debug.Log("Loading Threapist Menu");
            menu.SetActive(true);
        }
        else
        {
            //TODO set active patient menu
            GameObject menu = transform.parent.Find("PatientMenu").gameObject;
            Debug.Log("Loading Patient Menu");
            menu.SetActive(true);
        }
    }
}
