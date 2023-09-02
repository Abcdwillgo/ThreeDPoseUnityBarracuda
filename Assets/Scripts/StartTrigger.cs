using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    private GameManager manager;
    private RacketScript racket;
    private void Awake()
    {
        manager = FindAnyObjectByType<GameManager>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<RacketScript>().isStarting = true;
            Debug.Log("It is starting!!!");
        }
    }
}
