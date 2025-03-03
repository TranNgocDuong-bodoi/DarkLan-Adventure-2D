using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapTest;
    [SerializeField] private GameObject coinPrefab;
    private int xMax;
    private int xMin;
    private int yMax;
    private int yMin;
    int i;
    [SerializeField] private Sprite coin;
    void Start()
    {
        i = 1;
        BoundsInt bounds = tilemapTest.cellBounds;
        xMin = bounds.xMin;
        xMax = bounds.xMax;
        yMin = bounds.yMin;
        yMax = bounds.yMax;
        TestTileMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TestTileMap()
    {
       
        for(int x = xMin; x < xMax; x++){
           
            for(int y = yMin; y < yMax; y++){
                
                Vector3Int cellPosition = new Vector3Int(x,y,0);
                TileBase tile = tilemapTest.GetTile(cellPosition);
                
                if(tile != null)
                {
                    Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0);
                    GameObject Coin = new GameObject();
                    Coin.transform.position = worldPosition;
                    Coin.gameObject.SetActive(true);
                    Coin.AddComponent<SpriteRenderer>().sprite = coin;
                    Coin.GetComponent<SpriteRenderer>().sortingLayerName = "Character";
                    
                }
            }
        }

    }
}
