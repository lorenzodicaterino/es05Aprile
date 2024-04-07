# Esercitazione 05 Aprile

```sql
CREATE TABLE oggetto (
oggettoID INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(500) NOT NULL,
codice_oggetto VARCHAR(250) DEFAULT NEWID(),
data_scoperta DATE NOT NULL,
scopritore VARCHAR(250) NOT NULL,
tipologia VARCHAR(250) NOT NULL,
distanza_dalla_terra DECIMAL(20,2) NOT NULL,
modulo DECIMAL(20,2) NOT NULL,
azimut DECIMAL(20,2) NOT NULL,
deleted DATETIME
);

CREATE TABLE sistema (
sistemaID INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(500) NOT NULL,
codice_sistema VARCHAR(250) DEFAULT NEWID(),
tipo VARCHAR(250) NOT NULL,
deleted DATETIME
);

CREATE TABLE oggetto_sistema(
oggetto_sistemaID INT PRIMARY KEY IDENTITY(1,1),
oggettoRIF INT NOT NULL,
sistemaRIF INT NOT NULL,
FOREIGN KEY (oggettoRIF) REFERENCES oggetto(oggettoID),
FOREIGN KEY (sistemaRIF) REFERENCES sistema(sistemaID)
);

INSERT INTO oggetto (nome, data_scoperta, scopritore, tipologia, distanza_dalla_terra, modulo, azimut) VALUES
('Luna','2012-12-12','Colombo','Satellite',28991.12,12,16),
('Sole','1800-12-12','Cristoforo','Stella',1234123.12,13,15),
('Terra','1872-12-12','Cesare','Pianeta',0.01,14,14),
('Marte','1910-12-12','Napoleone','Pianeta',912309.12,15,13),
('SDLKC123','1982-12-12','Ciccio','Boh',1928349812834.12,16,12);

INSERT INTO sistema (nome, tipo) VALUES
('Sistema Solare','Pianetario'),
('Via Lattea','Galassia'),
('Boh','Costellazione'),
('Boh2','Sos');

INSERT INTO oggetto_sistema(oggettoRIF, sistemaRIF) VALUES
(1,1),
(1,2),
(2,1),
(2,1),
(3,3),
(4,4),
(4,3);

```

Ho provato ad implementare a livello front-end tutti i dettagli necessari, ma i miei grossi limiti sulla conoscenza di HTML e JS hanno limitato un po il tutto. Le funzionalità sono state sviluppate completamente a livello backend, comprese le operazioni di "JOIN".
