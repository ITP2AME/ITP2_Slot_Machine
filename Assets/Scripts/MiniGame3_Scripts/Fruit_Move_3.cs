using UnityEngine;
using System.Collections;

//Script for interaction with the fruits in Minigame 3
public class Fruit_Move_3 : MonoBehaviour {

    //Declaration of Variables and other useful objects
    public Rigidbody2D rb;
    float thrust = 100f;
    public int AddValue;
    public int SubValue;
    private ICON_Selector_3 IconType;
    private GameController_3 gameController;

    // Use this for initialization
    void Start()
    {
        //Instantiation of The game controller, Icon selector and rigid body component
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null) { gameController = GameControllerObject.GetComponent<GameController_3>();}
        if (gameController == null){Debug.Log("Cannot find 'GameController 3' script");}

        GameObject IconSelectorObject = GameObject.FindWithTag("ICON_Selector");
        if (IconSelectorObject != null){IconType = IconSelectorObject.GetComponent<ICON_Selector_3>();}
        if (IconSelectorObject == null) {Debug.Log("Cannot find 'Icon Selector 3' script"); }

        rb = this.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Function to detect interaction of mouse with fruits
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //If the right fruit (corresponding to the icon) is clicked points are added
            if(((this.gameObject.tag == "Lemon_2") && IconType.GetCurrentFruit() == 1) ||
             ((this.gameObject.tag == "Cherry_2") && IconType.GetCurrentFruit() == 2) ||
             ((this.gameObject.tag == "Orange_2") && IconType.GetCurrentFruit() == 3) ||
             ((this.gameObject.tag == "Banana_2") && IconType.GetCurrentFruit() == 0))
            {
                gameController.AddScore(AddValue);
                
                
            }

            //Otherwise points are subtracted
            else { gameController.SubtractScore(SubValue); }

           //Fruits are then destroyed
            Destroy(this.gameObject);

        }
    
    }
}
