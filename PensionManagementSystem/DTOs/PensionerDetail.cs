namespace PensionManagementSystem.DTOs
{
    public class PensionerDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public int SalaryEarned { get; set; }
        public int Allowances { get; set; }
        public string SelfOrFamilyPension { get; set; }
    }
}
