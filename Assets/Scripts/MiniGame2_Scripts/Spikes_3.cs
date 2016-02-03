using UnityEngine;
using System.Collections;

public class Spikes_3 : MonoBehaviour {

    
    private GameController_3 gameController_3;
    public int SubValue;
    public AudioSource Fruit_Smash;
    private ICON_Selector_3 IconType;

    // Use this for initialization
    void Start()
    {

       
        GameObject GameControllerObject_3 = GameObject.FindWithTag("GameController");
        if (GameControllerObject_3 != null)
        {
            gameController_3 = GameControllerObject_3.GetComponent<GameController_3>();
        }

        if (gameController_3 == null)
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

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {

      

        if (col.gameObject.tag == "Cherry_2" ||
        col.gameObject.tag == "Lemon_2" ||
        col.gameObject.tag == "Orange_2" ||
        col.gameObject.tag == "Banana_2")
        {

            Destroy(col.gameObject);

            if((IconType.GetCurrentFruit() == 1 && col.gameObject.tag == "Lemon_2") ||
              (IconType.GetCurrentFruit() == 2 && col.gameObject.tag == "Cherry_2") ||
              (IconType.GetCurrentFruit() == 3 && col.gameObject.tag == "Orange_2") ||
              (IconType.GetCurrentFruit() == 0 && col.gameObject.tag == "Banana_2"))
            {

                Fruit_Smash.Play();
                gameController_3.SubtractScore(SubValue);

            }
            
            

        }
    }
}
