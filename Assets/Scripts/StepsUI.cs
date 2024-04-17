using TMPro;
using UnityEngine;

public class StepsUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI stepsText;
    public int stepsRecord = 0;

    [SerializeField]
    TextMeshProUGUI stepsEndgameText;
    [SerializeField]
    public TextMeshProUGUI newRecordEndgameText;
    public bool activateMedal = false;

    public GameObject medalSprite;


    void Start()
    {

        PlayerBehaviour.instance.steps = PlayerPrefs.GetInt("Score", 0);
        stepsRecord = PlayerPrefs.GetInt("Record", 0);

        UpdateStepText();
    }

    void Update()
    {

        PlayerPrefs.SetInt("Steps", PlayerBehaviour.instance.steps);
        PlayerPrefs.Save();

        if (PlayerBehaviour.instance.steps > stepsRecord)
        {
            stepsRecord = PlayerBehaviour.instance.steps;
            activateMedal = true;
            PlayerPrefs.SetInt("Record", stepsRecord);
            PlayerPrefs.Save();
        }

        UpdateStepText();

        if (PlayerBehaviour.instance.needLoseCanvas == true)
        {
            PlayerBehaviour.instance.ShowLoseScreenUI();
            stepsEndgameText.text = "Score: " + PlayerBehaviour.instance.steps.ToString() + "\nRecord: " + stepsRecord.ToString();
        }
    }
    private void UpdateStepText()
    {
        stepsText.text = "Score: " + PlayerBehaviour.instance.steps.ToString() + "\nRecord: " + stepsRecord.ToString();
    }
}