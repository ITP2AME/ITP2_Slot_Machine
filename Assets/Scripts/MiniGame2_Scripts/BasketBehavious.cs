using UnityEngine;
using System.Collections;

//Deals with the behaviour of the baskets in minigame 2
public class BasketBehavious : MonoBehaviour {

    //Declaration of variables and other useful objects
    bool collided;
    public int AddValue;
    public int SubValue;
    private GameController_2 gameController;
    private ICON_Selector IconType;
    float MoveSpeed = 0.2f; //If you set it to public it will show in the unity GUI
    private Rigidbody2D rb;

    public AudioSource Good_fruit;
    public AudioSource Bad_fruit;



    // Use this for initialization
    void Start()
    {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null){ gameController = GameControllerObject.GetComponent<GameController_2>();}
        if (gameController == null){ Debug.Log("Cannot find 'GameController 2' script");}

    }

    // Update is called once per frame
    void Update()
    {


    }

    //Detects collisions and triggers the correct events in game
    void OnCollisionEnter2D(Collision2D col)
    {   //Destroyes any fruit hitting any basket
        if (col.gameObject.tag == "Cherry_2" ||
           col.gameObject.tag == "Lemon_2" ||
           col.gameObject.tag == "Orange_2" ||
           col.gameObject.tag == "Banana_2")
        {

            Destroy(col.gameObject);

            //If the right fruit hits the correct basket add score
            if (((this.gameObject.tag == "Basket_Lemon"  || this.gameObject.tag == "Basket") && col.gameObject.tag == "Lemon_2")  ||
                ((this.gameObject.tag == "Basket_Cherry" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Cherry_2") ||
                ((this.gameObject.tag == "Basket_Orange" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Orange_2") ||
                ((this.gameObject.tag == "Basket_Banana" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Banana_2"))
            {

                Good_fruit.Play();
                gameController.AddScore(AddValue);
            }

            //Else subtract the score
            else { gameController.SubtractScore(SubValue); Bad_fruit.Play(); }
        }

    }


}
