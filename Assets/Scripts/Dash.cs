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

    [Header("Dashing")]
    public float dashSpeed;
    public float dashUpwardForce;
    public float dashDuration;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashkey) && (Energy.CurrentEnergy - EnergyCost >= 0))
        {
            StartCoroutine(Dashing());
        }
    }
    IEnumerator Dashing()
    {
        Energy.ChangeEnergy(-EnergyCost);
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.Translate(Vector3.forward * dashSpeed);

            yield return null;
        }

    }

    void ResetDash ()
    {

    }
}
