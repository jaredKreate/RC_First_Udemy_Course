using UnityEngine;
using System.Collections;

public class bipedLook : MonoBehaviour {

	public bipedSight biSight;

	// Use this for initialization
	void Start () {
		biSight = GetComponent<bipedSight>();
	}

	// Update is called once per frame
	void LateUpdate () {
		if(biSight.playerSighted)
		{
			transform.LookAt(biSight.player);
			//			transform.Rotate(90,0,180);
		}
	}
}
