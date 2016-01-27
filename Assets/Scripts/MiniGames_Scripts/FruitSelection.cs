using UnityEngine;
using System.Collections;

public class FruitSelection : MonoBehaviour {

    public Transform Cherry;
    public Transform Lemon;
    public Transform Orange;
    public Transform Banana;

    public Sprite Cherry_;
    public Sprite Lemon_;
    public Sprite Orange_;
    public Sprite Banana_;


    Transform CurrentFruit;

    // Use this for initialization
    void Start () {

        
        //Instantiate(Cherry, new Vector3(-30, 3, 0), Quaternion.identity);
        //Instantiate(Lemon, new Vector3(-30, 3, 0), Quaternion.identity);
        //Instantiate(Orange, new Vector3(-30, 3, 0), Quaternion.identity);
        //Instantiate(Banana, new Vector3(-30, 3, 0), Quaternion.identity);

        

        

    }
	
	// Update is called once per frame
	void Update () {

        

    }

    void Create_Fruits()
    {
            
            RandomGenerator();
            Instantiate(CurrentFruit, new Vector3(-8, 3, 0), Quaternion.identity);

        

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
