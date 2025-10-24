using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MysteryEventManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject eventPanel;
    [SerializeField] private TMP_Text eventTitle;
    [SerializeField] private TMP_Text eventDescription;
    [SerializeField] private Image eventImage;
    [SerializeField] private Transform optionsContainer;
    [SerializeField] private GameObject optionButtonPrefab;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button continueButton;

    private MysteryEventData currentEvent;

    private void Start()
    {
        eventPanel.SetActive(false);
        resultText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    public void ShowEvent(MysteryEventData data)
    {
        currentEvent = data;
        eventPanel.SetActive(true);

        eventTitle.text = data.eventName;
        eventDescription.text = data.description;
        eventImage.sprite = data.eventImage;

        ClearOptions();
        foreach (var option in data.options)
        {
            var button = Instantiate(optionButtonPrefab, optionsContainer);
            button.GetComponentInChildren<TMP_Text>().text = option.optionText;
            button.GetComponent<Button>().onClick.AddListener(() => SelectOption(option));
        }
    }

    private void SelectOption(EventOption option)
    {
        // Apply effects
        ApplyOptionEffects(option);

        // Show result text
        eventDescription.text = option.resultText;

        // Hide old buttons
        ClearOptions();

        if (option.endsEvent)
        {
            // End the event immediately
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(CloseEvent);
        }
        else
        {
            // Wait for player to hit Continue (for multi-step chains later)
            resultText.gameObject.SetActive(true);
            resultText.text = option.resultText;
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(CloseEvent);
        }
    }

    private void ApplyOptionEffects(EventOption option)
    {
        // Plug in your player data or stat manager here
        Debug.Log($"Health change: {option.healthChange}, Gold change: {option.goldChange}");
    }

    private void ClearOptions()
    {
        foreach (Transform child in optionsContainer)
            Destroy(child.gameObject);
    }

    public void CloseEvent()
    {
        eventPanel.SetActive(false);
    }
}
