using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public abstract class ObjectPool<T> : MonoBehaviour
    where T : MonoBehaviour
    {
        [SerializeField] protected T objectToPool;
        [SerializeField] protected int poolSize = 50;
        [SerializeField] protected int amountToPool;

        public static ObjectPool<T> SharedInstance;

        protected List<T> pooledObjects = new();

        private void Awake()
        {
            SharedInstance = this;
        }

        protected virtual void Start()
        {
            T tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.gameObject.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public virtual T GetPooledObject()
        {
            foreach (T obj in pooledObjects)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    return obj;
                }
            }

            if (pooledObjects.Count < poolSize)
            {
                T tmp = Instantiate(objectToPool);
                pooledObjects.Add(tmp);
                return tmp;
            }

            return null;
        }

    }

}