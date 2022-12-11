using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Gameplay")]

    public GameMode gameMode;
    public enum GameMode
    {
        Jumpers, Rollers, Bouncers, Crawlers
    }
    
    [Space]
    [Range(1, 60), SerializeField] private float duration;
    [Range(1, 20), SerializeField] private float depthRange;
    [SerializeField] private float horizontalRange;

    [Header("Visuals")]
    [SerializeField] private Camera gameCamera;
    [SerializeField] private TextMeshProUGUI tmpInfoText;
    [SerializeField] private GameObject upperWall;
    [SerializeField] private GameObject lowerWall;

    [Header("Game: Jumpers")]
    [SerializeField] private GameObject jumpingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject rollingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject bouncersEnemyPrefab;
    [Range(1, 5), SerializeField] private int bouncingEnemiesAmount;

    [Header("Game: Crawlers")]
    [SerializeField] private GameObject crawlersEnemyPrefab;

    private float timer;
    private bool gameOver;
    private bool win;

    private void Awake()
    {
        #region Size Camera / Aspect Camera

        float baseAspect = 9f / 16f;
        float aspectVariation = gameCamera.aspect / baseAspect;
        horizontalRange = (aspectVariation * gameCamera.orthographicSize) / 2f;

        #endregion

        player.DepthRange = depthRange;
        player.HorizontalRange = horizontalRange;

        PositionWalls();

        timer = duration;

        InstanceEnemy();
    }

    private void Update()
    {
        if(player == null)
        {
            gameOver = true;
            win = false;
        }

        CheckTimerGame();
        FinishGame();
    }

    #region WALLS
    private void PositionWalls()
    {
        upperWall.transform.position = new Vector3(upperWall.transform.position.x, upperWall.transform.position.y, depthRange + 1.25f);
        lowerWall.transform.position = new Vector3(lowerWall.transform.position.x, lowerWall.transform.position.y - 1f, -depthRange - 1.25f);
    }
    #endregion

    #region CHECK TIMER GAME
    private void CheckTimerGame()
    {
        timer -= Time.deltaTime;

        if (timer > 0f)
            tmpInfoText.text = "Time: " + Mathf.Floor(timer);
        else
        {
            if (player != null)
            {
                gameOver = true;
                win = true;

                player.Invincible = true;
            }
        }
    }
    #endregion

    #region FINISH GAME
    private void FinishGame()
    {
        if (gameOver == true)
        {
            if (win)
            {
                tmpInfoText.text = "You Win!";
            }
            else
            {
                tmpInfoText.text = "You Lose!";
            }
        }
    }
    #endregion

    #region INSTANCE ENEMY
    private void InstanceEnemy()
    {
        switch (gameMode)
        {
            case GameMode.Jumpers:
                InstanceEnemyJumper();
                break;
            case GameMode.Rollers:
                InstanceEnemyRoller();
                break;
            case GameMode.Bouncers:
                InstanceEnemyBouncer();
                break;
            case GameMode.Crawlers:
                InstanceEnemyCrawlers();
                break;
        }
    }
    private void InstanceEnemyJumper()
    {
        player.LockZ = true;

        GameObject jumpingEnemyObject = Instantiate(jumpingEnemyPrefab);
        jumpingEnemyObject.transform.SetParent(transform);
        jumpingEnemyObject.GetComponent<JumpingEnemy>().HorizontalRange = horizontalRange;
    }
    private void InstanceEnemyRoller()
    {
        player.LockZ = false;

        GameObject rollingEnemyObject = Instantiate(rollingEnemyPrefab);
        rollingEnemyObject.transform.SetParent(transform);
        rollingEnemyObject.GetComponent<RollingEnemy>().DepthRange = depthRange;
        rollingEnemyObject.GetComponent<RollingEnemy>().HorizontalRange = horizontalRange;
    }
    private void InstanceEnemyBouncer()
    {
        player.LockZ = true;

        for (int i = 0; i < bouncingEnemiesAmount; i++)
        {
            GameObject bouncersEnemyObject = Instantiate(bouncersEnemyPrefab);
            bouncersEnemyObject.transform.SetParent(transform);

            bouncersEnemyObject.transform.position = new Vector3(
                (i % 2 == 0) ? horizontalRange : -horizontalRange,
                bouncersEnemyObject.transform.position.y,
                bouncersEnemyObject.transform.position.z
                );

            bouncersEnemyObject.GetComponent<BouncingEnemy>().DepthRange = depthRange;
            bouncersEnemyObject.GetComponent<BouncingEnemy>().HorizontalRange = horizontalRange;
        }
    }
    private void InstanceEnemyCrawlers()
    {
        player.LockZ = true;

        GameObject crawlersEnemyObject = Instantiate(crawlersEnemyPrefab);
        crawlersEnemyObject.transform.SetParent(transform);

        crawlersEnemyObject.transform.position = new Vector3(
            (Random.value > 0.5f) ? (horizontalRange + 0.8f) : (-horizontalRange - 0.8f),
            crawlersEnemyObject.transform.position.y,
            crawlersEnemyObject.transform.position.z);

    }
    #endregion
}
