using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : Colleague
{
    IBarManagerState storedBarState;

    [SerializeField]
    Button journalButton;

    [SerializeField]
    Button optionsButton;

    [SerializeField]
    Button quitButton;


   // [SerializeField]
   // Button openTavernKeeperJournal;

    [SerializeField]
    Image pausePanel;

    private PauseScrollDownComponent pauseScrollDown;

    private void Start()
    {
        pauseScrollDown = this.gameObject.GetComponent<PauseScrollDownComponent>();
    }

    public void TogglePauseGame()
    {
        if (!pauseScrollDown.IsMenuDown)
            pauseScrollDown.MoveMenuDown();

        else
            pauseScrollDown.MoveMenuUp();

    }

    public void OpenExitGamePopUp()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void OpenJournal()
    {

    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ResumeGame()
    {
        pausePanel.gameObject.SetActive(false);
    }

    public void StoreBarState(IBarManagerState stateToStore)
    {
        storedBarState = stateToStore;
    }

    public IBarManagerState getStoredBarState()
    {
        return storedBarState;
    }

}
