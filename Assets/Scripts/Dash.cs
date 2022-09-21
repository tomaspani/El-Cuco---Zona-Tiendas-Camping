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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pm = GetComponent<PlayerMovement>();
        Energy = GetComponent<Energy>();
        consume = GetComponent<Consume>();
        particleEffect = GameObject.FindGameObjectWithTag("SpeedEffect");
        particleEffect.SetActive(false);
    }
    private void Update()
    {
        DashTimer();

        if (Input.GetKeyDown(dashkey) && (Energy.CurrentEnergy - EnergyCost >= 0) && !isDashing && dashCD <= 0 && consume.isConsuming == false)
        {
            Dashing();
        }
    }

    public void DashTimer()
    {
        dashCD -= Time.deltaTime;
    }
    public void Dashing()
    {
        
        isDashing = true;
        particleEffect.SetActive(true);
        Vector3 forceToApply = orientation.forward * dashSpeed + orientation.up * dashUpwardForce;
        rb.AddForce(forceToApply, ForceMode.Impulse);
        
        Invoke(nameof(ResetDash), dashDuration);
        dashCD = dashCDTimer;
        Energy.ChangeEnergy(-EnergyCost);
        
        //float startTime = Time.time;
        //while (Time.time < startTime + dashDuration)
        //{
        //    transform.Translate(Vector3.forward * dashSpeed);

        //    yield return null;
        //}

    }

    void ResetDash ()
    {
        isDashing = false;
        particleEffect.SetActive(false);
    }
}
