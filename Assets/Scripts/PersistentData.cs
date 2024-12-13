using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PersistentData : MonoBehaviour
{
    [SerializeField] private  GameObject pauseButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private string playerName;
    [SerializeField] private int playerScore;
    private bool _isSettingsOpen = true;
    private bool _initialized;
    private static bool _nameSaved;
    [SerializeField] private GameObject gameSettings;
    [SerializeField] private GameObject[] settingsElements;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown difficultyDropdown;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private GameObject canvas;
    private float _volume;
    private int _difficulty;
    

    public static PersistentData Instance;

    private void Awake(){
        if (!Instance){
            DontDestroyOnLoad(this);
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        playerName = "";
        playerScore = 0;
        _volume = 0f;
        _difficulty = 0;
        InitializePersistentData();
    }

    public void InitializePersistentData()
    {
        if (!pauseButton)
        {
            pauseButton = GameObject.Find("Pause");
        }

        if (!playButton)
        {
            playButton = GameObject.Find("Resume");
        }
        
        if (!canvas)
        {
            canvas = GameObject.FindGameObjectWithTag("SettingsCanvas");
        }

        if(!gameSettings){
            gameSettings = GameObject.FindGameObjectWithTag("GameSettings");
        }
        
        if(!volumeSlider){
            volumeSlider = gameSettings.GetComponentInChildren<Slider>();
        }
        
        if(!difficultyDropdown){
            difficultyDropdown = gameSettings.GetComponentInChildren<TMP_Dropdown>();
        }        
        
        if (!playerNameInput)
        {
            playerNameInput = gameSettings.GetComponentInChildren<TMP_InputField>();
        }

        if (settingsElements.Length != 0) return;
        Buttons b = GameObject.Find("ButtonManager").GetComponentInChildren<Buttons>();
        b.ToggleSettings();
        settingsElements = GameObject.FindGameObjectsWithTag("SettingsElements");
        b.ToggleSettings();

    }

    public void SetScore(int s){
        playerScore = s;
    }

    public void SetName(string n){
        playerName = n;
    }

    public void SetDifficulty(int d){
        _difficulty = d;
    }

    public void SetVolume(float v){
        _volume = v;
    }

    public void SetIsSettingsOpen(bool b)
    {
        _isSettingsOpen = b;
    }

    public void SetInitialized(bool b)
    {
        _initialized = b;
    }

    public void SetPauseButton(GameObject b)
    {
        pauseButton = b;
    }

    public void SetPlayButton(GameObject b)
    {
        playButton = b;
    }

    public void SetNameSaved(bool b)
    {
        _nameSaved = b;
    }

    public GameObject GetPauseButton()
    {
        return pauseButton;
    }

    public GameObject GetPlayButton()
    {
        return playButton;
    }
    
     // public void SetSettingsElements(GameObject[] elements) {
     //        settingsElements = elements;
     // }
     
    public string GetName(){
        return playerName;
    }

    public int GetScore(){
        return playerScore;
    }

    public int GetDifficulty(){
        return _difficulty;
    }

    public float GetVolume(){
        return _volume;
    }
   
    public GameObject[] GetSettingsElements()
    {
        return settingsElements;
    }

    public Slider GetVolumeSlider()
    {
        return volumeSlider;
    }

    public TMP_Dropdown GetDifficultyDropdown()
    {
        if(difficultyDropdown == null){ difficultyDropdown = gameSettings.GetComponentInChildren<TMP_Dropdown>();}
        return difficultyDropdown;
    }

    public TMP_InputField GetPlayerNameInput()
    {
        return playerNameInput;
    }

    public bool GetIsSettingsOpen()
    {
        return _isSettingsOpen;
    }

    public bool GetInitialized()
    {
        return _initialized;
    }

    public bool GetNameSaved()
    {
        return _nameSaved;
    }
    
    private void OnDestroy()
    {
        _initialized = false;
    }

}
