using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.UI;

namespace Warcraft.Managers
{
    class ManagerUI
    {
        UI.UI ui;

        public ManagerUI(ManagerMouse managerMouse, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            ui = new Main(managerUnits, managerBuildings, managerMouse);
        }

        public void LoadContent(ContentManager content)
        {
            ui.LoadContent(content);
        }

        public void Update()
        {
            ui.Update();
        }

        public void DrawBack(SpriteBatch spriteBatch)
        {
            (ui as Main).DrawBack(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ui.Draw(spriteBatch);
        }
    }
}
