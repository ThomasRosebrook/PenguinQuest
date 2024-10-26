using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game3
{
    public class Tilemap
    {
        int _tileWidth;
        int _tileHeight;
        int _mapWidth;
        int _mapHeight;

        Texture2D _tilesetTexture;

        Rectangle[] _tiles;

        int[] _map;

        string _filename;

        public bool IsLoaded = false;

        public List<BlockGroup> Blocks { get; private set; }

        public int Width
        {
            get => _mapWidth;
        }

        public int Height
        {
            get => _mapHeight;
        }

        public int TileWidth
        {
            get => _tileWidth;
        }

        public int TileHeight
        {
            get => _tileHeight;
        }

        public Tilemap(string filename)
        {
            _filename = filename;
            Blocks = new List<BlockGroup>();
        }

        public void LoadContent(ContentManager content)
        {
            string data = File.ReadAllText(Path.Join(content.RootDirectory, _filename));
            var lines = data.Split('\n');
            
            //First line is the tileset filename
            var tilesetFilename = lines[0].Trim();
            _tilesetTexture = content.Load<Texture2D>(tilesetFilename);

            //Second line is the tile size
            var secondLine = lines[1].Split(',');
            _tileWidth = int.Parse(secondLine[0]);
            _tileHeight = int.Parse(secondLine[1]);

            int tilesetColumns = _tilesetTexture.Width / _tileWidth;
            int tilesetRows = _tilesetTexture.Height / _tileHeight;
            _tiles = new Rectangle[tilesetColumns * tilesetRows];

            for (int y = 0; y < tilesetColumns; y++)
            {
                for (int x = 0; x < tilesetRows; x++)
                {
                    int tileIndex = y * tilesetColumns + x;
                    _tiles[tileIndex] = new Rectangle(x * _tileWidth, y * _tileHeight, _tileWidth, _tileHeight);
                }
            }

            //Third line is the map size
            var thirdLine = lines[2].Split(',');
            _mapWidth = int.Parse(thirdLine[0]);
            _mapHeight = int.Parse(thirdLine[1]);

            //Now we can create our map
            var fourthLine = lines[3].Split(',');
            _map = new int[_mapWidth * _mapHeight];
            BlockGroup block = new BlockGroup(_tilesetTexture, new Vector2(0,0), _tileWidth, _tileHeight);
            //int prevIndex = -1;
            int index = -1;
            /*
            for (int k = 0; k < _mapWidth * _mapHeight; k++)
            {
                prevIndex = index;
                index = int.Parse(fourthLine[k]);
                if (index == -1 || prevIndex)
            }
            */

            for (int i = 0; i < _mapHeight; i++)
            {
                for (int j = 0; j < _mapWidth; j++)
                {
                    //prevIndex = index;
                    index = int.Parse(fourthLine[i * _mapWidth + j]);
                    if (index == -1)
                    {
                        if (i < _mapHeight - 1 || j < _mapWidth - 1)
                        {
                            if (block.BlockCount > 0)
                            {
                                Blocks.Add(block);
                                block = new BlockGroup(_tilesetTexture, new Vector2(0, 0), _tileWidth, _tileHeight);
                            }
                        }
                        else
                        {
                            Blocks.Add(block);
                        }
                    }
                    else
                    {
                        block.AddBlock(_tiles[index], new Vector2(j * _tileWidth + _tileWidth / 2, i * _tileHeight + _tileHeight / 2 + 96));
                    }
                }
            }
            if (block.BlockCount > 0) Blocks.Add(block);

            IsLoaded = true;
        }

        public void Unload()
        {
            IsLoaded = false;
            Blocks = new List<BlockGroup>();
        }

        /*
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                for (int x = 0; x < _mapHeight; x++)
                {
                    int index = _map[y * _mapWidth + x] - 1;
                    if (index == -1) continue;

                    spriteBatch.Draw(_tilesetTexture, new Vector2(x * _tileWidth, y * _tileHeight), _tiles[index], Color.White);
                }
            }
        }
        */
    }
}
