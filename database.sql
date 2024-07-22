USE [master]
GO
/****** Object:  Database [Hotel_management]    Script Date: 7/22/2024 3:06:55 PM ******/
CREATE DATABASE [Hotel_management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hotel_management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Hotel_management.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hotel_management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Hotel_management_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Hotel_management] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hotel_management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hotel_management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hotel_management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hotel_management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hotel_management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hotel_management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hotel_management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hotel_management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hotel_management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hotel_management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hotel_management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hotel_management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hotel_management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hotel_management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hotel_management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hotel_management] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Hotel_management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hotel_management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hotel_management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hotel_management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hotel_management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hotel_management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hotel_management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hotel_management] SET RECOVERY FULL 
GO
ALTER DATABASE [Hotel_management] SET  MULTI_USER 
GO
ALTER DATABASE [Hotel_management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hotel_management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hotel_management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hotel_management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hotel_management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hotel_management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hotel_management', N'ON'
GO
ALTER DATABASE [Hotel_management] SET QUERY_STORE = ON
GO
ALTER DATABASE [Hotel_management] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Hotel_management]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NOT NULL,
	[RoomID] [int] NOT NULL,
	[StaffID] [int] NULL,
	[CheckInDate] [date] NOT NULL,
	[CheckOutDate] [date] NOT NULL,
	[BookingDate] [date] NOT NULL,
	[StatusID] [int] NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingStatus]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingStatus](
	[StatusID] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[Password] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomID] [int] NOT NULL,
	[RoomNumber] [nvarchar](50) NULL,
	[RoomTypeID] [int] NULL,
	[StatusID] [int] NULL,
	[Price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomStatus]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomStatus](
	[StatusID] [int] NOT NULL,
	[StatusName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomType]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomType](
	[RoomTypeID] [int] NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoomTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[StaffID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[HireDate] [date] NULL,
	[Salary] [decimal](10, 2) NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 7/22/2024 3:06:55 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[RoomID] [int] NULL,
	[CustomerID] [int] NULL,
	[StaffID] [int] NULL,
	[TransactionDate] [date] NULL,
	[Amount] [decimal](10, 2) NULL,
	[Description] [nvarchar](max) NULL,
	[BookingID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 
GO
INSERT [dbo].[Booking] ([BookingID], [CustomerID], [RoomID], [StaffID], [CheckInDate], [CheckOutDate], [BookingDate], [StatusID]) VALUES (1002, 1, 2, NULL, CAST(N'2024-07-19' AS Date), CAST(N'2024-07-20' AS Date), CAST(N'2024-07-18' AS Date), NULL)
GO
INSERT [dbo].[Booking] ([BookingID], [CustomerID], [RoomID], [StaffID], [CheckInDate], [CheckOutDate], [BookingDate], [StatusID]) VALUES (1003, 4, 3, NULL, CAST(N'2024-07-24' AS Date), CAST(N'2024-07-27' AS Date), CAST(N'2024-07-22' AS Date), 2)
GO
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
INSERT [dbo].[BookingStatus] ([StatusID], [Description]) VALUES (0, N'Pending')
GO
INSERT [dbo].[BookingStatus] ([StatusID], [Description]) VALUES (1, N'Payment Success')
GO
INSERT [dbo].[BookingStatus] ([StatusID], [Description]) VALUES (2, N'Cancel')
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [Phone], [Email], [Password]) VALUES (1, N'John', N'Cena', N'123 Main St, Anytown, USA', N'+1234567890', N'john.doe@example.com', N'123')
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [Phone], [Email], [Password]) VALUES (2, N'Jane', N'Smith', N'456 Elm St, Othertown, USA', N'+1987654321', N'jane.smith@example.com', N'123')
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [Phone], [Email], [Password]) VALUES (3, N'Michael', N'Johnson', N'789 Oak St, Another Town, USA', N'+1122334455', N'michael.j@example.com', N'123')
GO
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Address], [Phone], [Email], [Password]) VALUES (4, N'Bui ', N'Manh', N'Tan Ky Tu Ky Hai Duong', N'0987271709', N'buimanhtk1979@gmail.com', N'amthambenem')
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (1, N'101', 1, 3, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (2, N'102', 1, 3, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (3, N'103', 2, 3, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (4, N'104', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (5, N'105', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (6, N'106', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (7, N'107', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (8, N'108', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (9, N'109', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (10, N'110', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (11, N'201', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (12, N'202', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (13, N'203', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (14, N'204', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (15, N'205', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (16, N'206', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (17, N'207', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (18, N'208', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (19, N'209', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (20, N'210', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (21, N'301', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (22, N'302', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (23, N'303', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (24, N'304', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (25, N'305', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (26, N'306', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (27, N'307', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (28, N'308', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (29, N'309', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (30, N'310', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (31, N'401', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (32, N'402', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (33, N'403', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (34, N'404', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (35, N'405', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (36, N'406', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (37, N'407', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (38, N'408', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (39, N'409', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (40, N'410', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (41, N'501', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (42, N'502', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (43, N'503', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (44, N'504', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (45, N'505', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (46, N'506', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (47, N'507', 1, 1, CAST(100.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (48, N'508', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (49, N'509', 2, 1, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[Room] ([RoomID], [RoomNumber], [RoomTypeID], [StatusID], [Price]) VALUES (50, N'510', 3, 1, CAST(200.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[RoomStatus] ([StatusID], [StatusName], [Description]) VALUES (1, N'Available', N'Room is ready for occupancy')
GO
INSERT [dbo].[RoomStatus] ([StatusID], [StatusName], [Description]) VALUES (2, N'Occupied', N'Currently occupied by a guest')
GO
INSERT [dbo].[RoomStatus] ([StatusID], [StatusName], [Description]) VALUES (3, N'Reserved', N'Room is booked for future stay')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [TypeName], [Description]) VALUES (1, N'Standard Room', N'Basic amenities')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [TypeName], [Description]) VALUES (2, N'Deluxe Room', N'More spacious, amenities')
GO
INSERT [dbo].[RoomType] ([RoomTypeID], [TypeName], [Description]) VALUES (3, N'Suite', N'Luxury, additional facilities')
GO
SET IDENTITY_INSERT [dbo].[Staff] ON 
GO
INSERT [dbo].[Staff] ([StaffID], [FirstName], [LastName], [PhoneNumber], [Email], [HireDate], [Salary], [Password], [Role]) VALUES (1, N'Williams', N'Butcher', N'+1122334455', N'rebecca.w@example.com', CAST(N'2023-01-15' AS Date), CAST(35000.00 AS Decimal(10, 2)), N'123', 1)
GO
INSERT [dbo].[Staff] ([StaffID], [FirstName], [LastName], [PhoneNumber], [Email], [HireDate], [Salary], [Password], [Role]) VALUES (2, N'David', N'Miller', N'+1987654321', N'david.m@example.com', CAST(N'2023-02-20' AS Date), CAST(40000.00 AS Decimal(10, 2)), N'123', 1)
GO
INSERT [dbo].[Staff] ([StaffID], [FirstName], [LastName], [PhoneNumber], [Email], [HireDate], [Salary], [Password], [Role]) VALUES (3, N'Sarah', N'Jones', N'+1234567890', N'sarah.j@example.com', CAST(N'2022-12-10' AS Date), CAST(50000.00 AS Decimal(10, 2)), N'123', 1)
GO
SET IDENTITY_INSERT [dbo].[Staff] OFF
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([TransactionID], [RoomID], [CustomerID], [StaffID], [TransactionDate], [Amount], [Description], [BookingID]) VALUES (14, 2, 1, 1, CAST(N'2024-07-19' AS Date), CAST(100.00 AS Decimal(10, 2)), N'hehehehehe', 1002)
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
ALTER TABLE [dbo].[Staff] ADD  DEFAULT ('123') FOR [Password]
GO
ALTER TABLE [dbo].[Staff] ADD  DEFAULT ((1)) FOR [Role]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([RoomID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_StatusID] FOREIGN KEY([StatusID])
REFERENCES [dbo].[BookingStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_StatusID]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD FOREIGN KEY([RoomTypeID])
REFERENCES [dbo].[RoomType] ([RoomTypeID])
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[RoomStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([RoomID])
REFERENCES [dbo].[Room] ([RoomID])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD FOREIGN KEY([StaffID])
REFERENCES [dbo].[Staff] ([StaffID])
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Booking] FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Booking]
GO
USE [master]
GO
ALTER DATABASE [Hotel_management] SET  READ_WRITE 
GO
