---   USER DEFAULT
sqlcmd -S 192.168.2.134 -U sa -P '0908Gle@' -C
sqlcmd -S 167.86.124.3 -U sa -P '0908Gle@' -C
sqlcmd -S 1.tcp.sa.ngrok.io,20889 -U sa -P '0908Gle@' -C



-- sqlcmd -S localhost -U jesussenhor -P '0908Gle@gle28poker' -C 
-- CREATE LOGIN jesussenhor WITH PASSWORD = '0908Gle@gle28poker';
-- CREATE USER jesussenhor FOR LOGIN jesussenhor
-- GRANT CREATE USER, CREATE DATABASE, EXECUTE, SELECT, INSERT, DELETE, UPDATE TO jesussenhor WITH GRANT OPTION;

-- LOCATION DEFAULT ADN SET LOCATION SQLSERVER
-- sudo /opt/mssql/bin/mssql-conf set filelocation.defaultbackupdir /var/opt/mssql/data/WITT

-- PERMISSIONS
-- sudo chown mssql /var/opt/mssql/data/WITT
-- GROUP
-- sudo chgrp  mssql /var/opt/mssql/data/WITT
-- COPY BACKUP
-- cp backuplocation/wittData.bak /var/opt/mssql/data/WITT




DROP DATABASE darkgames_2025
GO

CREATE DATABASE darkgames_2025
GO

USE darkgames_2025
GO

SELECT * FROM INFORMATION_SCHEMA.TABLES 
SELECT * FROM Sys.Tables
GO


-- BACKUP
BACKUP DATABASE darkgames_2025 TO DISK='DARKGAMES.bak'; 
-- RESTORE
RESTORE DATABASE adarkgames_2025 FROM DISK='DARKGAMES.bak' WITH REPLACE;

--CONFERE TABELAS
SELECT name FROM sys.tables
---QUERY ORGANIZADA SQLSERVER -------------------------------------------------------------------------------
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON

------------LOGIN ----------------------------------


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL BEGIN CREATE TABLE [__EFMigrationsHistory] ( [MigrationId] nvarchar(150) NOT NULL, [ProductVersion] nvarchar(32) NOT NULL, CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId]) ); END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] ( [Id] nvarchar(450) NOT NULL, [Name] nvarchar(256) NULL, [NormalizedName] nvarchar(256) NULL, [ConcurrencyStamp] nvarchar(max) NULL, CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id]) );
GO

CREATE TABLE [AspNetUsers] ( [Id] nvarchar(450) NOT NULL, [UserName] nvarchar(256) NULL, [NormalizedUserName] nvarchar(256) NULL, [Email] nvarchar(256) NULL, [NormalizedEmail] nvarchar(256) NULL, [EmailConfirmed] bit NOT NULL, [PasswordHash] nvarchar(max) NULL, [SecurityStamp] nvarchar(max) NULL, [ConcurrencyStamp] nvarchar(max) NULL, [PhoneNumber] nvarchar(max) NULL, [PhoneNumberConfirmed] bit NOT NULL, [TwoFactorEnabled] bit NOT NULL, [LockoutEnd] datetimeoffset NULL,[LockoutEnabled] bit NOT NULL, [AccessFailedCount] int NOT NULL, CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]));
GO

CREATE TABLE [AspNetRoleClaims] ( [Id] int NOT NULL IDENTITY, [RoleId] nvarchar(450) NOT NULL, [ClaimType] nvarchar(max) NULL, [ClaimValue] nvarchar(max) NULL, CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]), CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE );
GO

CREATE TABLE [AspNetUserClaims] ( [Id] int NOT NULL IDENTITY, [UserId] nvarchar(450) NOT NULL, [ClaimType] nvarchar(max) NULL, [ClaimValue] nvarchar(max) NULL, CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]), CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE);
GO

CREATE TABLE [AspNetUserLogins] ( [LoginProvider] nvarchar(128) NOT NULL, [ProviderKey] nvarchar(128) NOT NULL, [ProviderDisplayName] nvarchar(max) NULL, [UserId] nvarchar(450) NOT NULL, CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]), CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE );
GO

CREATE TABLE [AspNetUserRoles] ( [UserId] nvarchar(450) NOT NULL, [RoleId] nvarchar(450) NOT NULL, CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]), CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE, CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE );
GO

CREATE TABLE [AspNetUserTokens] ( [UserId] nvarchar(450) NOT NULL, [LoginProvider] nvarchar(250) NOT NULL, [Name] nvarchar(250) NOT NULL, [Value] nvarchar(max) NULL, CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]), CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE );
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230905213832_advocacia', N'6.0.21');
GO

COMMIT;
GO

------------- LOGIN ---------------------------------------

------------ ALTER TABLES ---------------------------------


ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])REFERENCES [dbo].[AspNetRoles] ([Id])ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])REFERENCES [dbo].[AspNetUsers] ([Id])ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserLogins] WITH CHECK ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])REFERENCES [dbo].[AspNetRoles] ([Id])ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])REFERENCES [dbo].[AspNetUsers] ([Id])ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])REFERENCES [dbo].[AspNetUsers] ([Id])ON DELETE CASCADE
GO

ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO


------------ ALTER TABLES             ---------------------
------------ TABLES FOR USE ON LAUDOS ---------------------

CREATE TABLE [dbo].[datalog]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](250) NOT NULL,[data_hora] [nvarchar](250) NOT NULL,[cargo] [nvarchar](250) NOT NULL,[login] [nvarchar](250) NOT NULL,[executou] [nvarchar](250) NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[dispositivo]([codigo] [int] IDENTITY(1,1) NOT NULL,[nome] [nvarchar](250) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[chave] [nvarchar](250) NOT NULL,[id] [nvarchar](250) NOT NULL,[url] [nvarchar](250) NOT NULL, [description] [nvarchar](250) NOT NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[usuario]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](250) NOT NULL,[foto] [nvarchar](max) NOT NULL,[nome] [nvarchar](450) NOT NULL,[telefone] [nvarchar](450) NOT NULL,[sobrenome] [nvarchar](450) NOT NULL,[bio] [nvarchar](1250) NOT NULL,[website] [nvarchar](450) NOT NULL,[username] [nvarchar](250) NOT NULL,[email] [nvarchar](450) NOT NULL,[senha] [nvarchar](450) NOT NULL,[perfil] [nvarchar](250) NOT NULL, [estado] [nvarchar](max) NULL,[cidade] [nvarchar](max) NULL,[local] [nvarchar](max) NULL,[sexos] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[logs]([codigo] [int] IDENTITY(1,1) NOT NULL,[hostname] [nvarchar](250) NOT NULL,  [data] [nvarchar](4000) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[cidade]([codigo] [int] IDENTITY(1,1) NOT NULL,[nome] [nvarchar](250) NOT NULL,[lat] [nvarchar](250) NOT NULL,[long] [nvarchar](250) NOT NULL, [Ativo] [bit] NOT NULL,PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[estado]([codigo] [int] IDENTITY(1,1) NOT NULL,[nome] [nvarchar](250) NOT NULL,[lat] [nvarchar](250) NOT NULL,[long] [nvarchar](250) NOT NULL, [Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[identificacao]([codigo] [int] IDENTITY(1,1) NOT NULL,[email] [nvarchar](250) NOT NULL,[ip] [nvarchar](250) NOT NULL,[Ativo] [bit] NOT NULL, UNIQUE (email,ip), PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[desconto]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[valor] [nvarchar](750) NULL,[descricao] [nvarchar](750) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL,  UNIQUE (valor), PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[local]([codigo] [int] IDENTITY(1,1) NOT NULL,[sigla] [nvarchar](250) NOT NULL,[nome] [nvarchar](250) NOT NULL,[lat] [nvarchar](250) NOT NULL,[long] [nvarchar](250) NOT NULL, [Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[mapa]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](250) NOT NULL,[aeronave] [nvarchar](250) NOT NULL,[foto] [nvarchar](max) NOT NULL,[latitude] [nvarchar](250) NOT NULL,[longitude] [nvarchar](250) NOT NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[sobre]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[categoria] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[categoria]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[nome] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO


CREATE TABLE [dbo].[banner]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[foto] [nvarchar](max) NULL,[categoria] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO


CREATE TABLE [dbo].[consoles]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[nome] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[fabricante]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[nome] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[produto]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[foto] [nvarchar](max) NULL,[categoria] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[console] [nvarchar](max) NULL,[multilanguage] [nvarchar](max) NULL,[global] [nvarchar](max) NULL,[valor] [nvarchar](max) NULL,[desconto] [nvarchar](max) NULL,[cupom] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[financeiro]([codigo] [int] IDENTITY(1,1) NOT NULL,[hashcode] [nvarchar](750) NOT NULL,[nome] [nvarchar](750) NOT NULL,[titulo] [nvarchar](750) NULL,[descricao] [nvarchar](max) NULL,[valor] [nvarchar](max) NULL,[reserva] [nvarchar](max) NULL,[estoque] [nvarchar](max) NULL,[data] DATETIME NOT NULL,[date] [nvarchar](250) NULL,[Ativo] [bit] NOT NULL, PRIMARY KEY CLUSTERED ([codigo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]
GO

------------ SELECT ------------------------------------------------------------------

------------ SELECT ------------------------------------------------------------------

---- ALTER TABLES -------------------------------------------
---- ALTER TABLES AVALIAR ESSAS COLUNAS O QUE FALTAM NELAS-------------------------------------------


ALTER TABLE [dbo].[datalog] ADD [datahora] [nvarchar](50) NULL;
ALTER TABLE [dbo].[datalog] ADD [ip] [nvarchar](150) NULL;
ALTER TABLE [dbo].[usuario] ADD UNIQUE (email);
ALTER TABLE [dbo].[AspNetUsers] ADD UNIQUE (Email);
ALTER TABLE [dbo].[identificacao] ADD [secret] [nvarchar](750) NULL;
ALTER TABLE [dbo].[identificacao] ADD [auth2fa] [nvarchar](750) NULL;
ALTER TABLE [dbo].[identificacao] ADD [token] [nvarchar](750) NULL;
ALTER TABLE [dbo].[identificacao] ADD [data] DATETIME NULL;
ALTER TABLE [dbo].[identificacao] ADD [cpf] [nvarchar](250) NULL;
ALTER TABLE [dbo].[datalog] ALTER COLUMN [data_hora] DATETIME NULL;

---- ALTER TABLES -------------------------------------------
---- ALTER TABLES -------------------------------------------

---  REVISAR PROCEDURES AO CONECTAR O APP ---

CREATE PROCEDURE sp_desconto AS SELECT * FROM dbo.desconto WHERE Ativo = 1
GO


CREATE PROCEDURE sp_login @hostname nvarchar(350) AS SELECT * FROM dbo.hostname WHERE hostname = @hostname and Ativo = 1
GO


------------ SELECT ------------------------------------------------------------------



------------ INSERT ------------------------------------------------------------------

CREATE PROCEDURE sp_dispositivo @hashcode NVARCHAR(max) = NULL, @nome NVARCHAR(750) = NULL, @chave NVARCHAR(750) =  NULL, @id NVARCHAR(750) =  NULL , @url NVARCHAR(750) =  NULL, @description NVARCHAR(max) =  NULL, @date NVARCHAR(max) =  NULL, @data DATETIME, @Ativo  NVARCHAR(50) = NULL AS BEGIN  INSERT INTO dbo.dispositivo (hashcode, nome, chave, id, url, description, date, data, Ativo)VALUES(@hashcode, @nome, @chave, @id, @url, @description, @date, @data, @Ativo) END
GO

CREATE PROCEDURE sp_mapa @hashcode NVARCHAR(max) = NULL, @foto NVARCHAR(max) = NULL, @aeronave NVARCHAR(750) =  NULL, @latitude NVARCHAR(250) =  NULL , @longitude NVARCHAR(250) =  NULL,  @Ativo  NVARCHAR(50) = NULL AS BEGIN  INSERT INTO dbo.mapa (hashcode, foto, aeronave, latitude, longitude, Ativo)VALUES(@hashcode, @foto, @aeronave, @latitude, @longitude, @Ativo) END
GO

CREATE PROCEDURE sp_logs  @hostname NVARCHAR(250) =  NULL,  @data NVARCHAR(250) =  NULL, @Ativo NVARCHAR(250) =  NULL AS BEGIN  INSERT INTO dbo.logs (hostname, data, Ativo)VALUES(@hostname, @data, @Ativo) END
GO
------------ UPDATE ------------------------------------------------------------------


-- CREATE PROCEDURE sp_Inativo_hostname  @hostname NVARCHAR(250) =  NULL, @codigo NVARCHAR(250) =  NULL AS BEGIN  UPDATE dbo.hostnamelogin set Ativo = 0 where hostname = @hostname and codigo = @codigo END
-- GO


-------------------------DELETE ------------------------

-- CREATE PROCEDURE sp_plano_delete  @hostname  NVARCHAR(250) = NULL AS  DELETE FROM dbo.plano where hostname = @hostname 
-- GO



-- EXECUTE sp_Inativo_hostname @hostname = "Joaosd", @codigo = "1"




----------------------------------SET QUOTED_IDENTIFIER -------------------------------------
SET QUOTED_IDENTIFIER ON
GO
----------------------------------SET QUOTED_IDENTIFIER -------------------------------------

----------------------------------SET ANSI_NULLS ON------------------------------------------
SET ANSI_NULLS ON
GO
----------------------------------SET ANSI_NULLS ON------------------------------------------



------------------------------///////////////////////////////////////////////////////////////////////////////////////////////////////

INSERT INTO dbo.AspNetUsers(Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount)VALUES('b5ef2f06-cfca-42f8-9979-e69434a57480','Gleison','GLEISON','lionmonkeydesignapp@gmail.com','lionmonkeydesignapp@gmail.com',0,'AQAAAAEAACcQAAAAEKxguH2LBhaJsDUHJ88VMhRXh1MzYDTdmiWsfFYqCjCA3aZ3EMSsjCqHkHmcxmAo7g==','L2FLOHN5K5AICZXAGL4C7TP7SID6E5AU','0fc55b05-b405-45aa-9b75-5ce013ef1120','',0,0,'',1,0);
GO
INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp)VALUES('60cd773b-3b13-4f3-a2cd-af35e8f1354c','Administrador','ADMINISTRADOR','2856098f-0219-4e15-a-769-294e60e2a6b4');
GO
INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp)VALUES('40cd773b-3b13-4f3-a2cd-af35e8f1354c','Revenda','REVENDA','1856098f-0219-4e15-a-769-294e60e2a6b4');
GO
INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp)VALUES('20cd773b-3b13-4f3-a2cd-af35e8f1354c','Parceiro','PARCEIRO','3856098f-0219-4e15-a-769-294e60e2a6b4');
GO
INSERT INTO dbo.AspNetRoles(Id,Name,NormalizedName,ConcurrencyStamp)VALUES('10cd773b-3b13-4f3-a2cd-af35e8f1354c','Cliente','CLIENTE','5856098f-0219-4e15-a-769-294e60e2a6b4');
GO
INSERT INTO dbo.AspNetUserRoles(UserId,RoleId)VALUES('b5ef2f06-cfca-42f8-9979-e69434a57480','60cd773b-3b13-4f3-a2cd-af35e8f1354c');
GO

------------------------------///////////////////////////////////////////////////////////////////////////////////////////////////////