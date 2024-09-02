using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using Cloth;
using System.Runtime.CompilerServices;

public class Player : Singleton<Player>
{

    public List<Collider> colliders;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;


    public Animator animator;

    [Header("Life")]
    public HealthBase healthBase;

    [Header("Run Setup")]
    public float speedRun = 1.5f;

    [Header("Atalhos")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Space]
    [SerializeField] private ClothChanger _clothChanger;

    private float _vSpeed = 0f;
    private bool _alive = true;
    private bool _isJumping = false;
    public bool canMove = true;
    public ClothType CurrentCloth { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.onDamage += Damage;
        healthBase.onKill += OnKill;
        CurrentCloth = ClothType.NONE;
        if(SaveManager.Instance._saveSetup.useSave)
            Invoke(nameof(SetLoadHealth), .1f);
    }



    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (_alive)
            {

                transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

                var inputAxisVertical = Input.GetAxis("Vertical");
                var speedVector = transform.forward * inputAxisVertical * speed;

                if (characterController.isGrounded)
                {
                    if (_isJumping)
                    {
                        _isJumping = false;
                        animator.SetTrigger("Land");
                    }

                    _vSpeed = 0;

                    if (Input.GetKeyDown(jumpKeyCode))
                    {
                        _vSpeed = jumpSpeed;
                        if (!_isJumping)
                        {
                            animator.SetTrigger("Jump");
                            _isJumping = true;
                        }
                    }

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
        }

        else animator.SetBool("Run", false);
    }

    public void Damage(HealthBase healthBase)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
    }

    private void SetLoadHealth()
    {
        float targetHealth = healthBase.startLife - SaveManager.Instance._saveSetup.currentHealth ;
        healthBase.Damage(targetHealth);
    }

    private void OnKill(HealthBase healthBase)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 3f);
        }
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);

    }

    public void Revive()
    {
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
        _alive = true;
    }

    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckPoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }
    IEnumerator ChangeSpeedCoroutine(float speed, float duration)
    {
        var defaultSpeed = this.speed;
        this.speed = speed;
        yield return new WaitForSeconds(duration);
        this.speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
        CurrentCloth = ClothType.NONE;
    }

}



