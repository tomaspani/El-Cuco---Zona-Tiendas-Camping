using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider sus;
    public Text candy;
    public Text candyplus;
    public TMP_Text kidsInBag;
    public Image stealth;
    public float timeMax;

    private PlayerController player;
    private ThrowObject candieMan;

    Color color;

    IEnumerator waitCouroutine()
    {
        yield return new WaitForSeconds(10.0f);
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        candieMan = FindObjectOfType<ThrowObject>();
        color = stealth.color;
    }

    public void addCandy(int cant)
    {
        candyplus.enabled = true;
        candyplus.text = "Candy +" + cant;
        
    }


    private void Update()
    {
        sus.value = player.suspicion;
        candy.text = "Candies: " + candieMan.Candies;
        kidsInBag.text = "" + player.kidsInBag;
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
            
            stealth.color = new Vector4(color.r, color.g, color.b, Mathf.Lerp(0f, 70f, 0.5f * Time.fixedDeltaTime));

        }
        else
            stealth.color = new Vector4(color.r, color.g, color.b, 0f);
    }


}
