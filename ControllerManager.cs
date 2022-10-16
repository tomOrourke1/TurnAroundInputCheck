using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{



    public Controls controls;
    public Controls.PlayerActions actions;

    public int savedInputs = 3;

    public int maxFrameRetractedAllowence = 5;

    public int maxFrameInputReversed = 5;
    public List<Vector2> inputs;



    Vector2 lastInput;
    Vector2 inputBeforeRetracted;

    bool extendingBool;
    bool retractingBool;
    bool turnAroundBool;


    int framesSinceRetracted;


    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();
        actions = controls.Player;
        actions.Enable();

        //inutvalues
        inputs = new List<Vector2>();


    }
    /*
    private void FixedUpdate()
    {

        var input = actions.LeftStick.ReadValue<Vector2>();



        // input for current frame
        //add input to the list
        //spawn circle for the input
        // put the sphere in the right place
        // add the sphere to a list

        input *= (joystickPosition.localScale.x * 0.5f) - (joystickInisde.localScale.x * 1.5f);

        inputs.Add(input);


        float scalarLength = (joystickPosition.localScale.x / joystickPosition.localScale.x) / 2 - (joystickInisde.localScale.x);
        
        joystickInisde.localPosition = (Vector3)input * scalarLength;



        for(int i = 0; i < inputs.Count; i++)
        {
            if(i == 0)
            {
                Debug.DrawLine(joystickPosition.position, joystickPosition.position + (Vector3)inputs[i], Color.blue);
            }
            else if(i == inputs.Count -1)
            {
                Debug.DrawLine(joystickPosition.position + (Vector3)inputs[i - 1], joystickPosition.position + (Vector3)inputs[i], Color.green);
            }
            else
            {
                Debug.DrawLine(joystickPosition.position + (Vector3)inputs[i - 1], joystickPosition.position + (Vector3)inputs[i], Color.red);

            }
        }



        Vector2 average = Vector2.zero;
        int avCount = 0;

        List<Vector2> dirList = new List<Vector2>();


        if (inputs.Count > 1)
        {

            for (int i = 0; i < inputs.Count; i++)
            {
                if (i != 0)
                {
                    var dir = inputs[i] - inputs[i - 1];

                    average += dir;
                    avCount++;

                    dirList.Add(dir);
                }
            }

        }
        else
        {
            dirList.Add(Vector2.zero);
        }

        
        average /= avCount;

        if (inputs.Count > 1)
        {
            var pos = joystickPosition.position + Vector3.right * 3;
            Debug.DrawLine(pos, pos + (Vector3)average.normalized, Color.green);
            pos += Vector3.right * 2;
            Debug.Log("count of dir: " + dirList.Count);
            Debug.DrawLine(pos, pos + (Vector3)lastFrameAverageInput.normalized, Color.white);
        }

        //average = average.magnitude > 0 ? average : lastFrameAverageInput;






        // dot product to deturming what is happening 
        float dotDirs = Vector2.Dot(lastInput.normalized, average.normalized);
        Debug.Log("agerage dir = " + input.normalized + " averageInp: " + average.normalized + " Dot: " + dotDirs);

        // retracting
        retractingBool = dotDirs < -0.5f;
        savedColor = retractingBool ? Color.green : Color.red;

        // extenging
        extendingBool = dotDirs > 0.5f;
        savedColor2 = extendingBool ? Color.green : Color.red;


        // bool for if thie thing is happening?



        framesSinceRetracted = retractingBool ? 0 : framesSinceRetracted;


        turnAroundBool = extendingBool && framesSinceRetracted < maxFrameRetractedAllowence;
        savedColor3 = turnAroundBool ? Color.green : Color.red;




        if (inputs.Count >= savedInputs)
        {
            inputs.RemoveAt(0);
        }


        lastFrameAverageInput = average.magnitude == 0 ? lastFrameAverageInput : average;
        lastInput = input;
        // set last bools
        lastExtendBool = extendingBool;
        lastRetractBool = retractingBool;

        framesSinceRetracted++;
    }
    */

    bool turnAround(Vector2 input)
    {

        inputs.Add(input);





        Vector2 average = Vector2.zero;
        int avCount = 0;

        List<Vector2> dirList = new List<Vector2>();


        if (inputs.Count > 1)
        {

            for (int i = 0; i < inputs.Count; i++)
            {
                if (i != 0)
                {
                    var dir = inputs[i] - inputs[i - 1];

                    average += dir;
                    avCount++;

                    dirList.Add(dir);
                }
            }

        }
        else
        {
            dirList.Add(Vector2.zero);
        }


        average /= avCount;





        // dot product to deturming what is happening 
        float dotDirs = Vector2.Dot(lastInput.normalized, average.normalized);

        // retracting
        retractingBool = dotDirs < -0.5f;

        // extenging
        extendingBool = dotDirs > 0.5f;



        framesSinceRetracted = retractingBool ? 0 : framesSinceRetracted;


        bool lastInputIsInOposition = Vector2.Dot(inputBeforeRetracted, input) < -0.5f;

        turnAroundBool = extendingBool && framesSinceRetracted < maxFrameRetractedAllowence && lastInputIsInOposition;


        if(framesSinceRetracted >= maxFrameRetractedAllowence)
        {
            inputBeforeRetracted = input;
        }


        if (inputs.Count >= savedInputs)
        {
            inputs.RemoveAt(0);
        }



        lastInput = input;
        // set last bools

        framesSinceRetracted++;


        return turnAroundBool;
    }





}
