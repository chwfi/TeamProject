using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; set; }

    protected virtual void Awake()
    {
        Instance = FindObjectOfType(typeof(T)) as T;
        DontDestroyOnLoad(Instance);
    }
}