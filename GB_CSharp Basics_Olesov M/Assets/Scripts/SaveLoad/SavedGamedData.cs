using System;
using System.Collections.Generic;

namespace BallGame
{
    [Serializable]
    public class SavedGamedData : SavedData
    {
        public PlayerSavedData Player;
        public List<ObjectSavedData> Objects;
        public GameStatsInfo Stats;
    }
}