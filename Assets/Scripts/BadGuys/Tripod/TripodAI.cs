using UnityEngine;
using System.Collections;

public class TripodAI : MonoBehaviour {

	[System.Serializable]
	public class Components
	{
		// This variable will contain our nav mesh agent which stores a lot of our movement data
		public NavMeshAgent agent;
		// This variable will store a reference to our animator
		public Animator myAnim;
		// This variable will store a reference to our sight script
		public TripodSight mySight;
		// This variable will store a reference to our Character Controller
		public CharacterController mycontroller;
		// Reference to Enemy Data Script
		public EnemyData myData;
		//Reference to Trigger Collider
		public Collider myTrigger;
		public GameObject myExplosion;
	}

	public Components components;

	// This state represents the states that the AI character can actively be in
	public enum State {
		IDLE,
		PATROL,
		CHASE,
		ATTACK,
		SPAWN,
		DEATH
	}

	// We need this variable in order to change the current state of the AI
	public State state;

	// We will need these if the AI needs to be patrolling
	[System.Serializable]
	public class Patrolling
	{		
		public bool amIOnPatrol;
		public GameObject[] waypoints;
		public int waypointInd;
		public float patrolSpeed;
		public float maxWaypointDistance;
	}

	public Patrolling patrolling;

	[System.Serializable]
	public class Chasing
	{
		public bool chase;
		public float chaseSpeed;
		public float minDistance;
		public float maxDistance;
	}

	public Chasing chasing;

	[System.Serializable]
	public class Attacking
	{
		public GameObject bullet;
		public float timer;
		public float waitShot = 2f;
		public Transform shotPos;
		public int ammoCount = 3;	
	}

	public Attacking attacking;

	[System.Serializable]
	public class Spawning
	{
		public GameObject minion;
		public int maxMinions;
		public Transform spawnLocation;
		public int minionsSpawned;
	}

	public Spawning spawning;

	// Use this for initialization
	void Awake () {
		components.agent = GetComponent<NavMeshAgent>();
		components.myAnim = GetComponent<Animator>();
		if (components.mySight == null)
		{
			components.mySight = GetComponentInChildren<TripodSight>();
		}
		components.mycontroller = GetComponent<CharacterController>();
		components.myData = GetComponent<EnemyData>();
//		patrolling.waypointInd = Random.Range(0,patrolling.waypoints.Length);
		patrolling.waypointInd = 3;
		state = TripodAI.State.IDLE;
		StartCoroutine("FSM");
	}

	// This is our Finite State machine which will run continuously until the AI character has been eliminated
	IEnumerator FSM()
	{
		while (components.myData.alive)
		{
			switch (state)
			{
			case State.IDLE:				
				Idle();
				Debug.Log("I am in state: " + state);
				break;
			case State.PATROL:
				Patrol ();
				Debug.Log("I am in state: " + state);
				break;
			case State.CHASE:
				Chase ();
				Debug.Log("I am in state: " + state);
				break;
			case State.ATTACK:
				Attack ();
				Debug.Log("I am in state: " + state);
				break;
			case State.SPAWN:				
				ReleaseMinions ();
				yield return new WaitForSeconds(1);
				Debug.Log("I am in state: " + state);
				break;
			case State.DEATH:
				Death ();
				yield return new WaitForSeconds(5);
				RemoveTripod();
				Debug.Log("I am in state: " + state);
				break;
			}
			yield return null;
		}
	}

	// Main Idle Method
	public void Idle()
	{
		// We shouldn't have to do anything here except check to see if we should be on patrol.
		if(patrolling.amIOnPatrol)
		{
			state = TripodAI.State.PATROL;
			components.myAnim.SetBool("shouldPatrol", true);
		}
	}

	// Main Patrolling Method
	public void Patrol()
	{
		components.agent.speed = patrolling.patrolSpeed;
		if (Vector3.Distance (this.transform.position, patrolling.waypoints[patrolling.waypointInd].transform.position) >= patrolling.maxWaypointDistance)
		{
			components.agent.SetDestination(patrolling.waypoints[patrolling.waypointInd].transform.position);
			components.mycontroller.Move (components.agent.desiredVelocity);
		}
		else if (Vector3.Distance (this.transform.position, patrolling.waypoints[patrolling.waypointInd].transform.position) <= patrolling.maxWaypointDistance)
		{
			patrolling.waypointInd = Random.Range(0,patrolling.waypoints.Length);
		}
		else
		{
			components.mycontroller.Move(Vector3.zero);
		}
		if(components.mySight.playerSighted)
		{
			patrolling.amIOnPatrol = false;
			components.myAnim.SetBool("shouldPatrol", false);
			state = TripodAI.State.CHASE;
		}
	}

	// Main Chase Method
	public void Chase()
	{
		components.agent.speed = chasing.chaseSpeed;
		if(Vector3.Distance(this.transform.position,components.mySight.player.position) < chasing.minDistance)
		{
			state = TripodAI.State.ATTACK;
		}
		if(Vector3.Distance(this.transform.position,components.mySight.player.position) >= chasing.minDistance)
		{
			components.agent.SetDestination(components.mySight.player.position);
			components.mycontroller.Move(components.agent.desiredVelocity);
		}
		if(Vector3.Distance(this.transform.position,components.mySight.player.position) > chasing.maxDistance)		
		{
			state = TripodAI.State.IDLE;
		}
		Debug.Log("Chase Method Called");
	}

	// Main Attack Method
	public void Attack()
	{
		Debug.Log("Attack Method Called");
		components.myAnim.SetBool("shouldPatrol", false);
		components.myAnim.SetBool("shouldFire", true);
		attacking.timer += Time.deltaTime;
		if (attacking.timer > attacking.waitShot) {
			GameObject bulletClone = Instantiate(attacking.bullet, attacking.shotPos.position, attacking.shotPos.rotation) as GameObject;
			attacking.timer = 0;
		}
	}

	// Method used to actually fire "bullets" 
	public void Fire()
	{
		
	}

	public void FireTwo()
	{
		
	}

	public void Death()
	{
		if(patrolling.amIOnPatrol)
		{
			patrolling.amIOnPatrol = false;
		}
		components.myAnim.SetBool("dead", true);
		foreach(GameObject go in components.mySight.lightbeams)
		{
			Destroy(go);
		}
		components.mycontroller.enabled = false;
		components.myData.enabled = false;
		components.mySight.enabled = false;
	}

	public void RemoveTripod()
	{
		Instantiate(components.myExplosion,this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			state = TripodAI.State.SPAWN;
		}
		else if(coll.gameObject.tag == "PlayerBullet")
		{
			components.mySight.playerSighted = true;
		}			
	}

	void ReleaseMinions()
	{
		components.myAnim.SetBool("shouldPatrol", false);
		spawning.minionsSpawned += 1;
		if(spawning.minionsSpawned <= spawning.maxMinions)
		{
			Instantiate(spawning.minion, spawning.spawnLocation.position, this.transform.rotation);
		}
		else
		{
			state = TripodAI.State.IDLE;
		}
	}

	// Update is called once per frame
	void Update() {
		if(!components.myData.alive)
		{
			state = TripodAI.State.DEATH;
		}
		if(components.mySight.playerSighted)
		{
			state = TripodAI.State.CHASE;
		}
	}
}