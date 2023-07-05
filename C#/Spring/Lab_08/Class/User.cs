namespace Lab_8.Class
{
    public class Lector
    {
		public int ID { get; set; }
        public string Department { get; set; }
        public string SNP { get; set; }
        public string Audience { get; set; }
		public Lector() { }
		public Lector(int id, string department, string sNP, string audience)
        {
			ID = id;
            Department = department;
            SNP = sNP;
            Audience = audience;
        }
    }
    public class Discipline
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public int Semester { get; set; }
		public int Kurs { get; set; }
		public int LectionsNum { get; set; }
		public int LabsNum { get; set; }
		public bool IsExam { get; set; }
		public byte[] Image { get; set; }
		public Lector Lector { get; set; }
		public Discipline()
		{
			ID = 0;
			Name = string.Empty;
		}

		public Discipline(int customerID, string name, int semester, int kurs, int lectionsNum, int labsNum, bool isExam, byte[] image, Lector lector)
		{
			ID = customerID;
			Name = name;
			Semester = semester;
			Kurs = kurs;
			LectionsNum = lectionsNum;
			LabsNum = labsNum;
			IsExam = isExam;
			Image = image;
			Lector = lector;
		}

		public override string ToString()
		{
			return $"CustomerID - {ID}\n" +
				   $"FirstName - {Name} \n" +
				   $"LastName - {Semester}\n" +
				   $"Email - {Kurs}\n" +
				   $"Phone - {LectionsNum}\n" +
				   $"Address - {LabsNum}";
		}
	}
}