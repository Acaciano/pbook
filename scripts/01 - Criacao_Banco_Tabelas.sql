CREATE DATABASE DB_PBook
GO

USE DB_PBook
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Assuntos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Assuntos] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Autores] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(255) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Livros] (
    [Id] int NOT NULL IDENTITY,
    [Titulo] nvarchar(255) NOT NULL,
    [Editora] nvarchar(150) NOT NULL,
    [Edicao] nvarchar(10) NOT NULL,
    [AnoPublicacao] int NOT NULL,
    [Preco] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Livros] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [LivroAssuntos] (
    [Id] int NOT NULL IDENTITY,
    [LivroId] int NOT NULL,
    [AssuntoId] int NOT NULL,
    CONSTRAINT [PK_LivroAssuntos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LivroAssuntos_Assuntos_AssuntoId] FOREIGN KEY ([AssuntoId]) REFERENCES [Assuntos] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LivroAssuntos_Livros_LivroId] FOREIGN KEY ([LivroId]) REFERENCES [Livros] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LivroAutores] (
    [Id] int NOT NULL IDENTITY,
    [LivroId] int NOT NULL,
    [AutorId] int NOT NULL,
    CONSTRAINT [PK_LivroAutores] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LivroAutores_Autores_AutorId] FOREIGN KEY ([AutorId]) REFERENCES [Autores] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LivroAutores_Livros_LivroId] FOREIGN KEY ([LivroId]) REFERENCES [Livros] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_LivroAssuntos_AssuntoId] ON [LivroAssuntos] ([AssuntoId]);
GO

CREATE INDEX [IX_LivroAssuntos_LivroId] ON [LivroAssuntos] ([LivroId]);
GO

CREATE INDEX [IX_LivroAutores_AutorId] ON [LivroAutores] ([AutorId]);
GO

CREATE INDEX [IX_LivroAutores_LivroId] ON [LivroAutores] ([LivroId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240806135354_InicialDB', N'8.0.7');
GO

COMMIT;
GO

