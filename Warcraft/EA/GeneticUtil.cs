using System;
namespace Warcraft.EA
{
    class GeneticUtil
	{
		public static String IntToBinary(int value)
		{
            return Convert.ToString(value, 2);
		}

        public static String IntToBinary(int value, int size)
        {
            return Convert.ToString(value, 2).PadLeft(size, '0'); // 0011
		}
    }
}
