using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public GameObject loadingPannel;

    public void startGameButtonPressed()
    {
        loadingPannel.SetActive(true);
        SceneManager.LoadScene("Scencely");
    }

    public void creditsButtonPressed()
    {

    }

}