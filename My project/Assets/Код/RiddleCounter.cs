using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class RiddleCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    [Header("Victory Settings")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TextMeshProUGUI victoryText;
    [SerializeField] private UnityEngine.UI.Button menuButton;
    [SerializeField] private string mainMenuScene = "Menu";

    [Header("Exit Gates (Optional)")]
    [SerializeField] private GameObject exitRightGate;
    [SerializeField] private GameObject exitLeftGate;
    [SerializeField] private Vector3 rightGateTargetRotation = new Vector3(0, 95.73f, 0);
    [SerializeField] private Vector3 leftGateTargetRotation = new Vector3(0, -101f, 0);

    private int riddlesSolved = 0;
    private int totalRiddles = 6;
    private bool gameWon = false;

    public static RiddleCounter Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCounterDisplay();

        // Hide victory panel at start
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        // Setup menu button
        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMainMenu);

        // Lock cursor for FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SolveRiddle()
    {
        if (riddlesSolved < totalRiddles && !gameWon)
        {
            riddlesSolved++;
            UpdateCounterDisplay();

            // Check if all 6 are captured
            if (riddlesSolved == totalRiddles)
            {
                Debug.Log("All 6 people captured! Showing victory screen...");
                ShowVictoryScreen();
            }
        }
    }

    void ShowVictoryScreen()
    {
        gameWon = true;

        // Rotate gates if they exist
        if (exitRightGate != null)
            exitRightGate.transform.eulerAngles = rightGateTargetRotation;
        if (exitLeftGate != null)
            exitLeftGate.transform.eulerAngles = leftGateTargetRotation;

        // Show victory panel
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            // Create emergency victory UI if none assigned
            CreateEmergencyVictoryUI();
        }

        // Set victory text
        if (victoryText != null)
        {
            victoryText.text = "Победа!\nСпасибо за игру!";
        }

        // Unlock cursor for menu button
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause game
        Time.timeScale = 0f;

        // Update counter text
        if (counterText != null)
        {
            counterText.text = "ПОБЕДА! Все пойманы!";
            counterText.color = Color.green;
        }

        Debug.Log("🏆 VICTORY SCREEN SHOWN! 🏆");
    }

    void CreateEmergencyVictoryUI()
    {
        // Create canvas
        GameObject canvasObj = new GameObject("VictoryCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();

        // Create dark background panel
        GameObject panel = new GameObject("Panel");
        panel.transform.parent = canvasObj.transform;
        UnityEngine.UI.Image panelImage = panel.AddComponent<UnityEngine.UI.Image>();
        panelImage.color = new Color(0, 0, 0, 0.9f);

        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create victory text
        GameObject textObj = new GameObject("VictoryText");
        textObj.transform.parent = panel.transform;
        victoryText = textObj.AddComponent<TextMeshProUGUI>();
        victoryText.text = "Победа!\nСпасибо за игру!";
        victoryText.fontSize = 60;
        victoryText.alignment = TextAlignmentOptions.Center;
        victoryText.color = Color.yellow;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0.6f);
        textRect.anchorMax = new Vector2(1, 0.8f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Create menu button
        GameObject buttonObj = new GameObject("MenuButton");
        buttonObj.transform.parent = panel.transform;
        menuButton = buttonObj.AddComponent<UnityEngine.UI.Button>();
        UnityEngine.UI.Image buttonImage = buttonObj.AddComponent<UnityEngine.UI.Image>();
        buttonImage.color = new Color(0.2f, 0.2f, 0.2f);

        // Button text
        GameObject buttonTextObj = new GameObject("Text");
        buttonTextObj.transform.parent = buttonObj.transform;
        TextMeshProUGUI buttonText = buttonTextObj.AddComponent<TextMeshProUGUI>();
        buttonText.text = "Перейти в меню";
        buttonText.fontSize = 40;
        buttonText.alignment = TextAlignmentOptions.Center;
        buttonText.color = Color.white;

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.4f, 0.3f);
        buttonRect.anchorMax = new Vector2(0.6f, 0.4f);
        buttonRect.offsetMin = Vector2.zero;
        buttonRect.offsetMax = Vector2.zero;

        RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.offsetMin = Vector2.zero;
        buttonTextRect.offsetMax = Vector2.zero;

        menuButton.onClick.AddListener(GoToMainMenu);

        victoryPanel = panel;

        Debug.Log("Emergency victory UI created!");
    }

    void GoToMainMenu()
    {
        Debug.Log("Going to main menu...");
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(mainMenuScene))
        {
            SceneManager.LoadScene(mainMenuScene);
        }
        else
        {
            Debug.Log("No menu scene, quitting game...");
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    public void UpdateCounterDisplay()
    {
        if (counterText != null)
        {
            counterText.text = $"Поймано {riddlesSolved}  из {totalRiddles} людей";
        }
    }

    public bool AreAllRiddlesSolved()
    {
        return riddlesSolved >= totalRiddles;
    }

    public int GetRiddlesSolved()
    {
        return riddlesSolved;
    }

    public int GetTotalRiddles()
    {
        return totalRiddles;
    }
}