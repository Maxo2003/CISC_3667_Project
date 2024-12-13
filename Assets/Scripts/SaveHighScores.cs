using TMPro;
using UnityEngine;

public class SaveHighScores : MonoBehaviour
{
    private const int NumHighScores = 5;
    private const string NameKey = "HighScoreName";
    private const string ScoreKey = "HighScore";

    [SerializeField] private string playerName;
    [SerializeField] private int playerScore;

    [SerializeField] private TextMeshProUGUI[] nameTexts;
    [SerializeField] private TextMeshProUGUI[] scoreTexts;


    // Start is called before the first frame update
    private void Start()
    {
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();
        
        SaveScore();
        DisplayHighScores();
    }

    // Update is called once per frame

    private void SaveScore()
    {
        for (var i = 0; i < NumHighScores; i++)
        {
            var currentNameKey = NameKey + i;
            var currentScoreKey = ScoreKey + i;

            {
                if (PlayerPrefs.HasKey(currentScoreKey))
                {
                    var currentScore = PlayerPrefs.GetInt(currentScoreKey);
                    if (playerScore <= currentScore) continue;
                    //handle this case
                    var tempName = PlayerPrefs.GetString(currentNameKey);

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);

                    playerScore = currentScore;
                    playerName = tempName;

                }
                else
                {
                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);
                    return;
                }
            }
        }
    }

    private void DisplayHighScores()
    {
        for (var i = 0; i < NumHighScores; i++)
        {
            
            nameTexts[i].text = PlayerPrefs.GetString(NameKey+i);
            scoreTexts[i].text = PlayerPrefs.GetInt(ScoreKey+i).ToString();
        }
    }

}
