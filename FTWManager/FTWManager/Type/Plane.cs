using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTWManager.Type
{
    class Plane
    {
        public string Name;

        public int EconemySeats;
        public int BusinessSeats;
        public int occupiedSeats;

        public double paxMoney;

        public int Cargo;
        public int occupiedCargo;
        public double cargoMoney;

        public int Payloud;
        public int occupiedPayloud;

        public  List<Assignment> load = new List<Assignment>();

        public List<Assignment> getLoad()
        {
            return load;
        }
            

        public bool LoadAssignment(Assignment _assignment)
        {
            if (_assignment.Type == 1)
            {
                if (_assignment.Amount + occupiedSeats <= EconemySeats + BusinessSeats && _assignment.paxCargo + occupiedCargo <= Cargo && _assignment.paxWeight + occupiedCargo + occupiedPayloud <= Payloud)
                {
                    occupiedSeats += _assignment.Amount;
                    occupiedCargo += _assignment.paxCargo;
                    occupiedPayloud += _assignment.paxCargo + _assignment.paxWeight;
                    paxMoney += _assignment.Money;                 
                }
                else
                {
                    return false;
                }
            }
            else if (_assignment.Type == 3)
            {
                if (_assignment.Amount + occupiedCargo <= Cargo && _assignment.Amount + occupiedPayloud <= Payloud)
                {
                    occupiedCargo += _assignment.Amount;
                    occupiedPayloud += _assignment.Amount;
                    cargoMoney += _assignment.Money;
                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            load.Add(_assignment);

            return true;

        }

        public void reset()
        {
            occupiedSeats = 0;
            occupiedCargo = 0;
            occupiedPayloud = 0;
            cargoMoney = 0;
            paxMoney = 0;
            load.Clear();
        }
    }
}
