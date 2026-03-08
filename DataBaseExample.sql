Use master
go
CREATE DATABASE DbSistemaAcademico;
GO
USE DbSistemaAcademico;
GO

CREATE TABLE Curso (
    IdCurso INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    DuracaoSemestres INT NOT NULL
);

CREATE TABLE Aluno (
    IdAluno INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(150) NOT NULL,
    Email VARCHAR(150),
    DataNascimento DATE
);

CREATE TABLE Matricula (
    IdMatricula INT IDENTITY(1,1) PRIMARY KEY,
    IdAluno INT NOT NULL,
    IdCurso INT NOT NULL,
    DataMatricula DATE NOT NULL,
    Status VARCHAR(50) DEFAULT 'Pendente',

    FOREIGN KEY (IdAluno) REFERENCES Aluno(IdAluno),
    FOREIGN KEY (IdCurso) REFERENCES Curso(IdCurso)
);

CREATE TABLE Boleto (
    IdBoleto INT IDENTITY(1,1) PRIMARY KEY,
    IdMatricula INT NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataVencimento DATE NOT NULL,
    Status VARCHAR(50) DEFAULT 'Pendente',

    FOREIGN KEY (IdMatricula) REFERENCES Matricula(IdMatricula)
);

-- DADOS DE EXEMPLO

INSERT INTO Curso (Nome, DuracaoSemestres)
VALUES 
('Engenharia de Software', 8),
('Administração', 8),
('Direito', 10);

INSERT INTO Aluno (Nome, Email, DataNascimento)
VALUES
('João Silva', 'joao@email.com', '2000-05-10'),
('Maria Oliveira', 'maria@email.com', '1999-08-21');

INSERT INTO Matricula (IdAluno, IdCurso, DataMatricula)
VALUES
(1, 1, GETDATE()),
(2, 2, GETDATE());

INSERT INTO Boleto (IdMatricula, Valor, DataVencimento)
VALUES
(1, 850.00, DATEADD(DAY, 30, GETDATE())),
(2, 750.00, DATEADD(DAY, 30, GETDATE()));