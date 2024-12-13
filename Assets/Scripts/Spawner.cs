using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{
    [FormerlySerializedAs("Max_Obstacles")] [SerializeField]
    private int maxObstacles;
    private const int XMin = -17;
    private const int XMax = 17;
    private const int YMin = 1;
    private const float YMax = 6.5f;
    [FormerlySerializedAs("Balloon")] [SerializeField]
    private GameObject balloon;
    [FormerlySerializedAs("RockInBalloon")] [SerializeField] private GameObject rockInBalloon;
    [SerializeField] private string sceneName;

    // Start is called before the first frame update
    private void Start()
    {
        if (!balloon)
        {
            balloon = GameObject.Find("BalloonPoppingSprite");
        }

        if (!rockInBalloon)
        {
            rockInBalloon = GameObject.Find("RockInBalloon");
        }

        //BalloonSpawner

        InvokeRepeating(nameof(BalloonSpawner), 0.0f, 2.5f);


        //ObstacleSpawner
        var difficulty = PersistentData.Instance.GetDifficulty();
        switch (difficulty)
        {
            case 0:
            {
                sceneName = SceneManager.GetActiveScene().name;
                if (sceneName is "Scene1" or "Scene2")
                {
                    ObstacleSpawner(SceneManager.GetActiveScene().buildIndex);
                }

                break;
            }
            case > 1:
                ObstacleSpawner(difficulty);
                break;
            default:
                return;
        }
        
    }

    private void BalloonSpawner(){
            var position = new Vector2(Random.Range(XMin, XMax), Random.Range(YMin, YMax));
            Instantiate(balloon, position, Quaternion.identity);
    }

    private void ObstacleSpawner(int sceneNum)
    {
        maxObstacles = sceneNum == 2 ? 2 : 4;

        for(var i = 0; i < maxObstacles; i++){
            var position = new Vector2(Random.Range(XMin, XMax), Random.Range(YMin, YMax));
            Instantiate(rockInBalloon, position, Quaternion.identity);
        }
    }
    
}
