using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEditor.SceneManagement;

public class GM : MonoBehaviour
{



    //Game settings refferences
    public static string[] currentGameSetting;
    public Prefabs myPrefabs;

    /* LVL COMP STATUS RELATED */
    //Total time of gameplay accessed in lvlcompstatus 
    public static float timeTotal = 0;
    public float waittoload = 0;
    public static string lvlCompStatus = "";
    public static int coinTotal = 0;
    public static int bananaTotal = 0;

    public static int watermelonTotal = 0;

    //Player's position in world incrementing over time
    public static float zScenePos = 4;
    public Transform player;


    //POWER_UPS
    public Transform HealthPWU; //+30 health
    public Transform ShieldPWU; //for 10sec or smt; 

    private float difference;

    // Use this for initialization
    void Start()
    {   
        
        if (currentGameSetting != null)
        {
            DynamicGameSettings.SetGameSettings(currentGameSetting);
        }
         //Set game settings 
        zScenePos = 4; //Start position 

    }



    // Update is called once per frame
    void Update()
    {


        //If the game is paused during gameplay
        if (PauseMenu.GameIsPaused)
        {
            return;
        }


        //Activated when time is set
        if (DynamicGameSettings.maxTimeInSeconds != -1 && timeTotal > DynamicGameSettings.maxTimeInSeconds)
        {
            lvlCompStatus = "Time reached";
            SceneManager.LoadScene(2);
        }



      
        timeTotal += Time.deltaTime; //Total Game time 
        difference = zScenePos - player.position.z; //For generating new ground tiles 


        if (difference < 60)
        {   
            //If Queue is not empty and position of peek element in queue is smaller than player object
            //Destroy the last element in queue
            if (!GroundDestroyerQueue.isEmpty() && player.position.z > GroundDestroyerQueue.GetPeekElement().position.z)
            {
                Transform tile = GroundDestroyerQueue.InitiateDestroyGroundOneShot();
                StartCoroutine(WaitAndDestroyGround(tile));
            }
    
            //Ground Tile
            CreateAllTiles();

        
            zScenePos += 12; //New position of ground to be generated in next update

        }

        if (lvlCompStatus == "Fail")
        {

            waittoload += Time.deltaTime;
        }

        if (waittoload > 5)
        {
            Debug.Log("LevelComp accessed");
            SceneManager.LoadScene("LevelComp");

        }

    }   

    private void CreateAllTiles()
    {
        Vector3 defaultPlacement = new Vector3(0, 0, zScenePos); //for ground
        Vector3 forCollectible = new Vector3(UnityEngine.Random.Range(-1, 2), 1, zScenePos + DynamicGameSettings.maxDistance); //for collectibles
        

        if (DynamicGameSettings.numberOfLeft != -1 && DynamicGameSettings.numberOfLeft > 0)
        {            
            myPrefabs.SetRandomGround(myPrefabs.LeftDown, defaultPlacement, 4);
            myPrefabs.SetRandomCollectible(myPrefabs.collectiblesList, forCollectible);
            
            DynamicGameSettings.numberOfLeft--;
        }
        else if (DynamicGameSettings.numberOfRight != -1 && DynamicGameSettings.numberOfRight > 0)
        {
            myPrefabs.SetRandomGround(myPrefabs.RightDown, defaultPlacement, 4);
            myPrefabs.SetRandomCollectible(myPrefabs.collectiblesList, forCollectible);
            
            DynamicGameSettings.numberOfRight--;
        }
        else if (DynamicGameSettings.numberOfDefaultTiles != -1 &&  DynamicGameSettings.numberOfDefaultTiles > 0)
        {
            myPrefabs.SetRandomGround(myPrefabs.TwoSided, defaultPlacement, 5);
            myPrefabs.SetRandomCollectible(myPrefabs.collectiblesList, forCollectible);
            
            DynamicGameSettings.numberOfDefaultTiles--;
        }
        else if (DynamicGameSettings.numberOfTopLeft != -1 && DynamicGameSettings.numberOfTopLeft > 0)
        {
            myPrefabs.SetRandomGround(myPrefabs.LeftUp, defaultPlacement, 4);
            myPrefabs.SetRandomCollectible(myPrefabs.collectiblesList, forCollectible);
            
            DynamicGameSettings.numberOfTopLeft--;
        }
        else if (DynamicGameSettings.numberOfTopRight != -1 && DynamicGameSettings.numberOfTopRight > 0)
        {
            myPrefabs.SetRandomGround(myPrefabs.RightUp, defaultPlacement, 4);
            myPrefabs.SetRandomCollectible(myPrefabs.collectiblesList, forCollectible);
            
            DynamicGameSettings.numberOfTopRight--;
        }
        else
        {
            //TODO end game somehow
            Debug.Log("All Events completed");
            myPrefabs.SetExit(myPrefabs.exit, defaultPlacement);
        }

    }

   IEnumerator WaitAndDestroyGround(Transform tile)
    {
        yield return new WaitForSeconds(10f);
        // do something
        Destroy(tile.gameObject);

    }
}
