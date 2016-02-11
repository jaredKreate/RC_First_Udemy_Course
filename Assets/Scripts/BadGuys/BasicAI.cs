using UnityEngine;
using System.Collections;

public class BasicAI : MonoBehaviour {

	// This variable will contain our nav mesh agent which stores a lot of our movement data
	public NavMeshAgent agent;

	// This state represents the states that the AI character can actively be in
	public enum State {
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

	// We will need these when the AI needs to be patrolling
	public GameObject[] waypoints;
	private int waypointInd;
	public float patrolSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	// This is our Finite State machine which will run continuously
	IEnumerator FSM()
	{
		while (alive)
		{
			switch (state)
			{
			case State.PATROL:
				Patrol ();
				break;
			case State.CHASE:
				Chase ();
				break;
			case State.ATTACK:
				Attack ();
				break;
			case State.EVADE:
				Evade ();
				break;
			case State.GETAMMO:
				GetAmmo ();
				break;
			case State.GETHEALTH:
				GetHealth ();
				break;
			}
			yield return null;
		}
	}

	public void Patrol()
	{
		
	}

	public void Chase()
	{
		
	}

	public void Attack()
	{
		
	}

	public void Evade()
	{
		
	}

	public void GetAmmo()
	{
		
	}

	public void GetHealth()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
