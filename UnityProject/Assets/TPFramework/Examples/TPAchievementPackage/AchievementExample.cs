using TPFramework.Unity;
using UnityEngine;

public class AchievementExample : MonoBehaviour
{
    [SerializeField] private TPAchievement Achievement;

    // Use this for initialization
    private void Start()
    {
        Achievement.OnCompleted += () => {
            Debug.Log("Achievement completed!");
        };

        TPAchievementSystem.OnNotifyActivation = CustomNotifyActive;

        for (int i = 0; i < Achievement.Data.ReachPoints; i++)
        {
            Achievement.AddPoints();
        }
    }

    private void CustomNotifyActive(float evaluatedTime, Transform notify)
    {
        notify.localScale = notify.localScale.Set(evaluatedTime);
    }
}

