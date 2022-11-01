using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucoUpgrades : MonoBehaviour
{
    [Header("References")]
    Consume _consume;
    Dash _dash;
    Energy _energy;
    PlayerMovement pm;
    [Header("Values")]
    [SerializeField] int lvl;
    [SerializeField] float _dashUpgrade;
    [SerializeField] float _speedUpgrade;
    
    private void Start()
    {
        _consume = GetComponent<Consume>();
        _dash = GetComponent<Dash>();
        _energy = GetComponent<Energy>();
        pm = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        _energy.RegenerateEnergy(lvl);
    }
    public void LevelUp (int currentConsumedKids)
    {
        lvl += currentConsumedKids;
        _dash.LevelUpDash(_dashUpgrade * currentConsumedKids);
        pm.LevelUpSpeed(_speedUpgrade * currentConsumedKids);
    }
}
