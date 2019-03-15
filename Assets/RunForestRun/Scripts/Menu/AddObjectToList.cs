using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddObjectToList : MonoBehaviour
{

    public static List<string> patients = new List<string>();
    public GameObject itemTemplate;

    public GameObject content;

    private bool isListed = false;

    public void AddButton_Click()
    {
        if (!isListed)
        {
            foreach (var patient in patients)
            {
                isListed = true;
           
                itemTemplate.GetComponentInChildren<TMP_Text>().text = patient.Trim();
                var copy = Instantiate(itemTemplate);
                copy.transform.SetParent(content.transform, false);
                copy.transform.localPosition = Vector3.zero;

                copy.GetComponentInChildren<Button>().onClick.AddListener(

                    () =>
                    {
                       //TODO
                       Debug.Log("Choosen :" + patient.Trim());
                       AddGamesToList.choosenPatient = patient.Trim();

                    }
                );



            }
        }



    }

}
