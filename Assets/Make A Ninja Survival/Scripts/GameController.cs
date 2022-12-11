using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("Gameplay")]

    public GameMode gameMode;
    public enum GameMode
    {
        Jumpers, Rollers, Shooters
    }
    
    [Space]
    [Range(1, 20), SerializeField] private float duration;
    [Range(1, 20), SerializeField] private float depthRange;
    [SerializeField] private float horizontalRange;

    [Header("Visuals")]
    [SerializeField] private Camera gameCamera;
    [SerializeField] private TextMeshProUGUI tmpInfoText;

    [Header("Game: Jumpers")]
    [SerializeField] private GameObject jumpingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject rollingEnemyPrefab;

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

        timer -= Time.deltaTime;

        if(timer > 0f)
        {
            tmpInfoText.text = "Time: " + Mathf.Floor(timer);
        }
        else
        {
            if(player != null)
            {
                gameOver = true;
                win = true;

                player.Invincible = true;
            }
        }

        if(gameOver == true)
        {
            if(win)
            {
                tmpInfoText.text = "You Win!";
            }
            else
            {
                tmpInfoText.text = "You Lose!";
            }
        }
    }

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
            case GameMode.Shooters:
                break;
        }
    }
    #endregion
}
