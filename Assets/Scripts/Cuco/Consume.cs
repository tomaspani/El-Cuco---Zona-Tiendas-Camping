using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consume : MonoBehaviour
{
    [Header("References")]
    private Energy _energy;
    private PlayerController _player;
    private SoundManager _sound;
    CucoUpgrades _cucoUpgrades;
    [SerializeField] UIKidCounter kidcounter; 
     
    [Header("Values")]
    public int restoreEnergy;
    public float maxTimer;
    public int totalConsumedKids;
    public Slider timer;

    public bool isConsuming;
    float counter = 0f;
    void Start()
    {
        _cucoUpgrades = GetComponent<CucoUpgrades>();
        _energy = GetComponent<Energy>();
        _player = GetComponent<PlayerController>();
        _sound = FindObjectOfType<SoundManager>();
        timer.gameObject.SetActive(false);
        kidcounter.UpdateTXT(totalConsumedKids);
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
                _sound.PlaySound("eating");
            }
            else if(counter > maxTimer)
            {
                ConsumeKids(_player.kidsInBag, restoreEnergy);
                timer.gameObject.SetActive(false);
                isConsuming = false;
                _sound.StopSound("eating");

            }

        }
        else
        {
            timer.value = 0f;
            counter = 0f;
            timer.gameObject.SetActive(false);
            isConsuming = false;
            _sound.StopSound("eating");
        }
    }

    private void ConsumeKids(int amountKids, int restoreEnergy)
    {
        //isConsuming = true;
        _energy.ChangeEnergy(restoreEnergy * amountKids);
        _cucoUpgrades.LevelUp(_player.kidsInBag);
        totalConsumedKids += _player.kidsInBag;
        kidcounter.UpdateTXT(totalConsumedKids);
        _player.Consume();
        
       
    }
}
