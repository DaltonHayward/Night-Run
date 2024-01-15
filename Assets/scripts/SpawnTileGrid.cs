using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnTileGrid : MonoBehaviour
{
    //[SerializeField] private GameObject[][] _tilesToPickFrom;
    [System.Serializable]
    public class TileRow
    {
        public GameObject[] tilesToPickFrom;
    }
    
    [SerializeField]
    public TileRow[] tileType;

    private (GameObject, int, int) previousInstantiation; // (previous game ojbect, number of times spawned, total times to spawn)
    [SerializeField] private int gridX, gridZ;
    [SerializeField] private float gridSpacingOffset = 1.0f;
    [SerializeField] private Vector3 gridOrigin;
    [SerializeField] private Vector3 rowOrigin;

    private Transform player;
    private float greatestZPos;

    [SerializeField] private GameObject cullBarBottom;
    [SerializeField] private GameObject cullBarBack;
    [SerializeField] private float cullBarDist;
    

    // Start is called before the first frame update
    void Start()
    {
        previousInstantiation = (tileType[0].tilesToPickFrom[0], 0, 3); // log

        // set the origin of grid and row based on dimentions of grid
        gridOrigin = new Vector3(-(gridX/2), 0, -(gridZ/3));
        rowOrigin = new Vector3(-(gridX/2), 0, 0);

        // position the game ojject culling bar
        cullBarBack.transform.position = new Vector3(0, 0, -cullBarDist);
        BoxCollider cullBarBackCollider = cullBarBack.GetComponent<BoxCollider>();
        cullBarBackCollider.size = new Vector3(gridX, 5, 1);

        cullBarBottom.transform.position = new Vector3(0, -5, 0);
        BoxCollider cullBarBottomCollider = cullBarBottom.GetComponent<BoxCollider>();
        cullBarBottomCollider.size = new Vector3(gridX, 1, gridZ+cullBarDist);

        // grab the player transform and their current z position
        player = GameObject.Find("Player").transform;
        greatestZPos = player.position.z;

        // create the initial grid
        SpawnGrid();

        // create the initial pathfinding grid
        AstarPath.active.Scan();
    }

    void Update()
    {    
        // if player moves to a new largest z position, spawn a new row at the top of the grid
        if (player.position.z > greatestZPos){
            SpawnRow();
            greatestZPos = player.position.z;
            cullBarBack.transform.position = cullBarBack.transform.position + transform.forward;
            cullBarBottom.transform.position = cullBarBottom.transform.position + transform.forward;
        }
    }


    void SpawnGrid() {
        for (int z = 0; z < gridZ; z++) {
            for ( int x = 0; x < gridX; x++) {
                Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                PickAndSpawn(spawnPosition, Quaternion.identity, x, z);
            }
        }
    }

    void SpawnRow() {
        for (int x = 0; x < gridX; x++) {
            Vector3 spawnPosition = new Vector3(x * gridSpacingOffset, 0, (player.position.z-1)+ gridZ + gridOrigin.z) + rowOrigin;
            PickAndSpawn(spawnPosition, Quaternion.identity, x, 21);
        }
    }

    void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn, int x_index, int z_index){
        GameObject clone;
        GameObject tileToSpawn;
        int randomIndex = Random.Range(0, 10000);

        // check if current x value is near an edge of the grid and spawn trees to make edge of map
        // first row from outside
        if ((0 <= x_index && x_index <= 15) || (gridX-15 <= x_index && x_index <= gridX-1))
        {
            tileToSpawn = tileType[5].tilesToPickFrom[Random.Range(0,2)];
        }
        // second row from outside
        else if (x_index == 16 || x_index == gridX-16)
        {
            if (randomIndex % 2 == 0) 
                tileToSpawn = tileType[5].tilesToPickFrom[Random.Range(0,2)];
            else
                tileToSpawn = tileType[0].tilesToPickFrom[0];
        }

        else if (z_index >= 12 && z_index <= 14 && x_index >= 29 && x_index <= 31) 
        {
            tileToSpawn = tileType[0].tilesToPickFrom[0];
        }

        // Log logic
        else if (previousInstantiation.Item1.tag == "Log")
        {
            if (previousInstantiation.Item2 <= previousInstantiation.Item3) 
            {
                tileToSpawn = tileType[2].tilesToPickFrom[0];
                previousInstantiation.Item2 += 1;
            }
            else 
            {
                previousInstantiation.Item2 = 0;
                previousInstantiation.Item3 = Random.Range(1,8);
                tileToSpawn = tileType[0].tilesToPickFrom[0];
            }
        }
            
        // any other grid square 
        // Grass Tile
        else 
        {
            if (0 < randomIndex && randomIndex <= 9000)
            {
                tileToSpawn = tileType[0].tilesToPickFrom[0];
            }
            // Log Tile
            else if (9000 < randomIndex && randomIndex <= 9200)
            {
                tileToSpawn = tileType[2].tilesToPickFrom[0];
            }
            // Tree Tiles
            else if (9200 < randomIndex && randomIndex <= 9900)
            {
                tileToSpawn = tileType[1].tilesToPickFrom[Random.Range(0,2)];
            }
            // Rock Tiles
            else if (9900 < randomIndex && randomIndex <= 9980)
            {
                tileToSpawn = tileType[3].tilesToPickFrom[Random.Range(0,2)];
            }
            // Barrel/Fire Tiles
            else 
            {
                //tileToSpawn = tileType[0].tilesToPickFrom[0];
                tileToSpawn = tileType[4].tilesToPickFrom[Random.Range(0,3)];
            }
        }
        

        // Instantiate chosen tile
        clone = Instantiate(tileToSpawn, positionToSpawn, rotationToSpawn);
        // for use with logs
        previousInstantiation = (clone, previousInstantiation.Item2, previousInstantiation.Item3);
    }
}
