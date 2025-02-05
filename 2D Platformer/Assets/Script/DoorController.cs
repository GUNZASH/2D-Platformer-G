using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public GameObject key;
    private bool isKeyInHand = false;

    private void Update()
    {
        if (key.activeSelf && key.transform.IsChildOf(GameObject.FindWithTag("Player").transform))
        {
            isKeyInHand = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isKeyInHand && collision.CompareTag("Player"))
        {
            Debug.Log("Door opened, moving to next level.");
        }
    }
}