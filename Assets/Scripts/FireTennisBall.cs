using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FireTennisBall : MonoBehaviour
{
    public GameObject TennisBall;
    public float BallSpeed;
    public Vector3 initPosition;
    public float numBalls = 5;
    public float maxBalls = 5;
    public float reloadTime = 2.5f;
    private bool isReloading = false;
    public GameObject currentAimAssist, aimAssistPrefab;

    public Text ballsText;


    // Start is called before the first frame update
    void Start()
    {
        initPosition = Camera.main.transform.position;
        numBalls = maxBalls;
        ballsText.text = $"Balls: {numBalls}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isReloading)
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
        currentAimAssist = Instantiate(aimAssistPrefab);
        Destroy(currentAimAssist, 3f);
        //Vector3 direction = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.2f, .2f), Random.Range(0, 1f)).normalized;
        Vector3 direction = (new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.2f, .2f), Random.Range(0, 1f))).normalized;
        currentAimAssist.transform.position = initPosition + direction * 10f;
        //yield return new WaitForSeconds(2f); 
        ball.transform.position = initPosition;
        System.Threading.Thread.Sleep(1000);
        ball.FireBall(direction, 10f, BallSpeed);
        StartCoroutine(Wait(1f));
    }

    public IEnumerator Reloading()
    {
        ballsText.text = "Balls: reloading...";
        yield return new WaitForSeconds(reloadTime);
        numBalls = maxBalls;
        isReloading = false;
        ballsText.text = $"Balls: {numBalls}";
    }
    public IEnumerator Wait(float Second)
    {
        yield return new WaitForSeconds(Second);
    }

}
