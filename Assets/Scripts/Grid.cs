using UnityEngine;

namespace Assets.Scripts
{
    public class Grid : MonoBehaviour
    {
        public int XLength;
        public int ZLength;

        public Vector3 StartingPoint;

        private float _xSize;
        private float _zSize;

        private int[,] _tiles;

        public float TileXSize { get; set; }
        public float TileZSize { get; set; }

        private void Awake()
        {
            Mesh mesh = transform.GetComponent<MeshFilter>().mesh;
            _xSize = mesh.bounds.size.x*transform.localScale.x;
            _zSize = mesh.bounds.size.z*transform.localScale.z;

            TileXSize = _xSize/XLength;
            TileZSize = _zSize/ZLength;

            _tiles = new int[ZLength, XLength];
        }

        public bool PositionOnGridAvailable(int z, int x, int zLength, int xLength)
        {
            bool available = true;

            for (int i = 0; i < zLength; i++)
            {
                if (z + i >= ZLength)
                {
                    available = false;
                    break;
                }

                for (int j = 0; j < xLength; j++)
                {
                    if (x + j >= XLength)
                    {
                        available = false;
                        break;
                    }

                    if (_tiles[i + z, j + x] != 0)
                    {
                        return false;
                    }
                }
            }

            return available; //_tiles[z, x] == 0;
        }

        public void AddStructureToGrid(Structure s)
        {
            for (int i = 0; i < s.ZLength; i++)
            {
                for (int j = 0; j < s.XLength; j++)
                {
                    _tiles[s.StartingZ + i, s.StartingX + j] = s.GetStructureType();
                }
            }
//            PrintGrid();
        }

        private void PrintGrid()
        {
            for (int i = 0; i < XLength; i++)
            {
                string s = "";
                for (int j = 0; j < ZLength; j++)
                {
                    s += _tiles[j, i] + ", ";
                }
                Debug.Log(s);
            }
        }
    }
}