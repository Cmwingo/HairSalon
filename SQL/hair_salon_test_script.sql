USE [hair_salon_test]
GO
/****** Object:  Table [dbo].[clients]    Script Date: 12/9/2016 4:31:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[stylist_id] [int] NULL,
	[appointment_day] [varchar](255) NULL,
	[appointment_time] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[stylists]    Script Date: 12/9/2016 4:31:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stylists](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[availability] [varchar](255) NULL,
	[services] [varchar](255) NULL
) ON [PRIMARY]

GO
