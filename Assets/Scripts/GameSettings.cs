using System;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }


    [Serializable]
    public class DifficultyParams
    {
        public Difficulty Difficulty;
        public int PathWidth;
        public float PlayerSpeed;
    }

    public DifficultyParams[] Params = new DifficultyParams[3];
}