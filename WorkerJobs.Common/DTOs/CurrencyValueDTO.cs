namespace WorkerJobs.Common.DTOs{
    public class CurrencyValueDTO {
        public decimal Bid { get; set; }
        public DateTime Create_Date  { get; set; }
        public string Code { get; set; }
        public string CodeIn { get; set; }

        public class Root {
            public CurrencyValueDTO USD { get; set; }
        }
    }
}
