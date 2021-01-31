using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class CellManager : MonoBehaviour
{
    public int LEVEL_WIDTH = 64;
    public int LEVEL_HEIGHT = 48;
    public bool gameIsPaused = false;
    public float secondsPerUpdate = 0.1f;

    // Cell Prefabs
    public Cell cornPrefab;
    public Cell flowerPrefab;
    public Cell dirtPrefab;
    public Cell pestPrefab;
    public Cell firePrefab;
    public Cell[,] grid;

    public string loadFile;

    private float timeSinceLastUpdate = 0f;
    private bool updateFlipFlop = true;
    private string savedLevelsDir = "Assets/LevelSaves";


    // Start is called before the first frame update
    void Start()
    {
        if (!string.IsNullOrEmpty(loadFile))
            LoadLevel(loadFile);
        else
            PlaceInitialCells();
    }

    void PlaceInitialCells()
    {
        grid = new Cell[LEVEL_WIDTH, LEVEL_HEIGHT];
        for (int y = 0; y < LEVEL_HEIGHT; y++)
            for (int x = 0; x < LEVEL_WIDTH; x++)
                SetCell(x, y, CellType.Dirt);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameIsPaused)
        {
            if (timeSinceLastUpdate >= secondsPerUpdate)
            {
                timeSinceLastUpdate = 0f;
                UpdateCells();
            }
            else
            {
                timeSinceLastUpdate += Time.deltaTime;
            }
        }
    }

    Cell GetCellPrefabFromEnum(CellType cellType)
    {
        if (cellType == CellType.Corn)
            return cornPrefab;
        else if (cellType == CellType.Flower)
            return flowerPrefab;
        else if (cellType == CellType.Pest)
            return pestPrefab;
        else if (cellType == CellType.Fire)
            return firePrefab;
        else
            return dirtPrefab;
    }

    public void SetCell(int x, int y, CellType cellType)
    {
        Cell prefab = GetCellPrefabFromEnum(cellType);
        if (grid[x,y] != null)
            Destroy(grid[x, y].gameObject);
        grid[x,y] = Instantiate(prefab, new Vector3(x, 0.5f, y), Quaternion.identity);
    }

    void UpdateCells()
    {
        if (updateFlipFlop)
        {
            CellCollectActionInfo();
            CellActionUpdate();               // Cells perform any special actions (like planting seeds) if they can perform any
        }
        else
        {
            CellCollectStatusInfo();
            CellStatusUpdate();               // Cells here determine if their environment affects them (includes plant death and seed sprouting)
        }
        updateFlipFlop = !updateFlipFlop;
    }

    void CellCollectActionInfo()
    {
        for (int y = 0; y < LEVEL_HEIGHT; y++)
            for (int x = 0; x < LEVEL_WIDTH; x++)
                grid[x, y].CollectActionInfo(grid, x, y, this);
    }

    void CellActionUpdate()
    {
        for (int y = 0; y < LEVEL_HEIGHT; y++)
            for (int x = 0; x < LEVEL_WIDTH; x++)
                grid[x, y].PerformAction(grid, x, y, this);
    }

    void CellCollectStatusInfo()
    {
        for (int y = 0; y < LEVEL_HEIGHT; y++)
            for (int x = 0; x < LEVEL_WIDTH; x++)
                grid[x, y].CollectStatusInfo(grid, x, y, this);
    }

    void CellStatusUpdate()
    {
        for (int y = 0; y < LEVEL_HEIGHT; y++)
            for (int x = 0; x < LEVEL_WIDTH; x++)
                grid[x, y].StatusUpdate(grid, x, y, this);
    }

    public void SaveLevel(string saveFileName)
    {

        if (!Directory.Exists(savedLevelsDir))
            Directory.CreateDirectory(savedLevelsDir);

        LevelInfo levelInfo = new LevelInfo();
        string levelString = null;


        for (int y = 0; y < LEVEL_HEIGHT; y++)
        {
            for (int x = 0; x < LEVEL_WIDTH; x++)
            {
                int cellInt = (int)grid[x, y].cellType;
                levelString += cellInt.ToString() + ",";
            }
        }

        levelInfo.levelString = levelString;
        levelInfo.height = LEVEL_HEIGHT;
        levelInfo.width = LEVEL_WIDTH;

        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));

        string filepath = savedLevelsDir + "/" + saveFileName + ".xml";

        StreamWriter writer = new StreamWriter(filepath);
        serializer.Serialize(writer.BaseStream, levelInfo);
        writer.Close();
    }

    public void LoadLevel(string loadFileName)
    {
        if (!Directory.Exists(savedLevelsDir))
            return;

        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));

        string filepath = savedLevelsDir + "/" + loadFileName + ".xml";

        StreamReader reader = new StreamReader(filepath);

        LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader.BaseStream);

        LEVEL_WIDTH = levelInfo.width;
        LEVEL_HEIGHT = levelInfo.height;
        grid = new Cell[LEVEL_WIDTH, LEVEL_HEIGHT];
        string[] splitLevelString = levelInfo.levelString.Split(',');

        int idx = 0;

        for (int y = 0; y < LEVEL_HEIGHT; y++)
        {
            for (int x = 0; x < LEVEL_WIDTH; x++)
            {
                int cellVal = int.Parse(splitLevelString[idx]);
                SetCell(x, y, (CellType)cellVal);
                idx++;
            }
        }


    }
}
