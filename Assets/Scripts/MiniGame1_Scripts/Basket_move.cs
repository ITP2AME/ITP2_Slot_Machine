using UnityEngine;
using System.Collections;

//Deals with the basket behaviour in Minigame 1
public class Basket_move : MonoBehaviour {

    //Declaration of variables and other useful objects
    bool collided;
    public int AddValue;
    public int SubValue;
    private GameController gameController;
    private ICON_Selector IconType;
    float MoveSpeed = 0.2f; //If you set it to public it will show in the unity GUI
    private Rigidbody2D rb;

    public AudioSource Good_fruit;
    public AudioSource Bad_fruit;

    
    // Use this for initialization
    void Start () {


        rb = GetComponent<Rigidbody2D>();

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null) { gameController = GameControllerObject.GetComponent<GameController>();}
        if (gameController == null)       { Debug.Log("Cannot find 'GameController' script");}


        GameObject IconSelectorObject = GameObject.FindWithTag("ICON_Selector");
        if (IconSelectorObject != null) { IconType = IconSelectorObject.GetComponent<ICON_Selector>();}
        if (IconSelectorObject == null){ Debug.Log("Cannot find 'Icon Selector' script");}


    }

	// Update is called once per frame
	void Update () {

        HandleMovement();

    }

    //Function to move the basket
    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow)) {this.transform.Translate(MoveSpeed, 0, 0);}
        if (Input.GetKey(KeyCode.LeftArrow))  { this.transform.Translate(-MoveSpeed, 0, 0); }
    }

    //Function to detect the collision 
    void OnCollisionEnter2D(Collision2D col)
    {
       
        //Destroys any fruit hitting the basket
        if (col.gameObject.tag == "Cherry" ||
           col.gameObject.tag == "Lemon"   ||
           col.gameObject.tag == "Orange"  ||
           col.gameObject.tag == "Banana")
        {

            Destroy(col.gameObject);

            //If the fruit is the one displayed in the ICON adds points to the score
            if ((IconType.GetCurrentFruit() == 1 && col.gameObject.tag == "Lemon") ||
                (IconType.GetCurrentFruit() == 2 && col.gameObject.tag == "Cherry") ||
                (IconType.GetCurrentFruit() == 3 && col.gameObject.tag == "Orange") ||
                (IconType.GetCurrentFruit() == 0 && col.gameObject.tag == "Banana"))
            {

                Good_fruit.Play();
                gameController.AddScore(AddValue);
            }

            //Otherwise subtracts them
            else { gameController.SubtractScore(SubValue); Bad_fruit.Play(); }
        }

    }


}
