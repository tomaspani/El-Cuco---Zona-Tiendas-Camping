using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sus;
    public Text candy;

    private PlayerController player;
    private ThrowObject candieMan;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        candieMan = FindObjectOfType<ThrowObject>();
    }


    private void Update()
    {
        sus.value = player.suspicion;
        candy.text = "Candies: " + candieMan.Candies;
    }
}
