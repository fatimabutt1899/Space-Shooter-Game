using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2P : MonoBehaviour
{
  
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCoOpGame()
    {
        SceneManager.LoadScene(2);
    }


}
