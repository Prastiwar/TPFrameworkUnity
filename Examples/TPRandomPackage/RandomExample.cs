using System.Collections;
using TP.Framework;
using UnityEngine;

public class RandomExample : MonoBehaviour
{
    private ProbabilityElementInt<GameObject>[] probabilityElements;

    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private int repeatCount = 5;
    
    // Use this for initialization
    private void Start()
    {
        int elLength = gameObjects.Length;
        probabilityElements = new ProbabilityElementInt<GameObject>[elLength];
        int[] randomProbabilities = TPRandom.RandomProbabilities(elLength, 20, 70);

        ExampleHelper.DrawLine();
        for (int i = 0; i < elLength; i++)
        {
            probabilityElements[i] = new ProbabilityElementInt<GameObject>(
                Instantiate(gameObjects[i], TP.Framework.Unity.RandomSystem.InsideUnitSquare() * 5, Quaternion.identity),
                randomProbabilities[i]);
            Debug.Log("Random probability of object: " + probabilityElements[i].Probability);
        }
        ExampleHelper.DrawLine();

        StartCoroutine(TPRandomToggleObject(repeatCount));
    }

    private IEnumerator TPRandomToggleObject(int repeat)
    {
        while (repeat >= 0)
        {
            GameObject selectedObject = TPRandom.PickObjectWithProbability(probabilityElements);
            selectedObject.SetActive(!selectedObject.activeSelf);
            repeat--;
            yield return ExampleHelper.WaitSecond;
        }
    }
}
