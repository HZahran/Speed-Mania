using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {

    public GameObject floor, objects; // Prefabs
    private float floorCount = 0; // Floor counter

	// Use this for initialization
	void Start () {
        // Create the initial floor
        CreateFloor();
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Create a new floor
    public void CreateFloor()
    {
        // Create Parent for both floor and its objects 
        GameObject parent = new GameObject("Floor + Objects");
        parent.transform.SetParent(this.transform);

        float posZ = floorCount * GameManager.length;

        // Create floor
        GameObject newFloor = Instantiate(floor, parent.transform);
        newFloor.transform.localScale = new Vector3(GameManager.width, newFloor.transform.localScale.y, GameManager.length);
        newFloor.transform.Translate(0, 0, posZ);

        // Create objects on that floor
        GameObject newObjects = Instantiate(objects, parent.transform);
        newObjects.GetComponent<ObjectGenerator>().CreateObjectsFrom(posZ);

        // Delete previous floor and objects
        if (transform.childCount > 2)
            Destroy(transform.GetChild(0).gameObject);

        // Update values
        floorCount++;
    }
}
