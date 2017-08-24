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

			spriteBatch.DrawString(font, "Você desbloqueou: Church", new Vector2(100, 300), Color.White);
		}

		public void DrawStart(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			spriteBatch.DrawString(font, "Risland, missão 1", new Vector2(100, 100), Color.White);
			spriteBatch.DrawString(font, "Seu objetivo é destruir todos os Orcs para alcançar o", new Vector2(100, 140), Color.White);
			spriteBatch.DrawString(font, "barco que lhe levará para a próxima ilha!", new Vector2(100, 160), Color.White);

			spriteBatch.DrawString(font, "1. Destruir todos os inimigos", new Vector2(100, 200), Color.White);
			spriteBatch.DrawString(font, "2. Não deixar Olivaw morrer", new Vector2(100, 220), Color.White);
			spriteBatch.DrawString(font, "* Cuidado com os mortos!", new Vector2(100, 260), Color.White);
            spriteBatch.DrawString(font, "! Você desbloqueará: Church", new Vector2(100, 300), Color.White);

			spriteBatch.DrawString(font, "Clique para continuar", new Vector2(100, 350), Color.White);
		}
    }
}
