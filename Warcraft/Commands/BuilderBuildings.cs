using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Buildings;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Util;

namespace Warcraft.Commands
{
    class BuilderBuildings : ICommand
    {
        public Buildings.Building building;

        private ManagerBuildings managerBuildings;
        private ManagerMouse managerMouse;
        ManagerUnits managerUnits;

        private Unit builder;

        public BuilderBuildings(Util.Buildings building, Unit builder, ManagerMouse managerMouse, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            this.builder = builder;

            this.managerMouse = managerMouse;
            this.managerUnits = managerUnits;
            this.managerBuildings = managerBuildings;

            this.building = Buildings.Building.Factory(building, managerMouse, managerBuildings.managerMap, managerUnits);
        }

        public void execute()
        {
            if (ManagerResources.CompareGold(managerUnits.index, building.information.CostGold) && ManagerResources.CompareFood(managerUnits.index, building.information.CostWood))
            {
                ManagerResources.ReduceGold(managerUnits.index, building.information.CostGold);

				if ((building.information as InformationBuilding).Type == Util.Buildings.CHICKEN_FARM ||
                    (building.information as InformationBuilding).Type == Util.Buildings.PIG_FARM)
                    ManagerResources.ReduceFood(managerUnits.index, -5);

				Data.Write("Construindo Predio [" + (building.information as InformationBuilding).Type + "]");

                builder.workState = WorkigState.WAITING_PLACE;
                building.builder();
            }
        }

        public void LoadContent(ContentManager content)
        {
            building.LoadContent(content);
        }

        public void Update()
        {
            if (building.isBuilding)
                building.Update();

            if (!building.isBuilding && building.isWorking)
            {
                managerBuildings.AddBuilding(building);
                building = Building.Factory((building.information as InformationBuilding).Type, managerMouse, managerBuildings.managerMap, managerUnits);

                builder.workState = WorkigState.NOTHING;
                builder.position.Y += 32 * 2;
                builder.MoveTo(0, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (building.isBuilding)
                building.Draw(spriteBatch);
        }
    }
}
