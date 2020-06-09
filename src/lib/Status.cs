namespace lib {
    public class Status : ICloneable<Status> {
        public string Name {
            get;
            private set;
        }

        public Status(string name) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
        public Status Clone() {
            return new Status(Name);
        }
    }
}