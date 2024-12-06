using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float sqaure_scale = 1.0f;

    private List<GameObject> grid_sqaures_ = new List<GameObject>();

    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare Scripts attached !");

        CreateGrid();
        SetGridNumber();

    }

    void Update()
    {

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                grid_sqaures_.Add(Instantiate(grid_square) as GameObject);
                grid_sqaures_[grid_sqaures_.Count - 1].transform.parent = this.transform;
                grid_sqaures_[grid_sqaures_.Count - 1].transform.localScale = new Vector3(sqaure_scale, sqaure_scale, sqaure_scale);
            }
        }
    }

    private void SetSquaresPosition()
    {
        var sqaure_rect = grid_sqaures_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = sqaure_rect.rect.width * sqaure_rect.transform.localScale.x + square_offset;
        offset.y = sqaure_rect.rect.height * sqaure_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject sqaure in grid_sqaures_)
        {
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;
            }

            var pos_x_offset = offset.x * column_number;
            var pos_y_offset = offset.y * row_number;
            sqaure.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;

        }

    }

    private void SetGridNumber()
    {
        foreach (var square in grid_sqaures_)
        {
            square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 10));

        }
    }
}