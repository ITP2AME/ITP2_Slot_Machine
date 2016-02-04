using UnityEngine;
using System.Collections;

public class ICON_Selector : MonoBehaviour {

    public Sprite Cherry_ICON;
    public Sprite Lemon_ICON;
    public Sprite Orange_ICON;
    public Sprite Banana_ICON;
    private int FruitIndex;

    private GameController gameController;

    Sprite CurrentFruit;


    float timer;
    float delay;

    void Start() {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");

        if (GameControllerObject != null) {gameController = GameControllerObject.GetComponent<GameController>();}
        if (gameController == null)
        {Debug.Log("Cannot find 'GameController' script");}

        timer = 10f;
        delay = 10f;

        RandomGenerator();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;

    }

    void Update() {

        if (gameController.timeRemaining <= 30 && gameController.timeRemaining > 10) { delay = 5; }
        if (gameController.timeRemaining <= 10) { delay = 3; }

        timer -= Time.deltaTime;
        if (timer <= 0) {

            RandomGenerator();
            this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;
            timer = delay;
        }

    }

    public int GetCurrentFruit()
    {

        return FruitIndex;
    }


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
