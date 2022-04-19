using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;

namespace CarFactory_Chassis
{
    public class ChassisProvider : IChassisProvider
    {
        private readonly ISteelSubcontractor _steelSubcontractor;
        private readonly IGetChassisRecipeQuery _chassisRecipeQuery;

        public ChassisProvider(ISteelSubcontractor steelSubcontractor, IGetChassisRecipeQuery chassisRecipeQuery)
        {
            _steelSubcontractor = steelSubcontractor;
            _chassisRecipeQuery = chassisRecipeQuery;
        }

        public Task<Chassis> GetChassis(Manufacturer manufacturer, int numberOfDoors)
        {
            return Task.Run(() =>
            {
                var chassisRecipe = _chassisRecipeQuery.Get(manufacturer);

                var chassisParts = new List<ChassisPart>
                {
                    new ChassisBack(chassisRecipe.BackId),
                    new ChassisCabin(chassisRecipe.CabinId),
                    new ChassisFront(chassisRecipe.FrontId)
                };

                CheckChassisParts(chassisParts);

                var steel = _steelSubcontractor.OrderSteel(chassisRecipe.Cost).Select(d => d.Amount).Sum();
                Interlocked.Add(ref _steelInventory, steel);

                CheckForMaterials(chassisRecipe.Cost);
                
                Interlocked.Add(ref _steelInventory, -chassisRecipe.Cost);

                var chassisWelder = new ChassisWelder();
                chassisWelder.StartWeld(chassisParts[0]);
                chassisWelder.ContinueWeld(chassisParts[1], numberOfDoors);
                chassisWelder.FinishWeld(chassisParts[2]);

                return chassisWelder.GetChassis();
            });
        }
        
        private int _steelInventory;

        private void CheckForMaterials(int cost)
        {
            if (_steelInventory < cost)
            {
                throw new Exception("Not enough chassis material");
            }
        }

        private void CheckChassisParts(List<ChassisPart> parts)
        {
            if (parts == null)
            {
                throw new Exception("No chassis parts");
            }

            if (parts.Count > 3)
            {
                throw new Exception("Chassis parts missing");
            }

            if (parts.Count < 3)
            {
                throw new Exception("To many chassis parts");
            }
        }
    }
}