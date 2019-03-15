using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour {


	public Toggle m_Toggle;

	public Slider m_Slider;

	public Slider m_SliderTime;
	GameObject go;
 
	
	private bool isMaxActive = false;

	// Use this for initialization
	void Start () {
		//go = GetComponent<Slider>().gameObject;
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

		
	}
	
	// Update is called once per frame
	void ToggleValueChanged (Toggle m_Toggle) {
		isMaxActive = !isMaxActive;
		
		if(isMaxActive)
		{		
			m_Slider.gameObject.SetActive(true);
			m_SliderTime.gameObject.SetActive(false);
		}
		else
		{
			m_Slider.gameObject.SetActive(false);
			m_SliderTime.gameObject.SetActive(true);
		}
	}
}
