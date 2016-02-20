﻿using UnityEngine;
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
	}

	public Components components;

	// This state represents the states that the AI character can actively be in
	public enum State {
		IDLE,
		PATROL,
		CHASE,
		ATTACK,
		EVADE,
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

	// Use this for initialization
	void Awake () {
		components.agent = GetComponent<NavMeshAgent>();
		components.myAnim = GetComponent<Animator>();
		if (components.mySight == null)
		{
			components.mySight = GetComponentInChildren<TripodSight>();
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
			state = TripodAI.State.CHASE;
		}
	}

	// Main Chase Method
	public void Chase()
	{

	}

	// Main Attack Method
	public void Attack()
	{
		InvokeRepeating("Fire",2,120);
	}

	public void Fire()
	{
		attacking.timer += Time.deltaTime;
		if (attacking.timer > attacking.waitShot) {
			GameObject bulletClone = Instantiate(attacking.bullet, attacking.shotPos.position, attacking.shotPos.rotation) as GameObject;
			attacking.timer = 0;
		}
	}

	// Main Evade Method
	public void Evade()
	{

	}

	// Update is called once per frame
	void Update() {

	}
}
