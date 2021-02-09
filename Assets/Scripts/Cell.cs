using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image outlineImage;
    public Vector2Int gridPosition = Vector2Int.zero;
    public Grid grid = null;
    public RectTransform rectTransform = null;
    public CellState cellState = CellState.Undiscovered;
    public CellWorth cellWorth = CellWorth.Nothing;

    public void Setup(Vector2Int newGridPosition, Grid newGrid)
    {
        cellState = CellState.Undiscovered;
        cellWorth = CellWorth.Nothing;

        gridPosition = newGridPosition;
        grid = newGrid;

        rectTransform = GetComponent<RectTransform>();
    }

    public void DoTask()
    {
        if (Manager.instance.playMode == PlayMode.Scan)
        {
            if (Manager.instance.remainingScans == 0)
                return;

            Manager.instance.Scanned();

            if (grid != null)
                grid.ScanCellClicked(this);
        }

        else if (Manager.instance.playMode == PlayMode.Extract)
        {
            if (Manager.instance.remainingExtracts == 1)
            {
                grid.ExtractCellClicked(this);
                Manager.instance.Extracted();
            }

            Manager.instance.Extracted();

            if (Manager.instance.remainingExtracts == 0)
                return;

            if (grid != null)
                grid.ExtractCellClicked(this);
        }
    }
}
