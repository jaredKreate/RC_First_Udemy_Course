using UnityEngine;
using System.Collections;

public class LightBeamMaterialChange : MonoBehaviour {

	public Renderer renderer;
	public Material currMat;
	public Material newMat;
	public TripodSight triSight;
	public float lightTexRot;
	public Light myLight;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		currMat = GetComponent<Material>();
		triSight = GetComponentInParent<TripodSight>();
	}
	
	// Update is called once per frame
	void Update () {		
		this.renderer.material.mainTextureOffset = new Vector2(Time.time * lightTexRot,0);
		if(triSight.playerSighted)
		{
			renderer.material = newMat;
			myLight.color = new Color(255,0,0);
		}
	}
}
