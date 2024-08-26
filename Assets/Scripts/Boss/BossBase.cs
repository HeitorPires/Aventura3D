using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;
using DG.Tweening;
using NaughtyAttributes.Test;
using System;
using Animation;


namespace Boss
{
    
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;
        

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBwtweenAttacks = .5f;

        public HealthBase healthBase;

        public float speed = 5f;
        public List<Transform> waypoints;

        private StateMachine<BossAction> stateMachine;
        [SerializeField]private AnimationBase _animationBase;

        private void Awake()
        {
            Init();
            healthBase.onKill += OnBossKill;
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction> ();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchState(BossAction.INIT);
        }

        public void OnBossKill(HealthBase healthBase)
        {
            SwitchState(BossAction.DEATH);
        }

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchDeath()
        {
            healthBase.Damage(healthBase.startLife);
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCorountine(endCallback));
        }

        IEnumerator AttackCorountine(Action endCallback)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                transform.DOScale(1.2f, .2f).SetLoops(2, LoopType.Yoyo);
                attacks++;
                yield return new WaitForSeconds(timeBwtweenAttacks);
            }
            endCallback?.Invoke();

        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f){
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).From().SetEase(startAnimationEase);
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchStates(state, this);
        }
        
        

        #endregion

    }
}
