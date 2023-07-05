namespace Lab_8.Class
{
    public class Address
    {
		public int ID { get; set; }
        public string City { get; set; }
        public string House { get; set; }
        public string Street { get; set; }
		public Address() { }
		public Address(int id, string department, string sNP, string audience)
        {
			ID = id;
            City = department;
            House = sNP;
            Street = audience;
        }
    }
    public class Flat
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Square { get; set; }
		public int BuildYear { get; set; }
		public int Floor { get; set; }
		public int Price { get; set; }
		public bool IsBricked { get; set; }
		public byte[] Image { get; set; }
		public Address Address { get; set; }
		public Flat()
		{
			ID = 0;
			Name = string.Empty;
		}

		public Flat(int customerID, string name, int semester, int kurs, int lectionsNum, int labsNum, bool isExam, byte[] image, Address lector)
		{
			ID = customerID;
			Name = name;
			Square = semester;
			BuildYear = kurs;
			Floor = lectionsNum;
			Price = labsNum;
			IsBricked = isExam;
			Image = image;
			Address = lector;
		}
	}
}