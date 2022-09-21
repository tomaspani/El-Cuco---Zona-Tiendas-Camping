using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consume : MonoBehaviour
{
    private Energy _energy;
    private PlayerController _player;

    public int restoreEnergy;
    public float maxTimer;

    public Slider timer;

    public bool isConsuming;
    float counter = 0f;
    void Start()
    {
        _energy = GetComponent<Energy>();
        _player = GetComponent<PlayerController>();
        timer.gameObject.SetActive(false);

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && _player.kidsInBag > 0)
        {
            
            timer.gameObject.SetActive(true);
            if (counter < maxTimer)
            {
                isConsuming = true;
                counter += Time.deltaTime;
                timer.value = counter;
                Debug.Log(counter);
            }
            else if(counter > maxTimer)
            {
                Debug.Log(counter);
                ConsumeKids(_player.kidsInBag, restoreEnergy);
                timer.gameObject.SetActive(false);
                isConsuming = false;
            }
            
        }
        else
        {
            timer.value = 0f;
            counter = 0f;
            timer.gameObject.SetActive(false);
            isConsuming = false;
        }
    }

    private void ConsumeKids(int amountKids, int restoreEnergy)
    {
        //isConsuming = true;
        _energy.ChangeEnergy(restoreEnergy * amountKids);
        _player.Consume();

    }
}
