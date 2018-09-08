using System.Collections;
using TP.Framework.Unity;
using UnityEngine;

public class ObjectPoolExample : MonoBehaviour
{
    private TPGameObjectPool gameObjectPool;
    private bool isRunning;

    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolCount = 10;
    [SerializeField] private bool run = true;

    // Use this for initialization
    private void Awake()
    {
        gameObjectPool = new TPGameObjectPool(prefab, poolCount);
        gameObjectPool.Grow(poolCount);
    }

    private void Update()
    {
        if (run && !isRunning)
        {
            StartCoroutine(TPObjectPoolSpawnObjects(gameObjectPool));
        }
    }

    private IEnumerator TPObjectPoolSpawnObjects(TPUnityPool<GameObject> pool)
    {
        isRunning = true;
        GameObject prevObj = pool.Get();
        prevObj.transform.position = TPRandom.InsideUnitSquare() * 5;
        while (run)
        {
            yield return ExampleHelper.WaitSecond;
            pool.Push(prevObj);

            prevObj = pool.Get();
            prevObj.transform.position = TPRandom.InsideUnitSquare() * 5;
            prevObj.SetActive(true);
        }
        isRunning = false;
    }
}
