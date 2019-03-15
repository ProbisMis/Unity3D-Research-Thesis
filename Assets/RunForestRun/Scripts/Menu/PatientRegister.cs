using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatientRegister : MonoBehaviour {

	public TMP_InputField nameField;
    public TMP_InputField passwordField;

    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(RegisterTherapist());
    }

    IEnumerator RegisterTherapist()
    {
        string targetURL = "http://localhost:8080/RunForestRun/registerpatient.php";
        Debug.Log("Registering patient in " + targetURL);


        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
		form.AddField("patientName", nameField.text );
        form.AddField("patientPass", passwordField.text);

        WWW www = new WWW(targetURL, form);
        yield return www;

        Debug.Log(www.text);
        if (www.text != "0")
        {
            Debug.Log("Registered Patient");
            gameObject.SetActive(false); //Disable RegisterPatientMenu
            GameObject menu = transform.parent.Find("TherapistMenu").gameObject;
            Debug.Log("Loading Threapist Menu");
            menu.SetActive(true);
        }
        else
        {
            Debug.Log("Patient register failed. Error #" + www.text);
        }


    }

    public void VerifyInputs()
    {
        submitButton.interactable = (nameField.text.Length >= 3 && passwordField.text.Length >= 3);
    }
}
