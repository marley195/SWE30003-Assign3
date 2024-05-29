namespace RelaxingKoala
{
    public class Staff
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Position { get; set; }
        public string ContactNumber { get; set; }

        public Staff(string staffName, string staffID, string staffPosition, string staffContactNumber)
        {
            Name = staffName;
            ID = staffID;
            Position = staffPosition;
            ContactNumber = staffContactNumber;
        }    
    }
}

