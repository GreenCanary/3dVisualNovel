using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private static DoNotDestroy _instance;

    private void Awake()
    {
        // Если экземпляр уже существует — уничтожаем текущий объект
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Устанавливаем текущий объект как единственный экземпляр
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
