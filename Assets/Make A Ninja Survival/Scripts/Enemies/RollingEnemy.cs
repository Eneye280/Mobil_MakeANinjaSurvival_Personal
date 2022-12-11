using UnityEngine;

public class RollingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;
    [Range(1, 10), SerializeField] private float horizontalRange;
    [Range(1, 10), SerializeField] private float waitingDuration;

    [Header("Checker's")]
    [SerializeField] private bool movingLeft;

    private float waitingTimer;

    #region VARIABLE COMPONENT'S
    private Rigidbody rbRollingEnemy; 
    #endregion

    private void Awake() => rbRollingEnemy = GetComponent<Rigidbody>();

    private void Start() => movingLeft = true;

    private void Update()
    {
        if(waitingTimer > 0)
        {
            waitingTimer -= Time.deltaTime;
            rbRollingEnemy.velocity = Vector3.zero;
        }
        else
        {
            MovementEnemy();
        }

        
        ChangeDirectionEnemy();
    }

    private void MovementEnemy() => rbRollingEnemy.velocity = new Vector3(movingLeft ? -speed : speed, rbRollingEnemy.velocity.y, rbRollingEnemy.velocity.z);

    private void ChangeDirectionEnemy()
    {
        if ((movingLeft && transform.position.x < - horizontalRange) ||
            (!movingLeft && transform.position.x > horizontalRange))
        {
            movingLeft = !movingLeft;
            waitingTimer = waitingDuration;
        }
    }
}
