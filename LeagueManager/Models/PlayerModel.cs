namespace LeagueManager.Models
{
    public class PlayerModel
    {
        public string Team { get; set; }
        public string PlayerName { get; set; }
        public long Entry { get; set; }
        public long Score { get; set; }
        public long BenchScore { get; set; }
        public long Transfers { get; set; }
        public string ChipsUsed { get; set; }
        public long PlayerTransfersCost { get; set; }
    }
}