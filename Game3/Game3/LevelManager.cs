using Microsoft.Xna.Framework;
using Game3.Screens;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Text;

namespace Game3
{
    public class LevelManager
    {
        public int CurrentLevel = 0;
        public int CurrentWorld = 0;
        private List<World> Worlds;
        public bool IsInitialized = false;

        private readonly Goal Goal;

        private int width;
        private int height;

        public bool HasNextLevel
        {
            get => IsInitialized && CurrentLevel + 1 < Worlds[CurrentWorld].Levels.Count;
        }

        public int NumWorlds
        {
            get => Worlds.Count;
        }

        public int NumLevels
        {
            get => Worlds[CurrentWorld].Levels.Count;
        }

        public SpriteBatch SpriteBatch { get; private set; }

        public LevelManager(Goal goal, int screenWidth, int screenHeight)
        {
            Goal = goal;
            width = screenWidth;
            height = screenHeight;
            //InitializeLevels();
        }

        public void InitializeLevels (ContentManager _content)
        {
            ContentLoader contentLoader = new ContentLoader(_content);
            Worlds = contentLoader.LoadContent(@"Levels\Levels.txt", Goal);
            IsInitialized = true;
        }

        public Level GetCurrentLevel()
        {
            if (IsInitialized) return Worlds[CurrentWorld].Levels[CurrentLevel];
            else return null;
        }

        public World GetCurrentWorld()
        {
            return Worlds[CurrentWorld];
        }

        public void SetCurrentWorld(int worldIndex)
        {
            if (worldIndex >= 0 && worldIndex < Worlds.Count) 
            { 
                CurrentWorld = worldIndex;
                CurrentLevel = 0;
            }
        }

        public void SaveLevelData()
        {
            StringBuilder saveText = new StringBuilder();
            foreach (World world in Worlds)
            {
                saveText.Append($"World{world.WorldIndex}\n");
                foreach (Level level in world.Levels)
                {
                    saveText.Append($"Level{level.LevelName.Split(' ')[1]},{level.GetBestTime()}\n");
                }

            }

            File.WriteAllText("Save.txt", saveText.ToString());
        }
        
    }
}
