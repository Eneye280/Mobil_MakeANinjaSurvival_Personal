using UnityEngine;

public class CrawlingEnemy : MonoBehaviour
{
    [Header("Values")]
    [Range(1, 10), SerializeField] private float speed;
    [Range(1, 10), SerializeField] private float lifeTime;

    private bool movingLeft;

    #region VARIBLE'S COMPONENT
    private Rigidbody rigidbodyCrawler;
    #endregion

    private void Start()
    {
        rigidbodyCrawler = GetComponent<Rigidbody>();

        int randomIndex = Random.Range(0, transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == randomIndex);
        }

        movingLeft = transform.position.x > 0;
    }

    private void Update()
    {
        MovementCrawler();

        lifeTime -= Time.deltaTime;

        if(lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void MovementCrawler()
    {
        rigidbodyCrawler.velocity = new Vector3(
            movingLeft ? -speed : speed,
            rigidbodyCrawler.velocity.y,
            rigidbodyCrawler.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Player>() != null)
        {
            collision.transform.GetComponent<Player>().Kill();
        }
    }
}
