using System;
using System.ComponentModel;
using System.Text;

namespace lib {
    public class Order {
        private HostingUnit hosting_unit;
        public string HostingUnitKey {
            get => hosting_unit.ID;
        }
        private GuestRequest guest_request;
        public string GuestRequestKey {
            get => guest_request.ID;
        }
        private int order_id;
        public string OrderID {
            get => order_id.ToString("D8");
        }

        private Status status;

        DateTime CreateDate;
        DateTime OrderDate;

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Order Details:");
            sb.AppendLine("Hosting Unit Key: " + HostingUnitKey);
            sb.AppendLine("Guest Request Key: " + GuestRequestKey);
            sb.AppendLine("Order Key: " + OrderID);
            sb.AppendLine("Status: " + status);
            return sb.ToString();

        }

    }
}