using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;
    [Range(1, 100), SerializeField] private float baseAngle;
    [Range(1, 50), SerializeField] private float variableAngle;

    private bool movingLeft;
    private bool movingUp;
    private float angle;
    private Vector3 targetVelocity;

    #region VARIABLE PROPERTIES
    private float depthRange;
    public float DepthRange { set { depthRange = value; } }

    private float horizontalRange;
    public float HorizontalRange { set { horizontalRange = value; } }
    #endregion

    #region VARIABLE COMPONENT'S
    private Rigidbody rigidbodyEnemy;
    #endregion

    private void Awake() => rigidbodyEnemy = GetComponent<Rigidbody>();

    private void Start()
    {
        PositionInitial();

        movingLeft = transform.position.x > 0;
        movingUp = Random.value > .5f;

        AngleInitial();
        VelocityInitial();
    }

    private void Update()
    {
        rigidbodyEnemy.velocity = targetVelocity;

        AngleEnemy();
        ChangeDirectionEnemy();
    }

    #region INITIALIZE METHOD'S
    private void PositionInitial()
    {
        transform.position = new Vector3(
            Random.value > .5f ? horizontalRange : -horizontalRange,
            transform.position.y, 0);
    }

    private void AngleInitial()
    {
        float targetAngle = baseAngle + (Random.Range(0, variableAngle));

        if (movingLeft)
        {
            if (movingUp)
                angle = 90 + targetAngle;
            else
                angle = 270 - targetAngle;
        }
        else
        {
            if (movingUp)
                angle = targetAngle;
            else
                angle = 270 + targetAngle;
        }
    }

    private void VelocityInitial()
    {
        targetVelocity = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad * speed),
            rigidbodyEnemy.velocity.y,
            Mathf.Sin(angle * Mathf.Deg2Rad * speed));
    } 
    #endregion

    private void AngleEnemy()
    {
        rigidbodyEnemy.velocity = new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad * speed),
            rigidbodyEnemy.velocity.y,
            Mathf.Sin(angle * Mathf.Deg2Rad * speed));
    }
    
    private void ChangeDirectionEnemy()
    {
        if (movingLeft && transform.position.x < -horizontalRange)
        {
            movingLeft = !movingLeft;
            transform.position = new Vector3(-horizontalRange, transform.position.y, transform.position.z);
            targetVelocity = new Vector3(-targetVelocity.x, targetVelocity.y, targetVelocity.z);
        }
        else if (!movingLeft && transform.position.x > horizontalRange)
        {
            movingLeft = !movingLeft;
            transform.position = new Vector3(horizontalRange, transform.position.y, transform.position.z);
            targetVelocity = new Vector3(-targetVelocity.x, targetVelocity.y, targetVelocity.z);
        }

        if (!movingUp && transform.position.z > depthRange)
        {
            movingUp = !movingUp;
            transform.position = new Vector3(transform.position.x, transform.position.y, depthRange);
            targetVelocity = new Vector3(targetVelocity.x, targetVelocity.y, -targetVelocity.z);
        }
        else if(!movingUp && transform.position.z < -depthRange)
        {
            movingUp = !movingUp;
            transform.position = new Vector3(transform.position.x, transform.position.y, -depthRange);
            targetVelocity = new Vector3(targetVelocity.x, targetVelocity.y, -targetVelocity.z);
        }
    }
}
