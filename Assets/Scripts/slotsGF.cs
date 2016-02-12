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

    //List of Game States
    public enum GameMode
    {
        Slots = 0,
        MiniGame = 1,
    }

    //List of the minigames
    public enum MiniGames
    {
        mini1,
        mini2,
        mini3,
    }

    //Main Framework class
    public class slotsGF : gameFramework
    {
        //Declaration of Global Variables and other useful objects
        private float gbp = 10.0f; //Credits
        public float TotScore = 0.0f; //Total current Score
        public float multiCurrent = 1.0f; // Current multiplier
        public int miniGame;
        public int miniGame_Type;
        private float bet = 1.0f;
        public GameMode gMode;
        public string[] helpTexts; //Modify in editor
        public float[] result;
        Text helpText, coinText, pinAText, pinBText, pinCText, otherPinAText, otherPinBText, otherPinCText, extraMultiA, extraMultiB, extraMultiC, toBet,TotalScore,CurrentMultUsed,FinalGameScore,HighScore,Display_HighScore;
        Button coinButton;
        bool UsedX2 = false;
        bool UsedX3 = false;
        bool UsedX4 = false;
        bool tutorialON = false;

        //variable for controlling the lever
        public bool leverMode;
        public GameObject leverGameObject; //a reference to the lever game object is declared
        public Animator leverAnimator; //An animator controlling the lever is declared
        AudioSource[] leverAudio; //a reference to the lever audio is declared
        AudioSource soundLeverDown;
        AudioSource soundLeverRight;

        //variable for controlling the pins
        Sprite[] pinSymbols;
        public Sprite pinSymbolGrape;
        public Sprite pinSymbolBell;
        public Sprite pinSymbolDimond;
        public Sprite pinSymbolSeven;
        public Sprite pinSymbolCherry;
        public Sprite pinSymbolClover;

        bool[] pinIsSpinning;
        AudioSource[] pin1Audio; 
        AudioSource[] pin2Audio; 
        AudioSource[] pin3Audio; 
        AudioSource[][] allPinAudio;
        public AudioSource Extra_Credit_sound;
        public AudioSource GameOver_sound;
        public AudioSource MiniStarts;
        public AudioSource No_Equal_Symb;

        //Variables for minigame text info
        GameObject miniGameText;
        GameObject miniGamePopUp;
        
        //Variables to access the minigames game controllers
        public GameController_3 gameController_3;
        public GameController_2 gameController_2;
        public GameController gameController_1;

        //Shuffle bags for random drawing of pin symbols
        bool otherSpinA, otherSpinB, otherSpinC = false;
        int[] multiStored = new int[] { 1, 0, 0 };
        int[] defShuffle = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0 };
        int[] shuffleA = new int[] { 5, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0, 0 };
        int[] shuffleB = new int[] { 5, 5, 4, 4, 4, 3, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0, 0 };
        int[] shuffleC = new int[] { 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 2, 2, 2, 1, 1, 1, 1, 0, 0 };

        

        shuffleBag toUse;
        shuffleBag sbDef; //default shuffleBag random result
        shuffleBag sbTempA; //Custom shufflebag random result for when modified by secondary Pins
        shuffleBag sbTempB; //Custom shufflebag random result for when modified by secondary Pins
        shuffleBag sbTempC; //Custom shufflebag random result for when modified by secondary Pins
        int otherPinState = 0;
        int[] results = new int[] { 0, 0, 0, 0, 0, 0}; // Vector holding values for the pins symbols. first three for main Pins, second three for secondary Pins

      //Init Function
        protected override void Awake()
        {
            //Settin up references to Components listed in the GM script (Buttons and other 'miscellaneous' references such as texts, sounds etc.)
            GM.Get().data.initSaveGame();
            CurrentMultUsed = GM.Get().scene.miscRefs[14].GetComponent<Text>();
            helpText = GM.Get().scene.miscRefs[0].GetComponent<Text>();
            coinText = GM.Get().scene.miscRefs[7].GetComponent<Text>();
            pinAText = GM.Get().scene.miscRefs[1].GetComponent<Text>();
            pinBText = GM.Get().scene.miscRefs[2].GetComponent<Text>();
            pinCText = GM.Get().scene.miscRefs[3].GetComponent<Text>();
            TotalScore = GM.Get().scene.miscRefs[13].GetComponent<Text>();
            HighScore = GM.Get().scene.miscRefs[17].GetComponent<Text>();
            Display_HighScore = GM.Get().scene.miscRefs[22].GetComponent<Text>();
            FinalGameScore = GM.Get().scene.miscRefs[9].GetComponent<Text>();
            otherPinAText = GM.Get().scene.miscRefs[4].GetComponentInChildren<Text>();
            otherPinBText = GM.Get().scene.miscRefs[5].GetComponentInChildren<Text>();
            otherPinCText = GM.Get().scene.miscRefs[6].GetComponentInChildren<Text>();
            extraMultiA = GM.Get().scene.buttonRefs[3].GetComponentInChildren<Text>();
            extraMultiB = GM.Get().scene.buttonRefs[4].GetComponentInChildren<Text>();
            extraMultiC = GM.Get().scene.buttonRefs[5].GetComponentInChildren<Text>();
            toBet = GM.Get().scene.miscRefs[8].GetComponent<Text>();

            //Setting up game states and activate/deactivate specific elements
            GM.Get().scene.miscRefs[23].SetActive(false);
            GM.Get().scene.miscRefs[24].SetActive(false);
            gMode = 0;


            //lever variables are innitalized
            leverMode = true;
            leverGameObject = GameObject.FindGameObjectWithTag("lever"); //Accessing the lever game object
            leverAnimator = leverGameObject.GetComponent<Animator>(); //Accessing the animator componenet on the lever object
            leverAudio = leverGameObject.GetComponents<AudioSource>(); //Accessing all the sound componenets on the lever object
            soundLeverDown = leverAudio[0]; //references to specific audio sources
            soundLeverRight = leverAudio[1];


            //pin variables are initialized
            pinSymbols = new Sprite[6];
            pinSymbols[0] = pinSymbolGrape;
            pinSymbols[1] = pinSymbolBell;
            pinSymbols[2] = pinSymbolDimond;
            pinSymbols[3] = pinSymbolSeven;
            pinSymbols[4] = pinSymbolCherry;
            pinSymbols[5] = pinSymbolClover;

            pinIsSpinning = new bool [4];
            allPinAudio = new AudioSource[4][];

          //Audio for pins
            allPinAudio[1] = GM.Get().scene.miscRefs[1].GetComponents<AudioSource>();
            allPinAudio[2] = GM.Get().scene.miscRefs[2].GetComponents<AudioSource>();
            allPinAudio[3] = GM.Get().scene.miscRefs[3].GetComponents<AudioSource>();

            //Other audio sources initialized
            Extra_Credit_sound = GM.Get().scene.miscRefs[18].GetComponent<AudioSource>();
            GameOver_sound = GM.Get().scene.miscRefs[19].GetComponent<AudioSource>();
            MiniStarts = GM.Get().scene.miscRefs[20].GetComponent<AudioSource>();
            No_Equal_Symb = GM.Get().scene.miscRefs[21].GetComponent<AudioSource>();

            //Setting up instances for other minigame-related text objects
            miniGameText = GameObject.FindGameObjectWithTag("miniGameText");
            miniGamePopUp = GameObject.FindGameObjectWithTag("miniGamePopUp");

            changeColor("greenLightbulp", true);
            changeColor("redLightbulp", false);


            result = new float[2];//Vector containing at position 0 information about the multiplier and at position 1 the output score of a minigame
            sbDef = new shuffleBag(3, defShuffle); //Scale is the number of times the shuffle bag stores multiple copies of the ratio before resetting (essentially maximimum times the best results should repeat kind of)
            sbTempA = new shuffleBag(3, shuffleA);
            sbTempB = new shuffleBag(3, shuffleB);
            sbTempC = new shuffleBag(3, shuffleC);
           

        }

        //function responsible for changing the light Bulbs color
        void changeColor(string whichObject, bool TrueOnFalseOff)
        {
            //local variable for controlling the lightbulps
            GameObject[] lightbulps;
            GameObject greenLightBulp;
            GameObject redLightBulp;
            SpriteRenderer greenLightBulpRenderer;
            SpriteRenderer redLightBulpRenderer;

            lightbulps = GameObject.FindGameObjectsWithTag("lightBulp");
            greenLightBulp = lightbulps[0];
            redLightBulp = lightbulps[1];

            Color greenLightBulpOn = new Color(0f, 255/255f, 56/255f, 1f);
            Color greenLightBulpOff = new Color(133/255f, 154/255f, 141/255f, 1f);
            Color redLightBulpOn = new Color(255/255f, 0f, 0f, 1f);
            Color redLightBulpOff = new Color(162/255f, 107/255f, 107/255f, 1f);


            if (whichObject == "greenLightbulp" && TrueOnFalseOff == true)
            {
                greenLightBulpRenderer = greenLightBulp.GetComponent<SpriteRenderer>();
                greenLightBulpRenderer.color = greenLightBulpOn;
            }
            else if (whichObject == "greenLightbulp" && TrueOnFalseOff == false)
            {
                greenLightBulpRenderer = greenLightBulp.GetComponent<SpriteRenderer>();
                greenLightBulpRenderer.color = greenLightBulpOff;
            }

            if (whichObject == "redLightbulp" && TrueOnFalseOff == true)
            {
                redLightBulpRenderer = redLightBulp.GetComponent<SpriteRenderer>();
                redLightBulpRenderer.color = redLightBulpOn;
            }
            else if (whichObject == "redLightbulp" && TrueOnFalseOff == false)
            {
                redLightBulpRenderer = redLightBulp.GetComponent<SpriteRenderer>();
                redLightBulpRenderer.color = redLightBulpOff;
            }

        }
      
        //Function to load a minigame  
        public void loadSelectedGame()
        {
            Debug.Log("GM.framework Loading");
            initSlots();
            miniGamePopUp.SetActive(false);
        }

        //Function to update the Multipliers
        void updateMulti()
        {
            Debug.Log(multiStored.Length);
            extraMultiA.text = multiStored[0].ToString() + " Left";
            extraMultiB.text = multiStored[1].ToString() + " Left";
            extraMultiC.text = multiStored[2].ToString() + " Left";

            if      (UsedX2) { CurrentMultUsed.text = "X2".ToString(); }
            else if (UsedX3) { CurrentMultUsed.text = "X3".ToString(); }
            else if (UsedX4) { CurrentMultUsed.text = "X4".ToString(); }
            else             { CurrentMultUsed.text = "None".ToString(); }
        }

        //Function to spin the images on the slot machine
        IEnumerator spinPins()
        {
            Debug.Log("SpinPins");
            StartCoroutine(stopPins());

            //while spinning the symbol of the three pins change randomly at a certain speed. No symbol is shown multiple times in a row on the same pin (to prevent it looking like the pin is stuck)
            for (int pinN = 1; pinN < 4; pinN++)
                pinIsSpinning[pinN] = true;
            int randomSymbol = 0; //variable refering to pin symbols
            int tempRandomymbol = 0; //variable storing last symbol showed onpin
            int pinNumber =1; //variable storing which pin is affected
            while (pinIsSpinning[1] == true || pinIsSpinning[2] == true || pinIsSpinning[3] == true)
            {
                for (;pinNumber<=3;pinNumber++)
                {
                    if (pinIsSpinning[pinNumber] == true)
                    {
                        randomSymbol = Random.Range(0, 6);
                        while (randomSymbol == tempRandomymbol)
                            randomSymbol = Random.Range(0, 6);
                        tempRandomymbol = randomSymbol;
                        GM.Get().scene.miscRefs[pinNumber].GetComponent<SpriteRenderer>().sprite = pinSymbols[randomSymbol];

                        //play audio of spinning pins if not already playing
                        if (allPinAudio[pinNumber][0].isPlaying == false)
                            allPinAudio[pinNumber][0].Play();
                    }

                }
                pinNumber = 1;
                yield return new WaitForSeconds(0.05f);
            }
            yield return null;
        }

        //Function to stop the pins from spinning, get the results and start a minigame if at least 2 equal symbols are drawn
        IEnumerator stopPins()
        {
            Debug.Log("StopPins");
            yield return new WaitForSeconds(5);
            otherSpinA = false;
            otherSpinB = false;
            otherSpinC = false;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < 3; i++)
            {
                //Switch between shuffle bags to draw from based on the second pins results
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

            leverAnimator.SetBool("LeverPulled", false); //makes the lever go up
            soundLeverDown.Play(); // plays a lever sound

            for (int i = 1; i<4; i++)
            {
                
                pinIsSpinning[i] = false;
                allPinAudio[i][0].Stop();
                allPinAudio[i][1].Play();
                switch (results[i-1])
                {
                    case 0:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolGrape;
                        break;
                    case 1:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolBell;
                        break;
                    case 2:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolCherry;
                        break;
                    case 3:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolSeven;
                        break;
                    case 4:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolDimond;
                        break;
                    case 5:
                        GM.Get().scene.miscRefs[i].GetComponent<SpriteRenderer>().sprite = pinSymbolClover;
                        break;
                }
                yield return new WaitForSeconds(1.0f);
            }

            
            leverMode = !leverMode;
            changeColor("greenLightbulp", true);
            changeColor("redLightbulp", false);


            //Starting minigame if at least 2 equal symbols are drawn
            if (results[0] == results[1] || results[0] == results[2] || results[1] == results[2])
            {   
                //If all symbols are equal, extra credits are awarded
                if ((results[0] == results[1]) && (results[1] == results[2]))
                {
                    if      (results[0] == 0) { gbp += bet * 2; }
                    else if (results[0] == 1) { gbp += bet * 3; }
                    else if (results[0] == 2) { gbp += bet * 4; }
                    else if (results[0] == 3) { gbp += bet * 5; }
                    else if (results[0] == 4) { gbp += bet * 10; }
                    else if (results[0] == 5) { gbp += bet * (Random.Range(0, 10)); }

                    Extra_Credit_sound.Play();
                }

                //If at least one clover is drawn bet credits are awarded
                else if (results[0] == 5 || results[1] == 5 || results[2] == 5)
                { gbp += bet;
                  Extra_Credit_sound.Play();
                  coinText.text = gbp.ToString();
                }

                coinText.text = gbp.ToString();
                int RandomMini = Randomizer();
                yield return new WaitForSeconds(1);
                MiniStarts.Play();
                miniGamePopUp.SetActive(true);

                //Display the name of the random minigame selected
                if      (RandomMini == 0) { miniGameText.GetComponent<Text>().text = "Fruit Catcher"; }
                else if (RandomMini == 1) { miniGameText.GetComponent<Text>().text = "Fruit Sorter"; }
                else if (RandomMini == 2) { miniGameText.GetComponent<Text>().text = "Click The Fruit"; }

                gMode = GameMode.MiniGame;
                yield return new WaitForSeconds(5);
                miniGamePopUp.SetActive(false);
              
                //Start the minigame randomly selected
                switch (RandomMini)
                {
                    case 0:
                        miniGame_Type = 1;
                        callMiniGame(MiniGames.mini1);
                        break;
                    case 1:
                        miniGame_Type = 2;
                        callMiniGame(MiniGames.mini2);
                        break;
                    case 2:
                        miniGame_Type = 3;
                        callMiniGame(MiniGames.mini3);
                        break;
                }

            }
            
            //If all symbols are different, reset the multipliers (if applied) and award bet credits if at least one element is a clover
            else
            {
                if (results[0] == 5 || results[1] == 5 || results[2] == 5)
                { gbp += bet;
                  Extra_Credit_sound.Play();
                  coinText.text = gbp.ToString();
                  yield return new WaitForSeconds(1);
                }

                UsedX2 = false;
                UsedX3 = false;
                UsedX4 = false;

                updateMulti();
                No_Equal_Symb.Play();
                checkHighScore();
            }

            yield return null;
        }

        //Function that returns a random number
        int Randomizer()
        {
            int randomInt = Random.Range(0, 3);
            return randomInt;
        }

        //Functions that controls the spinning of the other pins (numbered ones) that control the shuffle bag to draw from for the main pins
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

        //Functions to stop the spinning of the other pins
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

 //Function to load the minigames
        void callMiniGame(MiniGames mg)
        {
            switch (mg)
            {
                case MiniGames.mini1:
                    GM.Get().scene.miscRefs[15].SetActive(false);
                    GM.Get().scene.miscRefs[10].SetActive(true);
                    gameController_1.Init();
                    break;

                case MiniGames.mini2:
                    GM.Get().scene.miscRefs[15].SetActive(false);
                    GM.Get().scene.miscRefs[11].SetActive(true);
                    gameController_2.Init();
                    break;

                case MiniGames.mini3:
                    GM.Get().scene.miscRefs[15].SetActive(false);
                    GM.Get().scene.miscRefs[12].SetActive(true);
                    gameController_3.Init();
                    break;
            }
            return;
        }

        //called at the end of your mini game to take the results and switch back to slot machine mode
        public void endMiniGame()
        {
            gMode = GameMode.Slots;
            GM.Get().scene.miscRefs[15].SetActive(true);
            GM.Get().scene.miscRefs[10].SetActive(false);
            GM.Get().scene.miscRefs[11].SetActive(false);
            GM.Get().scene.miscRefs[12].SetActive(false);
          
            updateMulti();

            switch (miniGame_Type)
            {
                case 1:
                    result[1] = gameController_1.Score;
                    break;
                case 2:
                    result[1] = gameController_2.Score;
                    break;
                case 3:
                    result[1] = gameController_3.Score;
                    break;
            }

            

            //Total Game Score Update. The score of the minigame is added to the total game score. The minigame score is also multiplied by 2,3 or 4 is a multiplier is selected by the player
            if      (UsedX2) { TotScore += result[1] * 2; UsedX2 = false; }
            else if (UsedX3) { TotScore += result[1] * 3; UsedX3 = false; }
            else if (UsedX4) { TotScore += result[1] * 4; UsedX4 = false; }
            else { TotScore += result[1]; }
            TotalScore.text = TotScore.ToString();

            //Coin Update. Extra cfedits are added according to the minigame score (before multiplier is applied)
            if       (result[1] < 100.0f )                       { gbp += 0.0f; result[0] = 1.0f; }
            else  if (result[1] >= 100.0f && result[1] < 300.0f) { gbp += 1.0f; result[0] = 2.0f; Extra_Credit_sound.Play();}
            else if  (result[1] >= 300.0f && result[1] < 400.0f) { gbp += 3.0f; result[0] = 3.0f; Extra_Credit_sound.Play();}
            else if  (result[1] >= 400.0f)                       { gbp += 5.0f; result[0] = 4.0f; Extra_Credit_sound.Play();}
            coinText.text = gbp.ToString();

            //Multiplier update. Extra multipliers are awarded as well according to the score.
            switch ((int)result[0])
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
                case 1: break;
            }

            updateMulti();
            checkHighScore();
        }

        //This function updates the total High Score for the game and triggers a gameover if there are no more credits available
        void checkHighScore()
        {
            if (gMode == GameMode.Slots)
            {
                if (TotScore > GM.Get().data.highScore[0])
                {
                    
                    GM.Get().data.highScore[0] = TotScore;
                    GM.Get().data.saveGame();
                    Display_HighScore.text = "High Score:   " + GM.Get().data.highScore[0].ToString();
                }
                Debug.Log(GM.Get().data.highScore[0]);

                if (gbp <= 0)
                {
                    //Call End game
                    FinalGameScore.text = "Your Score: " + TotScore.ToString();
                    HighScore.text = "High Score: " + GM.Get().data.highScore[0].ToString();
                    GM.Get().scene.miscRefs[15].SetActive(false);
                    GM.Get().scene.miscRefs[16].SetActive(true);
                    GM.Get().data.saveGame();
                    GameOver_sound.Play();

                }

            }
        }

        //Initializes some slot machine values
        void initSlots()
        {
            toBet.text = bet.ToString();
            results[0] = sbDef.Next();
            results[1] = sbDef.Next();
            results[2] = sbDef.Next();
            updateMulti();
            Display_HighScore.text = "High Score:   " + GM.Get().data.highScore[0].ToString();
        }

        //Resets all slot machine values to default if a player hits "Play Again" after a game over
        void resetSlot() {

            GM.Get().scene.miscRefs[16].SetActive(false);
            GM.Get().scene.miscRefs[15].SetActive(true);

            GM.Get().scene.miscRefs[23].SetActive(false);
            GM.Get().scene.miscRefs[24].SetActive(false);


            gbp = 10.0f;
            coinText.text = gbp.ToString();

            TotScore = 0.0f;
            TotalScore.text = TotScore.ToString();

            multiCurrent = 1.0f;
            multiStored[0] = 1;
            multiStored[1] = 0;
            multiStored[2] = 0;
            updateMulti();

            bet = 1.0f;
            toBet.text = bet.ToString();

            Display_HighScore.text = "High Score:   " + GM.Get().data.highScore[0].ToString();

        }

//Function that deals with the buttons on the slot machine assigned in buttonrefs vector in _GM
        public void procCmds(int buttonIndex)
        {
            Debug.Log(gMode);
            switch (buttonIndex)
            {
                //Lever button for main pins spin
                case 0:
                    if (gMode == GameMode.Slots)
                    {
                        if (leverMode && (gbp>=bet))
                        {   //First Pull
                            leverAnimator.SetBool("LeverPulled", true); //makes the lever go down
                            soundLeverDown.Play(); // plays a lever sound
                            changeColor("greenLightbulp", false);
                            changeColor("redLightbulp", true);
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
                            gbp -= bet;
                            coinText.text = gbp.ToString();
                        }
                        else
                        {//Second to Fourth Side Pull to control the spinning of the other pins (numbered ones)

                            if (otherPinState < 3)
                            {
                                leverAnimator.SetTrigger("LeverPulledRight"); //makes the lever go right and back again
                                soundLeverRight.Play();                      // plays a lever sound
                                Debug.Log("Lever Side = " + otherPinState);
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
                                        break;
                                }

                                otherPinState++;
                            }
                        }
                    }
                    break;

                case 1: // Bets Minus button 
                    if (gMode == GameMode.Slots && bet > 1 && leverMode)
                    {
                        bet--;
                        toBet.text = bet.ToString();
                    }
                    break;

                case 2: // Bets Plus button
                    if (gMode == GameMode.Slots && bet < gbp && leverMode)
                    {
                        bet++;
                        toBet.text = bet.ToString();
                    }
                    break;

                case 3: //Multiplier X2
                    if (gMode == GameMode.Slots && leverMode)
                    {
                        if (multiStored[0] != 0 && !UsedX2 && !UsedX3 && !UsedX4)
                        {
                            multiStored[0]--;
                            UsedX2 = true;
                        }
                        updateMulti();
                    }
                    break;

                case 4: //Multiplier X3
                    if (gMode == GameMode.Slots && leverMode)
                    {
                        if (multiStored[1] != 0 && !UsedX2 && !UsedX3 && !UsedX4)
                        {
                            multiStored[1]--;
                            UsedX3 = true;
                        }
                        updateMulti();
                    }
                    break;

                case 5: //Multiplier X4
                    if (gMode == GameMode.Slots && leverMode)
                    {
                        if (multiStored[2] != 0 && !UsedX2 && !UsedX3 && !UsedX4)
                        {
                            multiStored[2]--;
                            UsedX4 = true;
                        }
                        updateMulti();
                    }
                    break;

                case 6: //Play Again button at gameover
                    resetSlot();
                    break;

                case 7: //Tutorial Text on and off
                    if (tutorialON) { GM.Get().scene.miscRefs[23].SetActive(false);
                                     GM.Get().scene.miscRefs[24].SetActive(false);
                                     tutorialON = false; }

                    else            { GM.Get().scene.miscRefs[23].SetActive(true);
                                      GM.Get().scene.miscRefs[24].SetActive(true);
                                      tutorialON = true; }
                    break;

                case 8: //Game bonus text on and off
                    Debug.Log("Tutorial pressed");
                    if (tutorialON)
                    {
                        GM.Get().scene.miscRefs[23].SetActive(false);
                        GM.Get().scene.miscRefs[24].SetActive(false);
                        tutorialON = false;
                    }
                    else
                    {
                        GM.Get().scene.miscRefs[23].SetActive(true);
                        GM.Get().scene.miscRefs[24].SetActive(true);
                        tutorialON = true;
                    }
                    break;
            }

        }
    }
}
