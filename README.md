# ITP2_Slot_Machine
#Intro to Programming Assignment 2 (Group)

#Slot Machine!

##Team: 
 * Mohammad Ali
 * Martin Skarregaard
 * Elio de Berardinis


##Introduction:

The Aim of this coursework was to create a simple slot machine game in Unity  with the possibility of adding a connecting metagame. 

Our team envisioned a simple 2-D slot machine that triggers some minigames if certain conditions were met after the spinning session (obtaining a certain number of equal symbols). The player would compete to beat a high score by playing as many minigame as possible within the available credits with the possibility of winning extra credits with a hig minigame score or obtaining equal symbols on the slot machine. The minigame were to be easy to pick up and play by anybody, connected to a solid framework with realistic probability logic and presented with a minimalistic art style.

For this purpose Ali was nominated the technical lead, Martin the art Lead and Elio the content Lead. Below the individual contributions are detailed: The final game is built as a PC executable, and the youtube video for the project is below.

https://www.youtube.com/watch?v=_l4ez8yD8lg


##Implementations:

###Ali -

I have a singleton instance as a game manager (see _GM gameobject or Managers/GM ), this game manager instance has variables to any other manager script that we may need access to in the current case the scene (see _SM gameobject or Managers/sceneManager ) and framework (see slotsGF) scripts. The scene manager holds all the references (to objects / ui / prefabs) in the particular scene.The framework is the bulk of the game play code ( score management and probability ratios etc ) excluding small script on objects to add behaviour. There is also a Math class (see Classes/Math ) that does the probability using shuffleBag logic. Essentially the 3 main pins spin randomly (fake spin) until a certain time has passed then shows a result. In the time taken for the pins to stop the result is precalculated using one of 4 different shuffleBags with variating rates for individual outcomes (defShuffle, shuffleA, shuffleB etc. in slotsGF) The shuffleBags are scaled to 3 which seem like a reasonable number of potential repeats (to get a minimum of three possible ‘hardest to get’ elements from the shufflebag). Also the shuffleBag uses a Fisher Yates Shuffle to randomize the elements in the list. 

So when you pull the lever the first time it starts spinning the pins, while they are spinning you have the option to increase your probability of getting a rarer outcome (not the probability of the outcomes being the same) by switching between shufflebags. To do this you click the lever again and then try to click it two more times to stop the secondary pins (the numbers below the picture pins) to stop as close to each other. So for example having all three to stop at 3 will boost your chances to get the most rare pin result (in the primary picture pins).


###Elio -

I designed and developed the 3 mini-games randomly triggered by the slot machine if the player obtains at least 2 symbols of the same kind in the main pins.All mini-games are 2-D and based on simple mechanics and inputs such as mouse click or left/right arrow keys with a fruit theme. The three mini-games are called: “Fruit Catcher”, “Fruit Sorter” and “Click the Fruit”.

In “Fruit catcher”, the player controls a vine basket at the bottom of the screen and his goal is to catch as many fruits as he can in 1 minute. The fruits are constantly falling from the top of the screen and the one to catch is shown in an icon on the upper left corner and changing at set intervals. Collecting a different fruit from the one displayed will deduct points. When 30s and 10s remain the difficulty increases (stronger gravity on the fruits and shorter fruit spawning time) but the rewards/deductions are doubled and tripled respectively. The path of the falling fruit is further randomized by the presence of spinning wood panels in the game scene.

In “Fruit Sorter”, the player has to sort the falling fruits in the correct basket displaying the matching icon (i.e. lemon in the basket displaying the lemon icon). The player can interact with the fruits directly by clicking on them. Sorting the fruits in the wrong baskets or letting them hit the spikes will deduct points. The difficulty increases at 30s and 10s left.

In “Click the Fruit” the fruits are randomly generated at different locations on the screen and start falling down. The player has to click on the currently displayed fruit before they hit the spikes to gain points. If the fruit displayed hit the spikes or the player clicks the wrong fruits points are lost. Once again, difficulty is increased towards the end of the game.

From the technical point of view each minigame has its own Game Controller object and script (GameController, Game_Controller_2, Game_Controller_3 respectively), a fruit generator Object and script (Cherries_Generator, Fruit_Generator, Generator_3 respectively) as well as scripts to control the behaviour of the baskets (in Fruit Catcher and Fruit Sorter) or the interaction with the fruits on click (Fruit Sorter and Click the fruit). Other scripts are present in each minigame to deal with individual properties of the spikes objects, the wood panels and the Current Fruit display icon. The Game Controller Objects keep track of time left to play, the score, difficulty level and connect the minigames with the main slot framework returning the score at the end of each game session.

Besides the minigame side of the project I also contributed to the main framework script by tweaking the logic of the slot machine (Winning/loosing/bonus scenarios), optimized the multiplier, betting and scoring system as well as adding sounds, on screen instruction and Game Over/ Continue screen. Finally, I tested and debugged the finalized project and corrected any detected errors before the submission.


###Martin -

For changing the light bulb a function was created (see slotsGF, changeColor() ) to simplify the main body of the code, when changing the colours of the lightbulbs. The function takes two inputs. A string and a Boolean. The string refer to which lightbulb we want to alter turn on or off, while the Boolean decides whether we are turning the lightbulb of or on.
For spinning the pins/symbols a while-for loop is used (see slotsGF, spinPins() ). Actually the pins are not spinning, but the symbols being displayed are merely being shown randomly. The while loop will keep running until “pinIsSpinning” is false for all pins. This happens in a coroutine that is called just before the while-for loop. The coroutine has a small delay after stopping each pin, to stop the pins at different times and not at once. The while-for loop also makes sure that the same symbol is not shown on the same pin twice in the row. This is done to prevent it looking like the pin does not spin. The audio is also triggered in this while-for loop and an if-statements makes sure that the audio is only triggered once per pin. 
 
In the function stopPins (see slotsGF) the coroutine mentioned before can be seen. A for-loop containing a switch makes sure that the pinIsSpinning is set to false, it stops the spinning sound, plays the pin stop sound and lastly it shows the end result (symbol) that is calculated in the shuffle bag. After the loop, the lever-mode is back to its initial state and the red light is turned off, while the green light is turned on, using the lightbulb-function mentioned earlier.


##Improvements:

In general we feel like we did really good in terms of coordinations and achieving our target. On a personal level I (Ali) think I could improve my template structure to make it easier for non technical people to work in, although both my teammates seemed to grasp it fairly quickly, certain features were not as obvious (like where to find an instance reference to what). There is probably some space for improvement there. Also I felt like the team did not make use of Git effectively (personal / feature branches etc.) which could also be improved upon. 

##Workload Matrix:

 |  Name  |  Responsility | 
 | --- | --- | 
 | Mohammad Ali | Basic scene and code structure / framework | 
 |  | Fisher Yates shuffling lists | 
 |  | shuffleBag implementation | 
 |  | Probability logic | 
 |  | Integration and debugging |
 | --- | --- | 
 | Elio de Berardinis |  Mini-Games design and development.  | 
 |  | Optimization of Betting, multipliers and scoring system.  |  
 |  | Sounds, Instructions and Game Over implementation.  | 
 |  | Testing, debugging and polishing. | 
 | --- | --- | 
 | Martin Skarregaard | Graphics, animations and sounds | 
 |  |  for the slot machine, including  | 
 |  | implementation of these. | 
 |  | Pop-up “sign” graphics including implementation of this. | 

