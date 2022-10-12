using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sus;
    public Text candy;
    public Text kidsInBag;
    public Image stealth;
    public float timeMax;

    private PlayerController player;
    private ThrowObject candieMan;

    Color color;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        candieMan = FindObjectOfType<ThrowObject>();
        color = stealth.color;
    }


    private void Update()
    {
        sus.value = player.suspicion;
        candy.text = "Candies: " + candieMan.Candies;
        kidsInBag.text = "Kids in the Bag: " + player.kidsInBag;


    }

    private void FixedUpdate()
    {
        Stealthed();
    }

    private void Stealthed()
    {
        if (player.isHidden == true)
        {
            /*float timeRemaining = timeMax;
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                color.a = 1 - (float)(timeRemaining / timeMax);
                stealth.color = color;
            }*/
            
            stealth.color = new Vector4(color.r, color.g, color.b, Mathf.Lerp(0f, 300f, 0.14f * Time.fixedDeltaTime));

        }
        else
            stealth.color = new Vector4(color.r, color.g, color.b, 0f);
    }


}
