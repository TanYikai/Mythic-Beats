using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public static Grid instance = null;

    public Material originalMaterial;
    public Material firstMaterial;
    public Material secondMaterial;
    public Material thirdMaterial;

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
        gridICorrectionValue = 3; //(int)Mathf.Floor(rows / 2.0f);
        gridJCorrectionValue = 4; //(int)Mathf.Floor(columns / 2.0f);

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

    public void resetAllGridMaterial() {
        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < columns; j++) {
                grid[i, j].GetComponent<Renderer>().material = originalMaterial;
            }
        }
    }

    public void decideAndChangeMaterial(int i, int j, int stage) {
        int gridI, gridJ;
        changeFromArrayIndexToGridIndex(i, j, out gridI, out gridJ);
        Material chosenMaterial = decideMaterial(stage);
        changeMaterial(gridI, gridJ, chosenMaterial);
    }

    private Material decideMaterial(int stage) {
        switch (stage) {
            case 0:
                return originalMaterial;
            case 1:
                return firstMaterial;
            case 2:
                return secondMaterial;
            case 3:
                return thirdMaterial;
            default:
                return originalMaterial;
        }
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
