using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

    // Prefabs for instantiating
    public Transform [] prefabs = new Transform[3];

    // Constant indices
    private const int OBSTACLE = 0;
    private const int RADAR = 1;
    private const int COIN = 2;

    public int maxObjectsCount = 15; // Maximum objects count/floor

    public Transform player;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // Generating new objects starting from posZ till the floor length
    public void CreateObjectsFrom(float posZ)
    {
        // Range of Z to generate on
        float startRange = posZ + 10;
        float endRange = posZ + GameManager.length - 10;

        // How many objects to generate
        int objectsCount = 0;
        int coinsProbability = 5;

        while(objectsCount++ < maxObjectsCount)
        {
            // Interpolate
            float objPos = Mathf.Lerp(startRange, endRange, (objectsCount * 1.0f/maxObjectsCount));

            // What to generate
            int randItem = Mathf.Min(Random.Range(0, coinsProbability), 2); 

            if (randItem == OBSTACLE || randItem == COIN)
            {
                // Where to generate
                int randLane = Random.Range(0, 3);
                Lane lane = (randLane == 2 ? Lane.Right : (randLane == 1 ? Lane.Middle : Lane.Left));
                // Generate object
                CreateObject(randItem, lane, objPos);
            }
            else
                CreateObject(RADAR, Lane.Middle, objPos);
        }
    }

    void CreateObject(int type, Lane lane, float posZ)
    {
        Transform prefab = prefabs[type];
        Transform parent = transform.GetChild(type);
        Instantiate(prefab, new Vector3(GameManager.lanePositions[lane],prefab.position.y, posZ),
            prefab.rotation, parent);

        // Destroy passed objects
        //if(player.position.z > parent.GetChild(0).position.z)
        //    Destroy(parent.GetChild(0).gameObject);
    }
}
