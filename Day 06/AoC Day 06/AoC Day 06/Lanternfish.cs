using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day_06
{
    public class Lanternfish
    {

        public uint spawnTimer { get; private set; }
        public Lanternfish()
        {
            spawnTimer = 8;
        }

        public Lanternfish(uint timer)
        {
            spawnTimer = timer;
        }

        public void timerTick()
        {
            if (spawnTimer > 0)
                spawnTimer--;
            else
                spawnTimer = 6;
        }
    }
}
