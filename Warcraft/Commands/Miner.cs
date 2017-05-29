using Microsoft.Xna.Framework;
using Warcraft.Buildings;
using Warcraft.Buildings.Humans;
using Warcraft.Buildings.Neutral;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Commands
{
    enum State
    {
        MINER,
        TOWN_HALL
    }

    class Miner : ICommand
    {
        public bool started;

        GoldMine goldMine;
        CityHall cityHall;
        Unit worker;

        ManagerBuildings managerBuildings;
		ManagerUnits managerUnits;

		float elapsed;

        State currentState;

        public Miner(ManagerBuildings managerBuildings, ManagerUnits managerUnits, Unit worker)
        {
            this.worker = worker;
            this.managerBuildings = managerBuildings;
            this.managerUnits = managerUnits;
        }

        public void execute()
        {
            cityHall = managerBuildings.buildings.Find((b) => 
                                                       (b.information as InformationBuilding).Type == Util.Buildings.TOWN_HALL ||
                                                       (b.information as InformationBuilding).Type == Util.Buildings.GREAT_HALL) as CityHall;

            if (cityHall != null)
            {
				float maxDistance = float.MaxValue;
				for (int i = 0; i < ManagerBuildings.goldMines.Count; i++)
				{
                    float distance = Vector2.Distance(ManagerBuildings.goldMines[i].Position, cityHall.Position);
                    if (distance < maxDistance && ManagerBuildings.goldMines[i].QUANITY > 0)
					{
						maxDistance = distance;
						goldMine = ManagerBuildings.goldMines[i];
					}
				}

                if (goldMine != null && goldMine.QUANITY > 0)
                {
                    started = true;

                    goldMine.workers.Add(worker as Builder);
                    worker.workState = WorkigState.GO_TO_WORK;
                    worker.Move((int)goldMine.Position.X / 32, (int)goldMine.Position.Y / 32);
                    worker.selected = false;

                    currentState = State.MINER;

                    Data.Write("Começar Mineração [" + (worker.information as InformationUnit).Type + ", GoldMiner]");
                }
            }
        }

        public void Update()
        {
            if (worker.workState == WorkigState.WORKING && goldMine.workers.Count > 0)
            {
                goldMine.animations.Change("working");

                elapsed += 0.1f;

                if (elapsed >= 10)
                {
                    worker.workState = WorkigState.GO_TO_WORK;

                    if (currentState == State.MINER)
                    {
                        worker.Move((int)cityHall.Position.X / 32, (int)cityHall.Position.Y / 32);
                        worker.animations.currentAnimation = Util.AnimationType.GOLD;

						goldMine.QUANITY -= 100;
						goldMine.animations.Change("normal");
                        currentState = State.TOWN_HALL;

                        Data.Write("Entregando Gold [" + (worker.information as InformationUnit).Type + ", GoldMiner]");
                    }
                    else
                    {
                        worker.Move((int)goldMine.Position.X / 32, (int)goldMine.Position.Y / 32);
                        worker.animations.currentAnimation = Util.AnimationType.WALKING;

                        ManagerResources.ReduceGold(managerUnits.index, -100);

                        if (goldMine.QUANITY <= 0)
                        {
                            goldMine.Fire();
                        }

                        goldMine.animations.Change("working");
                        currentState = State.MINER;

                        Data.Write("Minerando [" + (worker.information as InformationUnit).Type + ", GoldMiner]");
                    }

                    elapsed = 0;
                }
            }
        }
    }
}
