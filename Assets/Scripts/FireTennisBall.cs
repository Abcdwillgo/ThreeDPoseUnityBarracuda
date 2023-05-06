using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FireTennisBall : MonoBehaviour
{
    public GameObject TennisBall;
    public float BallSpeed;
    public Vector3 initPosition = Camera.main.transform.position;
    public float numBalls = 5;
    public float maxBalls = 5;
    public float reloadTime = 2.5f;
    private bool isReloading = false;


    public Text ballsText;


    // Start is called before the first frame update
    void Start()
    {
        numBalls = maxBalls;
        ballsText.text = $"Balls: {numBalls}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& !isReloading)
        {
            if (numBalls <= 0)
            {
                isReloading = true;
                StartCoroutine(Reloading());
            }
            else
            {


                GameObject clone = Instantiate(TennisBall);
                clone.transform.position = initPosition;
                Destroy(clone, 7);
                //to be continued...
                var ballScript = clone.GetComponent<BallScript>();
                FireBall(ballScript);
                numBalls--;
                ballsText.text = $"Balls: {numBalls}";
            }

        }

    }
    private void FireBall(BallScript ball)
    {
        Vector3 direction = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.2f, .2f), Random.Range(0, 1f)).normalized;
        ball.transform.position = initPosition;
        ball.FireBall(direction, 10f, BallSpeed);

    }

    public IEnumerator Reloading()
    {
        ballsText.text = "Balls: reloading...";
        yield return new WaitForSeconds(reloadTime);
        numBalls = maxBalls;
        isReloading = false;
        ballsText.text = $"Balls: {numBalls}";
    }


}