using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndOfDaySummaryManager : Colleague {

   private List<string> transactionsOfTheDay = new List<string>();

    [SerializeField]
    Text writtenTransactions;

    public void setEndOfDaySummaryBoardActive()
    {
        clearWrittenTransactions();
        this.gameObject.SetActive(true);
        writeEventsToBoard();
    }

    public override void EndPhase()
    {
        this.gameObject.SetActive(false);
        Director.EndPhase(this);
    }

    // variousTransactions
    public void QuestChosenTransaction(string patronWhoGaveQuest, string rumorGiven, string questChosen)
    {
        string recordToAdd;
        recordToAdd = string.Format("{0} told you about {1}. You chose to {2}. \n", patronWhoGaveQuest, rumorGiven, questChosen );
        addTransactionToList(recordToAdd);
    }

    public void RecordAdventureTransaction(Patron patronWithQuest) 
    {
        string recordToAdd;
        recordToAdd = string.Format("{0} left to complete the quest {1} located in {2}.", patronWithQuest.Name, patronWithQuest.QuestToCompleete.QuestName, capitalizeFirstLetter(patronWithQuest.QuestToCompleete.QuestLocation.ToString()));
        addTransactionToList(recordToAdd);
    }

    public void RecordPatronReturnTransaction(Patron returningPatron)  
    {
        string recordToAdd;
        recordToAdd = string.Format("{0} returned from their quest {1}!", returningPatron.Name, returningPatron.QuestToCompleete.QuestName);
        addTransactionToList(recordToAdd);
    }

    public void RecordPatronLevelUp(Patron patronThatLeveledUp)
    {
        string recordToAdd;
        recordToAdd = string.Format("Your bond with {0} grew! Your bond is now level {1}! \n", patronThatLeveledUp.Name, patronThatLeveledUp.Level);
        addTransactionToList(recordToAdd);
    }
   
    private void addTransactionToList(string transactionToAdd)
    {
        transactionsOfTheDay.Add(transactionToAdd);
    }

    private void writeEventsToBoard()
    {
        for (int i = 0; i < transactionsOfTheDay.Count; i++)
        {
            writtenTransactions.text += transactionsOfTheDay[i] + "\n";
        }
        transactionsOfTheDay.Clear();
    }

    private void clearWrittenTransactions()
    {
        writtenTransactions.text = string.Empty;
    }

    private string capitalizeFirstLetter(string wordToCapitalize)
    {
        string stringToReturn = wordToCapitalize[0].ToString();
        for (int i = 1; i < wordToCapitalize.Length; i++)
        {
            stringToReturn += wordToCapitalize[i].ToString().ToLower();
        }
        return stringToReturn;
    }

    // sort transactions based on kind

}
