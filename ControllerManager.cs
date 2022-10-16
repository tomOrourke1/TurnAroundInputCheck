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
