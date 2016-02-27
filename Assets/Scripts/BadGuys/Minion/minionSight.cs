using UnityEngine;
using System.Collections;

public class minionSight : MonoBehaviour {

	public bool playerSighted;
	public float heightMultiplier;
	public float angleDiff;
	public float sightDist;
	public Transform player;

	void Update () {
		if (!playerSighted)
		{
		RaycastHit hit;
			Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, this.transform.forward * sightDist, Color.red);
			Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward + transform.right * angleDiff) * sightDist, Color.red);
			Debug.DrawRay(this.transform.position + Vector3.up * heightMultiplier, (this.transform.forward - transform.right * angleDiff) * sightDist, Color.red);
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, this.transform.forward, out hit, sightDist))
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (this.transform.forward + transform.right * angleDiff), out hit, sightDist))
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
			if(Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (this.transform.forward -transform.right * angleDiff), out hit, sightDist))
			{
				if(hit.collider.gameObject.tag == "Player")
				{
					playerSighted = true;
				}
			}
		}
	}
}
