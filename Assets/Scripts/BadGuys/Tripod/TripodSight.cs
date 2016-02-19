using UnityEngine;
using System.Collections;

public class TripodSight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float angleDiff;
	public float sightDist;
	public GameObject[] lightbeams;
	public Transform player;

	// Update is called once per frame
	void Update () {
		if (!playerSighted)
		{
			Debug.Log("Stepped into Trisight Else");
			RaycastHit hit;
			foreach(GameObject go in lightbeams)
			{
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, -go.transform.up * sightDist, Color.red);
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, (-go.transform.up + transform.right * angleDiff) * sightDist, Color.red);
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, (-go.transform.up - transform.right * angleDiff) * sightDist, Color.red);
				if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, -go.transform.up, out hit, sightDist))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						playerSighted = true;
					}
				}
				if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-go.transform.up + transform.right * angleDiff), out hit, sightDist))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						playerSighted = true;
					}
				}
				if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-go.transform.up -transform.right * angleDiff), out hit, sightDist))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						playerSighted = true;
					}
				}
			}
		}
	}
}