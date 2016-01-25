using UnityEngine;
using System.Collections;

public class Cherries_Generator : MonoBehaviour {

    public Transform Cherry;

    public Transform Lemon;

    Transform CurrentFruit;



    // Use this for initialization
    void Start () {

        //Create_Fruits();

        InvokeRepeating("Create_Fruits", 0, 3);


    }
	
	// Update is called once per frame
	void Update () {

        


    }

    void Create_Fruits() {

        for (int x = -4; x < 6; x += 2)
        {

            RandomGenerator();
            Instantiate(CurrentFruit, new Vector3(x, 5, 0), Quaternion.identity);

        }

    }

    void RandomGenerator() {

        int randomInt = Random.Range(0, 2);

        if (randomInt == 1) { CurrentFruit = Lemon;}
        else                { CurrentFruit = Cherry; }
    }
}
