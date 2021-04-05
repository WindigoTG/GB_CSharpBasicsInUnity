using System;
using UnityEngine;

namespace BallGame
{
    [Serializable]
    public class StatsSavedData : SavedData
    {
        public GameStatsInfo GameStats;
    }
}