-- 28/10/2019 ---
CREATE TABLE [dbo].[Addresses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine] [nvarchar](max) NULL,
	[CountryId] [int] NOT NULL,
	[ProvinceId] [int] NOT NULL,
	[DistrictId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Districts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ProvinceId] [int] NOT NULL,
 CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[HostDefinitions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[HostName] [nvarchar](max) NULL,
 CONSTRAINT [PK_HostDefinitions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[OrganizationRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[OrganizationId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Organizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[AddressId] [int] NOT NULL
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[OrganizationUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Provinces](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Roles](
	[ID] [int] NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PasswordSalt] [nvarchar](max) NULL,
	[Fullname] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsEmailConfirmed] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastLoginDate] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE TABLE [dbo].[UserTokens](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
	[StartedTime] [datetime2](7) NOT NULL,
	[ExpiredTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
));

CREATE NONCLUSTERED INDEX [IX_Addresses_CountryId] ON [dbo].[Addresses]
(
	[CountryId] ASC
);
CREATE NONCLUSTERED INDEX [IX_Addresses_DistrictId] ON [dbo].[Addresses]
(
	[DistrictId] ASC
);
CREATE NONCLUSTERED INDEX [IX_Addresses_ProvinceId] ON [dbo].[Addresses]
(
	[ProvinceId] ASC
);
CREATE NONCLUSTERED INDEX [IX_Districts_ProvinceId] ON [dbo].[Districts]
(
	[ProvinceId] ASC
);
CREATE NONCLUSTERED INDEX [IX_HostDefinitions_OrganizationId] ON [dbo].[HostDefinitions]
(
	[OrganizationId] ASC
);
CREATE NONCLUSTERED INDEX [IX_OrganizationRoles_OrganizationId] ON [dbo].[OrganizationRoles]
(
	[OrganizationId] ASC
);
CREATE NONCLUSTERED INDEX [IX_Organizations_AddressId] ON [dbo].[Organizations]
(
	[AddressId] ASC
);
CREATE NONCLUSTERED INDEX [IX_OrganizationUsers_OrganizationId] ON [dbo].[OrganizationUsers]
(
	[OrganizationId] ASC
);
CREATE NONCLUSTERED INDEX [IX_OrganizationUsers_RoleId] ON [dbo].[OrganizationUsers]
(
	[RoleId] ASC
);
CREATE NONCLUSTERED INDEX [IX_OrganizationUsers_UserId] ON [dbo].[OrganizationUsers]
(
	[UserId] ASC
);
CREATE NONCLUSTERED INDEX [IX_Provinces_CountryId] ON [dbo].[Provinces]
(
	[CountryId] ASC
);
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
);
CREATE NONCLUSTERED INDEX [IX_UserRoles_UserId] ON [dbo].[UserRoles]
(
	[UserId] ASC
);
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [FK_Addresses_Countries_CountryId] FOREIGN KEY([CountryId]) REFERENCES [dbo].[Countries] ([ID]);
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [FK_Addresses_Districts_DistrictId] FOREIGN KEY([DistrictId]) REFERENCES [dbo].[Districts] ([ID]);
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [FK_Addresses_Provinces_ProvinceId] FOREIGN KEY([ProvinceId]) REFERENCES [dbo].[Provinces] ([ID]);
ALTER TABLE [dbo].[Districts] ADD  CONSTRAINT [FK_Districts_Provinces_ProvinceId] FOREIGN KEY([ProvinceId]) REFERENCES [dbo].[Provinces] ([ID]);
ALTER TABLE [dbo].[HostDefinitions]  ADD  CONSTRAINT [FK_HostDefinitions_Organizations_OrganizationId] FOREIGN KEY([OrganizationId]) REFERENCES [dbo].[Organizations] ([ID]) ON DELETE CASCADE;
ALTER TABLE [dbo].[OrganizationRoles]  ADD  CONSTRAINT [FK_OrganizationRoles_Organizations_OrganizationId] FOREIGN KEY([OrganizationId]) REFERENCES [dbo].[Organizations] ([ID]) ON DELETE CASCADE;
ALTER TABLE [dbo].[Organizations]  ADD  CONSTRAINT [FK_Organizations_Addresses_AddressId] FOREIGN KEY([AddressId]) REFERENCES [dbo].[Addresses] ([ID]) ON DELETE CASCADE;
ALTER TABLE [dbo].[OrganizationUsers] ADD  CONSTRAINT [FK_OrganizationUsers_OrganizationRoles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[OrganizationRoles] ([ID]) ON DELETE CASCADE;
ALTER TABLE [dbo].[OrganizationUsers]  ADD  CONSTRAINT [FK_OrganizationUsers_Organizations_OrganizationId] FOREIGN KEY([OrganizationId]) REFERENCES [dbo].[Organizations] ([ID]);
ALTER TABLE [dbo].[OrganizationUsers]  ADD  CONSTRAINT [FK_OrganizationUsers_Users_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([ID]);
ALTER TABLE [dbo].[Provinces] ADD  CONSTRAINT [FK_Provinces_Countries_CountryId] FOREIGN KEY([CountryId]) REFERENCES [dbo].[Countries] ([ID]);
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles] ([ID]) ON DELETE CASCADE;
ALTER TABLE [dbo].[UserRoles]  ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([ID]) ON DELETE CASCADE;
-- 28/10/2019 ---

-- 11/12/2019 ---
ALTER TABLE Organizations ADD UpdatedDate datetime2(7)