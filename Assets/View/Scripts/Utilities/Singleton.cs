using UnityEngine;

namespace CardGame.View.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = (T)FindObjectOfType(typeof(T));

                return _instance;
            }
        }
    }
}