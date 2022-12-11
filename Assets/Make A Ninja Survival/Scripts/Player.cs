using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 20), SerializeField] private float speed;
    [Range(1, 20), SerializeField] private float movementAmplitude;
    
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector2 clickOrigin;

    #region VARIABLE PROPERTIES
    private bool invincible = false;
    public bool Invincible { set { invincible = value; } }

    private bool lockZ = false;
    public bool LockZ { set { lockZ = value; } }

    private float depthRange;
    public float DepthRange { set { depthRange = value; } } 
    #endregion

    private void Start()
    {
        clickOrigin = Vector2.zero;
    }

    private void Update()
    {
        Vector2 viewportCoordinates = new Vector2
        (
            Input.mousePosition.x / Screen.width,
            Input.mousePosition.y / Screen.height
        );

        if(Input.GetMouseButton(0))
        {
            if(clickOrigin == Vector2.zero)
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
            clickOrigin = Vector2.zero;
        }

        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        transform.position = new Vector3(smoothPosition.x, transform.position.y, smoothPosition.z);

        if (transform.position.z > depthRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, depthRange);
        }
        else if (transform.position.z < -depthRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -depthRange);
        }
    }

    internal void Kill()
    {
        if (invincible) return;

        Destroy(gameObject);
    }
}
