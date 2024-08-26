using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{

    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public ParticleSystem ParticleSystem;
        public FlashColor flashColor;

        public bool lookAtPlayer = false;

        public float startLife = 10;

        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Animation")]
        [SerializeField]private AnimationBase _animationBase;

        [SerializeField]private float _currentLife;

        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        public virtual void Update()
        {
            if (lookAtPlayer) transform.LookAt(_player.transform.position);
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithBornAnimation) BornAnimation();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Kill()
        {
            OnKill();
        }

        protected virtual void OnKill()
        {
            if(collider != null) collider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (ParticleSystem != null) ParticleSystem.Emit(15);
            _currentLife -= f;
            if( _currentLife <= 0 )
                OnKill();
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }


        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).From().SetEase(startAnimationEase);
        }

        private void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }




        #endregion

    }
}
