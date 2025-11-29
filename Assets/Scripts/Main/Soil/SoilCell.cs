using UnityEngine;

namespace Main.Soil
{
    public class SoilCell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CellState State { get; private set; }

        public SoilCell(int x, int y, CellState state)
        {
            X = x;
            Y = y;
            State = state;
        }

        public bool ComparePosition(Vector3Int position)
        {
            return X == position.x && Y == position.y;
        }

        public void SetState(CellState state)
        {
            State = state;
        }
    }
}