namespace tthk_kinoteater.Interfaces
{
    public interface IPlace
    {
        public int Number { get; set; }
        public int Row { get; set; }
        public double TicketAdditionalCost { get; }
    }
}