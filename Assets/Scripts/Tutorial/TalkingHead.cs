using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TalkingHead : MonoBehaviour {

    [SerializeField]
    Text textToDisplay;
    [SerializeField]
    Image imageToDisplay;

    Conversation loadedConversation;

    public void sendMessageToTalkingHead(Conversation conversationToLoad, byte patronID )
    {
        activateTalkingHead();
        loadedConversation = conversationToLoad;
        imageToDisplay.sprite = ApperanceManager.instance.ThisPatronsToken(patronID);
        outputNextStanza();
    }

    public void outputNextStanza()
    {
        if (loadedConversation.IsConversationOver) { deactivateTalkingHead();}
        else
        textToDisplay.text = loadedConversation.dioOut();
    }

    private void activateTalkingHead()
    {
        this.gameObject.SetActive(true);
    }

    private void deactivateTalkingHead()
    {
        this.gameObject.SetActive(false);
        loadedConversation = null;
    }
}
