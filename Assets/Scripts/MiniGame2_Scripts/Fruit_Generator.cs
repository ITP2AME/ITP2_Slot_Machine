using UnityEngine;
using System.Collections;

//Generates the fruits for Minigame 2
public class Fruit_Generator : MonoBehaviour {

    //Declaration of variables and other useful objects
    public GameObject Cherry;
    public GameObject Lemon;
    public GameObject Orange;
    public GameObject Banana;
    public GameController_2 gameController;
    float rateSpawn;
    GameObject CurrentFruit;

    // Use this for initialization
    void Start()
    {

        Cherry.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Lemon.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Orange.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Banana.GetComponent<Rigidbody2D>().gravityScale = 0.1f;


        rateSpawn = 6.0f;

        StartCoroutine(SpawnStuff());


    }

    // Update is called once per frame
    void Update()
    {
        //Changes the rate at which the fruits are spawn
        if (gameController.timeRemaining < 30 && gameController.timeRemaining > 10) { rateSpawn = 5.0f;}
        if (gameController.timeRemaining <= 10)                                     {  rateSpawn = 4.0f;}

    }

    //Co-Routine to create the fruits
    IEnumerator SpawnStuff()
    {
        while (true)
        {
            Create_Fruits();
            yield return new WaitForSeconds(rateSpawn);
        }
    }

    //Function to randomly instantiate the fruits
    void Create_Fruits()
    {
            RandomGenerator();
            Instantiate(CurrentFruit, new Vector3(-8, 5, 0), Quaternion.identity);

    }

    //Random Genertor function
    void RandomGenerator()
    {

        int randomInt = Random.Range(0, 4);
        if      (randomInt == 1) { CurrentFruit = Lemon; }
        else if (randomInt == 2) { CurrentFruit = Cherry; }
        else if (randomInt == 3) { CurrentFruit = Orange; }
        else                     { CurrentFruit = Banana; }
    }

}
