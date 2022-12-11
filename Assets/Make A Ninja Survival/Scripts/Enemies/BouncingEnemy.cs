using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;

    private bool movingLeft;
    private bool movingUp;

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
        rigidbodyEnemy.velocity = new Vector3(
            movingLeft ? -speed : speed,
            rigidbodyEnemy.velocity.y,
            rigidbodyEnemy.velocity.z);
    }
}
