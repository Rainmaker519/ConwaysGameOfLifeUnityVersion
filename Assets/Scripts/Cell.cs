using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell: MonoBehaviour {

    public CellState main_cell;
    private Color live_color = Color.blue;
    private Color dead_color = Color.white;

    public int num_alive_neighbors_viewer_var = 0;

    // Start is called before the first frame update
    void Start()
    {
        System.Random random = GameObject.Find("CellController").GetComponent<CellControllerScript>().random;
        float randomValue = (float)(random.NextDouble());
        bool is_alive;
        if (randomValue > .5) {
            is_alive = true;
        } else {
            is_alive = false;
        }
        this.main_cell = new CellState(is_alive, new List<float> {GameObject.Find("CellController").GetComponent<CellControllerScript>().scale_x, GameObject.Find("CellController").GetComponent<CellControllerScript>().scale_y});
    }

    // Update is called once per frame
    void Update()
    {
        this.num_alive_neighbors_viewer_var = main_cell.get_num_living_neighbors();

        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (main_cell.isAlive()) {
            sprite.color = live_color;
            return;
        } 
        sprite.color = dead_color;
        return;
    }

    public bool addNeighbor(Cell neighbor) {
        this.main_cell.addNeighbor(neighbor);
        return true;
    }

    public List<Cell> getNeighbors() {
        return this.main_cell.getNeighbors();
    }

    public bool setNeighbors(List<Cell> neighbors) {
        this.main_cell.setNeighbors(neighbors);
        return true;
    }
}
