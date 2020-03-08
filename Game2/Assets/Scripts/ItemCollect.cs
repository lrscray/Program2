using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollect : MonoBehaviour
{
    [SerializeField] private GameObject UIText;
    [SerializeField] private GameObject UIButton;
    //NOTE TO SELF: Gem is using a box Collider.

    void Start()
    {
        UIText.SetActive(false);
        UIButton.SetActive(false);
    }

    //NOTE TO SELF: Make sure to set "isTrigger" to "ON" in the Gem prefab!

    private void OnTriggerEnter(Collider other)
    {
        //Be sure to set player tag to "Player"
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            UIText.SetActive(true);
            Winner();
            UIButton.SetActive(true);
        }

    }


    void Winner()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //**
        //You need to free the cursor after locking it in game, Or you won't be able to click on the menu items
        //Cursor.lockState = CursorLockMode.None; //**
        //Freezes game once you win. Just here for testing
        Time.timeScale = 0f;
    }
}
