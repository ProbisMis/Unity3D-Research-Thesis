using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Prefabs : MonoBehaviour{

	//PREFABS
    public  List<Transform> LeftDown; //4

    public  List<Transform> LeftUp;//4
    public  List<Transform> RightDown;//4

    public  List<Transform> RightUp;//4

    public  List<Transform> Up; //2

    public  List<Transform> TwoSided; //5

	public  List<Transform> collectiblesList; //3

	public Transform exit;

	

	public  void SetRandomGround(List<Transform> tiles, Vector3 defaultPlacement, int size)
	{
		int tileObj = UnityEngine.Random.Range(0, size);   	
		Transform tile = Instantiate(tiles[tileObj], defaultPlacement , tiles[tileObj].rotation);    
		GroundDestroyerQueue.InitiateEnqueueGround(tile);      
	}

	public  void SetRandomCollectible(List<Transform> collectibles, Vector3 defaultPlacement)
	{
		Transform collectibleObj = collectibles[UnityEngine.Random.Range(0, 3)];
        Transform collectible = Instantiate(collectibleObj, defaultPlacement , collectibleObj.rotation);
        
	}

	public void SetExit(Transform exit, Vector3 defaultPlacement)
	{
		Transform tile = Instantiate(exit, defaultPlacement , exit.rotation);
	}

	public void GetList(){
		
	}
}
