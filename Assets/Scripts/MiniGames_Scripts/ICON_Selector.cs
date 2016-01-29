using UnityEngine;
using System.Collections;

public class ICON_Selector : MonoBehaviour {

    public Sprite Cherry_ICON;
    public Sprite Lemon_ICON;
    public Sprite Orange_ICON;
    public Sprite Banana_ICON;
    private int FruitIndex;

    Sprite CurrentFruit;
    Sprite OtherFruit;

    float timer = 10f;
    float delay = 10f;

    void Start() {

        RandomGenerator();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;

    }

    void Update() {

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
