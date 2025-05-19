using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        // Eğer sahnede bu scriptten başka bir tane varsa yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Yeni gelen kopyayı sil
            return;
        }

        Instance = this as T;
        ChildAwake();
    }

    protected virtual void ChildAwake() { }

    protected virtual void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}