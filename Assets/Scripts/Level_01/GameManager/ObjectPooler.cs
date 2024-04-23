using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;



    private void Awake()
    {
        Instance = this;
    }



    public GameObject[] SetPooledObjectsArray(int amountToPool, GameObject prefab, Transform poolParent)
    {
        GameObject[] pooledObjects = new GameObject[amountToPool];

        for (int i = 0; i < pooledObjects.Length; i++)
        {
            GameObject objectToPool = Instantiate(prefab, poolParent);
            objectToPool.SetActive(false);

            pooledObjects[i] = objectToPool;
        }
        
        return pooledObjects;
    }
}