CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetRoles` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` varchar(256) NULL,
        `NormalizedName` varchar(256) NULL,
        `ConcurrencyStamp` longtext NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetUsers` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `WeChatOpenId` longtext NULL,
        `WeChatUnionId` longtext NULL,
        `UserName` varchar(256) NULL,
        `NormalizedUserName` varchar(256) NULL,
        `Email` varchar(256) NULL,
        `NormalizedEmail` varchar(256) NULL,
        `EmailConfirmed` tinyint(1) NOT NULL,
        `PasswordHash` longtext NULL,
        `SecurityStamp` longtext NULL,
        `ConcurrencyStamp` longtext NULL,
        `PhoneNumber` longtext NULL,
        `PhoneNumberConfirmed` tinyint(1) NOT NULL,
        `TwoFactorEnabled` tinyint(1) NOT NULL,
        `LockoutEnd` datetime NULL,
        `LockoutEnabled` tinyint(1) NOT NULL,
        `AccessFailedCount` int NOT NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `T_Albums` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` varchar(200) NOT NULL,
        `Description` varchar(500) NULL,
        `Artist` varchar(100) NOT NULL,
        `ReleaseDate` datetime(6) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        `DeletedAt` datetime(6) NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `T_Schedules` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` varchar(200) NOT NULL,
        `Description` varchar(1000) NULL,
        `Artist` varchar(100) NOT NULL,
        `Location` varchar(200) NULL,
        `StartTime` date NOT NULL,
        `EndTime` date NOT NULL,
        `Type` int NOT NULL,
        `Status` int NOT NULL,
        `IsPublic` tinyint(1) NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        `DeletedAt` datetime(6) NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `UploadedFiles` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `FileName` longtext NOT NULL,
        `FileSize` bigint NOT NULL,
        `FileSHA256Hash` longtext NOT NULL,
        `BackUpUrl` longtext NOT NULL,
        `RemoteUrl` longtext NOT NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        `DeletedAt` datetime(6) NULL,
        PRIMARY KEY (`Id`)
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetRoleClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `RoleId` int NOT NULL,
        `ClaimType` longtext NULL,
        `ClaimValue` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetUserClaims` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `UserId` int NOT NULL,
        `ClaimType` longtext NULL,
        `ClaimValue` longtext NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetUserLogins` (
        `LoginProvider` varchar(255) NOT NULL,
        `ProviderKey` varchar(255) NOT NULL,
        `ProviderDisplayName` longtext NULL,
        `UserId` int NOT NULL,
        PRIMARY KEY (`LoginProvider`, `ProviderKey`),
        CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetUserRoles` (
        `UserId` int NOT NULL,
        `RoleId` int NOT NULL,
        PRIMARY KEY (`UserId`, `RoleId`),
        CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `AspNetUserTokens` (
        `UserId` int NOT NULL,
        `LoginProvider` varchar(255) NOT NULL,
        `Name` varchar(255) NOT NULL,
        `Value` longtext NULL,
        PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
        CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `T_Concerts` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` varchar(200) NOT NULL,
        `Description` varchar(1000) NULL,
        `StartAt` datetime(6) NOT NULL,
        `EndAt` datetime(6) NOT NULL,
        `Address` varchar(300) NOT NULL,
        `CoverUrl` longtext NULL,
        `AlbumId` int NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        `DeletedAt` datetime(6) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_T_Concerts_T_Albums_AlbumId` FOREIGN KEY (`AlbumId`) REFERENCES `T_Albums` (`Id`) ON DELETE SET NULL
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE TABLE `T_Tracks` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Title` varchar(200) NOT NULL,
        `Subtitle` varchar(200) NULL,
        `Description` varchar(1000) NULL,
        `Duration` int NOT NULL,
        `ReleaseDate` datetime(6) NOT NULL,
        `Artist` varchar(100) NOT NULL,
        `Composer` varchar(100) NOT NULL,
        `Lyricist` varchar(100) NOT NULL,
        `CoverUrl` longtext NULL,
        `Type` int NOT NULL,
        `AlbumId` int NULL,
        `CreatedAt` datetime(6) NOT NULL,
        `UpdatedAt` datetime(6) NOT NULL,
        `IsDeleted` tinyint(1) NOT NULL,
        `DeletedAt` datetime(6) NULL,
        PRIMARY KEY (`Id`),
        CONSTRAINT `FK_T_Tracks_T_Albums_AlbumId` FOREIGN KEY (`AlbumId`) REFERENCES `T_Albums` (`Id`) ON DELETE SET NULL
    );
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_AspNetRoleClaims_RoleId` ON `AspNetRoleClaims` (`RoleId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_AspNetUserClaims_UserId` ON `AspNetUserClaims` (`UserId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_AspNetUserLogins_UserId` ON `AspNetUserLogins` (`UserId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_AspNetUserRoles_RoleId` ON `AspNetUserRoles` (`RoleId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_T_Albums_Title` ON `T_Albums` (`Title`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE UNIQUE INDEX `IX_T_Concerts_AlbumId` ON `T_Concerts` (`AlbumId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_T_Tracks_AlbumId` ON `T_Tracks` (`AlbumId`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    CREATE INDEX `IX_T_Tracks_Title` ON `T_Tracks` (`Title`);
END;

IF NOT EXISTS(SELECT * FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20260908065721_init')
BEGIN
    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20260908065721_init', '10.0.7');
END;

COMMIT;

