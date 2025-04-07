CREATE DATABASE WriteDb;
GO

USE WriteDb;
GO

CREATE TABLE Utilisateur
(
    [Id] INT NOT NULL IDENTITY,
    [Nom] NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Utilisateur PRIMARY KEY ([Id]),
    CONSTRAINT UK_Utilisateur_Nom UNIQUE ([Nom])
);

CREATE TABLE Message
(
    [Uid] UNIQUEIDENTIFIER NOT NULL,
    [Date] DATE NOT NULL,
    [Content] NVARCHAR(1000) NOT NULL,
    [UtilisateurId] INT NOT NULL,
    CONSTRAINT PK_Message PRIMARY KEY ([Uid], [Date]),
    CONSTRAINT FK_Message_Utilisateur FOREIGN KEY ([UtilisateurId]) REFERENCES [Utilisateur]([Id])
);
GO

CREATE PROCEDURE CSP_AddMessage
    @Uid UNIQUEIDENTIFIER,
    @Date Date,
    @Nom NVARCHAR(50),
    @Content NVARCHAR(1000)
AS
BEGIN
    SET @Nom = TRIM(@Nom);
    SET @Content = TRIM(@Content);

    BEGIN TRANSACTION AddMessage;
    BEGIN TRY
        if LEN(@Nom) = 0
        BEGIN
            RAISERROR ('Le nom est invalide', 16, 1);
        END

        IF NOT EXISTS (SELECT * FROM Utilisateur WHERE Nom = @Nom)
        BEGIN
            INSERT INTO Utilisateur (Nom) VALUES (@Nom);
        END

        DECLARE @UtilisateurId INT;
        SELECT @UtilisateurId = Id FROM Utilisateur WHERE Nom = @Nom;

        INSERT INTO [Message] ([Uid], [Date], [Content], [UtilisateurId])
        VALUES (@Uid, @Date, @Content, @UtilisateurId);

        COMMIT TRANSACTION AddMessage;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @Severity INT = ERROR_SEVERITY();
        DECLARE @State INT = ERROR_STATE();

        RAISERROR (@ErrorMessage, @Severity, @State);
        ROLLBACK TRANSACTION AddMessage;
    END CATCH

    IF @@TRANCOUNT = 1
    BEGIN
        COMMIT;
    END
END