using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour//, IDamageable
{

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;


    public Animator animator;

    public HealthBase healthBase;

    [Header("Run Setup")]
    public float speedRun = 1.5f;

    [Header("Atalhos")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    private float _vSpeed = 0f;

    [Header("Flash")]
    public List<FlashColor> flashColors;


    private void Awake()
    {
        OnValidate();
        healthBase.onDamage += Damage;
    }

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

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

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(runKeyCode))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }

            else
                animator.speed = 1f;
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);

        
    }

    public void Damage(HealthBase healthBase)
    {
        flashColors.ForEach(i => i.Flash());
    }

    public void Damage(float damage, Vector3 dir)
    {
        
    }

}



