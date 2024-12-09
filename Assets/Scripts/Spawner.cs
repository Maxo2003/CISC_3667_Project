using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] int numBalloons = 0;
   // [SerializeField] int numObstacles = 0;
    [SerializeField] const int MAX_BALLOONS = 1;
    [SerializeField] int Max_Obstacles;
    [SerializeField] const int X_MIN = -17;
    [SerializeField] const int X_MAX = 17;
    [SerializeField] const int Y_MIN = 1;
    [SerializeField] const float Y_MAX = 6.5f;
    [SerializeField] GameObject Balloon;
    [SerializeField] GameObject RockInBalloon;
    [SerializeField] string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (Balloon == null)
        {
            Balloon = GameObject.Find("BalloonPoppingSprite");
        }

        if (RockInBalloon == null)
        {
            RockInBalloon = GameObject.Find("RockInBalloon");
        }

        //BalloonSpawner

        InvokeRepeating("BalloonSpawner", 0.0f, 4.0f);


        //ObstacleSpawner
        sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Scene1" || sceneName == "Scene2"){
            ObstacleSpawner(SceneManager.GetActiveScene().buildIndex);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void BalloonSpawner(){
            Vector2 position = new Vector2(Random.Range(X_MIN, X_MAX), Random.Range(Y_MIN, Y_MAX));
            Instantiate(Balloon, position, Quaternion.identity);
            numBalloons++;
    }

    void ObstacleSpawner(int sceneNum){
        if (sceneNum == 1) {
            Max_Obstacles = 2;
        } else {
            Max_Obstacles = 4;
        }

        for(int i = 0; i < Max_Obstacles; i++){
            Vector2 position = new Vector2(Random.Range(X_MIN, X_MAX), Random.Range(Y_MIN, Y_MAX));
            Instantiate(RockInBalloon, position, Quaternion.identity);
        }
    }

    public void BalloonPopped(){
            numBalloons--;
        }
}
