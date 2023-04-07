using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellControllerScript : MonoBehaviour
{
    private IEnumerator coroutine;

    //Square cell prefab
    public GameObject cellPrefab;
    public System.Random random = new System.Random();

    //just for now by default
    public float scale_x;
    public float scale_y;

    //grid of cells w and height in tiles
    public int grid_width;
    public int grid_height;

    //all prefabs stored in a list right to left and top to bottom
    public List<GameObject> cellPrefabs;

    public bool got_neighbors_already = false;

    // Start is called before the first frame update
    void Start()
    {
        createCellGrid(60,60);
        StartCoroutine(wait_for_seconds(.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.got_neighbors_already) {
            get_cell_neighbors();
            this.got_neighbors_already = true;
        }
    }

    public bool createCellGrid(double x, double y) {
        //just for now by default
        scale_x = .18f;
        scale_y = .18f;

        this.grid_width = (int)x;
        this.grid_height = (int)y;

        float x_offset_from_center = (float)scale_x / 2;
        float y_offset_from_center = (float)scale_y / 2;

        int dist_from_center_x = (int)System.Math.Floor(x/2);
        int dist_from_center_y = (int)System.Math.Floor(y/2);
        for (int i = -dist_from_center_x; i < dist_from_center_x; i++) {
            for (int j = -dist_from_center_y; j < dist_from_center_y; j++) {
                GameObject prefab = Instantiate(
                    this.cellPrefab, 
                    new Vector3(scale_x * (i+x_offset_from_center), 
                    scale_y * (j+y_offset_from_center), 0), 
                    Quaternion.identity
                    );
                prefab.transform.localScale = new Vector3(scale_x, scale_y, 1);
                this.cellPrefabs.Add(prefab);
            }
        }
        return true;
    }

    public bool get_cell_neighbors() {
        for (int i = 0; i < this.cellPrefabs.Count; i++) {
            Cell cell = this.cellPrefabs[i].GetComponent<Cell>();
            List<Cell> neighbors = new List<Cell>();

            double x = grid_width;
            double y = grid_height;

            //left neighbor
            if (i - 1 >= 0) {
                neighbors.Add(cellPrefabs[i - 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[cellPrefabs.Count - 1].GetComponent<Cell>());
            }
            //right neighbor
            if (i + 1 < cellPrefabs.Count) {
                neighbors.Add(cellPrefabs[i + 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[0].GetComponent<Cell>());
            }
            //top neighbor
            if (i - (int)x >= 0) {
                neighbors.Add(cellPrefabs[i - (int)x].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[cellPrefabs.Count - 1].GetComponent<Cell>());
            }
            //bottom neighbor
            if (i + (int)x < cellPrefabs.Count) {
                neighbors.Add(cellPrefabs[i + (int)x].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[0].GetComponent<Cell>());
            }
            //top left neighbor
            if (i - (int)x - 1 >= 0) {
                neighbors.Add(cellPrefabs[i - (int)x - 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[cellPrefabs.Count - 1].GetComponent<Cell>());
            }
            //top right neighbor
            if (i - (int)x + 1 >= 0) {
                neighbors.Add(cellPrefabs[i - (int)x + 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[cellPrefabs.Count - 1].GetComponent<Cell>());
            }
            //bottom left neighbor
            if (i + (int)x - 1 < cellPrefabs.Count) {
                neighbors.Add(cellPrefabs[i + (int)x - 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[0].GetComponent<Cell>());
            }
            //bottom right neighbor
            if (i + (int)x + 1 < cellPrefabs.Count) {
                neighbors.Add(cellPrefabs[i + (int)x + 1].GetComponent<Cell>());
            }
            else {
                neighbors.Add(cellPrefabs[0].GetComponent<Cell>());
            }
            cell.setNeighbors(neighbors); 
        }
        return true;
    }

    public bool update_cell_states() {
        for (int i = 0; i < this.cellPrefabs.Count; i++) {
            Cell cell = this.cellPrefabs[i].GetComponent<Cell>();
            cell.update_cell_state();
        }
        return true;
    }

    private IEnumerator wait_for_seconds(float seconds) {
        while (true) {
            yield return new WaitForSeconds(seconds);
            update_cell_states();
        }
    }
}
