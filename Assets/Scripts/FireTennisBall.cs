using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FireTennisBall : MonoBehaviour
{
    public GameObject TennisBall;
    public float BallSpeed;
    public Transform playerTransform;
    public float minDistanceFromPlayer = 5f;
    public float maxDistanceFromPlayer = 10f;
    public float numBalls = 5;
    public float maxBalls = 5;
    public float reloadTime = 2.5f;
    public float timeBetweenShots = 2.0f;  // Delay between each shot
    private bool isReloading = false;
    public GameObject aimAssistPrefab;

    public Text ballsText;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        numBalls = maxBalls;
        ballsText.text = $"Balls: {numBalls}";
    }

    void Update()
    {
        if (gameManager.gameStarted && !isReloading)
        {
            StartCoroutine(FireBallsSequence());
        }
    }

    IEnumerator FireBallsSequence()
    {
        isReloading = true;

        while (numBalls > 0)
        {
            StartCoroutine(FireBall());
            yield return new WaitForSeconds(timeBetweenShots);
        }

        StartCoroutine(Reloading());
    }


    private IEnumerator FireBall()
    {
        // Calculate distance and direction relative to the player
        float distanceFromPlayer = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

        // Separate the direction into components
        float xDirection = Random.Range(-3f, 3f);
        float yDirection = Random.Range(0.1f, 2f); // Ensure y is positive to avoid shooting into the ground
        float zDirection = directionToPlayer.z; // Maintain the original z-direction towards the player

        // Combine them back into a single vector
        Vector3 direction = new Vector3(xDirection, yDirection, zDirection).normalized;
        Vector3 aimPosition = playerTransform.position + direction * distanceFromPlayer;

        // Create aim assist prefab
        GameObject aimAssist = Instantiate(aimAssistPrefab, aimPosition, Quaternion.identity);

        // Wait for 2 seconds to let the player adjust
        yield return new WaitForSeconds(2f);

        // Spawn and fire ball
        GameObject ball = Instantiate(TennisBall, transform.position, Quaternion.identity);
        BallScript ballScript = ball.GetComponent<BallScript>();
        ballScript.FireBall((aimAssist.transform.position - transform.position).normalized, BallSpeed, 0);

        Destroy(aimAssist); // Destroy aim assist now that the ball has been fired
        Destroy(ball, 7f);

        numBalls--;
        ballsText.text = $"Balls: {numBalls}";
    }



    IEnumerator Reloading()
    {
        ballsText.text = "Balls: reloading...";
        yield return new WaitForSeconds(reloadTime);
        numBalls = maxBalls;
        isReloading = false;
        ballsText.text = $"Balls: {numBalls}";
    }
}
