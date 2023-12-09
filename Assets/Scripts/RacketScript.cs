using UnityEngine;

public class RacketScript : MonoBehaviour
{
    public Rigidbody rb;
    public float requiredVelocityToStart = 5f;
    public GameManager gameManager;
    public GameObject startTrigger;
    public bool isStarting = false;
    float elapsed = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        startTrigger = GameObject.FindGameObjectWithTag("Start");
    }

    
    private void Update()
    {

        if (isStarting)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 2)
            {
                gameManager.StartGame();
                startTrigger.SetActive(false);
                elapsed = 0f;
                isStarting = false;
            }
        }
        else
            elapsed = 0f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            gameManager.AddPoint();
        }
    }
}
