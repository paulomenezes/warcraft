using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Units;

namespace Warcraft.Util
{
    class GeneticUtil
    {
        static Random random = new Random();

        public static string[] Encode(Unit enemy)
        {
            string damage = IntToBinary(enemy.information.Damage);
            string armor = IntToBinary(enemy.information.Armor);
            string hitPoints = IntToBinary(Convert.ToInt32(enemy.information.HitPointsTotal));
            string sight = IntToBinary(enemy.information.Sight);
            string spawn = IntToBinary(enemy.information.Spawn);
            string precision = IntToBinary(enemy.information.Precision);
            string type = IntToBinary((int)enemy.information.Type);
            
            return new string[7] { damage, armor, hitPoints, sight, spawn, precision, type };
        }

        public static InformationUnit Decode(string[] data)
        {
            string damage = data[0];
            string armor = data[1];
            string hitPoints = data[2];
            string sight = data[3];
            string spawn = data[4];
            string precision = data[5];
            string type = data[6];

            int _damage = BinaryToInt(damage);
            int _armor = BinaryToInt(armor);
            int _sight = BinaryToInt(sight);
            int _precision = BinaryToInt(precision);

            int _spawn = BinaryToInt(spawn);
            int _type = BinaryToInt(type);

            float _hitPoints = BinaryToInt(hitPoints);

            InformationUnit info = null;
            if (_type < 3)
                info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.GRUNT);
            else if (_type >= 3)
                info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, _hitPoints, _armor, _sight, 10, 600, 1, Buildings.NONE, 60, _damage, _precision, 1, _spawn, Units.TROLL_AXETHROWER);

            return info;
        }

        public static float BinaryToFloat(string value)
        {
            int ii = Convert.ToInt32(value, 2);
            byte[] bb = BitConverter.GetBytes(ii);
            float cc = BitConverter.ToSingle(bb, 0);

            char[] valueChar = value.ToCharArray();

            for (int j = 0; j < valueChar.Length; j++)
            {
                if (random.Next(0, 100) < 5)
                {
                    if (valueChar[j] == '1')
                        valueChar[j] = '0';
                    else
                        valueChar[j] = '1';
                }
            }

            value = new string(valueChar);

            int i = Convert.ToInt32(value, 2);
            byte[] b = BitConverter.GetBytes(i);
            float c = BitConverter.ToSingle(b, 0);

            return c;
        }

        public static string FloatToBinary(float value)
        {
            byte[] b = BitConverter.GetBytes(value);
            int i = BitConverter.ToInt32(b, 0);
            return Convert.ToString(i, 2);
        }

        public static string IntToBinary(int value)
        {
            return Convert.ToString(value, 2);
        }

        public static int BinaryToInt(string value)
        {
            char[] valueChar = value.ToCharArray();

            for (int j = 0; j < valueChar.Length; j++)
            {
                if (random.Next(0, 100) < 5)
                {
                    if (valueChar[j] == '1')
                        valueChar[j] = '0';
                    else
                        valueChar[j] = '1';
                }
            }

            value = new string(valueChar);

            return Convert.ToInt32(value, 2);
        }

        public static UnitEnemy[] RouletteWheelSelection(List<UnitEnemy> enemies)
        {
            enemies = enemies.OrderBy(e => e.information.Fitness).ToList();
            enemies.Reverse();

            float[] fitness = new float[enemies.Count];
            for (int i = 0; i < fitness.Length; i++)
            {
                if (i == 0)
                    fitness[i] = enemies[i].information.Fitness;
                else
                    fitness[i] = fitness[i - 1] + enemies[i].information.Fitness;
            }

            Random random = new Random();
            double value01 = random.NextDouble() * fitness[fitness.Length - 1];
            double value02 = random.NextDouble() * fitness[fitness.Length - 1];

            int[] index = new int[2];
            index[0] = -1;
            index[1] = -1;

            for (int i = 0; i < fitness.Length; i++)
            {
                if (index[0] == -1 && fitness[i] > value01)
                    index[0] = i;

                if (index[1] == -1 && fitness[i] > value02)
                    index[1] = i;

                if (index[0] != -1 && index[1] != -1)
                    break;
            }

            if (index[0] == index[1])
                index = noRepeat(index, fitness);

            return new UnitEnemy[2] { enemies[index[0]], enemies[index[1]] };
        }

        private static int[] noRepeat(int[] indexes, float[] fitness)
        {
            Random random = new Random();

            while (indexes[0] == indexes[1])
            {
                indexes[1] = -1;

                double value01 = random.NextDouble() * fitness[fitness.Length - 1];

                for (int i = 0; i < fitness.Length; i++)
                {
                    if (indexes[1] == -1 && fitness[i] > value01)
                        indexes[1] = i;

                    if (indexes[1] != -1)
                        break;
                }
            }

            return indexes;
        }
    }
}
