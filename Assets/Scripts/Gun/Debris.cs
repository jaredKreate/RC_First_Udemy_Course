using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Debris : MonoBehaviour {

    public float maxForce = 50;
    public float life = 4;

    Rigidbody rBody;
    float lifeTimer = 0;

    float b;

	void Start () {
        rBody = GetComponent<Rigidbody>();
        rBody.AddForce(new Vector3(Random.Range(-maxForce, maxForce), Random.Range(0, maxForce), Random.Range(-maxForce, maxForce)));
	}
	
	void Update () {
        //self destruct
        lifeTimer += Time.deltaTime;
        if (lifeTimer > life)
            Destroy(gameObject);
	}
}
