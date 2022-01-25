using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public Sprite[] sprites = new Sprite[5];
    //public List<Sprite> sprites = new List<Sprite>();
    public List<Tile> tiles = new List<Tile>();

    private const int xSize = 6;
    private const int ySize = 10;
    private Tile[ , ] gameBoard = new Tile[ySize, xSize];

    private Tile selectedTile = null;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CreateGameBoard();
    }


    void Update()
    {
        /*if (Input.touchCount == 1)
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.touches[0].position)); // �������� ���� �� ����� �������
            if (ray != false) // ���� ��� ����� � ���� - ��������/����� ��������� � �����
            {
                ChangeTileSelection(ray.collider.gameObject.GetComponent<Tile>());
            }
            
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D ray = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition)); // �������� ���� �� ����� �������
            if (ray != false) // ���� ��� ����� � �����, �� �������� ��� ����� ��������� � �����
            {
                TileSelection(ray.collider.gameObject.GetComponent<Tile>());
            }

        }
    }


    // �������� �������� ����
    private void CreateGameBoard()
    {
        // ���������� ������� ��� ������ ����� (������ ����� ����)
        float xPos = transform.position.x;
        float yPos = transform.position.y;
        int rndType;
        // ���������� ���� �������
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                rndType = Random.Range(0, 5);

                // �������� ������������ ���� ����� ����� � ����� �� �����
                if ((i > 1) || (j > 1))
                {
                    if ((j > 1) && 
                        ((gameBoard[i, j - 2].type == rndType) && (gameBoard[i, j - 1].type == rndType)))
                    {
                        while (gameBoard[i, j - 1].type == rndType)
                        {
                            rndType = Random.Range(0, 5);
                        }
                    }
                    else if ((i > 1) &&
                        ((gameBoard[i - 2, j].type == rndType) && (gameBoard[i - 1, j].type == rndType)))
                    {
                        while (gameBoard[i - 1, j].type == rndType)
                        {
                            rndType = Random.Range(0, 5);
                        }
                    }
                }

                // �������� �����
                gameBoard[i, j] = Instantiate(tiles[rndType], new Vector2(xPos, yPos), Quaternion.identity);
                gameBoard[i, j].xPos = j;
                gameBoard[i, j].yPos = i;
                xPos += 0.9f;
            }
            yPos += 0.9f;
            xPos = transform.position.x;
        }
    }


    // ���������/������ ��������� � �����
    private void TileSelection(Tile tile)
    {
        if (tile.isSelected) // ���� ����� ��� �������, ����� ���������
        {
            tile.isSelected = false;
            tile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
            selectedTile = null;
        }
        else // ����� �����
        {
            tile.isSelected = true;
            tile.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f); // ���������� ��������� �����
            
            if (selectedTile) // ���� ����� ���� ������� �����
            {
                if (((tile.xPos - 1 == selectedTile.xPos) || (tile.xPos + 1 == selectedTile.xPos)) && (tile.yPos == selectedTile.yPos))
                {
                    TileSwap(selectedTile, tile);
                }
                else if (((tile.yPos - 1 == selectedTile.yPos) || (tile.yPos + 1 == selectedTile.yPos)) && (tile.xPos == selectedTile.xPos))
                {
                    TileSwap(selectedTile, tile);
                }
                else
                {
                    selectedTile.isSelected = false;
                    selectedTile.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                }
            }

            selectedTile = tile; // ��������� ��������� �����
        }
    }


    // ����������� �����
    private void TileSwap(Tile tileA, Tile tileB)
    {
        Debug.Log("Swap!");
        tileA.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        tileB.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}
