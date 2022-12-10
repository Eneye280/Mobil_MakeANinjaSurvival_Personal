using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tmpInfoText;

    [Header("Gameplay")]
    [SerializeField] private float duration;

    [Header("Game: Jumpers")]
    [SerializeField] private GameObject jumpingEnemyPrefab;

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

    private void InstanceEnemy()
    {
        GameObject jumpingEnemyObject = Instantiate(jumpingEnemyPrefab);
        jumpingEnemyObject.transform.SetParent(transform);
    }
}
