using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alien_Defense.Model;

internal class Field
{
    public readonly FieldCell[,] Cells;
    public readonly Cell[,] Cells3;
    public readonly List<Cell> Cells2;
    public readonly List<TowerCell> Towers = new List<TowerCell>();
    public readonly HashSet<TowerCell> towerCells;
    public event Func<TowerCell> addTowerEvent;
    private readonly int cellSpriteSize = 64;
    public Field(FieldCell[,] cells)
    {
        Cells = cells;
        Cells3 = new Cell[cells.GetLength(0), cells.GetLength(1)];
        towerCells = new HashSet<TowerCell>();
        Cells2 = new List<Cell>();
        var width = Cells.GetLength(0);
        var height = Cells.GetLength(1);
        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Rectangle position = new Rectangle(x * cellSpriteSize, y * cellSpriteSize,
                                                cellSpriteSize, cellSpriteSize);

                if (Cells[x, y] == FieldCell.Tower)
                {
                    Cells3[x, y] = new Cell(position, CellState.TowerCell);
                }
                else if (Cells[x, y] == FieldCell.Route)
                {
                    Cells3[x, y] = new Cell(position, CellState.Route);
                }
                else
                    Cells3[x,y] = new Cell(position, CellState.Background);
                


            }
        var d = Cells3.Length;
    }
    public void AddTowerEvent(Func<TowerCell> addTowerEvent)
    {
        this.addTowerEvent += addTowerEvent;
        AddTower();
    }
    private void AddTower()
    {
        var tower = addTowerEvent?.Invoke();
        Towers.Add(tower);
        addTowerEvent = null;
    }
    public static Field FromText(string text)
    {
        var lines = text.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        return FromLines(lines);
    }
    public static Field FromLines(string[] lines)
    {
        var field = new FieldCell[lines[0].Length, lines.Length];
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                switch (lines[y][x])
                {
                    case '#':
                        field[x, y] = FieldCell.Back;
                        break;
                    case 'R':
                        field[x, y] = FieldCell.Route;
                        break;
                    case 'T':
                        field[x, y] = FieldCell.Tower;
                        break;
                    default:
                        field[x, y] = FieldCell.Back;
                        break;
                }
            }
        }
        return new Field(field);
    }
}

