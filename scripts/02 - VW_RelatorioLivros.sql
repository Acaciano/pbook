 USE DB_PBook
 GO

 CREATE VIEW VW_RelatorioLivros AS
	SELECT 
			L.Titulo,
			L.Editora,
			L.Edicao,
			L.AnoPublicacao,
			L.Preco,
			A.Nome AS Assunto,
			Autor.Nome Autor
		FROM Livros L
		INNER JOIN LivroAssuntos LA ON L.Id = LA.LivroId
		INNER JOIN Assuntos A ON A.Id = LA.AssuntoId
		INNER JOIN LivroAutores LAutor ON L.Id = LAutor.LivroId
		INNER JOIN Autores Autor ON Autor.Id = LAutor.AutorId
GO