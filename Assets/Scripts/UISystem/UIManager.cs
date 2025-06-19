using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// UI管理器，负责主菜单、提示、按钮等界面逻辑
public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] public GameObject settingsPanel; // 设置面板

    [Header("UI Texts")]
    [SerializeField] public TextMeshProUGUI titleText; // 标题文本
    [SerializeField] public TextMeshProUGUI startPrompt; // 开始提示文本
    [SerializeField] public TextMeshProUGUI controlsInfo; // 控制信息文本

    [Header("UI Buttons")]
    [SerializeField] public Button exitButton; // 退出按钮

    private bool gameStarted = false;
    private float blinkTimer = 0f; // 闪烁计时器
    private bool isVisible = true; // 当前文本可见状态
    private const float blinkInterval = 0.5f; // 闪烁间隔时间

    void Start()
    {
        Time.timeScale = 0f; // 暂停游戏
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (titleText != null) titleText.gameObject.SetActive(true);
        if (startPrompt != null)
        {
            startPrompt.gameObject.SetActive(true);
            startPrompt.alpha = 1f;
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);
            EventSystem.current.SetSelectedGameObject(exitButton.gameObject);
        }
    }

    void Update()
    {
        if (!gameStarted)
        {
            UpdateBlinkEffect(); // 更新闪烁效果
            if (Input.anyKeyDown && !IsMouseClick())
            {
                StartGame(); // 检测任意键开始游戏
            }
        }
    }

    // 更新开始提示文本的闪烁效果
    void UpdateBlinkEffect()
    {
        if (startPrompt != null)
        {
            blinkTimer += Time.unscaledDeltaTime;
            if (blinkTimer >= blinkInterval)
            {
                blinkTimer = 0f;
                isVisible = !isVisible;
                startPrompt.alpha = isVisible ? 1f : 0f;
            }
        }
    }

    // 判断是否为鼠标点击
    private bool IsMouseClick()
    {
        return Input.GetMouseButtonDown(0) ||
               Input.GetMouseButtonDown(1) ||
               Input.GetMouseButtonDown(2);
    }

    // 开始游戏，隐藏菜单，恢复时间
    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (titleText != null) titleText.gameObject.SetActive(false);
        if (startPrompt != null)
        {
            startPrompt.gameObject.SetActive(false);
            startPrompt.alpha = 1f;
        }
        if (controlsInfo != null) controlsInfo.gameObject.SetActive(true);
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        EnablePlayerInput(); // 启用玩家输入
        Debug.Log("游戏已开始！");
    }

    // 启用玩家输入
    void EnablePlayerInput()
    {
        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }

    // 退出按钮点击事件
    void OnExitButtonClicked()
    {
        Debug.Log("退出按钮被点击！");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}