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
    private int gridICorrectionValue;
    private int gridJCorrectionValue;

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
        gridICorrectionValue = (int)Mathf.Floor(rows / 2.0f);
        gridJCorrectionValue = (int)Mathf.Floor(columns / 2.0f);

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
        int gridI, gridJ;
        changeFromArrayIndexToGridIndex(i, j, out gridI, out gridJ);
        changeMaterial(gridI, gridJ, firstMaterial);
    }

    public void changeToSecondMaterial(int i, int j) {
        int gridI, gridJ;
        changeFromArrayIndexToGridIndex(i, j, out gridI, out gridJ);
        changeMaterial(gridI, gridJ, secondMaterial);
    }

    public void changeToOriginalMaterial(int i, int j) {
        int gridI, gridJ;
        changeFromArrayIndexToGridIndex(i, j, out gridI, out gridJ);
        changeMaterial(gridI, gridJ, originalMaterial);
    }

    private void changeMaterial(int i, int j, Material newMaterial) {
        grid[i, j].GetComponent<Renderer>().material = newMaterial;
    }

    private void changeFromArrayIndexToGridIndex(int i, int j, out int gridI, out int gridJ) {
        gridI = Mathf.Abs(i - gridICorrectionValue);
        gridJ = j + gridJCorrectionValue;
    }

    private void print() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                Debug.Log(grid[i,j]);
            }
        }
    }
}
