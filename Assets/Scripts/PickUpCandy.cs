using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCandy : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        /*if(other.gameObject.GetComponent<PlayerController>())
        {
            Debug.Log("debugggggggggg");
            Player.GetComponent<ThrowObject>().Candies += 5;
            Destroy(gameObject);
        }*/
        Debug.Log("debugggggggggg");
        Player.GetComponent<ThrowObject>().Candies += 5;
        Destroy(gameObject);
    }


}
