using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ThirdPersonStart()
    {
        SceneManager.LoadScene("TPSMap1");

    }

}
