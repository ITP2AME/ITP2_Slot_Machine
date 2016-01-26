//----------------------------------------
//		Unity3D Games Template (C#)
// Copyright © 2015 Lord Meshadieme
// 	   skype : lord_meshadieme
//----------------------------------------

/// <version>
/// 0.1.0
/// </version>
/// <summary>
/// A Parent Class for most of our scripts,
/// To make it easier to manage debugging code and modify debug output as needed.
/// Aswell as avoiding debug messages is a non-development build.
/// </summary>
/// CHANGELOG: 
///	*	0.1.0: C# Game Template Base
/// TODO: Consolidate Debug Toggles in Editor for easiers access (C#)
/// 

using Meshadieme;
using Meshadieme.Math;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Meshadieme
{

    //Update text every frame
    //get inputs for lever
    //make lever move properly
    //trigger functions from lever
    //shufflebag
    //make sure highscore is saving
    //select mutli to use (make an array) of 5 add more buttons
    // add apply button
    //add music on off button

    /*
    Sequence of events
    plus minus betting amount
    pull lever down
    change help text to bonus game
    minus bet amount from coins
    start rolling pins using shuffle bag (fake it at first)
    start rolling other pins 
    if lever pulled to the right
    stop first other pin loop three times
    use results from other pin to weigh the shufflebag then pick result
    transition result into fake spin
    do matching logic for three results
    trigger mini game call function (that returns an array of integer for two (new coins ammount and new multiplier amount)
    store new multi into array (max of 5)
    accept button to get reward and apply select multiplier (positive or nothing)
    change back text
    save highscore
    show highscore
    */

    public enum GameMode
    {
        Slots = 0,
        MiniGame = 1,
    }

    public enum MiniGames
    {
        something,
    }

    public class slotsGF : gameFramework
    {

        public float gbp = 50.0f;
        public float multiCurrent = 1.0f;
        public int miniGame;
        public float bet = 5.0f;
        public GameMode gMode;
        public string[] helpTexts; //Modify in editor
        public float[] result;
        Text helpText, coinText, pinAText, pinBText, pinCText, otherPinAText, otherPinBText, otherPinCText, extraMultiA, extraMultiB, extraMultiC, toBet;
        Button coinButton;
        public bool leverMode;
        public Animator leverAnimator; //An animator controlling the lever is declared
        bool spinning, otherSpinA, otherSpinB, otherSpinC = false;
        int[] multiStored = new int[] { 1, 0, 0 };
        int[] defShuffle = new int[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        int[] shuffleA = new int[] { 4, 4, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0 };
        int[] shuffleB = new int[] { 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0 };
        int[] shuffleC = new int[] { 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 0, 0, 0 };
        shuffleBag toUse;
        shuffleBag sbDef; //default shuffleBag random result
        shuffleBag sbTempA; //Custom shufflebag random result for when modified by secondary Pins
        shuffleBag sbTempB; //Custom shufflebag random result for when modified by secondary Pins
        shuffleBag sbTempC; //Custom shufflebag random result for when modified by secondary Pins
        int otherPinState = 0;
        int[] results = new int[] { 0, 0, 0, 0, 0, 0}; // first three for main Pins, second three for secondary Pins

        protected override void Awake()
        {
            helpText = GM.Get().scene.miscRefs[0].GetComponent<Text>();
            coinText = GM.Get().scene.miscRefs[7].GetComponent<Text>();
            pinAText = GM.Get().scene.miscRefs[1].GetComponent<Text>();
            pinBText = GM.Get().scene.miscRefs[2].GetComponent<Text>();
            pinCText = GM.Get().scene.miscRefs[3].GetComponent<Text>();
            otherPinAText = GM.Get().scene.miscRefs[4].GetComponentInChildren<Text>();
            otherPinBText = GM.Get().scene.miscRefs[5].GetComponentInChildren<Text>();
            otherPinCText = GM.Get().scene.miscRefs[6].GetComponentInChildren<Text>();
            extraMultiA = GM.Get().scene.buttonRefs[3].GetComponent<Text>();
            extraMultiB = GM.Get().scene.buttonRefs[4].GetComponent<Text>();
            extraMultiC = GM.Get().scene.buttonRefs[5].GetComponent<Text>();
            toBet = GM.Get().scene.miscRefs[8].GetComponent<Text>();
            gMode = 0;
            leverMode = true;
            leverAnimator = GameObject.FindGameObjectWithTag("lever").GetComponent<Animator>(); //Accessing the animator componenet on the lever object
            result = new float[2];
            sbDef = new shuffleBag(10, defShuffle); //Scale is the number of times the shuffle bag stores multiple copies of the ratio before resetting (essentially maximimum times the best results should repeat kind of)
            sbTempA = new shuffleBag(10, shuffleA);
            sbTempB = new shuffleBag(10, shuffleB);
            sbTempC = new shuffleBag(10, shuffleC);
        }
        
        public void loadSelectedGame()
        {
            Debug.Log("GM.framework Loading");
            initSlots();
            //callMiniGame(MiniGames.something);

        }

        void updateMulti()
        {
            Debug.Log(multiStored.Length);
            extraMultiA.text = multiStored[0].ToString() + " Left";
            extraMultiB.text = multiStored[1].ToString() + " Left";
            extraMultiC.text = multiStored[2].ToString() + " Left";
        }

        IEnumerator spinPins()
        {
            Debug.Log("SpinPins");
            StartCoroutine(stopPins());
            spinning = true;
            while (spinning)
            {
                int newImg = Random.Range(0, 5);
                Sprite tempSprite = Resources.Load<Sprite>("symbols_" + newImg);
                //Debug.Log(tempSprite);
                GM.Get().scene.miscRefs[1].GetComponent<SpriteRenderer>().sprite = tempSprite;
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        IEnumerator stopPins()
        {
            Debug.Log("StopPins");
            yield return new WaitForSeconds(8);
            otherSpinA = false;
            otherSpinB = false;
            otherSpinC = false;
            yield return new WaitForSeconds(0.5f);
            spinning = false;
            for (int i = 0; i < 3; i++)
            {
                switch (results[i+3])
                {
                    case 0:
                        toUse = sbDef;
                        break;
                    case 1:
                        toUse = sbTempA;
                        break;
                    case 2:
                        toUse = sbTempB;
                        break;
                    case 3:
                        toUse = sbTempC;
                        break;
                }
                results[i] = toUse.Next();
            }
            Debug.Log("The Results are PinA = " + results[0] + " / PinB = " + results[1] + " / PinC = " + results[2]);
            leverMode = !leverMode;
            //Martin Animate lever going up automatically here, this will remove the need for a second right click input.
            //Martin use results[0 - 2] for where to stop the image

            callMiniGame(MiniGames.something);
            yield return null;
        }

        IEnumerator spinOtherPinA()
        {
            Debug.Log("SpinA");
            otherSpinA = true;
            while (otherSpinA)
            {
                if (results[3] == 3) results[3] = -1;
                results[3]++;
                otherPinAText.text = results[3].ToString();
                yield return new WaitForSeconds(0.19f);
            }
            yield return null;
        }

        IEnumerator spinOtherPinB()
        {
            Debug.Log("SpinB");
            otherSpinB = true;
            while (otherSpinB)
            {
                if (results[4] == 3) results[4] = -1;
                results[4]++;
                otherPinBText.text = results[4].ToString();
                yield return new WaitForSeconds(0.19f);
            }
            yield return null;
        }

        IEnumerator spinOtherPinC()
        {
            Debug.Log("SpinC");
            otherSpinC = true;
            while (otherSpinC)
            {
                if (results[5] == 3) results[5] = -1;
                results[5]++;
                otherPinCText.text = results[5].ToString();
                yield return new WaitForSeconds(0.19f);
            }
            yield return null;
        }

        void stopOtherPinA()
        {
            Debug.Log("StopA");
            otherSpinA = false;
            return;
        }

        void stopOtherPinB()
        {
            Debug.Log("StopB");
            otherSpinB = false;
            return;
        }

        void stopOtherPinC()
        {
            Debug.Log("StopC");
            otherSpinC = false;
            return;
        }

        //GM.Get().scene.miscRefs[0] = "Welcome to Paper Slots"; <-- Example how to reference stuff
        // miscRef are in editor in _SM game object along with button ref for buttons.

        void callMiniGame(MiniGames mg)
        {
            switch (mg)
            {
                case MiniGames.something:
                    GM.Get().scene.miscRefs[15].SetActive(false);
                    GM.Get().scene.miscRefs[16].SetActive(true);

                    result[0] = 1.0f;
                    result[1] = 0.0f;
                    //enable minigame node / objects and access them (u can use your own script to access GM.Get().scene... scene references
                    // Set result variable (its a array of 2 floats) this first float is the new nultiplier to store, second float is the bonus coins won. 
                    break;
            }
            return;
        }

        //call this at the end of your mini game to take the results
        public void endMiniGame()
        {
            GM.Get().scene.miscRefs[15].SetActive(true);
            GM.Get().scene.miscRefs[16].SetActive(false);
            switch ((int) result[0])
            {
                case 2:
                    multiStored[0]++;
                    break;
                case 3:
                    multiStored[1]++;
                    break;
                case 4:
                    multiStored[2]++;
                    break;
            }
            updateMulti();
            gbp += result[1];
            coinText.text = gbp.ToString();
            //checkHighScore();
        }

        void initSlots()
        {
            toBet.text = bet.ToString();
            results[0] = sbDef.Next();
            results[1] = sbDef.Next();
            results[2] = sbDef.Next();
            //martin update the Pin images here to have random Pins at the start as well.
            updateMulti();
        }

        public void procCmds(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0: //Lever Left Click (level goes up / down)
                    if (leverMode)
                    {//First Pull
                        leverAnimator.SetBool("LeverPulled", true); //makes the lever go down
                        Debug.Log("Lever Down");
                        leverMode = !leverMode;
                        StartCoroutine(spinPins());
                        otherPinState = 0;
                        StartCoroutine(spinOtherPinA());
                        StartCoroutine(spinOtherPinB());
                        StartCoroutine(spinOtherPinC());
                        results[0] = sbDef.Next();
                        results[1] = sbDef.Next();
                        results[2] = sbDef.Next();
                        toUse = sbDef;
                    }
                    else
                    {//Second - Fourth Side Pull
                        
                        if (otherPinState < 3)
                        {
                            leverAnimator.SetTrigger("LeverPulledRight"); //makes the lever go right and back again
                            Debug.Log("Lever Side = " + otherPinState);
                            //Martin Animate Side Pull here and make it go back automatically
                            switch (otherPinState)
                            {
                                case 0:
                                    stopOtherPinA();
                                    break;
                                case 1:
                                    stopOtherPinB();
                                    break;
                                case 2:
                                    stopOtherPinC();
                                    leverAnimator.SetBool("LeverPulled", false); //makes the lever go up
                                    break;
                            }
                            
                            otherPinState++;
                        }
                    }
                    break;
                case 1: //Minus
                    bet--;
                    toBet.text = bet.ToString();
                    break;
                case 2: //Plus
                    bet++;
                    toBet.text = bet.ToString();
                    break;
                case 3: //MultiA
                    Debug.Log("x2");
                    break;
                case 4: //MultiB
                    Debug.Log("x3");
                    break;
                case 5: //MultiC
                    Debug.Log("x4");
                    break;
            }

        }
    }
}
