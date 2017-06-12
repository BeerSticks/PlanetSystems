namespace PostgreSQLData
{
    public partial class OwnPlanetarySystem
    {
        public OwnPlanetarySystem()
        {
            this.User = new User();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}