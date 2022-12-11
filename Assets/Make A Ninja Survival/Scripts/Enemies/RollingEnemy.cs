using UnityEngine;

public class RollingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;
    [Range(1, 10), SerializeField] private float horizontalRange;
    [Range(1, 10), SerializeField] private float waitingDuration;

    [Space]
    [Range(-10, 10), SerializeField] private float[] depthPercentages;

    private bool movingLeft;
    private float waitingTimer;
    private bool frozen;
    
    #region VARIABLE PROPERTIES
    private float depthRange;
    public float DepthRange { set { depthRange = value; } } 
    #endregion

    #region VARIABLE COMPONENT'S
    private Rigidbody rigidbodyEnemy; 
    #endregion

    private void Awake() => rigidbodyEnemy = GetComponent<Rigidbody>();

    private void Start()
    {
        transform.position = new Vector3(Random.value > 0.5f ? horizontalRange : -horizontalRange, transform.position.y, transform.position.z);

        movingLeft = transform.position.x > 0;

        RandomPositionEnemy();
    }

    private void Update()
    {
        if(waitingTimer > 0 || frozen)
        {
            waitingTimer -= Time.deltaTime;
            rigidbodyEnemy.velocity = Vector3.zero;
        }
        else
            MovementEnemy();

        ChangeDirectionEnemy();
    }

    private void MovementEnemy() => rigidbodyEnemy.velocity = new Vector3(movingLeft ? -speed : speed, rigidbodyEnemy.velocity.y, rigidbodyEnemy.velocity.z);

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
        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y, 
            ((depthRange + depthRange) * depthPercentages[Random.Range(0, depthPercentages.Length)]) - depthRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            frozen = true;
            collision.transform.GetComponent<Player>().Kill();
        }
    }
}
