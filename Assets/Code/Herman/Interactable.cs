using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool canInteract = false;
    public GameObject canvas;
    public int cluesFound = 0;
    void Start()
    {
        
    }

    private void Update()
    {
        if (canInteract)
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canInteract = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canInteract = false;
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            canvas.SetActive(true);
            cluesFound++;
        }
        if(cluesFound >= 2)
        {
            SceneManager.LoadScene("SpaceCruiseWinScreen");
        }


    }

}
