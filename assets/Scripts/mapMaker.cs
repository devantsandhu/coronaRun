using first.ast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapMaker : MonoBehaviour
{
    public int mazeColumns;
    public int mazeRows;

    public GameObject wallRemover; // moves to given coords and destroys anything on touch

    public GameObject playerPrefab;
    public GameObject cellPrefab;
    public GameObject bombPrefab;
    public GameObject goldPrefab;
    public GameObject enemyPrefab;
    public GameObject finishPrefab;

    public Vector2 startPoint;
    public Vector2 finishPoint;
    private List<Vector2> bombLocations = new List<Vector2>();
    private List<Vector2> goldLocations = new List<Vector2>();
    public List<List<Vector3>> paths = new List<List<Vector3>>();
    public List<List<Vector3>> enemies = new List<List<Vector3>>();

    //public Scene game;

    public Camera overheadCam; // to be used when player wins or loses
    private float cellSize = 2.15f; // for scaling purposes
    private double widthToBeSeen; // for scaling purposes

    private GameObject mainPlayer; // use to refer to actual player on screen after instantiated.

    // Start is called before the first frame update
    void Start()
    {
        mazeRows = Parser.row;
        mazeColumns = Parser.col;
        startPoint = Parser.startP;
        finishPoint = Parser.finishP;
        bombLocations = Parser.bombLocations;
        //Debug.Log(bombLocations.Count);
        goldLocations = Parser.goldLocations;
        enemies = Parser.enemies;
        paths = Parser.paths;
        CreateLayout();
        setPaths();
        SpawnPlayer();
        SpawnFinishPoint();
        SpawnBombs();
        SpawnGold();
        SpawnEnemies();
        widthToBeSeen = cellSize * mazeRows * 2;
        //Debug.Log(Screen.width);
        overheadCam.orthographicSize = (float) (widthToBeSeen * Screen.height / Screen.width * 0.5);
        overheadCam.transform.position = new Vector3( (float) ( ( (float) (mazeRows/2f)-0.5f)  * cellSize), (float) ( ((float) (mazeColumns / 2f) -0.5f) * cellSize),-1);
        //SceneManager.SetActiveScene(game);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateLayout()
    {
        //Vector2 startPos = new Vector2(-(cellSize * (mazeColumns / 2)) + (cellSize / 2), -(cellSize * (mazeRows / 2)) + (cellSize / 2));
        Vector2 startPos = new Vector2(0, 0);
        Vector2 spawnPos = startPos;

        for (int x = 1; x <= mazeColumns; x++)
        {
            for (int y = 1; y <= mazeRows; y++)
            {
                GameObject cell = Instantiate(cellPrefab, spawnPos, Quaternion.identity);
                cell.gameObject.name = "cell(" + x.ToString() + "," + y.ToString() + ")";
                // Increase spawnPos y.
                spawnPos.y += cellSize;
            }

            // Reset spawnPos y and increase spawnPos x.
            spawnPos.y = startPos.y;
            spawnPos.x += cellSize;
        }
    }
    public void SpawnBombs() {
        for (int i = 0; i < bombLocations.Count; i++)
        {
            Vector2 spawnPos = (bombLocations[i] * cellSize);
            spawnPos.x += 0.24f;
            spawnPos.y += 0.24f;
            GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity);
        }
    }
    public void SpawnGold()
    {
        for (int i = 0; i < goldLocations.Count; i++)
        {
            Vector2 spawnPos = (goldLocations[i] * cellSize);
            //spawnPos.x += 0.24f;
            //spawnPos.y += 0.24f;
            GameObject coin = Instantiate(goldPrefab, spawnPos, Quaternion.identity);
        }
    }

    public void SpawnPlayer()
    {
        mainPlayer = Instantiate(playerPrefab, startPoint * cellSize, Quaternion.identity);
    }

    public void SpawnFinishPoint()
    {
        GameObject finishFlag = Instantiate(finishPrefab, finishPoint * cellSize, Quaternion.identity);
    }

    public void setPaths()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            GameObject path = Instantiate(wallRemover, paths[i][0] * cellSize, Quaternion.identity);
            path.GetComponent<wallRemover>().startPosition = paths[i][0] * cellSize;
            path.GetComponent<wallRemover>().currentPoint = paths[i][0] * cellSize;
            for (int j = 0; j < paths[i].Count; j++)
            {
                path.GetComponent<wallRemover>().moveToPoints.Add(paths[i][j]* cellSize);
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemies[i][0] * cellSize, Quaternion.identity);
            enemy.GetComponent<enemyScript>().startPosition = enemies[i][0] * cellSize;
            enemy.GetComponent<enemyScript>().currentPoint = enemies[i][0] * cellSize;
            for (int j = 0; j < enemies[i].Count; j++) {
                enemy.GetComponent<enemyScript>().moveToPoints.Add(enemies[i][j] * cellSize);
            }
        }
    }
}
