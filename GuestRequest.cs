using System;
using System.Collections.Generic;

namespace FluffyOctoRobot {
    class GuestRequest {

        public GuestRequest(int duration, Date date) {
            try {
                if (duration > 365 || duration < 1) {
                    throw new System.IO.InvalidDataException();
                }

            } catch () {

            }
        }

        public class Date {
            private int date, month, year;
            public Date(int d, int m, int y) {

            }
        }
    }
}

// jagged array