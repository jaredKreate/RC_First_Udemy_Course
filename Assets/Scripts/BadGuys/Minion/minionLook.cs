using UnityEngine;
using System.Collections;

public class minionLook : MonoBehaviour {

	public minionSight minSight;

	// Use this for initialization
	void Start () {
		minSight = GetComponent<minionSight>();
	}

	// Update is called once per frame
	void LateUpdate () {
		if(minSight.playerSighted)
		{
			transform.LookAt(minSight.player);
//			transform.Rotate(90,0,180);
		}
	}
}
