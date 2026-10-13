public class AnalyticsSummaryResponse
{
    public int TotalBooks { get; set; }
    public int ActiveReaders { get; set; }
    public int OverdueBooks { get; set; }
    public int ActiveReadersCount { get; set; }
    public double AverageRenewalsPerReader { get; set; }
}
