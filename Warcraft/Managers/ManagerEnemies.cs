using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Orcs;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        public List<UnitEnemy> enemies = new List<UnitEnemy>();

        ContentManager content;

        public static Random random = new Random(Guid.NewGuid().GetHashCode());
        int wavesEnemies = 10;
        int generation = 0;

        ManagerMouse managerMouse;
        ManagerMap managerMap;
        ManagerBuildings managerBuildings;

        public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;

            for (int i = 0; i < wavesEnemies; i++)
            {
                int spawn = random.Next(0, 4);
                int armor = random.Next(0, 20);
                int sight = random.Next(1, 360);
                int damage = random.Next(1, 20);
                int precision = random.Next(1, 100);
                int hitPoints = random.Next(1, 200);

                if (random.Next(0, 100) >= 50)
                {
                    InformationUnit info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, hitPoints, armor, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 1, spawn, Util.Units.GRUNT);
                    enemies.Add(new Grunt(info, managerMouse, managerMap, managerBuildings));
                }
                else
                {
                    InformationUnit info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, hitPoints, armor, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 5, spawn, Util.Units.TROLL_AXETHROWER);
                    enemies.Add(new TrollAxethrower(info, managerMouse, managerMap, managerBuildings));
                }
            }
        }

        public void LoadContent()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].information.HitPoints > 0)
                    enemies[i].LoadContent(content);
            }

            Console.WriteLine("#--------------#");
        }

        public void LoadContent(ContentManager content)
        {
            if (this.content == null)
                this.content = content;

            enemies.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            int alives = 0;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update();

                if (enemies[i].information.HitPoints > 0)
                    alives++;
            }

            if (alives == 0)
            {
                string s = "";
                List<UnitEnemy> waveDead = new List<UnitEnemy>();
                for (int i = wavesEnemies * generation; i < (wavesEnemies * generation) + (wavesEnemies); i++)
                {
                    waveDead.Add(enemies[i]);

                    s += (enemies[i].information.Fitness + ", ");
                }

                for (int i = 0; i < waveDead.Count / 2; i++)
                {
                    UnitEnemy[] indexes = GeneticUtil.RouletteWheelSelection(waveDead);
                    string[] parent01 = GeneticUtil.Encode(indexes[0]);
                    string[] parent02 = GeneticUtil.Encode(indexes[1]);

                    int cut = random.Next(0, parent01.Length);

                    string[] child01 = new string[7];
                    string[] child02 = new string[7];

                    if (random.Next(0, 100) > 80)
                    {
                        for (int j = 0; j < parent01.Length; j++)
                        {
                            if (j < cut)
                            {
                                child01[j] = parent01[j];
                                child02[j] = parent02[j];
                            }
                            else
                            {
                                child01[j] = parent02[j];
                                child02[j] = parent01[j];
                            }
                        }
                    }
                    else
                    {
                        child01 = parent01;
                        child02 = parent02;
                    }

                    InformationUnit info01 = GeneticUtil.Decode(child01);
                    InformationUnit info02 = GeneticUtil.Decode(child02);
                    
                    if (info01.Type == Util.Units.GRUNT)
                        enemies.Add(new Grunt(info01, managerMouse, managerMap, managerBuildings));
                    else
                        enemies.Add(new TrollAxethrower(info01, managerMouse, managerMap, managerBuildings));
                   
                    if (info02.Type == Util.Units.GRUNT)
                        enemies.Add(new Grunt(info02, managerMouse, managerMap, managerBuildings));
                    else
                        enemies.Add(new TrollAxethrower(info02, managerMouse, managerMap, managerBuildings));
                }

                Console.WriteLine(s);

                generation++;

                LoadContent();
            }

            //for (int i = 0; i < enemies.Count; i++)
            //{
            //    if (enemies[i].information.HitPoints > 0)
            //    {
            //        enemies[i].information.HitPoints = 0;
            //        enemies[i].information.Fitness = random.Next(0, 500);
            //    }
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemies.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            enemies.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].selected)
                    selecteds.Add(enemies[i]);
            }

            return selecteds;
        }
    }
}
