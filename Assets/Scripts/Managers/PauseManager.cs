using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : Colleague
{
    IBarManagerState storedBarState;

    [SerializeField]
    Button continueButton;

    [SerializeField]
    Button quitButton;

   // [SerializeField]
   // Button openTavernKeeperJournal;

    [SerializeField]
    Image pausePanel;
  
    public void pauseGame(IBarManagerState barStateToStore)
    {
        storedBarState = barStateToStore;
        pausePanel.gameObject.SetActive(true);
        // pull up pause menu menu
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void resumeGame()
    {
        pausePanel.gameObject.SetActive(false);
        EndPhase();
    }

    public override void EndPhase()
    {
        Director.EndPhase(this);
    }

    public IBarManagerState getStoredBarState()
    {
        return storedBarState;
    }

}
