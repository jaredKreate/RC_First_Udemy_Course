using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject Debris;
    public float life = 5;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    public float speed = 50;

    float lifeTimer = 0;

    void Update()
    {
        //self destruct
        lifeTimer += Time.deltaTime;
        if (lifeTimer > life)
            Destroy(gameObject);

        //move
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    public delegate void HitIndicatorDelegate();
    public static event HitIndicatorDelegate SetIndicators;

    void OnTriggerEnter(Collider col)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(Debris, transform.position, Quaternion.identity)as GameObject;
            Material hitMat = col.GetComponent<MeshRenderer>().material;
            Material debrisMat = go.GetComponent<MeshRenderer>().material;
            debrisMat.color = hitMat.color;
        }
        SetIndicators();
        Destroy(gameObject);
    }
}
