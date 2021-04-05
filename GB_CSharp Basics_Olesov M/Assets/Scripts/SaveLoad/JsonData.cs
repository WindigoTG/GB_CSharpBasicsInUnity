using System.IO;
using UnityEngine;

namespace BallGame
{
    public class JsonData<T> : IData<T>
    {
        public void Save(T data, string path = null)
        {
            var str = JsonUtility.ToJson(data);
            File.WriteAllText(path, Crypto.CryptoXOR(str));
        }

        public T Load(string path = null)
        {
            var data = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(Crypto.CryptoXOR(data));
        }
    }
}
