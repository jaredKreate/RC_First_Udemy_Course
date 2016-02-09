using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [System.Serializable]
    public struct Damage { public int min; public int max; }
    public Damage damage;
    public float life = 5;
    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    public GameObject Debris;
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
    public delegate void TargetDelegate(EnemyData enemyData);
    public static event TargetDelegate ActivateTarget;

    void OnTriggerEnter(Collider col)
    {
        if (!col.tag.Equals("Pickup") && !col.tag.Equals("Player") && !col.tag.Equals("Enemy"))
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject go = Instantiate(Debris, transform.position, Quaternion.identity) as GameObject;
                Material hitMat = col.GetComponent<MeshRenderer>().material;
                Material debrisMat = go.GetComponent<MeshRenderer>().material;
                debrisMat.color = hitMat.color;
            }
            SetIndicators();
            Destroy(gameObject);
        }
        else if (col.tag.Equals("Enemy"))
        {
            EnemyData enemyData = col.GetComponent<EnemyData>();
            enemyData.ModifyHealth(-Random.Range(damage.min, damage.max));
            ActivateTarget(enemyData);
        }
    }
}
