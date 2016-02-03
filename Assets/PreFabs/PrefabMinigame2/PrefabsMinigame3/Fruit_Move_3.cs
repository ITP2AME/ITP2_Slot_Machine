using UnityEngine;
using System.Collections;

public class Fruit_Move_3 : MonoBehaviour {


    public Rigidbody2D rb;
    float thrust = 100f;
    

    public int AddValue;
    public int SubValue;

    private ICON_Selector_3 IconType;

    private GameController_3 gameController;

    // Use this for initialization
    void Start()
    {
        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject != null)
        {
            gameController = GameControllerObject.GetComponent<GameController_3>();
        }

        if (gameController == null)
        {

            Debug.Log("Cannot find 'GameController 3' script");
        }

        GameObject IconSelectorObject = GameObject.FindWithTag("ICON_Selector");
        if (IconSelectorObject != null)
        {
            IconType = IconSelectorObject.GetComponent<ICON_Selector_3>();
        }

        if (IconSelectorObject == null)
        {

            Debug.Log("Cannot find 'Icon Selector 3' script");
        }

        rb = this.GetComponent<Rigidbody2D>();
        


    }

    // Update is called once per frame
    void Update()
    {

       

    }

    void OnMouseDown()
    {
        // this object was clicked - do something
        


        if (Input.GetMouseButtonDown(0))
        {

            if(((this.gameObject.tag == "Lemon_2") && IconType.GetCurrentFruit() == 1) ||
             ((this.gameObject.tag == "Cherry_2") && IconType.GetCurrentFruit() == 2) ||
             ((this.gameObject.tag == "Orange_2") && IconType.GetCurrentFruit() == 3) ||
             ((this.gameObject.tag == "Banana_2") && IconType.GetCurrentFruit() == 0))
            {
                gameController.AddScore(AddValue);
                
                
            }

            else { gameController.SubtractScore(SubValue); }

           
            Destroy(this.gameObject);

        }



        
    }
}
