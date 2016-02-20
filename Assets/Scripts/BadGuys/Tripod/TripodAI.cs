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
		public EnemySight mySight;
		// This variable will store a reference to our Character Controller
		public CharacterController mycontroller;	
	}

	public Components components;

	// This state represents the states that the AI character can actively be in
	public enum State {
		IDLE,
		PATROL,
		CHASE,
		ATTACK,
		EVADE,
		GETAMMO,
		GETHEALTH,
		DEATH
	}

	// We need this variable in order to change the current state of the AI
	public State state;
	// We need this variable to indicate whether the AI unit is currently alive or not
	private bool alive;

	// We will need these if the AI needs to be patrolling
	[System.Serializable]
	public class Patrolling
	{		
		public bool amIOnPatrol;
		public GameObject[] waypoints;
		public int waypointInd;
		public float patrolSpeed = 0.5f;
	}

	public Patrolling patrolling;

	// Use this for initialization
	void Awake () {
		components.agent = GetComponent<NavMeshAgent>();
		components.myAnim = GetComponent<Animator>();
		if (components.mySight == null)
		{
			components.mySight = GetComponentInChildren<EnemySight>();
		}
		components.mycontroller = GetComponent<CharacterController>();
//		patrolling.waypointInd = Random.Range(0,patrolling.waypoints.Length);
		patrolling.waypointInd = 3;
		alive = true;
		state = TripodAI.State.IDLE;
		StartCoroutine("FSM");
	}

	// This is our Finite State machine which will run continuously until the AI character has been eliminated
	IEnumerator FSM()
	{
		while (alive)
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
			case State.EVADE:
				Evade ();
				Debug.Log("I am in state: " + state);
				break;
			case State.GETAMMO:
				GetAmmo ();
				Debug.Log("I am in state: " + state);
				break;
			case State.GETHEALTH:
				GetHealth ();
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
		if (Vector3.Distance (this.transform.position, patrolling.waypoints[patrolling.waypointInd].transform.position) >= 2)
		{
			components.agent.SetDestination(patrolling.waypoints[patrolling.waypointInd].transform.position);
			components.mycontroller.Move (components.agent.desiredVelocity);
			Debug.Log("Distance: " + Vector3.Distance(this.transform.position, patrolling.waypoints[patrolling.waypointInd].transform.position));
		}
		else if (Vector3.Distance (this.transform.position, patrolling.waypoints[patrolling.waypointInd].transform.position) <= 2)
		{
			patrolling.waypointInd = Random.Range(0,patrolling.waypoints.Length);
		}
		else
		{
			components.mycontroller.Move(Vector3.zero);
		}
	}

	// Main Chase Method
	public void Chase()
	{

	}

	// Main Attack Method
	public void Attack()
	{

	}

	// Main Evade Method
	public void Evade()
	{

	}

	// Main GetAmmo Method
	public void GetAmmo()
	{

	}

	// Main GetHealth Method
	public void GetHealth()
	{

	}

	// Update is called once per frame
	void Update() {

	}
}
