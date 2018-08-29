using System.Collections;
using TPFramework.Unity;
using UnityEngine;

public class ObjectPoolExample : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolCount = 10;
    [SerializeField] private float last = 20;

    // Use this for initialization
    private void Start()
    {
        TPUnityPool<GameObject> gameObjectPool = new TPUnityPool<GameObject>(prefab, poolCount);
        gameObjectPool.Grow(poolCount);
        StartCoroutine(TPObjectPoolSpawnObjects(gameObjectPool, last));
    }

    private IEnumerator TPObjectPoolSpawnObjects(TPUnityPool<GameObject> pool, float last)
    {
        while (last >= 0)
        {
            GameObject obj = pool.Get();
            obj.transform.position = TPRandom.InsideUnitSquare() * 5;
            obj.SetActive(true);
            last--;
            yield return ExampleHelper.WaitSecond;
        }
    }
}
