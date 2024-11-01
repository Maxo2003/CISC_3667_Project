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
    public TMP_Text scoreTxt;
    [SerializeField] TMP_Text sceneTxt;
    [SerializeField] int level;

    // Start is called before the first frame update
    void Start()
    {
        if (scoreTxt == null)
            {
                scoreTxt = GameObject.Find("ScoreTxt").GetComponent<TMP_Text>();
            }
        if (sceneTxt == null)
            {
                sceneTxt = GameObject.Find("SceneTxt").GetComponent<TMP_Text>();
            }
        DisplayScore();
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

    public void AddPoints(int increases)
    {
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
        Debug.Log("score: " + score);
        DisplayScore();
    }

    public void DisplayScore(){
        scoreTxt.text = "Score: " + score;
        sceneTxt.text = "Scene: " + SceneManager.GetActiveScene().buildIndex;
    }

//     public void DisplayScene(){
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//     }

    public void changeScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
