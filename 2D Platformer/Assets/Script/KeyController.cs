using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject key; 
    public string requiredTag = "Player";  
    private bool isKeyPickedUp = false;  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isKeyPickedUp && collision.CompareTag(requiredTag))
        {
            key.transform.SetParent(collision.transform);
            key.SetActive(false); 
            isKeyPickedUp = true;
            Debug.Log("Key picked up by Player and attached to Player.");
        }
    }
}