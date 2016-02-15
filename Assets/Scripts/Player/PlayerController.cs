using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//Movement parameters
	public float forwardSpeed = 7;
	public float strafeSpeed = 7;
	public float turnSpeed = 12;
	public float jumpSpeed = 10;
	public float gravity = 0.25f;
	public float distanceToGrounded = 0.52f;
	public Transform capsuleTop, capsuleBottom;
    public float capsuleRadius;
	public LayerMask groundLayer;

	//Helper parameters
	Vector3 velocity = Vector3.zero;
	Rigidbody rBody;
	Quaternion targetRotation;

	//Input parameters
	float forward, turn, strafe, jump, walk;

    public float ForwardInput { get { return forward; } }
    public float TurnInput { get { return turn; } }
    public float StrafeInput { get { return strafe; } }
    public float WalkInput { get { return walk; } }

	public bool Grounded()
	{
        if (Physics.CheckCapsule(capsuleTop.position, capsuleBottom.position, capsuleRadius, groundLayer))
            return true;
        return false;
	}

	public Quaternion GetRotation()
	{
		return transform.rotation;
	}

	void Start()
	{
		rBody = GetComponent<Rigidbody>();
	}

	void GetInput()
	{
		forward = Input.GetAxis("Vertical");
		turn = Input.GetAxis("Mouse X");
		strafe = Input.GetAxis("Horizontal");
		jump = Input.GetAxis("Jump");
        walk = Input.GetAxis("Walk");
	}
	
	public void UpdateController()
	{
		GetInput();
		Run();
		Strafe();
		Turn();
		Jump();

		rBody.velocity = transform.TransformDirection(velocity);
	}

	void Run()
	{
		if (Mathf.Abs(forward) > 0.1f)
		{
			velocity.z = forward * forwardSpeed * (walk > 0 ? 0.2f : 1);
		}
		else
		{
			velocity.z = Mathf.Lerp(velocity.z, 0, 5 * Time.deltaTime);
		}
	}

	///<summary>
	///Similar to Move, but manipulating the 'x' axis of velocity with 'strafeSpeed'
	///</summary>
	void Strafe()
	{
		if (Mathf.Abs(strafe) > 0.1f)
		{
			velocity.x = strafe * strafeSpeed * (walk > 0 ? 0.2f : 1);
		}
		else
		{
			velocity.x = Mathf.Lerp(velocity.x, 0, 5 * Time.deltaTime);
		}
	}


	void Turn()
	{
		targetRotation = Quaternion.AngleAxis(turn * turnSpeed * Time.deltaTime, Vector3.up);
		transform.rotation *= targetRotation;
	}

	void Jump()
	{
		if (jump > 0)
		{
			if (Grounded())
				velocity.y = jumpSpeed;
			else
				velocity.y -= gravity;
		}
		else
		{
			if (!Grounded())
				velocity.y -= gravity;
			else
				velocity.y = 0;
		}
	}
}
