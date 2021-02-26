/**
*   Authored by Tomasz Piowczyk
*   License: https://github.com/Prastiwar/TPFrameworkUnity/blob/master/LICENSE
*   Repository: https://github.com/Prastiwar/TPFrameworkUnity 
*/

using System;
using UnityEngine;

namespace TP.Framework.Unity
{
    [Serializable]
    public struct AchievementModel : IAchievementData
    {
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private float points;
        [SerializeField] private float reachPoints;
        [SerializeField] private bool isCompleted;

        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public float Points { get { return points; } set { points = value; } }
        public float ReachPoints { get { return reachPoints; } set { reachPoints = value; } }
        public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
    }
}
