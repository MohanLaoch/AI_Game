using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 public void SceneTransition()
    {

        SceneManager.LoadScene("James Scene");

    }
    public void Quit()
    {
        Application.Quit();
    }

}
