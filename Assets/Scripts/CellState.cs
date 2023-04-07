using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellState {
    private List<float> local_scale;
    private bool is_alive;
    public int num_living_neighbors;

    public List<Cell> neighbors = new List<Cell>();

    //Simple reference to the cell's x position in the grid
    //with respect to the scale of cell size.
    //(right from center)
    private float x_offset_from_center;

    //Simple reference to the cell's y position in the grid
    //with respect to the scale of cell size.
    //(up from center)
    private float y_offset_from_center;
    
    public CellState(bool is_alive, List<float> local_scale) {
        this.is_alive = is_alive;
        this.local_scale = local_scale;
        this.setOffsetFromCenter();
        num_living_neighbors = 0;
    }

    public void setNeighbors(List<Cell> neighbors) {
        this.neighbors = neighbors;
    }

    public List<Cell> getNeighbors() {
        return neighbors;
    }

    public bool resurrect() {
        this.is_alive = true;
        return true;
    }

    public bool kill() {
        this.is_alive = false;
        return true;
    }

    public bool isAlive() {
        return this.is_alive;
    }

    public bool addNeighbor(Cell neighbor) {
        this.neighbors.Add(neighbor);
        return true;
    }

    public bool removeNeighbor(Cell neighbor) {
        this.neighbors.Remove(neighbor);
        return true;
    }

    public Vector2 getOffsetFromCenter() {
        return new Vector2(this.x_offset_from_center, this.y_offset_from_center);
    }

    public void setOffsetFromCenter() {
        this.x_offset_from_center = this.local_scale[0] / 2;
        this.y_offset_from_center = this.local_scale[1] / 2;
    }

    public int get_num_living_neighbors() {
        this.num_living_neighbors = 0;
        foreach (Cell neighbor in this.neighbors) {
            if (neighbor.main_cell.isAlive()) {
                this.num_living_neighbors++;
            }
        }
        return this.num_living_neighbors;
    }
}
