USE Senatur_Tarde;

SELECT * FROM TiposUsuario;

SELECT IdUsuario, Email, Senha, TiposUsuario.Titulo FROM Usuarios
INNER JOIN TiposUsuario ON TiposUsuario.IdTipoUsuario = Usuarios.IdTipoUsuario

SELECT * FROM Pacotes