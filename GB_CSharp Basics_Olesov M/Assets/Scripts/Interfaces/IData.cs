namespace BallGame
{
    public interface IData<T>
    {
        void Save(T playerData, string path = null);
        T Load(string path = null);
    }

}