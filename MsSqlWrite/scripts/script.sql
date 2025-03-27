CREATE TABLE Utillisateur
(
    [Id] INT NOT NULL IDENTITY,
    [Nom] NVARCHAR(50) NOT NULL,
    CONSTRAINT PK_Utilisateur PRIMARY KEY ([Id]),
    CONSTRAINT UK_Utilisateur_Nom UNIQUE ([Nom])
);

CREATE TABLE Message
(
    [Uid] UNIQUEIDENTIFIER NOT NULL,
    [Message] NVARCHAR(1000) NOT NULL,
    [UtilisateurId] INT NOT NULL,
    CONSTRAINT PK_Message PRIMARY KEY ([Uid]),
    CONSTRAINT FK_Message_Utilisateur FOREIGN KEY ([UtilisateurId]) REFERENCES [Utilisateur]([Id])
);
GO

CREATE PROCEDURE CSP_AddMessage
    @Nom NVARCHAR(50),
    @Message NVARCHAR(1000)
AS
BEGIN
    BEGIN TRY
        
    END TRY
    BEGIN CATCH
    END CATCH
END