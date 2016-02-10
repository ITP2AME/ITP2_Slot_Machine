using UnityEngine;
using System.Collections;

//Fruit Generator for MiniGame 3
public class Generator_3 : MonoBehaviour {

    //Declaration of the Fruits Prefabs and other useful variables/Objects
    public GameObject Cherry;
    public GameObject Lemon;
    public GameObject Orange;
    public GameObject Banana;
    public GameController_3 gameController;
    float rateSpawn;
    GameObject CurrentFruit;

    // Use this for initialization
    public void Init()
    {
        //Setting the gravity scale of Fruits Prefabs
        Cherry.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Lemon.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Orange.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
        Banana.GetComponent<Rigidbody2D>().gravityScale = 0.2f;

        //Setting The rate at which the fruits are spawn
        rateSpawn = 0.5f;

        //Starting the Co-routine to spawn the fruits
        StartCoroutine(SpawnStuff());


    }

    // Update is called once per frame
    void Update()
    {
        //Adjusting the spawn rate according to the time left (time left is set in GameController_3)
        if (gameController.timeRemaining < 30 && gameController.timeRemaining > 10){rateSpawn = 0.40f;}
        if (gameController.timeRemaining <= 10){ rateSpawn = 0.30f;}

    }


    //Function to spawn the fruit at set intervals
    IEnumerator SpawnStuff()
    {
        while (true)
        {
            Create_Fruits_2();
            yield return new WaitForSeconds(rateSpawn);
        }
    }

  
    //Function to instantiate the fruits prefab in the world
    void Create_Fruits_2()
    {
        int x = Random.Range(-7, 8);
        int y = Random.Range(0, 6);

        RandomGenerator();
        Instantiate(CurrentFruit, new Vector3(x, y, 0), Quaternion.identity);   
    }

    //Function to randomly set which fruit to spawn
    void RandomGenerator()
    {

        int randomInt = Random.Range(0, 4);

        if (randomInt == 1) { CurrentFruit = Lemon; }
        else if (randomInt == 2) { CurrentFruit = Cherry; }
        else if (randomInt == 3) { CurrentFruit = Orange; }
        else { CurrentFruit = Banana; }
    }

}
