using UnityEngine;
using System.Collections;

//Generates the fruits in minigame 1
public class Cherries_Generator : MonoBehaviour {

    //Declaration of variables and other useful objects
    public GameObject Cherry;
    public GameObject Lemon;
    public GameObject Orange;
    public GameObject Banana;
    GameObject CurrentFruit;
    public GameController gameController;
    float rateSpawn;

    // Use this for initialization
    void Start () {

        Cherry.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Lemon.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Orange.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Banana.GetComponent<Rigidbody2D>().gravityScale = 0.2f;


        rateSpawn = 3.0f;
        StartCoroutine(SpawnStuff());


    }
	
	// Update is called once per frame
	void Update () {

        if (gameController.timeRemaining < 30 && gameController.timeRemaining > 10) { rateSpawn = 2.0f; }
        if (gameController.timeRemaining <= 10)                                     {rateSpawn = 1.0f;}

    }

    //Co-routine to create the fruits at fixed intervals
    IEnumerator SpawnStuff()
    {
        while (true)
        {
            Create_Fruits();
            yield return new WaitForSeconds(rateSpawn);
        }
    }

    //Function to instantiate the fruits
    void Create_Fruits() {

        for (int x = -6; x < 8; x += 2)
        {

            RandomGenerator();
            Instantiate(CurrentFruit, new Vector3(x, 5, 0), Quaternion.identity);

        }

    }

    //Random fruit generator
    void RandomGenerator() {

        int randomInt = Random.Range(0, 4);

        if      (randomInt == 1) { CurrentFruit = Lemon; }
        else if (randomInt == 2) { CurrentFruit = Cherry; }
        else if (randomInt == 3) { CurrentFruit = Orange; }
        else                     { CurrentFruit = Banana; }
    }

}
