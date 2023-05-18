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

//Набор свойств и полей
//Методы изменения свойств и полей
internal class Field
{
    public readonly ICell[,] StaticCells;
    public HashSet<Tower> Towers = new HashSet<Tower>();
    public HashSet<TowerCell> TowerCells;
    public static Vector2 firstRoute;
    public static List<ICell> RoutesCells = new List<ICell>();
    public static Rectangle initPos;
    public static int cellSpriteSize;
    private static string levelFilePath = "Maps\\";
    public Field(ICell[,] staticCells, HashSet<TowerCell> towerCells)
    {
        StaticCells = staticCells;
        TowerCells = towerCells;
    }
    public static Field FromText(string mapName)
    {
        mapName = levelFilePath + mapName  + ".txt";
        var text = File.ReadAllText(mapName);
        var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        return FromLines(lines);
    }
    public static Field FromLines(string[] lines)
    {
        var staticCells = new ICell[lines[0].Length, lines.Length];
        var towerCells = new HashSet<TowerCell>();
        var width = staticCells.GetLength(0);
        var height = staticCells.GetLength(1);
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Rectangle position = new Rectangle(x * cellSpriteSize, y * cellSpriteSize,
                                                cellSpriteSize, cellSpriteSize);
                switch (lines[y][x])
                {
                    case '#':
                        staticCells[x, y] = new Cell(position, CellState.Wall);
                        break;
                    case 'I':
                        staticCells[x, y] = new Cell(position, CellState.Route);
                        initPos = position;
                        break;
                    case 'P':
                        staticCells[x, y] = new Cell(position, CellState.Route);
                        RoutesCells.Add(new Cell(position, CellState.Route));
                        break;
                    case 'R':
                        staticCells[x, y] = new Cell(position, CellState.Rocket);
                        break;
                    case 'T':
                        staticCells[x, y] = new TowerCell(position, CellState.TowerCellFree);
                        towerCells.Add(new TowerCell(position, CellState.TowerCellFree));
                        break;
                    default:
                        staticCells[x, y] = new Cell(position, CellState.Wall);
                        break;
                }
            }

        return new Field(staticCells, towerCells);
    }
}

