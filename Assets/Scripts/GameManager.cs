using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private int KidCount;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        KidCount = GameObject.FindGameObjectsWithTag("Kid").Length;
    }

    private void Update()
    {
        if (player.suspicion >= 100f)
        {
            Debug.Log("Perdiste lince");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

       


        if (player.kidnappedKids == KidCount)
        {
            Debug.Log("Ganaste pro");
            SceneManager.LoadScene(1);
        }
    }
}