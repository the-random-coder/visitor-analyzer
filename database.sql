CREATE TABLE [dbo].[IpAddressLookups] (
    [IpAddress]   VARCHAR (52)   NOT NULL,
    [Date]        DATE           NOT NULL,
    [CountryName] NVARCHAR (255) NULL,
    [CountryCode] NVARCHAR (10)  NULL,
    [City]        NVARCHAR (255) NULL,
    [Region]      NVARCHAR (255) NULL,
    [RegionCode]  NVARCHAR (255) NULL,
    [Continent]   NVARCHAR (50)  NULL,
    [Latitude]    FLOAT (53)     NULL,
    [Longitude]   FLOAT (53)     NULL,
    CONSTRAINT [PK_IpAddressLookups] PRIMARY KEY CLUSTERED ([IpAddress] ASC, [Date] ASC)
);

GO
CREATE NONCLUSTERED INDEX [IX_IpAddressLookups_CityGrouping]
    ON [dbo].[IpAddressLookups]([Date] ASC, [CountryCode] ASC, [City] ASC);