using UnityEngine;
using System.Collections;

public class ICON_Selector : MonoBehaviour {

    public Sprite Cherry_ICON;
    public Sprite Lemon_ICON;
    public Sprite Orange_ICON;
    public Sprite Banana_ICON;

    Sprite CurrentFruit;
    Sprite OtherFruit;

    float timer = 5f;
    float delay = 5f;

    void Start() {

        RandomGenerator();
        RandomGenerator2();
        this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;

    }

    void Update() {

        timer -= Time.deltaTime;
        if (timer <= 0) {

            RandomGenerator();
            RandomGenerator2();

            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == CurrentFruit)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = OtherFruit;
                timer = delay;
                return;
            }

            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == OtherFruit)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = CurrentFruit;
                timer = delay;
                return;
            }


        }

    }


    void RandomGenerator()
    {

        int randomInt = Random.Range(0, 4);

        if (randomInt == 1) { CurrentFruit = Lemon_ICON; }
        else if (randomInt == 2) { CurrentFruit = Cherry_ICON; }
        else if (randomInt == 3) { CurrentFruit = Orange_ICON; }
        else { CurrentFruit = Banana_ICON; }
    }

    void RandomGenerator2()
    {

        int randomInt = Random.Range(0, 4);

        if (randomInt == 1) { OtherFruit = Lemon_ICON; }
        else if (randomInt == 2) { OtherFruit = Cherry_ICON; }
        else if (randomInt == 3) { OtherFruit = Orange_ICON; }
        else { OtherFruit = Banana_ICON; }
    }
}
