using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MysteryEventManager : MonoBehaviour
{
    public static MysteryEventManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject eventPanel;          // root panel (inactive by default)
    [SerializeField] private TMP_Text descriptionText;       // paragraph text
    [SerializeField] private Image eventImage;               // artwork
    [SerializeField] private Transform optionsContainer;     // has VerticalLayoutGroup + ContentSizeFitter
    [SerializeField] private GameObject optionButtonPrefab;  // UI Button (UGUI) prefab with TMP child
    [SerializeField] private Button continueButton;          // hidden until an option is chosen
    [SerializeField] private TMP_Text resultText;            // optional; if null we reuse descriptionText

    [Header("Layout")]
    [SerializeField] private float optionRowHeight = 48f;    // explicit row height

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if (eventPanel) eventPanel.SetActive(false);
        if (continueButton) continueButton.gameObject.SetActive(false);
        if (resultText) resultText.gameObject.SetActive(false);
    }

    // Entry point from your trigger
    public void ShowEvent(MysteryEventData data)
    {
        if (!data) { Debug.LogWarning("ShowEvent called with NULL data."); return; }

        eventPanel.SetActive(true);

        // Fill
        if (descriptionText)
        {
            descriptionText.gameObject.SetActive(true);
            descriptionText.text = data.description ?? string.Empty;
        }
        if (resultText) resultText.gameObject.SetActive(false);
        if (eventImage) eventImage.sprite = data.eventImage;

        // Reset actions
        continueButton.gameObject.SetActive(false);
        ClearOptions();

        int count = (data.options != null) ? data.options.Count : 0;
        Debug.Log($"ShowEvent: desc len={descriptionText.text.Length}");
        Debug.Log($"Refs -> panel:{eventPanel} desc:{descriptionText} image:{eventImage} options:{optionsContainer}");
        Debug.Log($"Spawning {count} option(s)…");

        for (int i = 0; i < count; i++)
            CreateOptionButton(data.options[i], i);

        // Force layout to recalc after children exist
        var crt = optionsContainer as RectTransform;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(crt);
        Debug.Log($"OptionsContainer now has {crt.childCount} child(ren), width={crt.rect.width}");

        // No options? Just show Continue to close.
        if (count == 0)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(CloseEvent);
        }
    }

    private void CreateOptionButton(EventOption opt, int index)
    {
        if (!optionButtonPrefab || !optionsContainer)
        {
            Debug.LogWarning("Option spawn failed: missing prefab or container.");
            return;
        }

        var go = Instantiate(optionButtonPrefab, optionsContainer);
        go.name = $"Option_{index}_{opt.optionText}";

        // === Force Rect to stretch horizontally and have explicit height ===
        var rt = go.GetComponent<RectTransform>();
        if (rt != null)
        {
            // Stretch across container width; align by layout group vertically
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(1f, 0f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.sizeDelta = new Vector2(0f, optionRowHeight);   // definitive height
            rt.anchoredPosition = Vector2.zero;
            rt.localScale = Vector3.one;
        }

        // Ensure layout element cooperates with container
        var le = go.GetComponent<LayoutElement>() ?? go.AddComponent<LayoutElement>();
        le.preferredHeight = optionRowHeight;
        le.flexibleWidth = 1f;   // allow width expansion
        le.flexibleHeight = 0f;

        // Label text
        var label = go.GetComponentInChildren<TMP_Text>(true);
        if (label)
        {
            label.enableWordWrapping = true;
            label.richText = true;
            label.color = Color.white;
            label.text = opt.optionText ?? string.Empty;

            // Fill button with padding
            var lrt = label.rectTransform;
            lrt.anchorMin = Vector2.zero;
            lrt.anchorMax = Vector2.one;
            lrt.offsetMin = new Vector2(12f, 6f);
            lrt.offsetMax = new Vector2(-12f, -6f);
        }
        else
        {
            Debug.LogWarning($"Option '{go.name}' has no TMP_Text child.");
        }

        // Click handler
        var btn = go.GetComponent<Button>();
        if (btn)
        {
            btn.onClick.RemoveAllListeners();
            var captured = opt;
            btn.onClick.AddListener(() => OnOptionClicked(captured));
        }

        Debug.Log($"Spawned option {index}: '{opt.optionText}'");
    }

    private void OnOptionClicked(EventOption option)
    {
        ApplyOptionEffects(option);

        // Hide options
        ClearOptions();

        // Swap to result text (or reuse description)
        if (resultText)
        {
            descriptionText.gameObject.SetActive(false);
            resultText.gameObject.SetActive(true);
            resultText.text = option.resultText ?? string.Empty;
        }
        else
        {
            descriptionText.text = option.resultText ?? string.Empty;
        }

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(CloseEvent);
    }

    private void ApplyOptionEffects(EventOption option)
    {
        // Hook to your game systems here
        if (option.healthChange != 0) Debug.Log($"[Effect] Health += {option.healthChange}");
        if (option.goldChange != 0) Debug.Log($"[Effect] Gold   += {option.goldChange}");
        // e.g. PlayerStats.Instance.ModifyHealth(option.healthChange);
        //      PlayerStats.Instance.ModifyGold(option.goldChange);
    }

    private void ClearOptions()
    {
        if (!optionsContainer) return;
        for (int i = optionsContainer.childCount - 1; i >= 0; i--)
            Destroy(optionsContainer.GetChild(i).gameObject);
    }

    public void CloseEvent()
    {
        ClearOptions();
        continueButton.gameObject.SetActive(false);
        if (resultText) { resultText.gameObject.SetActive(false); }
        if (descriptionText) { descriptionText.gameObject.SetActive(true); }
        eventPanel.SetActive(false);
    }
}
