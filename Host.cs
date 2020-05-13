using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FluffyOctoRobot {
    public class Host {
        private int HostKey;
        private List<HostingUnit> units = new List<HostingUnit>();

        // Host constructor
        Host(int HKey, short numberOfHostingUnits) {
            HostKey = HKey;

            for (int i = 1; i <= numberOfHostingUnits; ++i) {
                units.Add(new HostingUnit());
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (HostingUnit unit in units) {
                sb.AppendLine("-------------");
                sb.AppendLine(unit.ID);
                sb.AppendLine();
                sb.AppendLine(unit.ToString());
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private static Random random = new Random();

        private string SubmitRequest(GuestRequest guest_request) {
            List<HostingUnit> available_units = units.FindAll(unit => unit.Available(guest_request));
            if (available_units.Count != 0) {
                return available_units[random.Next(available_units.Count)].ID;
            }
            return "";
        }

        public int AnnualOccupancy() {
            int sum = 0;
            foreach (HostingUnit unit in units) {
                sum += unit.AnnualOccupancy();
            }
            return sum;
        }

        public void SortUnits() {
            units.Sort();
        }

        public bool AssignRequests(params GuestRequest[] requests) {
            bool accepted = true;
            foreach (GuestRequest request in requests) {
                if (SubmitRequest(request) == "") {
                    accepted = false;
                }
            }
            return accepted;
        }

        public HostingUnit this [string ID] {
            get {
                foreach (HostingUnit unit in units) {
                    if (unit.ID == ID) {
                        return unit;
                    }
                }
                return null;
            }
        }

        public HostingUnit IEnumerable
    }

}