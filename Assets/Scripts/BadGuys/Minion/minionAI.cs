﻿using UnityEngine;
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
		components.mySight = GetComponent<minionSight>();
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
				Debug.Log("I am in state Chase");
				Chase();
				break;
			case State.ATTACK:
				Debug.Log("I am in state ATTACK");
				Attack();
				break;
			case State.DEATH:
				Debug.Log("I am Dead");
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
			components.mycontroller.SimpleMove(transform.forward * idling.idleSpeed);
			if (components.mySight.playerSighted) {
				state = minionAI.State.CHASE;
			}
		}
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

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player");
		{
			components.mySight.playerSighted = true;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
