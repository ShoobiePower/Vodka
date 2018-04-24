using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RumorBoardUI : Colleague {

    [SerializeField]
    Text RumorName;

    [SerializeField]
    Text RumorDescription;

    [SerializeField]
    Button SelectQuestOptionButton;

    [SerializeField]
    QuestOptionUI[] questOptionsForRumor;

    private int highLightedQuestsIndex;


    public string getRumorName()
    {
        return RumorName.text;
    }
    // we want to activate
    public void activatePatronRumorBoard()
    {
        this.gameObject.SetActive(true);
    }

    // we want to deActivate
    public void deActivatePatronRumorBoard()
    {
        this.gameObject.SetActive(false);
        deactivateQuestOptions();
    }

    private void activateSelectQuestButton()
    {
        SelectQuestOptionButton.gameObject.SetActive(true);
    }

    private void deActivateSelectQuestButton()
    {
        SelectQuestOptionButton.gameObject.SetActive(false);
    }

    private void deactivateQuestOptions()
    {
        for (int i = 0; i < questOptionsForRumor.Length; i++)
        {
            questOptionsForRumor[i].deactivateQuestOption();
        }
    }

    //We want to display the rumor passed to us from the rumor manager. This includes a name, a description, and it's respective quest options
    public void labelPatronRumorBoard(Rumor rumorToLabelBoardWith)
    {
        RumorName.text = rumorToLabelBoardWith.RumorName;
        RumorDescription.text = rumorToLabelBoardWith.RumorDescription; 
        for (int i = 0; i < rumorToLabelBoardWith.QuestForThisRumor.Count; i++)
        {
            questOptionsForRumor[i].activateQuestOption();
            questOptionsForRumor[i].MakeQuestOptionSizeNormal();
            questOptionsForRumor[i].QuestContainedInOption = rumorToLabelBoardWith.QuestForThisRumor[i];
            questOptionsForRumor[i].UseDefaultPosition();
        }
        if (rumorToLabelBoardWith.QuestForThisRumor.Count < 2) // Magic number to be refactored
        {
           questOptionsForRumor[0].CenterOption0();
        }

        deActivateSelectQuestButton();
    }

    public void HilightQuestOption(int indexer)
    {
        for (int i = 0; i < questOptionsForRumor.Length; i++)
        {
            questOptionsForRumor[i].ShrinkQuestOption();
        }

        questOptionsForRumor[indexer].GrowQuestOption();
        highLightedQuestsIndex = indexer;
        activateSelectQuestButton();
    }

    public void SelectQuestFromOptions()
    {
        Director.GetQuestFromBoard(questOptionsForRumor[highLightedQuestsIndex].QuestContainedInOption);
        EndPhase();
    }

    public override void EndPhase()
    {
        deActivatePatronRumorBoard();
        Director.EndPhase(this);
    }


    // We want to turn on as many QuestOptionUI as there are options for the quest;

    // Once selected, we want to return a quest to the rumor manager. 





}
