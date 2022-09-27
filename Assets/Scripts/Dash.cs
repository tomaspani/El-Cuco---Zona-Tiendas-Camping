using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playercam;
    private Rigidbody rb;
    private PlayerMovement Pm;
    private Energy Energy;
    private Consume consume;
    [SerializeField] GameObject particleEffect;

    [Header("Dashing")]
    public float dashSpeed;
    public float dashUpwardForce;
    public float dashDuration;
    public bool isDashing;

    [Header("Cooldowns")]
    public float dashCD;
    public float dashCDTimer;
    public int EnergyCost;

    [Header("Input")]
    public KeyCode dashkey;


    private SoundManager _sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pm = GetComponent<PlayerMovement>();
        Energy = GetComponent<Energy>();
        consume = GetComponent<Consume>();
        particleEffect = GameObject.FindGameObjectWithTag("SpeedEffect");
        particleEffect.SetActive(false);
        _sound = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        DashTimer();

        if (Input.GetKeyDown(dashkey) && (Energy.CurrentEnergy - EnergyCost >= 0) && !isDashing && dashCD <= 0 && consume.isConsuming == false)
        {
            Dashing();
            _sound.PlaySound("dash");
        }
    }

    public void DashTimer()
    {
        dashCD -= Time.deltaTime;
    }
    public void Dashing()
    {
        
        isDashing = true;
        Vector3 forceToApply = orientation.forward * dashSpeed + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
        particleEffect.SetActive(true);
        Invoke(nameof(ResetDash), dashDuration);
        dashCD = dashCDTimer;
        Energy.ChangeEnergy(-EnergyCost);
       

    }

    void ResetDash ()
    {
        isDashing = false;
        particleEffect.SetActive(false);
        _sound.StopSound("dash");
    }
}
