using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("LivingRoom");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void LoadMenu() 
    {
        if(InventoryCollection.Instance != null)
        {
            InventoryCollection.Instance.ResetInventory();
        }
        SceneManager.LoadScene("MainMenu");
    }
}
