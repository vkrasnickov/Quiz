using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
  public void PlayGame()
  {
    Debug.Log("Игра запущена");
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
  }
  public void ExitGame()
  {
    Debug.Log("Игра окончена");
    Application.Quit();
  }
}

