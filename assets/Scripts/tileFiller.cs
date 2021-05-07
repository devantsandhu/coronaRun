using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileFiller : MonoBehaviour

{
    public Tile grass;
    public Tilemap map;

    // Start is called before the first frame update
    void Start()
    {

        Vector3Int minPos = new Vector3Int(-5, -5, 0);
        Vector3Int maxPos = new Vector3Int(4, 4, 0);
        map.SetTile(minPos, grass);
        map.SetTile(maxPos, grass);
        map.BoxFill(new Vector3Int(0, 0, 0), grass, 0, 0, 4, 4);
        Debug.Log("bounds: " + map.cellBounds);
        map.BoxFill(new Vector3Int(0, 0, 0), null, 2, 3, 2, 3);
        BoundsInt boundss = new BoundsInt(new Vector3Int(1, 1, 0), new Vector3Int(2, 2, 0));

        map.SetTile(new Vector3Int(0, 4, 0), null);
        map.SetTile(new Vector3Int(0, 3, 0), null);
        map.SetTile(new Vector3Int(0, 2, 0), null);

        map.SetTile(new Vector3Int(1, 2, 0), null);
        map.SetTile(new Vector3Int(0, 4, 0), null);

        //map.SetColliderType(maxPos, Tile.ColliderType.Grid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
