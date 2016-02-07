using UnityEngine;
using System.Collections;

public class FPSCamera : MonoBehaviour {

	//Movement parameters
	public Vector3 offsetFromTarget = Vector3.zero;
	public float followSmooth = 10f;
	public float lookSmooth = 5f;
	public float lookUpMax = -60;
	public float lookDownMax = 80;
	public float lookSpeed = 250;

	//Helper parameters
	public PlayerController controller;
	Vector3 rotation;
	Vector3 lookVel;
	Vector3 targetPos;

	//Input parameters
	float vertical; //input for looking up and down;

    bool cursorToggle = false;

	void Start()
	{
		Cursor.visible = false;
		rotation = controller.GetRotation().ToEuler();
		targetPos = controller.transform.position;
	}

	void GetInput()
	{
		vertical = Input.GetAxis("Mouse Y");
        if (Input.GetKeyDown(KeyCode.T))
        {
            cursorToggle = !cursorToggle;
            Cursor.visible = cursorToggle;
        }
	}

	void FixedUpdate()
	{
		GetInput();
		controller.UpdateController();
		MoveToTarget();
		Look();
	}

	void Look()
	{
		rotation.x -= vertical * lookSpeed * Time.deltaTime;
		rotation.x = Mathf.Clamp(rotation.x, lookDownMax, lookUpMax);
		transform.rotation = Quaternion.Slerp(transform.rotation, 
											  controller.GetRotation() * Quaternion.Euler(rotation),
											  lookSmooth * Time.fixedDeltaTime);
	}

	void MoveToTarget()
	{
		targetPos = controller.transform.position +
					transform.TransformDirection(Vector3.right * offsetFromTarget.x) + //relative x positioning
					transform.TransformDirection(Vector3.forward * offsetFromTarget.z) + //relative z positioning
					Vector3.up * offsetFromTarget.y;

		transform.position = Vector3.Lerp(transform.position, targetPos, followSmooth * Time.fixedDeltaTime);
	}
}
