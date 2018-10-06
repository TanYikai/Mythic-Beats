using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static Grid instance = null;

    public Material originalMaterial;
    public Material firstMaterial;
    public Material secondMaterial;

    private int rows;
    private int columns;
    GameObject[,] grid;

    void Awake() {
        if (instance == null) {
            instance = this;
            instance.Init();
        }
    }

    // Use this for initialization
    void Init () {
        rows = 6;
        columns = 9;
        setupGrid();
	}

    private void setupGrid() {
        grid = new GameObject[rows, columns];
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                grid[i, j] = transform.GetChild(i*columns + j).gameObject;
            }
        }
    }

    public void changeToFirstMaterial(int i, int j) {
        changeMaterial(i, j, firstMaterial);
    }

    public void changeToSecondMaterial(int i, int j) {
        changeMaterial(i, j, secondMaterial);
    }

    public void changeToOriginalMaterial(int i, int j) {
        changeMaterial(i, j, originalMaterial);
    }

    private void changeMaterial(int i, int j, Material newMaterial) {
        grid[i, j].GetComponent<Renderer>().material = newMaterial;
    }

    private void print() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                Debug.Log(grid[i,j]);
            }
        }
    }
}
