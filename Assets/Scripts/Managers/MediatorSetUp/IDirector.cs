using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IDirector {

    void EndPhase(Colleague sender);

    // Stuff I would Like to refactor;
    void OpenRumorBoard();
    void OpenMapFromBar(Patron patronToSend);
    void SendRumorToBoard(Rumor rumor);
    void GetQuestFromBoard(Quest questToAdd);
    void BarManagerOpensBattleReport();
    void UnlockContent(List<Unlocker> itemsToUnlock);

    // I would like controls to be handled by something else. But This director is the only thing that knows about these classes

    void ActivateMapManagerCommand(Command commandToActivate);
    void ActivateBarManagerCommand(Command commandToActivate);
    void ActivateEndOfDayManagerCommand(Command comandToActivate);
    void ActivateEndOfDaySummaryCommand(Command commandToActivate);
    void ActivateRumorBoardUICommand(Command comandToActivate);
    void ActivateBattleReportCommand(Command commandToActivate);
    void ActivatePauseManagerCommand(Command commandToActivate);
    void pauseGame();


    // I need to fix my ducttape at some point

    void ReportOnPatronSentOnQuest(Patron patronSentOnQuest);

    void ReportOnPatronReturning(Patron returningPatron);

    void RecordPatronLevelUp(Patron patronThatLeveledUp);




}
