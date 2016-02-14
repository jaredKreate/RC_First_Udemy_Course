using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float sightDist;

	// Update is called once per frame
	void Update () {
		if(!playerSighted)
		{					
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, -transform.up * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (-transform.up + transform.right) * sightDist, Color.green);
			Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (-transform.up - transform.right) * sightDist, Color.green);
			RaycastHit hit;
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, -transform.up, out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{					
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.up + transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{					
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (-transform.up - transform.right), out hit, sightDist))				
			{
				if(hit.collider.gameObject.tag == "Player")
				{					
					playerSighted = true;
				}
			}
		}
	}
}