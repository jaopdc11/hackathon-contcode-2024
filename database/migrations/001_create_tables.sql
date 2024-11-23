-- 001_create_tables.sql

-- Criando tabela usuarios
CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(30) NOT NULL
);

-- Criando tabela cargas
CREATE TABLE cargas (
    id SERIAL PRIMARY KEY,
    carga VARCHAR(25) DEFAULT 'vazio' NOT NULL,
    categoria VARCHAR(25) NOT NULL,
    quantidade INT NOT NULL,
    peso FLOAT NOT NULL
);

-- Criando tabela caminhoes
CREATE TABLE caminhoes (
    id SERIAL PRIMARY KEY,
    placa VARCHAR(7) UNIQUE NOT NULL,
    cargaId INT NOT NULL,
    status VARCHAR(15) DEFAULT 'aguardando' NOT NULL,
    CONSTRAINT fk_carga_caminhao FOREIGN KEY (cargaId) REFERENCES cargas (id)
);

-- Criando tabela operacoes
CREATE TABLE operacoes (
    id SERIAL PRIMARY KEY,
    caminhaoId INT NOT NULL,
    cargaId INT NOT NULL,
    usuarioId INT NOT NULL,
    tipoOperacao VARCHAR(15) DEFAULT 'entrada' NOT NULL,
    dataHoraOperacao TIMESTAMP DEFAULT now() NOT NULL,
    CONSTRAINT fk_caminhao_operacao FOREIGN KEY (caminhaoId) REFERENCES caminhoes (id),
    CONSTRAINT fk_carga_operacao FOREIGN KEY (cargaId) REFERENCES cargas (id),
    CONSTRAINT fk_usuario_operacao FOREIGN KEY (usuarioId) REFERENCES usuarios (id)
);

-- Criando tabela vagas
CREATE TABLE vagas (
    id SERIAL PRIMARY KEY,
    localizacao VARCHAR(3) UNIQUE NOT NULL,
    cargaId INT NULL,
    status VARCHAR(15) DEFAULT 'livre' NOT NULL,
    CONSTRAINT fk_carga_vaga FOREIGN KEY (cargaId) REFERENCES cargas (id)
);

-- Criando tabela estoque
CREATE TABLE estoque (
    id SERIAL PRIMARY KEY,
    cargaId INT NULL,
    quantidade INT NOT NULL,
    vagaId INT NOT NULL,
    CONSTRAINT fk_carga_estoque FOREIGN KEY (cargaId) REFERENCES cargas (id),
    CONSTRAINT fk_vaga_estoque FOREIGN KEY (vagaId) REFERENCES vagas (id)
);
