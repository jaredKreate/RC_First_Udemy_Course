using UnityEngine;
using System.Collections;

public class PlayerAnimatorController : MonoBehaviour {

    Animator anim;
    PlayerController controller;

    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
    }
    
    void Update()
    {
        anim.SetFloat("Forward", controller.ForwardInput);
        anim.SetFloat("AbsForward", Mathf.Abs(controller.ForwardInput));
        anim.SetFloat("Turn", Mathf.Abs(controller.TurnInput));
        anim.SetFloat("Strafe", controller.StrafeInput);
        anim.SetFloat("AbsStrafe", Mathf.Abs(controller.StrafeInput));
        anim.SetFloat("Walk", controller.WalkInput);
        anim.SetBool("Grounded", controller.Grounded());
        anim.SetBool("Alive", PlayerData.Instance.alive);
    }
}
