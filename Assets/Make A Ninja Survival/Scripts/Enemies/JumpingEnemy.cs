using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [SerializeField] private float horizontalRange;
    [SerializeField] private float jumpingForce;
    [SerializeField] private float speed;

    private bool isMovingDown;
    private float targetHorizontalPosition;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        isMovingDown = true;
    }

    private void Update()
    {
        if(isMovingDown == false)
        {
            if(_rigidbody.velocity.y < 0f)
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
        _rigidbody.AddForce(0, jumpingForce, 0);

        if(collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().Kill();
        }
    }
}
