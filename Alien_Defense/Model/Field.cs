using Alien_Defense.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model;

/// <summary>
/// Класс поля, хранящий клетки и ракету
/// </summary>
public class Field
{
    public ICell[,] AllCells;
    public HashSet<ITower> Towers = new HashSet<ITower>();
    public HashSet<TowerCell> TowerCells;
    public List<ICell> RoutesCells = new List<ICell>();
    public CellCoordinatesArray initAlienPos;
    public int cellSpriteSize = 64;
    private string levelFilePath = "Maps\\";
    public Rocket Rocket;
    public CellCoordinatesArray rocketPos;
    public Field(string lvlName)
    {
        FromText(lvlName);
    }
    /// <summary>
    /// Метод получения уровня из файла
    /// </summary>
    /// <param name="mapName"> названия файла </param>
    public void FromText(string mapName)
    {
        mapName = levelFilePath + mapName + ".txt";
        var text = File.ReadAllText(mapName);
        var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        FromLines(lines);
    }
    /// <summary>
    /// Метод построения карты из файла
    /// </summary>
    /// <param name="lines"> массив строк </param>
    public void FromLines(string[] lines)
    {
        var allCells = new ICell[lines[0].Length, lines.Length];
        var towerCells = new HashSet<TowerCell>();
        var width = allCells.GetLength(0);
        var height = allCells.GetLength(1);
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Rectangle position = new Rectangle(x * cellSpriteSize, y * cellSpriteSize,
                                                cellSpriteSize, cellSpriteSize);
                switch (lines[y][x])
                {
                    case '#':
                        allCells[x, y] = new Cell(position, CellState.Wall);
                        break;
                    case 'I':
                        allCells[x, y] = new Cell(position, CellState.Route);
                        initAlienPos = new CellCoordinatesArray(x, y);
                        break;
                    case 'P':
                        allCells[x, y] = new Cell(position, CellState.Route);
                        RoutesCells.Add(new Cell(position, CellState.Route));
                        break;
                    case 'R':
                        allCells[x, y] = new Cell(position, CellState.Rocket);
                        rocketPos = new CellCoordinatesArray(x, y);
                        Rocket = new Rocket(500, new Vector2(position.X, position.Y));
                        break;
                    case 'T':
                        var towerCell = new TowerCell(position, CellState.TowerCellFree, new CellCoordinatesArray(x, y));
                        allCells[x, y] = towerCell;
                        towerCells.Add(towerCell);
                        break;
                    default:
                        allCells[x, y] = new Cell(position, CellState.Wall);
                        break;
                }
            }
        AllCells = allCells;
        TowerCells = towerCells;
    }
}

