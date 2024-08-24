using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{

    public class EnemyBase : MonoBehaviour
    {
        public float startLife = 10;

        [SerializeField]private float _currentLife;

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
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
            Destroy(gameObject);
        }

        public void OnDamage(float f)
        {
            _currentLife -= f;
            if( _currentLife <= 0 )
                OnKill();
        }

        #region debug
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        #endregion

    }
}
