using UnityEngine;
using System.Collections;

public class TripodLook : MonoBehaviour {

	public TripodSight triSight;

	// Use this for initialization
	void Start () {
		triSight = GetComponent<TripodSight>();
	}
	
	// Update is called once per frame
	void Update () {
		if(triSight.playerSighted)
		{
			transform.LookAt(triSight.player.transform);
		}
	}
}
