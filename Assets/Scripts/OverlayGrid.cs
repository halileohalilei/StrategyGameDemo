using UnityEngine;
using System.Collections;

public class OverlayGrid : MonoBehaviour {

    public int gridWidth;
    public int gridHeight;
    public float distanceBetweenTiles;

    public GameObject tilePrefab;

    private GameObject[,] grid;

    void Awake()
    {
        grid = new GameObject[gridWidth, gridHeight];
        this.drawGrid();
    }

    private void drawGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                GameObject tile = Instantiate(tilePrefab,
                    //assuming each tile has a size of 1x1
                    new Vector3(-((gridWidth + ((gridWidth - 1) * distanceBetweenTiles)) / 2f) + i * (distanceBetweenTiles + 1),
                                0.01f,
                                -((gridHeight + ((gridHeight - 1) * distanceBetweenTiles)) / 2f) + j * (distanceBetweenTiles + 1)),
                    tilePrefab.transform.rotation) as GameObject;
                tile.transform.parent = transform;
                Tile tileComponent = tile.GetComponent<Tile>();
                tileComponent.i = i;
                tileComponent.j = j;
                grid[i, j] = tile;
            }
        }
    }

}
