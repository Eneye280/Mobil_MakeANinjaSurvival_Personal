using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float horizontalRange;
    [Range(1, 2000), SerializeField] private float jumpingForce;
    [Range(1, 20), SerializeField] private float speed;

    private bool isMovingDown;
    private float targetHorizontalPosition;

    #region VARIABLE COMPONENT'S
    private Rigidbody rigidbodyEnemy; 
    #endregion

    private void Awake() => rigidbodyEnemy = GetComponent<Rigidbody>();

    private void Start() => isMovingDown = true;

    private void Update()
    {
        if(isMovingDown == false)
        {
            if(rigidbodyEnemy.velocity.y < 0f)
            {
                isMovingDown = true;
                targetHorizontalPosition = Random.Range(-horizontalRange, horizontalRange);
            }
        }

        Vector3 targetPosition = new Vector3(targetHorizontalPosition, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isMovingDown = false;
        rigidbodyEnemy.AddForce(0, jumpingForce, 0);

        if(collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().Kill();
        }
    }
}
