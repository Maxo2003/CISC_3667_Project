using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Scorekeeper : MonoBehaviour
{
    [SerializeField] int score = 0;
    const int DEFAULT_REPEATS = 4;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI sceneTxt;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreTxt == null) {
                scoreTxt = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
        }

        if (sceneTxt == null) {
                sceneTxt = GameObject.Find("SceneTxt").GetComponent<TextMeshProUGUI>();
        }
        if (nameTxt ==null){
            nameTxt = GameObject.Find("NameTxt").GetComponent<TextMeshProUGUI>();
        }

        score = PersistentData.Instance.GetScore();
        level = SceneManager.GetActiveScene().buildIndex;
        DisplayScore();
        DisplayName();
      //  DisplayScene();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoints()
    {
        AddPoints(DEFAULT_REPEATS);
    }

    public void AddPoints(int increases) {
        if(increases == 0){
            score += 5;
        } else if(increases == 1){
            score += 3;
        } else if (increases == 2){
            score += 2;
        } else if (increases == 3) {
            score += 1;
        } else {
            score += 0;
        }
        DisplayScore();
        PersistentData.Instance.SetScore(score);
    }

    public void DisplayScore(){
        scoreTxt.text = "Score: " + score;
        sceneTxt.text = "Scene: " + (level-1);
    }

    public void changeScene(){
        SceneManager.LoadScene(level + 1);
    }

    private void DisplayName(){
        nameTxt.text = "Welcome, " + PersistentData.Instance.GetName() + "!";
    }

}
