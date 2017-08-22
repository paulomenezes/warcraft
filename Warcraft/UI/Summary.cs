using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;

namespace Warcraft.UI
{
    class Summary : UI
    {
        public static int UNITS = 0;
        public static int BUILDINGS = 0;

        public static int ENEMEY_UNITS = 0;
        public static int ENEMEY_BUILDINGS = 0;

        public static int SKELETONS = 0;

        public Summary()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, ManagerUnits playerUnits, ManagerUnits botsUnits)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Resumo ilha " + (Warcraft.ISLAND), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, "Unidades: " + playerUnits.units.Count, new Vector2(100, 140), Color.White);
            spriteBatch.DrawString(font, "Construções: " + BUILDINGS, new Vector2(100, 160), Color.White);

            spriteBatch.DrawString(font, "Unidades inimigas: " + botsUnits.units.Count, new Vector2(100, 200), Color.White);
            spriteBatch.DrawString(font, "Construções inimigas: " + ENEMEY_BUILDINGS, new Vector2(100, 220), Color.White);

            spriteBatch.DrawString(font, "Esqueletos: " + SKELETONS, new Vector2(100, 260), Color.White);
        }
    }
}
