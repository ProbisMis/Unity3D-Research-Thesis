using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name == "Treasure(Clone)") { 
            transform.Rotate(0, 3, 0);
        }
        if (gameObject.name == "Coin(Clone)")
        {
            transform.Rotate(0, 0, 3);
        }

        if (gameObject.name == "Crystal(Clone)")
        {
            transform.Rotate(0, 3, 0);
        }
       
	}
}
