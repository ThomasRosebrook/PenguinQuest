using Game3.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace Game3
{
    public class ContentLoader
    {
        ContentManager _content;
        string RootDirectory = "";

        public ContentLoader(ContentManager content)
        {
            _content = content;
            RootDirectory = _content.RootDirectory;
        }

        public List<World> LoadContent(string filename, Goal goal)
        {
            string data = File.ReadAllText(Path.Join(RootDirectory, filename));
            var lines = data.Split('\n');
            List<World> worlds = new List<World>();

            World currWorld = new World(0,"Null",null);
            foreach (string line in lines)
            {
                if (line[0] == '-')
                {
                    worlds.Add(currWorld);
                }
                else if (line[0] != '+')
                {
                    currWorld = new World(int.Parse($"{line[5]}"), line.Substring(7), new List<Level>());
                }
                else if (currWorld.WorldIndex != 0) currWorld.Levels.Add(LoadLevel($"Level {line[6]}", goal, $"{@"Levels\"}{currWorld.WorldIndex}{line.Substring(1).Trim()}.txt"));
            }

            LoadSaveData(ref worlds);

            return worlds;
        }

        Level LoadLevel (string name, Goal goal, string filename)
        {
            string data = File.ReadAllText(Path.Join(RootDirectory, filename));
            var lines = data.Split('\n');

            Tilemap tilemap = new Tilemap(filename);

            Level level = new Level(name, goal, tilemap);

            var fifthLine = lines[4].Split(',');
            level.SetGoalPosition(new Vector2(float.Parse(fifthLine[0]), float.Parse(fifthLine[1])));

            if (lines[5] != "-")
            {
                var obstacleLine = lines[5].Split(',');
                var obstacleSizeLine = lines[6].Split(',');
                int i;
                for (i = 0; i < int.Parse(obstacleLine[1]); i++)
                {
                    var pos = lines[7 + i].Split(',');
                    level.AddObstacle(
                        new Vector2(int.Parse(pos[0]),
                        int.Parse(pos[1]) + 96),
                        int.Parse(obstacleSizeLine[0]),
                        int.Parse(obstacleSizeLine[1]),
                        obstacleLine[0].Trim());
                }
            }
            

            return level;
        }

        void LoadSaveData(ref List<World> worlds)
        {
            string data = File.ReadAllText("Save.txt");
            var lines = data.Split('\n');

            int currWorld = 0;

            foreach (string line in lines)
            {
                if (line == "") continue;
                if (line[0] == 'W') currWorld = int.Parse($"{line[5]}");
                else if (line[0] == 'L')
                {
                    var levelInfo = line.Split(',');
                    if (levelInfo[1] != "") worlds[currWorld - 1].Levels[int.Parse($"{line[5]}") - 1].BestTime = float.Parse(levelInfo[1]);
                }
            }
        }


    }
}
