USE [master]
GO
/****** Object:  Database [fruitkha]    Script Date: 12/30/2023 10:05:03 PM ******/
CREATE DATABASE [fruitkha]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'fruitkha', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fruitkha.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'fruitkha_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\fruitkha_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [fruitkha] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [fruitkha].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [fruitkha] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [fruitkha] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [fruitkha] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [fruitkha] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [fruitkha] SET ARITHABORT OFF 
GO
ALTER DATABASE [fruitkha] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [fruitkha] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [fruitkha] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [fruitkha] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [fruitkha] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [fruitkha] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [fruitkha] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [fruitkha] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [fruitkha] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [fruitkha] SET  ENABLE_BROKER 
GO
ALTER DATABASE [fruitkha] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [fruitkha] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [fruitkha] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [fruitkha] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [fruitkha] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [fruitkha] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [fruitkha] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [fruitkha] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [fruitkha] SET  MULTI_USER 
GO
ALTER DATABASE [fruitkha] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [fruitkha] SET DB_CHAINING OFF 
GO
ALTER DATABASE [fruitkha] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [fruitkha] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [fruitkha] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [fruitkha] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [fruitkha] SET QUERY_STORE = OFF
GO
USE [fruitkha]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[Username] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[IDBill] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](50) NULL,
	[UseID] [int] NULL,
	[Address] [nvarchar](300) NULL,
	[Phone] [char](10) NULL,
	[Status] [bit] NULL,
	[OrderDate] [datetime] NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[IDBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[IDBlog] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](500) NULL,
	[Title] [nvarchar](500) NULL,
	[ShortContent] [nvarchar](2000) NULL,
	[Contents] [nvarchar](max) NULL,
	[CreatedDate] [date] NULL,
	[ModifyDate] [date] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[IDBlog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[IDComment] [int] IDENTITY(1,1) NOT NULL,
	[IDCommentator] [int] NULL,
	[CommentContent] [nvarchar](1000) NULL,
	[IDBlog] [int] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[IDComment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailedInvoice]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailedInvoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Quantity] [float] NULL,
	[TotalPrice] [money] NULL,
	[IDBill] [int] NULL,
 CONSTRAINT [PK_DetailedInvoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 0) NULL,
	[PromotionalPrice] [decimal](18, 0) NULL,
	[Image] [nvarchar](500) NULL,
	[IDType] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[IDType] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NULL,
	[Images] [nvarchar](500) NULL,
	[IsHome] [bit] NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[IDType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IDRole] [int] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IDRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UseID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Sdt] [char](10) NULL,
	[UseName] [nvarchar](500) NULL,
	[Password] [nvarchar](500) NULL,
	[Address] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([Id], [FullName], [Phone], [Address], [Username], [Password], [IsActive]) VALUES (1, N'admin', N'0997752127', N'Hà Nội', N'admin', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1)
INSERT [dbo].[Admin] ([Id], [FullName], [Phone], [Address], [Username], [Password], [IsActive]) VALUES (2, N'thanhpv', N'0997752127', N'30 Đặng Thùy Trâm', N'admin01', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Bill] ON 

INSERT [dbo].[Bill] ([IDBill], [OrderCode], [UseID], [Address], [Phone], [Status], [OrderDate]) VALUES (2, N'2023122511123', 3, N'Hà Nội', N'0997752127', 1, CAST(N'2023-12-25T00:00:00.000' AS DateTime))
INSERT [dbo].[Bill] ([IDBill], [OrderCode], [UseID], [Address], [Phone], [Status], [OrderDate]) VALUES (4, N'231227014648987', 4, N'65 Cầu Giấy ', N'0997752127', 1, CAST(N'2023-12-27T01:46:49.643' AS DateTime))
INSERT [dbo].[Bill] ([IDBill], [OrderCode], [UseID], [Address], [Phone], [Status], [OrderDate]) VALUES (5, N'231227022923633', 4, N'65 Cầu Giấy ', N'0997752127', 0, CAST(N'2023-12-27T02:29:23.660' AS DateTime))
SET IDENTITY_INSERT [dbo].[Bill] OFF
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([IDBlog], [Image], [Title], [ShortContent], [Contents], [CreatedDate], [ModifyDate]) VALUES (2, N'231225\20231225_3-495-1702756596-977-width740height495.jpg', N'Nóng: Tỷ phú Anh mua xong 25% cổ phần MU, "Quỷ đỏ" nhận hàng trăm triệu bảng', N'MU đã ra thông báo chính thức về việc tỷ phú Jim Ratcliffe trở thành 1 trong những cổ đông quan trọng tại sân Old Trafford vào tối ngày 24/12 (giờ Việt Nam).', N'<p>MU x&aacute;c nhận tỷ&nbsp;ph&uacute; nắm trong tay tập đo&agrave;n&nbsp;INEOS sẽ sớm bỏ ra số tiền 300 triệu USD (237 triệu bảng) để hỗ trợ cho &quot;Quỷ đỏ&quot;. Số tiền đ&oacute; được sử dụng&nbsp;để cải thiện cơ sở hạ tầng của đội chủ s&acirc;n&nbsp;<a href="https://www.24h.com.vn/manchester-united-c48e1521.html" title="Old Trafford">Old Trafford</a>.</p>

<p>Tỷ ph&uacute;&nbsp;Jim Ratcliffe trong ng&agrave;y trở th&agrave;nh một phần của ban l&atilde;nh đạo MU đ&atilde; c&oacute; những chia sẻ đ&aacute;ng ch&uacute; &yacute;: &quot;T&ocirc;i l&agrave; CĐV trung th&agrave;nh của MU từ khi c&ograve;n l&agrave; cậu b&eacute;. Do vậy, t&ocirc;i rất vui khi đ&atilde; c&oacute; thể đi đến thống nhất với Hội đồng quản trị của MU trong việc giao cho ch&uacute;ng t&ocirc;i tr&aacute;ch nhiệm quản l&yacute; những hoạt động b&oacute;ng đ&aacute;.</p>

<p>Mặt th&agrave;nh c&ocirc;ng của thương vụ n&agrave;y nằm ở việc MU đảm bảo lu&ocirc;n c&oacute; sẵn nguồn lực tốt để gi&agrave;nh những danh hiệu lớn, ph&aacute;t huy những tiềm năng chưa được khai ph&aacute; trong thời gian gần đ&acirc;y. Ch&uacute;ng t&ocirc;i mang tới kiến thức, chuy&ecirc;n m&ocirc;n từ tập đo&agrave;n&nbsp;INEOS để gi&uacute;p MU ph&aacute;t triển.</p>

<p>Ch&uacute;ng t&ocirc;i sẽ gắn b&oacute; l&acirc;u d&agrave;i với CLB v&agrave; x&aacute;c định rằng c&ograve;n nhiều th&aacute;ch thức, kh&oacute; khăn phải đối diện. Ch&uacute;ng t&ocirc;i cam kết sẽ gi&uacute;p MU tiến bộ. Tham vọng của ch&uacute;ng t&ocirc;i rất r&otilde; r&agrave;ng, đ&oacute; l&agrave; đưa MU trở lại với đỉnh cao&nbsp;b&oacute;ng đ&aacute; Anh cũng như tại ch&acirc;u &Acirc;u v&agrave; thế giới&quot;.</p>

<p>Việc tỷ ph&uacute;&nbsp;Jim Ratcliffe tiếp quản c&aacute;c hoạt động b&oacute;ng đ&aacute; ở MU sẽ ảnh hưởng kh&ocirc;ng nhỏ tới tương lai của HLV Ten Hag. Hiện chiến lược gia người H&agrave; Lan gặp kh&ocirc;ng &iacute;t th&aacute;ch thức khi &ocirc;ng kh&ocirc;ng thể gi&uacute;p đội chủ s&acirc;n Old Trafford&nbsp;đạt phong độ tốt.&nbsp;</p>
', CAST(N'2023-12-25' AS Date), NULL)
INSERT [dbo].[Blog] ([IDBlog], [Image], [Title], [ShortContent], [Contents], [CreatedDate], [ModifyDate]) VALUES (3, N'231227\20231227_1609_321949204153195_3827195600297111697_n--2--1703604575-108-width740height495.jpg', N'Tranh cãi V-League: Sao TP.HCM đánh cùi chỏ vào mặt đối thủ chỉ nhận thẻ vàng', N'Cầu thủ Thanh Thảo bên phía TP HCM đã có hành động phi thể thao với Văn Hạnh của Hà Tĩnh ở vòng 8 V-League.', N'<p>Ph&uacute;t 90&#39;+2, TP.HCM được hưởng n&eacute;m bi&ecirc;n v&agrave; dồn l&ecirc;n t&igrave;m b&agrave;n gỡ. Trong t&igrave;nh huống n&agrave;y, Văn Hạnh đ&atilde; theo s&aacute;t k&egrave;m Thanh Thảo của đội chủ nh&agrave;. V&agrave; trong khoảnh khắc n&agrave;y, Nguyễn Thanh Thảo đ&atilde; c&oacute; h&agrave;nh động phi thể thao khi c&ugrave;i chỏ trực tiếp v&agrave;o mặt cầu thủ&nbsp;<a href="https://www.24h.com.vn/ha-tinh-c46e4517.html" title="Hà Tĩnh">H&agrave; Tĩnh</a>.</p>

<p>Tưởng chừng hậu vệ của TP.HCM phải&nbsp;nhận thẻ đỏ trực tiếp từ trọng t&agrave;i nhưng Thanh Thảo chỉ nhận thẻ v&agrave;ng. Đ&acirc;y l&agrave; điều khiến BHL cũng như&nbsp;cầu thủ H&agrave; Tĩnh v&agrave;&nbsp;kh&aacute;n giả c&oacute; mặt&nbsp;tr&ecirc;n s&acirc;n kh&ocirc;ng h&agrave;i l&ograve;ng với quyết định n&agrave;y.</p>
', CAST(N'2023-12-27' AS Date), NULL)
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([IDComment], [IDCommentator], [CommentContent], [IDBlog], [Date]) VALUES (2, 4, N'oke hay', 2, CAST(N'2023-12-27' AS Date))
INSERT [dbo].[Comment] ([IDComment], [IDCommentator], [CommentContent], [IDBlog], [Date]) VALUES (3, 4, N'ko hay lắm đúng ko??', 2, CAST(N'2023-12-27' AS Date))
INSERT [dbo].[Comment] ([IDComment], [IDCommentator], [CommentContent], [IDBlog], [Date]) VALUES (4, 4, N'ổn mà bạn ơi', 2, CAST(N'2023-12-27' AS Date))
INSERT [dbo].[Comment] ([IDComment], [IDCommentator], [CommentContent], [IDBlog], [Date]) VALUES (7, 4, N'aaa', 2, CAST(N'2023-12-27' AS Date))
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[DetailedInvoice] ON 

INSERT [dbo].[DetailedInvoice] ([ID], [ProductID], [Quantity], [TotalPrice], [IDBill]) VALUES (2, 1, 2, 80000.0000, 2)
INSERT [dbo].[DetailedInvoice] ([ID], [ProductID], [Quantity], [TotalPrice], [IDBill]) VALUES (3, 1, 2, 200000.0000, 4)
INSERT [dbo].[DetailedInvoice] ([ID], [ProductID], [Quantity], [TotalPrice], [IDBill]) VALUES (4, 4, 3, 1200000.0000, 5)
SET IDENTITY_INSERT [dbo].[DetailedInvoice] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [PromotionalPrice], [Image], [IDType]) VALUES (1, N'Chuột FullHen', N'<p>abc121345678trewuio</p>
', CAST(120000 AS Decimal(18, 0)), CAST(100000 AS Decimal(18, 0)), N'231224\20231224_340-tay-da-chet-body-dove.jpg', 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [PromotionalPrice], [Image], [IDType]) VALUES (3, N'Táo mỹ', N'<p>aaaaaaaaa</p>
', CAST(50000 AS Decimal(18, 0)), CAST(45000 AS Decimal(18, 0)), N'231227\20231227_fresh-skin.jpg', 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [PromotionalPrice], [Image], [IDType]) VALUES (4, N'Nho úc 240', N'<p>oke nh&aacute;</p>
', CAST(500000 AS Decimal(18, 0)), CAST(400000 AS Decimal(18, 0)), N'231227\20231227_error403.jpg', 2)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [PromotionalPrice], [Image], [IDType]) VALUES (5, N'Quất hồng bì', N'<p>aaaaaaaaaaaaaaaaaaaaaaaaaaa</p>
', CAST(50000 AS Decimal(18, 0)), CAST(48000 AS Decimal(18, 0)), N'231227\20231227_iconrepeat.png', 2)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([IDType], [TypeName], [Images], [IsHome]) VALUES (2, N'Loại 2', N'231224\20231224_3-495-1702756596-977-width740height495.jpg', 1)
SET IDENTITY_INSERT [dbo].[ProductType] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UseID], [Name], [Sdt], [UseName], [Password], [Address], [IsActive]) VALUES (3, N'User', N'0915245123', N'user', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'Hà Nội', 1)
INSERT [dbo].[User] ([UseID], [Name], [Sdt], [UseName], [Password], [Address], [IsActive]) VALUES (4, N'Thanh Phạm', N'0997752127', N'thanhpv', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', N'65 Cầu Giấy ', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_User] FOREIGN KEY([UseID])
REFERENCES [dbo].[User] ([UseID])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_User]
GO
ALTER TABLE [dbo].[DetailedInvoice]  WITH CHECK ADD  CONSTRAINT [FK_DetailedInvoice_Bill] FOREIGN KEY([IDBill])
REFERENCES [dbo].[Bill] ([IDBill])
GO
ALTER TABLE [dbo].[DetailedInvoice] CHECK CONSTRAINT [FK_DetailedInvoice_Bill]
GO
ALTER TABLE [dbo].[DetailedInvoice]  WITH CHECK ADD  CONSTRAINT [FK_DetailedInvoice_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[DetailedInvoice] CHECK CONSTRAINT [FK_DetailedInvoice_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductType] FOREIGN KEY([IDType])
REFERENCES [dbo].[ProductType] ([IDType])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductType]
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetAdminListAll]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetAdminListAll]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT a.*
	, COUNT(1) OVER() as TotalRow
	FROM [Admin] a
	Where (@search is null 
	OR a.FullName like N'%'+@search+'%'
	OR a.Username like N'%'+@search+'%')
	Order By a.Id DESC
	offset @offset rows
	fetch next @limit rows only;

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetBillListAllPaging]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetBillListAllPaging] 
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT b.*
	, u.UseName
	, u.[Name] as FullName
	, COUNT(1) OVER() as TotalRow
	FROM Bill b
	Left Join [User] u ON u.UseID = b.UseID
	Where (@search is null 
	OR u.[Name] like N'%'+@search+'%'
	OR b.OrderCode like N'%'+@search+'%'
	OR b.Phone like N'%'+@search+'%')
	Order by b.IDBill DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetBlogListAllPaging]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetBlogListAllPaging]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT *
	,COUNT(1) OVER() as TotalRow
	FROM Blog
	Where (@search is null 
	OR Title like N'%'+@search+'%')
	Order by IDBlog DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetCommentListAllPaging]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetCommentListAllPaging]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT c.*, b.Title, u.UseName,
	COUNT(1) OVER() as TotalRow
	FROM Comment c
	INNER JOIN Blog b On b.IDBlog = c.IDBlog
	INNER JOIN [User] u ON u.UseID = c.IDCommentator 
	Where (@search is null
	OR b.Title like N'%'+@search+'%'
	OR u.UseName like N'%'+@search+'%')
	Order by c.IDComment DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetDetailInvoiceByBillIdListAll]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetDetailInvoiceByBillIdListAll]
	@IdBill int,
	@offset int,
	@limit int
AS
BEGIN
	Select de.*, p.ProductName,
	COUNT(1) OVER() as TotalRow,
	SUM(de.TotalPrice) OVER() as TotalPriceAll
	FROM DetailedInvoice  de
	INNER JOIN Product p ON p.ProductID = de.ProductID
	Where de.IDBill = @IdBill
	Order By de.ID ASC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetProductListAllPaging]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetProductListAllPaging] 
	@search nvarchar(500),
	@typeId int = 0,
	@offset int,
	@limit int
AS
BEGIN
	SELECT p.*, pt.TypeName, COUNT(1) OVER() as TotalRow
	From Product p
	INNER JOIN ProductType pt ON pt.IDType = p.IDType
	Where (@search is null 
	OR pt.TypeName like N'%'+@search+'%'
	OR p.ProductName like N'%'+@search+'%')
	AND (@typeId = 0 OR p.IDType = @typeId)
	ORDER BY p.ProductID DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_GetProductType]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_GetProductType]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT *,
	COUNT(1) OVER() as TotalRow
	From ProductType
	Where (@search is null 
	Or TypeName like N'%'+@search+'%')
	Order By IDType DESC
	offset @offset rows 
	fetch next @limit rows only
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetUser_ListAllPaging]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetUser_ListAllPaging]
	@search nvarchar(500),
	@offset int,
	@limit int
AS
BEGIN
	SELECT *
	, COUNT(1) OVER() AS TotalRow
	FROM [User]
	Where (@search is null
	OR [Name] like N'%'+@search+'%'
	OR UseName like N'%'+@search+'%'
	OR [Sdt] like N'%'+@search+'%')
	Order By UseID DESC
	offset @offset rows
	fetch next @limit rows only;
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_InsertBill]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Sp_InsertBill]
	@OrderCode nvarchar(50),
	@UserId int,
	@Status int,
	@PhoneNumber nvarchar(50),
	@Address nvarchar(MAX),
	@NewId int OUTPUT
AS
BEGIN
	INSERT INTO Bill(OrderCode, UseID, OrderDate, [Status], Phone, [Address] )
	VALUES(@OrderCode, @UserId, GETDATE(), @Status, @PhoneNumber, @Address)
	SET @NewId = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ReportHome]    Script Date: 12/30/2023 10:05:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_ReportHome] 
	
AS
BEGIN
	;WITH months(MonthNumber) AS
	(
		SELECT 1
		UNION ALL
		SELECT MonthNumber+1 
		FROM months
		WHERE MonthNumber < 12
	),
	temp_Bill as (
		Select MONTH(b.OrderDate) as MonthOrder, SUM(dt.TotalPrice) as TotalPriceMonth
		FROM Bill b
		JOIN (
			select IDBill, SUM(TotalPrice) as TotalPrice
			from DetailedInvoice
			GROUP By IDBill
		) dt ON dt.IDBill = b.IDBill
		Where b.[Status] = 1 AND YEAR(b.OrderDate) = YEAR(GETDATE())
		GROUP By MONTH(b.OrderDate)
	)
	select m.MonthNumber as MonthNumber
	, ISNULL(temp_Bill.TotalPriceMonth,0) as TotalPriceMonth
	from months m
	left join temp_Bill on m.MonthNumber = temp_Bill.MonthOrder
END
GO
USE [master]
GO
ALTER DATABASE [fruitkha] SET  READ_WRITE 
GO
