using UnityEngine;
using System.Collections;

//Code to display wich fruit to catch
public class ICON_Selector_3 : MonoBehaviour {

    //Declaration of variables and other useful Objects
    public Sprite Cherry_ICON;
    public Sprite Lemon_ICON;
    public Sprite Orange_ICON;
    public Sprite Banana_ICON;
    private int FruitIndex;
    private GameController_3 gameController;
    Sprite CurrentFruit;
    float timer;
    float delay;

    //Initializes things
    void Start()
    {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");

        if (GameControllerObject != null) { gameController = GameControllerObject.GetComponent<GameController_3>(); }
        if (gameController == null)       { Debug.Log("Cannot find 'GameController 3' script"); }

        timer = 5.0f;
        delay = 5.0f;

        RandomGenerator();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;

    }

    //Called every frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        if (timer <= 0)
        {

            RandomGenerator();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;
            timer = delay;
        }

    }

    //Function that returns the currently displayed fruit icon
    public int GetCurrentFruit()
    {

        return FruitIndex;
    }

    //Function to Randomly generates the fruit to display
    void RandomGenerator()
    {

        int randomInt = Random.Range(0, 4);
        FruitIndex = randomInt;

        if (randomInt == 1) { CurrentFruit = Lemon_ICON; }
        else if (randomInt == 2) { CurrentFruit = Cherry_ICON; }
        else if (randomInt == 3) { CurrentFruit = Orange_ICON; }
        else { CurrentFruit = Banana_ICON; }
    }


}
