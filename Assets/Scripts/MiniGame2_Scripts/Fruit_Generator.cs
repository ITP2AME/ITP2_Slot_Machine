using UnityEngine;
using System.Collections;

public class Fruit_Generator : MonoBehaviour {

    public GameObject Cherry;
    public GameObject Lemon;
    public GameObject Orange;
    public GameObject Banana;

    GameObject CurrentFruit;

    public GameController_2 gameController;

    float rateSpawn;

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

        if (gameController.timeRemaining < 30 && gameController.timeRemaining > 10)
        {
            rateSpawn = 5.0f;
        }

        if (gameController.timeRemaining <= 10)
        {
            rateSpawn = 4.0f;
        }

    }


    IEnumerator SpawnStuff()
    {
        while (true)
        {
            Create_Fruits();
            yield return new WaitForSeconds(rateSpawn);
        }
    }

    void Create_Fruits()
    {

       

            RandomGenerator();
            Instantiate(CurrentFruit, new Vector3(-8, 5, 0), Quaternion.identity);

        

    }

    void RandomGenerator()
    {

        int randomInt = Random.Range(0, 4);

        if (randomInt == 1) { CurrentFruit = Lemon; }
        else if (randomInt == 2) { CurrentFruit = Cherry; }
        else if (randomInt == 3) { CurrentFruit = Orange; }
        else { CurrentFruit = Banana; }
    }

}
