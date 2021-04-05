using System;
namespace BallGame
{
    [Serializable]
    public struct GameStatsInfo
    {
        public int NecessaryBonusCount;
        public int ScoreCount;
        public int GameTime;

        public GameStatsInfo(int necessaryBonusCount, int scoreCount, int gameTime)
        {
            NecessaryBonusCount = necessaryBonusCount;
            ScoreCount = scoreCount;
            GameTime = gameTime;
        }
    }
}