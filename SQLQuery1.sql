CREATE TABLE [dbo].[Room] (
    [Room]          NVARCHAR (50)  NOT NULL,
	[Tempature] DECIMAL (3, 2) NOT NULL,
    [CO2]       DECIMAL (3, 2) NOT NULL,
	[Humdity]	DECIMAL (3, 2) NOT NULL,
    [PumpStatus]    BIT            DEFAULT ('0') NOT NULL,
);

