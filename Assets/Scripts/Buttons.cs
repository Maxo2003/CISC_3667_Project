using UnityEngine;
using UnityEngine.SceneManagement;
//
using static UnityEngine.GameObject;

public class Buttons : MonoBehaviour
{
    private static float _volume = 0.35f;
    public int difficulty;
   // public static Buttons Instance;
    // Start is called before the first frame update
    private void Start()
    {
        InitializeButtons();
    }

    public void InitializeButtons()
    {
        if (!PersistentData.Instance.GetPauseButton())
        {
            PersistentData.Instance.SetPauseButton(Find("Pause"));
        }

        if (!PersistentData.Instance.GetPlayButton())
        {
            PersistentData.Instance.SetPlayButton(Find("Resume"));
        }
        
        if (PersistentData.Instance.GetInitialized()) return;
        AudioListener.volume = _volume;
        PersistentData.Instance.SetVolume(_volume);

        //volumeSlider.value = volume; 
        PersistentData.Instance.GetVolumeSlider().onValueChanged.AddListener(delegate {ChangeVolume();});

        PersistentData.Instance.GetDifficultyDropdown().onValueChanged.AddListener(delegate { DifficultySelected(); });
        difficulty = PersistentData.Instance.GetDifficultyDropdown().value;

        if (PersistentData.Instance.GetIsSettingsOpen())
        {
            foreach (var g in PersistentData.Instance.GetSettingsElements())
            {
                g.SetActive(false);
            }

            PersistentData.Instance.SetIsSettingsOpen(false);
        }
        PersistentData.Instance.GetPlayerNameInput().onEndEdit.AddListener(delegate { SavePlayerName(); });
        PersistentData.Instance.SetInitialized(true);
        
        if (PersistentData.Instance.GetPlayButton() == null) return;
        PersistentData.Instance.GetPlayButton().SetActive(false);
    }

    public void StartGame(){
        SceneManager.LoadScene("Scene0");
    }

    public void Instructions(){
        SceneManager.LoadScene("Instructions");
        if (!PersistentData.Instance.GetPlayerNameInput().gameObject.activeSelf) return;
        PersistentData.Instance.GetPlayerNameInput().gameObject.SetActive(false);
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
        PersistentData.Instance.SetScore(0);
        if (PersistentData.Instance.GetNameSaved() || PersistentData.Instance.GetPlayerNameInput().gameObject.activeInHierarchy)
        {
            return;
        }
        PersistentData.Instance.GetPlayerNameInput().gameObject.SetActive(true);
    }
    public void SettingsMainMenu()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu")) return;
        SceneManager.LoadScene("MainMenu");
        PersistentData.Instance.SetScore(0);
    }
    public void ChangeVolume(){
        var v = PersistentData.Instance.GetVolumeSlider().value;
        AudioListener.volume = v;
        PersistentData.Instance.SetVolume(v);
    }
    
     public void ToggleSettings(){
         if (!PersistentData.Instance.GetIsSettingsOpen())
         {
             foreach (var g in PersistentData.Instance.GetSettingsElements())
             {
                 g.SetActive(true);
             }
         }
         else
         {
             foreach (var g in PersistentData.Instance.GetSettingsElements())
             {
                 g.SetActive(false);
             }
         }
         PersistentData.Instance.SetIsSettingsOpen(!PersistentData.Instance.GetIsSettingsOpen());
     }
     
    public void CloseSettings(){
         foreach(var g in PersistentData.Instance.GetSettingsElements()){
            g.SetActive(false);
         }
    }


    public void DifficultySelected(){
        difficulty = PersistentData.Instance.GetDifficultyDropdown().value;
        PersistentData.Instance.SetDifficulty(difficulty);
    }

    public void Pause(){
        Time.timeScale = 0.0f;

        PersistentData.Instance.GetPauseButton().gameObject.SetActive(false);
        PersistentData.Instance.GetPlayButton().gameObject.SetActive(true);
    }

    public void Resume(){
        Time.timeScale = 1.0f;

        PersistentData.Instance.GetPauseButton().SetActive(true);
        PersistentData.Instance.GetPlayButton().SetActive(false);
    }

    public void SavePlayerName(){
        var s = PersistentData.Instance.GetPlayerNameInput().text;
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return;
        PersistentData.Instance.SetName(s);
        PersistentData.Instance.GetPlayerNameInput().gameObject.SetActive(false);
        PersistentData.Instance.SetNameSaved(true);
    }



}
