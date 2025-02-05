using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject key;
    private bool isPressedByPlayer = false;
    private bool isPressedByBox = false;
    private bool isButtonPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isButtonPressed) return;

        if (collision.CompareTag("Player"))
        {
            isPressedByPlayer = true;
        }
        else if (collision.CompareTag("Pushable"))
        {
            isPressedByBox = true;
        }

        if (isPressedByPlayer && isPressedByBox && !key.activeSelf)
        {
            key.SetActive(true);
            Debug.Log("Both buttons pressed. Key is now visible!");
            isButtonPressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPressedByPlayer = false;
        }
        else if (collision.CompareTag("Pushable"))
        {
            isPressedByBox = false;
        }
    }
}