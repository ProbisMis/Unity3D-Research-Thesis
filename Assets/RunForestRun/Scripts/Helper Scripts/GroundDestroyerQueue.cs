using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GroundDestroyerQueue  {


	static Queue<Transform> myQueue = new Queue<Transform>();
	// Use this for initialization


	public static Transform InitiateDestroyGroundOneShot()
	{	
		Debug.Log("Destroying Ground behind camera");
		Transform tile = myQueue.Dequeue();
		return tile;

	}

	public  static void InitiateEnqueueGround(Transform tile)
	{	

		Debug.Log("Adding Ground Object to Queue");
		myQueue.Enqueue(tile);
	}

	public static Transform GetPeekElement()
	{	
		Transform peek  = myQueue.Peek();
		return peek;
	}

	public static bool isEmpty()
	{
		if (myQueue.Count == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public static void resetQueue()
	{
		myQueue = new Queue<Transform>();
	}

}
