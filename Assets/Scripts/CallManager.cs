
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CallManager : MonoBehaviour
{
    [Header("Call Data")]
    public CallData callData;

    [Header("UI References")]
    public TMP_Text callerText;
    public TMP_Text messageText;
    public TMP_Text scoreText;

    public Button[] optionButtons;

    public GameObject feedbackPanel;
    public TMP_Text feedbackText;
    public Button replayButton;

    private int currentNodeIndex;
    private int score;
    private string lastFeedback;

    private void Start()
    {
        replayButton.onClick.AddListener(StartCall);
        StartCall();
    }

    public void StartCall()
    {
        score = 0;
        lastFeedback = "";
        currentNodeIndex = callData.startingNodeIndex;

        feedbackPanel.SetActive(false);

        callerText.text = callData.callerName;
        UpdateScore();
        ShowNode();
    }

    private void ShowNode()
    {
        DialogueNode node = callData.nodes[currentNodeIndex];
        messageText.text = node.message;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            Button button = optionButtons[i];

            button.onClick.RemoveAllListeners();

            if (i < node.options.Count)
            {
                DialogueOption option = node.options[i];

                button.gameObject.SetActive(true);

                TMP_Text label =
                    button.GetComponentInChildren<TMP_Text>();

                label.text = option.optionText;

                button.onClick.AddListener(
                    () => ChooseOption(option));
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    private void ChooseOption(DialogueOption option)
    {
        score += option.scoreChange;
        UpdateScore();

        if (!string.IsNullOrWhiteSpace(option.feedback))
        {
            lastFeedback = option.feedback;
        }

        if (option.endsCall || option.nextNodeIndex < 0)
        {
            EndCall();
            return;
        }

        currentNodeIndex = option.nextNodeIndex;
        ShowNode();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    private void EndCall()
    {
        foreach (Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }

        messageText.text = "Call ended.";

        feedbackPanel.SetActive(true);

        feedbackText.text =
            lastFeedback + "\n\nFinal score: " + score;
    }
}
