namespace BallGame
{
    public sealed class SavedData<T> where T : struct
    {
        public int ScoreCount;
        public T PlayerID = default;
    }
}