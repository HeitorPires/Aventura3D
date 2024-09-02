using UnityEngine;

namespace Core.Singleton
{

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
                Instance = GetComponent<T>();
            else
                Destroy(gameObject);
        }
    }

    public class SingletonPersistent<T> : Singleton<T> where T: MonoBehaviour
    {
        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }
}

