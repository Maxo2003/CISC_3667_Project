using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] private int score;
    private const int DefaultRepeats = 4;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI sceneTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private int level;

    // Start is called before the first frame update
    private void Start()
    {
        if (!scoreTxt) {
                scoreTxt = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        }

        if (!sceneTxt) {
                sceneTxt = GameObject.Find("SceneTxt").GetComponent<TextMeshProUGUI>();
        }
        if (!nameTxt){
            nameTxt = GameObject.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        }

        score = PersistentData.Instance.GetScore();
        level = SceneManager.GetActiveScene().buildIndex;
        DisplayScore();
        DisplayName();
        PersistentData.Instance.GetPlayButton().SetActive(false);

    }

    public void AddPoints(int increases = DefaultRepeats) {
        var multiplier = 1;
        if (PersistentData.Instance.GetDifficulty() == 3) { multiplier = 2;}

        score += increases switch
        {
            0 => (5 * multiplier),
            1 => (3 * multiplier),
            2 => (2 * multiplier),
            3 => (1 * multiplier),
            _ => 0
        };

        DisplayScore();
        PersistentData.Instance.SetScore(score);
    }

    private void DisplayScore(){
        scoreTxt.text = "Score: " + score;
        sceneTxt.text = "Scene: " + (level-1);
    }

    public void ChangeScene(){
        SceneManager.LoadScene(level + 1);
 //       Buttons.Instance.ToggleSettings();
        PersistentData.Instance.InitializePersistentData();
      //  Buttons.Instance.InitializeButtons();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(level);
    }

    private void DisplayName(){
        nameTxt.text = "Welcome, " + PersistentData.Instance.GetName() + "!";
    }
    
}
