using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
//     [SerializedField] bool isPaused = false;
//     [SerializedField] bool isResumed = false;
//
    [SerializeField] GameObject[] pauseMode;
    [SerializeField] GameObject[] playMode;
    [SerializeField] GameObject[] settings;

    // Start is called before the first frame update
    void Start()
    {
//         pauseMode = GameObject.FindGameObjectsWithTag("ShowInPauseMode");
//         playMode = GameObject.FindGameObjectsWithTag("ShowInPlayMode");
//         openSettings = GameObject.FindGameObjectsWithTag("GameSettings");
//
//         foreach(GameObject g in openSettings)
//             g.SetActive(false);
//
//         foreach (GameObject g in pauseMode)
//             g.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame(){
        SceneManager.LoadScene("Scene0");
    }

    public void instructions(){
        SceneManager.LoadScene("Instructions");
    }

    public void mainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

//      public void openSettings(){
//          foreach(GameObject g in openSettings)
//             g.SetActive(true);
//      }

//     public void pause(){
//     Time.timeScale = 0.0f;
//
//     foreach(GameObject g in pauseMode)
//         g.SetActive(true);
//
//     foreach(GameObject g in playMode)
//         g.SetActive(false);
//     }

//     public void resume(){
//         Time.timeScale = 1.0f;
//
//         foreach (GameObject g in pauseMode)
//             g.SetActive(false);
//
//         foreach (GameObject g in playMode)
//             g.SetActive(true);
//     }

//     public void closeSettings(){
//         foreach(GameObject g in openSettings)
//             g.SetActive(false);
//     }
}
