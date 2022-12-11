using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;

    private bool movingLeft;
    private bool movingUp;
    private float angle;

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
        transform.position = new Vector3(
            Random.value > 0.5f ? horizontalRange : -horizontalRange, 
            transform.position.y, 
            Random.Range(-depthRange, depthRange));

        movingLeft = transform.position.x > 0;
        movingUp = transform.position.z < 0;
    }

    private void Update()
    {
        AngleEnemy();
        ChangeDirectionEnemy();
    }

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
        }
        else if (!movingLeft && transform.position.x > horizontalRange)
        {
            movingLeft = !movingLeft;
        }

        if (!movingUp && transform.position.z > depthRange)
        {
            movingUp = !movingUp;
        }
        else if(!movingUp && transform.position.z > depthRange)
        {
            movingUp = !movingUp;
        }
    }
}
