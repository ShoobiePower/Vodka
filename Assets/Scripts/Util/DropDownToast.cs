using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropDownToast : AbstractDropDownObject
{

    [SerializeField]
    Text dropDownTextObject;

    [SerializeField]
    float timePopDownPersists;

    private float countdownForPopDown;

    private Queue<string> messagesToSend = new Queue<string>();
  
    private void Start()
    {
        dropDownTextObject = this.GetComponentInChildren<Text>();
        changeAnimationState(animationStates.STOP);
        offscreenStartPosition = this.transform.position;
        initLocationOfDrop();

    }

    private void Update()
    {
        switch (currentAnimationState)
        {
            case animationStates.STOP:
                {
                    checkIfWeCanSendMessage();
                    break;
                }
            case animationStates.COOLDOWN:
                {
                    tickDownPopDownTimer();
                    break;
                }
        }
        base.Update();
    }

    private void checkIfWeCanSendMessage()
    {
        if (messagesToSend.Count > 0)
        {
            SendMessageOut();
        }
    }

    // Add a message to our queue of messages
    public void AddMessageToQueue(string messageToAdd)
    {
        messagesToSend.Enqueue(messageToAdd);
    }

    // Lable Text box with new message
     private void SendMessageOut()
    {
        dropDownTextObject.text = messagesToSend.Dequeue();
        changeAnimationState(animationStates.MOVEDOWN);
    }

    protected override void checkIfWeReachedBottomPosition(Vector3 desiredLocation)
    {
        if (this.transform.position.y <= (desiredLocation.y + offset))
        {
            setCountDownTimer();
            changeAnimationState(animationStates.COOLDOWN);
        }
    }

    private void setCountDownTimer()  
    {
        countdownForPopDown = timePopDownPersists;
    }

    private void tickDownPopDownTimer()
    {
        countdownForPopDown -= Time.deltaTime;
        if (countdownForPopDown <= 0)
        {
            changeAnimationState(animationStates.MOVEUP);
        }
    }

}
