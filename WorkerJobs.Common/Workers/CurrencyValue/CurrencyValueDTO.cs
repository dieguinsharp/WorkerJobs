namespace WorkerJobs.Common.Workers.CurrencyValue {
    public class CurrencyValueDTO {
        public decimal Bid { get; set; }
        public DateTime Create_Date  { get; set; }

        public class Root {
            public CurrencyValueDTO USD { get; set; }
        }
    }
}
