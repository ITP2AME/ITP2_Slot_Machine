using UnityEngine;
using System.Collections;

public class BasketBehavious : MonoBehaviour {

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
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<GameController_2>();
        }

        if (gameController == null)
        {

            Debug.Log("Cannot find 'GameController 2' script");
        }


        


    }

    // Update is called once per frame
    void Update()
    {

        //HandleMovement();


    }


    //void HandleMovement()
    //{
       


    //}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision Enter");

        if (col.gameObject.tag == "Cherry_2" ||
           col.gameObject.tag == "Lemon_2" ||
           col.gameObject.tag == "Orange_2" ||
           col.gameObject.tag == "Banana_2")
        {

            Destroy(col.gameObject);

            if (((this.gameObject.tag == "Basket_Lemon"  || this.gameObject.tag == "Basket") && col.gameObject.tag == "Lemon_2")  ||
                ((this.gameObject.tag == "Basket_Cherry" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Cherry_2") ||
                ((this.gameObject.tag == "Basket_Orange" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Orange_2") ||
                ((this.gameObject.tag == "Basket_Banana" || this.gameObject.tag == "Basket") && col.gameObject.tag == "Banana_2"))
            {

                Good_fruit.Play();
                gameController.AddScore(AddValue);
            }

            else { gameController.SubtractScore(SubValue); Bad_fruit.Play(); }
        }

    }


}
