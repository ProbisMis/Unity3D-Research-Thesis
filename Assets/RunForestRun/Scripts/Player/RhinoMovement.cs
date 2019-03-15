using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhinoMovement : MonoBehaviour {
	
	public float rhinoPosition = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		 if (PauseMenu.GameIsPaused)
        {
            return;
        }
		
		//rhinoPosition += DynamicGameSettings.maxSpeed; //Speed
		transform.position = new Vector3(0, 1, moveorb.selfRigidbody.transform.position.z + 3); //Move Forward
	}
}
