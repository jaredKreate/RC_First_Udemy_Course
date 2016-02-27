using UnityEngine;
using System.Collections;

public class bipedAI : MonoBehaviour {

	[System.Serializable]
	public class Components
	{
		// This variable will contain our nav mesh agent which stores a lot of our movement data
		public NavMeshAgent agent;
		// This variable will store a reference to our animator
		public Animator myAnim;
		// This variable will store a reference to our sight script
		public bipedSight mySight;
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
		public Vector3 rotDir;
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
		components.mySight = GetComponent<bipedSight>();
		components.mycontroller = GetComponent<CharacterController>();
		components.myData = GetComponent<EnemyData>();
		alive = true;
		state = bipedAI.State.IDLE;
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
				yield return new WaitForSeconds(3);
				RemoveMinion();
				break;
			}
			yield return null;
		}
	}

	public void Idle()
	{
		if(!components.mySight.playerSighted)
		{			
			components.myAnim.SetBool("walk", true);
			components.mycontroller.SimpleMove(transform.forward * idling.idleSpeed);
		}
		else {
			state = bipedAI.State.CHASE;
		}
	}

	public void Chase()
	{		
		components.agent.speed = chasing.chaseSpeed * Time.deltaTime;
		if(Vector3.Distance(components.mySight.player.position,this.transform.position) >= chasing.maxDistance)
		{
			components.agent.SetDestination(components.mySight.player.position);
			components.mycontroller.Move(components.agent.desiredVelocity);
		}
		else{
			state = bipedAI.State.ATTACK;
		}
	}

	public void Attack()
	{
		components.myAnim.SetBool("walk", false);
		components.myAnim.SetBool("attack", true);
		chasing.rotDir = components.mySight.player.position - transform.position;
		chasing.rotDir.Normalize();
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(chasing.rotDir), chasing.chaseSpeed * Time.deltaTime);
		if(Vector3.Distance(components.mySight.player.position, this.transform.position) >= chasing.maxDistance)
		{
			components.myAnim.SetBool("attack", false);
			components.myAnim.SetBool("walk", true);
			state = bipedAI.State.CHASE;
		}
	}

	public void Death()
	{
		components.myAnim.enabled = false;
		components.myAnim.SetBool("walk", false);
		components.myAnim.SetBool("attack", false);
		//		components.myLook.enabled = false;
		components.mycontroller.enabled = false;
		components.myData.enabled = false;
		components.mySight.enabled = false;
	}

	public void RemoveMinion()
	{
		Instantiate(components.myExplosion,this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			components.mySight.playerSighted = true;
		}
	}
	// Update is called once per frame
	void Update () {
		if(!components.myData.alive)
		{
			state = bipedAI.State.DEATH;
		}
	}
}
