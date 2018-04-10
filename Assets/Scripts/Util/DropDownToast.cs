using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DropDownToast : MonoBehaviour {

    [SerializeField]
    Text dropDownTextObject;

    [SerializeField]
    float timePopDownPersists;

    [SerializeField]
    float howLongIsTransition;

    [SerializeField]
    Transform positionToMoveTo;

    private Vector3 offscreenStartPosition;

    private Vector3 velocity;

    private const float offset = 0.1f;

    private enum animationStates { STOP, MOVEDOWN, COOLDOWN, MOVEUP };  // code smell, but I really need to get this done.
    animationStates currentAnimationState;

    private float countdownForPopDown;

    private Queue<string> messagesToSend = new Queue<string>();
  

    private void changeAnimationState(animationStates newAnimationState)
    {
        currentAnimationState = newAnimationState;
    }

    private void Start()
    {
        dropDownTextObject = this.GetComponentInChildren<Text>();
        changeAnimationState(animationStates.STOP);
        offscreenStartPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update ()
    {
       switch(currentAnimationState)
        {
            case animationStates.STOP:
                {
                    checkIfWeCanSendMessage();
                    break;
                }
            case animationStates.MOVEDOWN:
                {
                    repositionTextBox(positionToMoveTo.position);
                    checkIfWeReachedBottomPosition(positionToMoveTo.position);
                    break;
                }
            case animationStates.COOLDOWN:
                {
                    tickDownPopDownTimer();
                    break;
                }
            case animationStates.MOVEUP:
                {
                    repositionTextBox(offscreenStartPosition);
                    checkIfWeReachedStartPosition(offscreenStartPosition);
                    break;
                }

        }
	}

    private void repositionTextBox(Vector3 desiredLocation)
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, desiredLocation, ref velocity, howLongIsTransition);
    }

    private void checkIfWeReachedBottomPosition(Vector3 desiredLocation)
    {
        if (this.transform.position.y <= (desiredLocation.y + offset))
        {
            setCountDownTimer();
            changeAnimationState(animationStates.COOLDOWN);
        }
    }

    private void checkIfWeReachedStartPosition(Vector3 desiredLocation)
    {
        if (this.transform.position.y >= (desiredLocation.y - offset))
        {
            changeAnimationState(animationStates.STOP);
        }
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

    private void setCountDownTimer() // Refactor me out to an Itimeable with a timer base class, when I am feelin better. 
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
