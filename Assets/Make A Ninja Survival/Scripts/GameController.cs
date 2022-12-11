using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Gameplay")]

    public GameMode gameMode;
    public enum GameMode
    {
        Jumpers, Rollers, Bouncers
    }
    
    [Space]
    [Range(1, 60), SerializeField] private float duration;
    [Range(1, 20), SerializeField] private float depthRange;
    [SerializeField] private float horizontalRange;

    [Header("Visuals")]
    [SerializeField] private Camera gameCamera;
    [SerializeField] private TextMeshProUGUI tmpInfoText;

    [Header("Game: Jumpers")]
    [SerializeField] private GameObject jumpingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject rollingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject bouncersEnemyPrefab;
    [Range(1, 5), SerializeField] private int bouncingEnemiesAmount;

    private float timer;
    private bool gameOver;
    private bool win;

    private void Start()
    {
        #region Size Camera / Aspect Camera

        float baseAspect = 9f / 16f;
        float aspectVariation = gameCamera.aspect / baseAspect;
        horizontalRange = (aspectVariation * gameCamera.orthographicSize) / 2f;

        #endregion

        player.DepthRange = depthRange;
        player.HorizontalRange = horizontalRange;

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
                player.LockZ = true;

                GameObject jumpingEnemyObject = Instantiate(jumpingEnemyPrefab);
                jumpingEnemyObject.transform.SetParent(transform);
                jumpingEnemyObject.GetComponent<JumpingEnemy>().HorizontalRange = horizontalRange;
                break;
            case GameMode.Rollers:
                player.LockZ = false;

                GameObject rollingEnemyObject = Instantiate(rollingEnemyPrefab);
                rollingEnemyObject.transform.SetParent(transform);
                rollingEnemyObject.GetComponent<RollingEnemy>().DepthRange = depthRange;
                rollingEnemyObject.GetComponent<RollingEnemy>().HorizontalRange = horizontalRange;
                break;
            case GameMode.Bouncers:
                player.LockZ = true;

                for (int i = 0; i < bouncingEnemiesAmount; i++)
                {
                    GameObject rbouncersEnemyObject = Instantiate(bouncersEnemyPrefab);
                    rbouncersEnemyObject.transform.SetParent(transform);
                    rbouncersEnemyObject.GetComponent<BouncingEnemy>().DepthRange = depthRange;
                    rbouncersEnemyObject.GetComponent<BouncingEnemy>().HorizontalRange = horizontalRange;
                }
                break;
        }
    }
    #endregion
}
