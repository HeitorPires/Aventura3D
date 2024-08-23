using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;


    public Animator animator;

    [Header("Atalhos")]
    public KeyCode jumpKeyCode = KeyCode.Space;

    private float _vSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
                _vSpeed = jumpSpeed;
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);

        
    }
}
