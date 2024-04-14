using TMPro;
using UnityEngine;

public class StepsUI : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;

    [SerializeField]
    TextMeshProUGUI stepsText;
    private int stepsRecord = 0;

    [SerializeField]
    TextMeshProUGUI stepsEndgameText;

    [SerializeField]
    CanvasGroup canvasGroupLoseScreen;


    void Start()
    {

        playerBehaviour.steps = PlayerPrefs.GetInt("Score", 0);
        stepsRecord = PlayerPrefs.GetInt("Record", 0);

        UpdateStepText();
    }

    void Update()
    {

        PlayerPrefs.SetInt("Steps", playerBehaviour.steps);
        PlayerPrefs.Save();

        if (playerBehaviour.steps > stepsRecord)
        {
            stepsRecord = playerBehaviour.steps;
            PlayerPrefs.SetInt("Record", stepsRecord);
            PlayerPrefs.Save();
        }

        UpdateStepText();

        if (playerBehaviour.needLoseCanvas == true)
        {
            playerBehaviour.ShowLoseScreenUI();
            stepsEndgameText.text = "Score: " + playerBehaviour.steps.ToString() + "\nRecord: " + stepsRecord.ToString();
        }
    }
    private void UpdateStepText()
    {
        stepsText.text = "Score: " + playerBehaviour.steps.ToString() + "\nRecord: " + stepsRecord.ToString();
    }
}
