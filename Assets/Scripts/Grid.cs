using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    Undiscovered, Discovered, Used
}

public enum CellWorth
{
    Nothing, Quarter, Half, Max
}

public class Grid : MonoBehaviour
{
    public static int cellSizeX = 32;
    public static int cellSizeY = 32;

    public GameObject cellPrefab;
    public Cell[,] allCells = new Cell[cellSizeX, cellSizeY];

    public void Init()
    {
        for (int i = 0; i < cellSizeX; i++)
            for (int j = 0; j < cellSizeY; j++)
            {
                GameObject newCell = Instantiate(cellPrefab, transform);

                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector3((j * 25f) + 12.5f, (i * 25f) + 12.5f);

                allCells[j, i] = newCell.GetComponent<Cell>();
                allCells[j, i].Setup(new Vector2Int(j, i), this);
            }

        for (int i = 0; i < 18; i++)
            MakeDeposit();
    }

    public void MakeDeposit()
    {
        int targetX = Random.Range(0, cellSizeX);
        int targetY = Random.Range(0, cellSizeY);

        for (int i = -2; i < 3; i++)
        {
            if (i + targetX < 0 || i + targetX >= cellSizeX)
                continue;

            for (int j = -2; j < 3; j++)
            {
                if (j + targetY < 0 || j + targetY >= cellSizeY)
                    continue;

                Image image2 = allCells[i + targetX, j + targetY].GetComponent<Image>();
                image2.color = Color.green;
                allCells[i + targetX, j + targetY].cellWorth = CellWorth.Quarter;
            }
        }

        for (int i = -1; i < 2; i++)
        {
            if (i + targetX < 0 || i + targetX >= cellSizeX)
                continue;

            for (int j = -1; j < 2; j++)
            {
                if (j + targetY < 0 || j + targetY >= cellSizeY)
                    continue;

                Image image2 = allCells[i + targetX, j + targetY].GetComponent<Image>();
                image2.color = Color.blue;
                allCells[i + targetX, j + targetY].cellWorth = CellWorth.Half;
            }
        }

        Image image = allCells[targetX, targetY].GetComponent<Image>();
        image.color = Color.yellow;
        allCells[targetX, targetY].cellWorth = CellWorth.Max;
    }

    public void ExtractCellClicked(Cell cellClicked)
    {
        int targetX = cellClicked.gridPosition.x;
        int targetY = cellClicked.gridPosition.y;

        if (cellClicked.cellState != CellState.Discovered)
        {
            Debug.Log("Not discovered!");
            return;
        }

        switch (cellClicked.cellWorth)
        {
            case CellWorth.Max:
                Manager.instance.AddGold(100);
                break;

            case CellWorth.Half:
                Manager.instance.AddGold(50);
                break;

            case CellWorth.Quarter:
                Manager.instance.AddGold(25);
                break;

            case CellWorth.Nothing:
                Manager.instance.AddGold(10);
                break;
        }

        for (int i = -2; i < 3; i++)
        {
            if (i + targetX < 0 || i + targetX >= cellSizeX)
                continue;

            for (int j = -2; j < 3; j++)
            {
                if (j + targetY < 0 || j + targetY >= cellSizeY)
                    continue;

                if (allCells[i + targetX, j + targetY].cellState == CellState.Used)
                    continue;

                switch (allCells[i + targetX, j + targetY].cellWorth)
                {
                    case CellWorth.Max:
                        allCells[i + targetX, j + targetY].cellWorth = CellWorth.Half;
                        allCells[i + targetX, j + targetY].GetComponent<Image>().color = Color.blue;
                        break;

                    case CellWorth.Half:
                        allCells[i + targetX, j + targetY].cellWorth = CellWorth.Quarter;
                        allCells[i + targetX, j + targetY].GetComponent<Image>().color = Color.green;
                        break;

                    case CellWorth.Quarter:
                        allCells[i + targetX, j + targetY].cellWorth = CellWorth.Nothing;
                        allCells[i + targetX, j + targetY].GetComponent<Image>().color = Color.red;
                        break;

                    case CellWorth.Nothing:
                        allCells[i + targetX, j + targetY].cellWorth = CellWorth.Nothing;
                        allCells[i + targetX, j + targetY].GetComponent<Image>().color = Color.red;
                        break;
                }
            }
        }

        Image image2 = cellClicked.GetComponent<Image>();
        image2.color = Color.black;
        cellClicked.cellState = CellState.Used;

    }

    public void ScanCellClicked(Cell cellClicked)
    {
        int targetX = cellClicked.gridPosition.x;
        int targetY = cellClicked.gridPosition.y;

        for (int i = -1; i < 2; i++)
        {
            if (i + targetX < 0 || i + targetX >= cellSizeX)
                continue;

            for (int j = -1; j < 2; j++)
            {
                if (j + targetY < 0 || j + targetY >= cellSizeY)
                    continue;

                if (allCells[i + targetX, j + targetY].cellState != CellState.Used)
                    allCells[i + targetX, j + targetY].cellState = CellState.Discovered;

                allCells[i + targetX, j + targetY].transform.Find("Outline").gameObject.SetActive(false);
            }
        }
    }
}
