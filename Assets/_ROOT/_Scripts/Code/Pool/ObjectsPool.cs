using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class ObjectsPool<T> : MonoBehaviour
    where T : MonoBehaviour
    {
        [SerializeField] protected List<T> objectsToPool;
        [SerializeField] protected int poolSizePerObject = 50;
        [SerializeField] protected int amountToPoolPerObject;

        public static ObjectsPool<T> SharedInstance;

        protected List<T> pooledObjects = new();

        private void Awake()
        {
            SharedInstance = this;
            /* if (SharedInstance != null && SharedInstance != this)
            {
                Destroy(this);
                return;
            } */
        }

        protected virtual void Start()
        {
            T tmp;
            foreach (T objectToPool in objectsToPool)
            {
                for (int i = 0; i < amountToPoolPerObject; i++)
                {
                    tmp = Instantiate(objectToPool);
                    tmp.gameObject.SetActive(false);
                    pooledObjects.Add(tmp);
                }
            }
        }

        public virtual T GetPooledObject()
        {
            List<T> auxList = new(pooledObjects);
            while (auxList.Count > 0)
            {
                int itemIndex = Random.Range(0, auxList.Count);
                T obj = auxList[itemIndex];
                if (!obj.gameObject.activeInHierarchy)
                {
                    return obj;
                }
                auxList.RemoveAt(itemIndex);
            }

            if (pooledObjects.Count > poolSizePerObject)
            {
                int itemIndex = Random.Range(0, pooledObjects.Count);
                T objectToPool = pooledObjects[itemIndex];
                T tmp = Instantiate(objectToPool);
                pooledObjects.Add(tmp);
                return tmp;
            }

            return null;
        }

    }
}