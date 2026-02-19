using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class анна : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(LoadGame);
        }
    }
    void LoadGame()
    {
        SceneManager.LoadScene("Анна");
    }
}
