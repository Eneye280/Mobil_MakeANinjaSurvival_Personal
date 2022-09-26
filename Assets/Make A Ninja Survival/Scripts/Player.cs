using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float movementAmplitude;
    [SerializeField] private bool lockZ = false;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private Vector2 clickOrigin;

    private bool invincible = false;
    public bool Invincible
    {
        set
        {
            invincible = value;
        }
    }

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
    }

    internal void Kill()
    {
        if (invincible) return;

        Destroy(gameObject);
    }
}
