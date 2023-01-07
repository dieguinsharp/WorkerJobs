CREATE DATABASE WorkerJobs;
USE WorkerJobs;
CREATE TABLE [dbo].[CurrencyValue](
	[IdCurrencyValue] [int] IDENTITY(1,1) NOT NULL,
	[Currency] [varchar](10) NOT NULL,
	[Value] [float] NOT NULL,
	[CurrentValueTimestamp] [datetime2](7) NULL,
	[WorkedTimestamp] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED
(
	[IdCurrencyValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
