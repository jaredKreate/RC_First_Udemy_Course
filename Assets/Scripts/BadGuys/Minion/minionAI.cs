using UnityEngine;
using System.Collections;

public class minionAI : MonoBehaviour {

	[System.Serializable]
	public class Components
	{
		// This variable will contain our nav mesh agent which stores a lot of our movement data
		public NavMeshAgent agent;
		// This variable will store a reference to our animator
		public Animator myAnim;
		// This variable will store a reference to our sight script
		public minionSight mySight;
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
		CHASE,
		ATTACK,
		SPAWN,
		DEATH
	}

	// We need this variable in order to change the current state of the AI
	public State state;
	// We need this variable to indicate whether the AI unit is currently alive or not
	private bool alive;

	[System.Serializable]
	public class Idling
	{
		public float idleSpeed;
		public float changeInterval;
		public float maxdirectionChange;
		public Vector3 waypoint = new Vector3(0,0,0);
		public float range;
		private Vector3 baseVector3 = new Vector3(1,0,1);
		public float timer;
		public float heading;
		public float maxHeadingChange;
		public Vector3 targetRotation;
		public float idlingSpeed;
	}

	public Idling idling;

	[System.Serializable]
	public class Chasing
	{
		public bool chase;
		public float chaseSpeed;
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
		public Transform[] spawnLocations;
		public int minionsSpawned;
	}

	void Awake()
	{
		components.agent = GetComponent<NavMeshAgent>();
		components.myAnim = GetComponent<Animator>();
		if (components.mySight == null)
		{
			components.mySight = GetComponentInChildren<minionSight>();
		}
		components.mycontroller = GetComponent<CharacterController>();
		components.myData = GetComponent<EnemyData>();
		alive = true;
		state = minionAI.State.IDLE;
		StartCoroutine("FSM");
	}

	IEnumerator FSM() {
		while(alive) {
			switch(state) {
			case State.IDLE:
				Debug.Log("I am in state IDLE");
				Idle();
				break;
			case State.CHASE:
				Debug.Log("I am in state ATTACK");
				Chase();
				break;
			case State.ATTACK:
				Debug.Log("I am in state ATTACK");
				Attack();
				break;
			case State.DEATH:
				Debug.Log("I am in state ATTACK");
				Death();
				break;
			}
			yield return null;
		}
	}

	public void Idle()
	{
		if(!components.mySight.playerSighted)
		{
			StartCoroutine(NewHeading());
			transform.position += transform.forward * idling.idlingSpeed * Time.deltaTime;
			if (components.mySight.playerSighted) {
				state = minionAI.State.CHASE;
			}
		}
	}

	IEnumerator NewHeading ()
	{
		idling.timer += Time.deltaTime;
		if (idling.timer > idling.changeInterval) {			
			idling.waypoint = UnityEngine.Random.insideUnitSphere * 5;
			idling.waypoint = new Vector3(idling.waypoint.x, 0 , idling.waypoint.z);
			transform.LookAt(idling.waypoint);
			idling.timer = 0;
			yield return null;
		}
	}

	void NewHeadingRoutine ()
	{
		var floor = Mathf.Clamp(idling.heading - idling.maxHeadingChange, 0, 360);
		var ceil  = Mathf.Clamp(idling.heading + idling.maxHeadingChange, 0, 360);
		idling.heading = UnityEngine.Random.Range(floor, ceil);
		idling.targetRotation = new Vector3(0, idling.heading, 0);
	}

	public void Chase()
	{
		
	}

	public void Attack()
	{
		
	}

	public void Death()
	{
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "Player");
		{
			components.mySight.playerSighted = true;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
