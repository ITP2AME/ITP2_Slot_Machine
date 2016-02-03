using UnityEngine;
using System.Collections;

public class Generator_3 : MonoBehaviour {

    public GameObject Cherry;
    public GameObject Lemon;
    public GameObject Orange;
    public GameObject Banana;

    GameObject CurrentFruit;

    public GameController_3 gameController;

    float rateSpawn;

    // Use this for initialization
    void Start()
    {

        Cherry.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Lemon.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Orange.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        Banana.GetComponent<Rigidbody2D>().gravityScale = 0.1f;


        rateSpawn = 0.5f;

        StartCoroutine(SpawnStuff());


    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.timeRemaining < 30 && gameController.timeRemaining > 10)
        {
            rateSpawn = 0.40f;
        }

        if (gameController.timeRemaining <= 10)
        {
            rateSpawn = 0.30f;
        }

    }


    IEnumerator SpawnStuff()
    {
        while (true)
        {
            Create_Fruits_2();
            yield return new WaitForSeconds(rateSpawn);
        }
    }

  

    void Create_Fruits_2()
    {
        int x = Random.Range(-7, 8);
        int y = Random.Range(0, 6);

       

        RandomGenerator();
        Instantiate(CurrentFruit, new Vector3(x, y, 0), Quaternion.identity);

        
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
