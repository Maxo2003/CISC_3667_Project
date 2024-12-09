using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PersistentData : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] int playerScore;
    private float volume;
    private int difficulty;

    public static PersistentData Instance;

    private void Awake(){
        if (Instance == null){
            DontDestroyOnLoad(this);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = "";
        playerScore = 0;
        volume = 0f;
        difficulty = 0;
    }

    public void SetScore(int s){
        playerScore = s;
    }

    public void SetName(string n){
        playerName = n;
    }

    public void SetDifficulty(){

    }

    public void SetVolume(){

    }

    public string GetName(){
        return playerName;
    }

    public int GetScore(){
        return playerScore;
    }

    public int GetDifficulty(){
        return difficulty;
    }

    public float GetVolume(){
        return volume;
    }
}
