using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{

/*
    [SerializeField] bool isPaused = false;
    [SerializeField] bool isResumed = false;
*/
    [SerializeField] TMP_InputField playerNameInput;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject[] settingsElements;
    [SerializeField] Slider volumeSlider;
    public static float volume = 0.5f;
    [SerializeField] TMP_Dropdown difficultyDropdown;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
         if(settings == null){
            settings = GameObject.FindGameObjectWithTag("GameSettings");
         }

         if(settingsElements == null || settingsElements.Length == 0){
            settingsElements = GameObject.FindGameObjectsWithTag("SettingsElements");
         }

         if(volumeSlider == null){
            volumeSlider = settings.GetComponentInChildren<Slider>();
         }

         //volumeSlider.value = volume;
         volumeSlider.onValueChanged.AddListener(delegate {ChangeVolume(volumeSlider.value); });

         if(pauseButton == null){
            pauseButton = GameObject.Find("Pause");
         }

         if(playButton == null){
            playButton = GameObject.Find("Resume");
         }

        if(difficultyDropdown == null){
            difficultyDropdown = settings.GetComponentInChildren<TMP_Dropdown>();
        }

        difficultyDropdown.onValueChanged.AddListener(delegate {DifficultySelected();});
        difficulty = difficultyDropdown.value;

        //pauseButton.SetActive(false);

        Debug.Log("Input var empty?");
        Debug.Log(playerNameInput == null);

        foreach(GameObject g in settingsElements){
            g.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

//     public void GetSettingsElements(){
//         return settingsElements;
//     }

    public void StartGame(){
        SceneManager.LoadScene("Scene0");
    }

    public void Instructions(){
        SceneManager.LoadScene("Instructions");
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeVolume(float v){
        volume = v;
        AudioListener.volume = v;
    }


     public void OpenSettings(){
         Debug.Log($"Found {settingsElements.Length} objects for SettingsElements for OpenSettings method");
         int counter = 0;
         foreach(GameObject g in settingsElements){
            Debug.Log(counter++);
            g.SetActive(true);
         }
         Debug.Log(counter + " items set active");
     }
    public void CloseSettings(){
         Debug.Log("CloseSettings called");

         int counter = 0;
         foreach(GameObject g in settingsElements){
            counter++;
            g.SetActive(false);
         }
         Debug.Log(counter + " items set inactive");

    }


    public void DifficultySelected(){
        Debug.Log(difficultyDropdown.value);
    }

    public void Pause(){
        Time.timeScale = 0.0f;

        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    public void Resume(){
        Time.timeScale = 1.0f;

        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }

    public void SavePlayerName(){
        Debug.Log(playerNameInput == null);
        string s = playerNameInput.text;
        PersistentData.Instance.SetName(s);
        playerNameInput.gameObject.SetActive(false);
    }



}
