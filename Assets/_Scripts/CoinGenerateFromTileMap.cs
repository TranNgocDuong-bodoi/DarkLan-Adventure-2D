using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoinCollectionManagement : MonoBehaviour
{
    [SerializeField]private GameObject coinPrefabs;
    [SerializeField] private Tilemap coinTilemap;
    [SerializeField] private GameObject coinParent;
    private int xMin;
    private int xMax;
    private int yMin;
    private int yMax;

    void Start()
    {
        GenerateCoinPrefab();
    }
    private void GenerateCoinPrefab()
    {
        BoundsInt bounds = coinTilemap.cellBounds;
        xMin = bounds.xMin;
        xMax = bounds.xMax;
        yMin = bounds.yMin;
        yMax = bounds.yMax;
        for(int x = xMin; x < xMax; x++)
        {
            for(int y = yMin; y < yMax; y++)
            {
                Vector3Int cellPosition = new Vector3Int(x,y,0);
                TileBase tile = coinTilemap.GetTile(cellPosition);
                if(tile != null)
                {
                    Vector3 worldPosition = new Vector3(cellPosition.x + 0.5f, cellPosition.y + 0.5f, 0); 
                    GameObject coin = Instantiate(coinPrefabs,worldPosition,transform.rotation);
                    coin.transform.SetParent(coinParent.transform);
                }
            }
        }
        coinTilemap.ClearAllTiles();
        Debug.Log("Clear all");
    }
}
