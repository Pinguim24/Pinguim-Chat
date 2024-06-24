CREATE DATABASE PENGUIN_CHAT

-- Cirar tebelas 

CREATE TABLE Pessoa(
    email INT PRIMARY KEY NOT NULL,
    nome VARCHAR(200) NOT NULL,
    nickname VARCHAR(200) NOT NULL
);

CREATE TABLE Conversa(
    nickname VARCHAR(200)
);

CREATE TABLE Contactos(
    email INT PRIMARY KEY NOT NULL,
    nicknameAmigo VARCHAR(200) NOT NULL, 

    FOREIGN KEY (email) REFERENCES Pessoa(email)
    FOREIGN KEY (nicknameAmigo) REFERENCES Conversa(nickname)
);