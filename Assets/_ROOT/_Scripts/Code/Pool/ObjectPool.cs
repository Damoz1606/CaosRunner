using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public abstract class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int poolSize = 50;
        [SerializeField] private int amountToPool;

        public static ObjectPool SharedInstance;

        private List<GameObject> pooledObjects = new();

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            GameObject tmp;
            for (int i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            foreach (GameObject obj in pooledObjects)
            {
                if (!obj.activeInHierarchy)
                {
                    return obj;
                }
            }

            if (pooledObjects.Count > poolSize)
            {
                GameObject tmp = Instantiate(objectToPool);
                pooledObjects.Add(tmp);
                return tmp;
            }

            return null;
        }

    }

}