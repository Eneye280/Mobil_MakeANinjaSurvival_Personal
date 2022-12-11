using UnityEngine;

public class RollingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;
    [Range(1, 10), SerializeField] private float horizontalRange;
    [Range(1, 10), SerializeField] private float waitingDuration;

    [Space]
    [Range(-10, 10), SerializeField] private int[] depthValues;

    [Header("Checker's")]
    [SerializeField] private bool movingLeft;

    private float waitingTimer;

    #region VARIABLE COMPONENT'S
    private Rigidbody rbRollingEnemy; 
    #endregion

    private void Awake() => rbRollingEnemy = GetComponent<Rigidbody>();

    private void Start()
    {
        transform.position = new Vector3(Random.value > 0.5f ? horizontalRange : -horizontalRange, transform.position.y, transform.position.z);

        movingLeft = transform.position.x > 0;

        RandomPositionEnemy();
    }

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
            RandomPositionEnemy();
        }
    }

    private void RandomPositionEnemy()
    {
        waitingTimer = waitingDuration;
        transform.position = new Vector3(transform.position.x, transform.position.y, depthValues[Random.Range(0, depthValues.Length)]);
    }
}
