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
        TownHall townHall;
        Unit worker;

        ManagerBuildings managerBuildings;

        float elapsed;

        State currentState;

        public Miner(ManagerBuildings managerBuildings, Unit worker)
        {
            goldMine = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.GOLD_MINE) as GoldMine;
            townHall = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.TOWN_HALL) as TownHall;

            this.worker = worker;
            this.managerBuildings = managerBuildings;
        }

        public void execute()
        {
            townHall = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.TOWN_HALL) as TownHall;

            if (townHall != null)
            {
                started = true;

                goldMine.workers.Add(worker as Peasant);
                worker.workState = WorkigState.GO_TO_WORK;
                worker.Move((int)goldMine.Position.X / 32, (int)goldMine.Position.Y / 32);
                worker.selected = false;

                currentState = State.MINER;

                Data.Write("Começar Mineração [Peasant, GoldMiner]");
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
                        worker.Move((int)townHall.Position.X / 32, (int)townHall.Position.Y / 32);
                        worker.animations.currentAnimation = Util.AnimationType.GOLD;

                        goldMine.animations.Change("normal");
                        currentState = State.TOWN_HALL;

                        Data.Write("Entregando Gold [Peasant, GoldMiner]");
                    }
                    else
                    {
                        worker.Move((int)goldMine.Position.X / 32, (int)goldMine.Position.Y / 32);
                        worker.animations.currentAnimation = Util.AnimationType.WALKING;

                        Warcraft.GOLD += 100;

                        goldMine.animations.Change("working");
                        currentState = State.MINER;

                        Data.Write("Minerando [Peasant, GoldMiner]");
                    }

                    elapsed = 0;
                }
            }
        }
    }
}
