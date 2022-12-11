using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 20), SerializeField] private float speed;
    [Range(1, 20), SerializeField] private float movementAmplitude;
    [Range(1, 100), SerializeField] private float jumpingAngle;
    [Range(1, 2000), SerializeField] private float jumpingForce;
    
    [Header("General")]
    [SerializeField] private GameObject model;
    
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector2 clickOrigin;

    private bool lookingLeft;

    #region VARIABLE PROPERTIES
    private bool invincible = false;
    public bool Invincible { set { invincible = value; } }

    private bool lockZ = false;
    public bool LockZ { set { lockZ = value; } }

    private float depthRange;
    public float DepthRange { set { depthRange = value; } }

    private float horizontalRange;
    public float HorizontalRange { set { horizontalRange = value; } }

    private bool canJump = false;
    public bool CanJump { set { canJump = value; } }
    #endregion

    #region VARIABLE'S COMPONENT'S
    private Rigidbody rigidbodyPlayer;
    #endregion

    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();

        clickOrigin = Vector2.zero;
    }

    private void Update()
    {
        MovementPlayer();
        SmoothPositionPlayer();
        RotatePlayer();
        LimitTransformPlayer();
    }

    private void MovementPlayer()
    {
        Vector2 viewportCoordinates = new Vector2(
        Input.mousePosition.x / Screen.width,
        Input.mousePosition.y / Screen.height );

        if (Input.GetMouseButton(0))
        {
            if (clickOrigin == Vector2.zero)
            {
                originalPosition = transform.position;
                clickOrigin = viewportCoordinates;
            }
            else
            {
                Vector2 variation = viewportCoordinates - clickOrigin;

                targetPosition = new Vector3
                (
                    originalPosition.x + variation.x * movementAmplitude,
                    transform.position.y,
                    lockZ ? transform.position.z : originalPosition.z + variation.y * movementAmplitude
                );
            }
        }
        else
        {
            if(clickOrigin != Vector2.zero)
            {
                rigidbodyPlayer.AddForce( 
                    Mathf.Cos(jumpingAngle * Mathf.Deg2Rad) * jumpingForce * (lookingLeft ? -1 : 1), 
                    Mathf.Sin(jumpingAngle * Mathf.Deg2Rad) * jumpingForce, 0);

            }

            clickOrigin = Vector2.zero;
        }
    }

    private void SmoothPositionPlayer()
    {
        lookingLeft = targetPosition.x < transform.position.x;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector3(smoothPosition.x, transform.position.y, smoothPosition.z);
    }

    private void RotatePlayer()
    {
        targetRotation = Quaternion.Euler(0, (lookingLeft ? 180 : 0), 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    private void LimitTransformPlayer()
    {
        if (transform.position.z > depthRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, depthRange);
        else if (transform.position.z < -depthRange)
            transform.position = new Vector3(transform.position.x, transform.position.y, -depthRange);

        if (transform.position.x > horizontalRange)
            transform.position = new Vector3(horizontalRange, transform.position.y, transform.position.z);
        else if (transform.position.x < -horizontalRange)
            transform.position = new Vector3(-horizontalRange, transform.position.y, transform.position.z);
    }

    internal void Kill()
    {
        if (invincible) return;

        Destroy(gameObject);
    }
}
