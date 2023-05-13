using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 65;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void FireBall(Vector3 direction, float power, float maxOffset)
    {
        Vector3 offset = new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), 0f);
        //rb.position += offset;
        rb.AddForce(direction * power, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<BallScript>() != null || other.gameObject.CompareTag("Player"))
        {
            return;
        }
        Vector3 velocity = rb.velocity;
        Debug.Log("velocity after hit: " + "m/s");
        Destroy(this.gameObject);
    }

}