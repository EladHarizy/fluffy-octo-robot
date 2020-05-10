using System;
using System.Collections;
using System.Collections.Generic;

namespace FluffyOctoRobot {
    public class Host {
        private int HostKey;
        private List<HostingUnit> units = new List<HostingUnit>();

        //not sure what this does
        //   internal List<HostingUnit> Units { get => units; set => units = value; }

        // Host constructor
        Host(int HKey, short numberOfHostingUnits) {
            HostKey = HKey;

            for (int i = 1; i <= numberOfHostingUnits; ++i) {
                units.Add(new HostingUnit());
            }
        }
    }

}