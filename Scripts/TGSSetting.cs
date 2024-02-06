using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class TGSSetting : MonoBehaviour
{
    TerrainGridSystem tgs;

    public const int CELL_DEFAULT = 1;
    public const int CELL_PLAYER = 2;
    public const int CELL_ENEMY = 4;
    public const int CELL_OBSTACLE = 8;
    public const int CELLS_ALL_NAVIGATABLE = ~(CELL_OBSTACLE | CELL_PLAYER | CELL_ENEMY);

    public List<int> cells;

    public List<GameObject> Enemy = null;

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
