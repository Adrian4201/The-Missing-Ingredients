using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MysteryEventManager : MonoBehaviour
{
    public static MysteryEventManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject eventPanel;        // root panel (inactive by default)
    [SerializeField] private TMP_Text descriptionText;     // Description (main text)
    [SerializeField] private Image eventImage;             // artwork
    [SerializeField] private Transform optionsContainer;   // OptionsContainer (vertical layout)
    [SerializeField] private GameObject optionButtonPrefab;// prefab with Button + TMP_Text child
    [SerializeField] private TMP_Text resultText;          // ResultText (hidden initially)
    [SerializeField] private Button continueButton;        // ContButton (hidden initially)

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        Instance = this;
    }

    private void Start()
    {
        eventPanel.SetActive(false);
        resultText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    // show the event popup
    public void ShowEvent(MysteryEventData data)
    {
        ClearOptions();
        eventPanel.SetActive(true);

        descriptionText.text = data.description;
        eventImage.sprite = data.eventImage;

        // spawn option buttons
        foreach (var opt in data.options)
        {
            var btnObj = Instantiate(optionButtonPrefab, optionsContainer);
            var btn = btnObj.GetComponent<Button>();
            var label = btnObj.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = opt.optionText;

            // cache local copy for closure
            var localOpt = opt;
            btn.onClick.AddListener(() => OnOptionClicked(localOpt));
        }

        resultText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }

    private void OnOptionClicked(EventOption option)
    {
        // apply effects (hook into your PlayerStats or GameManager)
        ApplyOptionEffects(option);

        // hide all option buttons
        foreach (Transform t in optionsContainer) Destroy(t.gameObject);

        // show the result text and continue button
        descriptionText.text = option.resultText; // replace main description with result text
        resultText.gameObject.SetActive(false);   // not needed separately if using description
        continueButton.gameObject.SetActive(true);

        // wire continue: if endsEvent close, otherwise you could chain
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            if (option.endsEvent) CloseEvent();
            else CloseEvent(); // you can replace this to chain into another event
        });
    }

    private void ApplyOptionEffects(EventOption option)
    {
        // Example: call your player code
       // if (option.healthChange != 0)
            //PlayerStats.Instance.ModifyHealth(option.healthChange);

        //if (option.goldChange != 0)
            //PlayerStats.Instance.ModifyGold(option.goldChange);

        // Add more effect handling here
    }

    private void ClearOptions()
    {
        foreach (Transform t in optionsContainer)
            Destroy(t.gameObject);
    }

    public void CloseEvent()
    {
        ClearOptions();
        eventPanel.SetActive(false);
        resultText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }
}
