using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileParent;
    private GameObject[] _pooledProjectiles;

    public int projectilesAmountToPool;



    private void Start()
    {
        ObjectPooler pooler = ObjectPooler.Instance;

        if (pooler != null )
        {
            _pooledProjectiles = SetUpPooledProjectilesArray(pooler);
        }
    }


    
    private GameObject[] SetUpPooledProjectilesArray(ObjectPooler pooler)
    {
        GameObject[] pooledProjectiles = pooler.SetPooledObjectsArray(projectilesAmountToPool, _projectile, _projectileParent);

        if (pooledProjectiles.Length > 0 )
        {
            foreach (GameObject projectile in pooledProjectiles)
            {
                Vector3 startPosition = _projectileParent.position;
                projectile.transform.position = startPosition;
            }
        }

        return pooledProjectiles;
    }

    public GameObject GetInactiveProjectile()
    {
        for (int i = 0; i < _pooledProjectiles.Length; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
            {
                return _pooledProjectiles[i];
            }
        }

        return null;
    }
}