using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Consume consumeeeee;
    public int KidCount;

    private void Awake()
    {
        KidCount = GameObject.FindGameObjectsWithTag("Kid").Length;
        consumeeeee = FindObjectOfType<Consume>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
       
    }

    private void Update()
    {
        if (player.suspicion >= 100f)
        {
            Debug.Log("Perdiste lince");
            player.kidnappedKids = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (consumeeeee.totalConsumedKids == KidCount)
        {
            Debug.Log("Ganaste pro");
            SceneManager.LoadScene(1);
        }
    }
}