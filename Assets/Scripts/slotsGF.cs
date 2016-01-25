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
    public class slotsGF : gameFramework {

        public float gbp = 50.0f;
        public List<float> multiStored = new List<float>();
        public float multiCurrent = 1.0f;
        public int miniGame;
        public float bet = 5.0f;
        public GameMode gMode;
        public string[] helpTexts; //Modify in editor
        public float[] result;
        Text helpText, coinText, pinAText, pinBText, pinCText, otherPinAText, otherPinBText, otherPinCText, currMulti, extraMultiA, extraMultiB, extraMultiC, extraMultiD, extraMultiE, toBet;
        Button coinButton;
        public bool leverMode;
        int[] defShuffle = [4, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0, 0];

        protected override void Awake ()
        {
            helpText = GM.Get().scene.miscRefs[0].GetComponent<Text>();
            coinText = GM.Get().scene.miscRefs[7].GetComponent<Text>();
            pinAText = GM.Get().scene.miscRefs[1].GetComponent<Text>();
            pinBText = GM.Get().scene.miscRefs[2].GetComponent<Text>();
            pinCText = GM.Get().scene.miscRefs[3].GetComponent<Text>();
            otherPinAText = GM.Get().scene.miscRefs[4].GetComponent<Text>();
            otherPinBText = GM.Get().scene.miscRefs[5].GetComponent<Text>();
            otherPinCText = GM.Get().scene.miscRefs[6].GetComponent<Text>();
            currMulti = GM.Get().scene.miscRefs[9].GetComponentInChildren<Text>();
            extraMultiA = GM.Get().scene.miscRefs[10].GetComponentInChildren<Text>();
            extraMultiB = GM.Get().scene.miscRefs[11].GetComponentInChildren<Text>();
            extraMultiC = GM.Get().scene.miscRefs[12].GetComponentInChildren<Text>();
            extraMultiD = GM.Get().scene.miscRefs[13].GetComponentInChildren<Text>();
            extraMultiE = GM.Get().scene.miscRefs[14].GetComponentInChildren<Text>();
            toBet = GM.Get().scene.miscRefs[8].GetComponent<Text>();
            gMode = 0;
            leverMode = true;
        }

        public void loadSelectedGame() {
			Debug.Log ("GM.framework Loading");
            initSlots();
            //callMiniGame(MiniGames.something);

        }

        void updateMulti()
        {
            currMulti.text = "x" + multiCurrent.ToString();
            Debug.Log(multiStored.Count);
            if (multiStored.Count > 0)
            {
                extraMultiA.text = "x" + multiStored[0].ToString();
            }
            if (multiStored.Count > 1)
            {
                extraMultiB.text = "x" + multiStored[1].ToString();
            }
            if (multiStored.Count > 2)
            {
                extraMultiC.text = "x" + multiStored[2].ToString();
            }
            if (multiStored.Count > 3)
            {
                extraMultiD.text = "x" + multiStored[3].ToString();
            }
            if (multiStored.Count > 4)
            {
                extraMultiE.text = "x" + multiStored[4].ToString();
            }
        }
        

        //GM.Get().scene.miscRefs[0] = "Welcome to Paper Slots"; <-- Example how to reference stuff
        // miscref are in editor in _SM game object along with button ref for buttons.

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
        public void endMiniGame ()
        {
            GM.Get().scene.miscRefs[15].SetActive(true);
            GM.Get().scene.miscRefs[16].SetActive(false);
            if (multiStored.Capacity > 4) multiStored.RemoveAt(0);
            multiStored.Add(result[0]);
            updateMulti();
            gbp += result[1];
            coinText.text = gbp.ToString();
            //checkHighScore();
        }

        void initSlots()
        {
            result = new float[2];
            toBet.text = bet.ToString();
            multiStored.Add(2.0f);
            updateMulti();
        }

        public void procCmds(int buttonIndex)
        {
            switch (buttonIndex)
            {
                case 0: //Minus
                    bet--;
                    toBet.text = bet.ToString();
                    break;
                case 1: //Plus
                    bet++;
                    toBet.text = bet.ToString();
                    break;
                case 2: //Lever
                    if (leverMode)
                    {//First Pull
                        leverMode = !leverMode;
                        //start Spin / Shuffle Bag
                    } else
                    {//Second-Fourth Side Pull

                    }
                    //Playanimation
                    break;
                case 3: //Multi
                    break;
                case 4: //MultiA
                    break;
                case 5: //MultiB
                    break;
                case 6: //MultiC
                    break;
                case 7: //MultiD
                    break;
                case 8: //MultiE
                    break;
            }

        }
    }
}
