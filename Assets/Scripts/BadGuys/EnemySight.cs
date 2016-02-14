using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float angleDiff;
	public float sightDist;
	public GameObject[] lightbeams;

	// Update is called once per frame
	void Update () {
		if(!playerSighted)
		{
			RaycastHit hit;
			foreach(GameObject go in lightbeams)
			{
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, -go.transform.up * sightDist, Color.red);
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, -go.transform.up * sightDist, Color.red);
				Debug.DrawRay(go.transform.position + Vector3.up * heightMultiplier, -go.transform.up * sightDist, Color.red);
				if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, -go.transform.up, out hit, sightDist))
				{
					if(hit.collider.gameObject.tag == "Player")
					{
						playerSighted = true;
					}
				}
			}
//			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, -transform.up * sightDist, Color.green);
//			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (-transform.up + transform.right) * sightDist, Color.green);
//			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (-transform.up - transform.right) * sightDist, Color.green);
//			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, -transform.up, out hit, sightDist))				
//			{
//				if(hit.collider.gameObject.tag == "Player")
//				{					
//					playerSighted = true;
//				}
//			}
//			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.up + transform.right), out hit, sightDist))				
//			{
//				if(hit.collider.gameObject.tag == "Player")
//				{					
//					playerSighted = true;
//				}
//			}
//			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.up - transform.right), out hit, sightDist))				
//			{
//				if(hit.collider.gameObject.tag == "Player")
//				{					
//					playerSighted = true;
//				}
//			}
		}
	}
}