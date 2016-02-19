using UnityEngine;
using System.Collections;

public class LightBeamMaterialChange : MonoBehaviour {

	public Renderer renderer;
	public Material currMat;
	public Material newMat;
	public TripodSight triSight;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		currMat = GetComponent<Material>();
		triSight = GetComponentInParent<TripodSight>();
	}
	
	// Update is called once per frame
	void Update () {
		if(triSight.playerSighted)
		{
			renderer.material = newMat;
		}
	}
}
