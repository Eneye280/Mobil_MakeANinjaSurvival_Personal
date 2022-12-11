using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tmpInfoText;

    [Header("Gameplay")]
    [SerializeField] private float duration;
    public enum GameMode
    {
        Jumpers, Rollers, Shooters
    }

    public GameMode gameMode;

    [Header("Game: Jumpers")]
    [SerializeField] private GameObject jumpingEnemyPrefab;

    [Header("Game: Rollers")]
    [SerializeField] private GameObject rollingEnemyPrefab;

    private float timer;
    private bool gameOver;
    private bool win;

    private void Start()
    {
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
                InstanceManagerEnemy(jumpingEnemyPrefab, true);
                break;
            case GameMode.Rollers:
                InstanceManagerEnemy(rollingEnemyPrefab, false);
                break;
            case GameMode.Shooters:
                break;
        }
    }

    private void InstanceManagerEnemy(GameObject objectInstance, bool isPlayerLockZ)
    {
        player.LockZ = isPlayerLockZ;
        GameObject objectReferenceInstance = Instantiate(objectInstance);
        objectReferenceInstance.transform.SetParent(transform);
    } 
    #endregion
}
