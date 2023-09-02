using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 4;
    public GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void FireBall(Vector3 direction, float power, float maxOffset)
    {
        rb.AddForce(direction * power, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.AddPoint();
            Destroy(this.gameObject);
        }
    }
    public float zBoundary = -10f; // Set this to whatever value makes sense for your game

    void Update()
    {
        if (transform.position.z < zBoundary)
        {
            gameManager.Miss();
            Destroy(this.gameObject);
        }
    }
}
