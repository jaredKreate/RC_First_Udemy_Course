using UnityEngine;
using System.Collections;

public class TripodLook : MonoBehaviour {

	public TripodSight triSight;
	public Transform[] lightBeams;

	// Use this for initialization
	void Start () {
		triSight = GetComponent<TripodSight>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(triSight.playerSighted)
		{
			transform.LookAt(triSight.player);
			transform.Rotate(90,0,180);
			foreach(Transform tr in lightBeams)
			{
				tr.LookAt(triSight.player);
				tr.Rotate(90,0,180);
			}
		}
	}
}
