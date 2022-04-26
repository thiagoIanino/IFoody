create database Ifoody
use Ifoody

create table [Cliente] (
    id uniqueidentifier Not null PRIMARY KEY,
    nome varchar(60) not null,
    email varchar(60) not null,
    senha varchar(40) not null
)


insert into Cliente(id,nome,email,senha,idStripe,role) values 
('f3312420-96f7-47c4-872b-12f308a35a17','Pedro Augusto','pedrin@gmail.com','pedrinDOmal','cus_LDy3gyqyCK2K17','cliente'),
('79e0480a-a50c-4b76-aef9-f1cb29f67a7e','ADM_IFOODY','ifoodyAdm@gmail.com','admifoody','cus_LDy3gyqyCK2K17','cliente')

create table [Restaurante] (
    id uniqueidentifier Not null PRIMARY KEY,
    nomeRestaurante varchar(60) not null,
    nomeDonoRestaurante varchar(60) not null,
    cnpj char(14) not null,
    email varchar(60) not null,
    senha varchar(40) not null,
    tipo varchar(60) not null
)


select * from Restaurante

create table Avaliacao(
    id UNIQUEIDENTIFIER not null ,
    nota FLOAT not null,
    descricao VARCHAR(150),
    data DATETIME not null,
    idRestaurante UNIQUEIDENTIFIER not null,
    idCliente UNIQUEIDENTIFIER not null,
    PRIMARY key([id]),
    FOREIGN KEY([idRestaurante]) REFERENCES [Restaurante]([id]),
    FOREIGN KEY([idCliente]) REFERENCES [Cliente]([id])
)

create table StatusAvaliacaoRestaurante(
    idRestaurante UNIQUEIDENTIFIER not null,
    status bit not null
)


create table [Prato](
    id UNIQUEIDENTIFIER not null PRIMARY key,
    nomePrato VARCHAR(50) not null,
    descricao VARCHAR(80) not null,
    urlImagem VARCHAR(150) not null,
    valor FLOAT not null,
    idRestaurante UNIQUEIDENTIFIER not null,

    FOREIGN KEY([idRestaurante]) REFERENCES [Restaurante]([id])
)

create table Cartao(
    idCliente UNIQUEIDENTIFIER not null,
    idCartao UNIQUEIDENTIFIER PRIMARY key not null,
    numero varchar (20) not null,
    numeroMascarado varchar (20) not null,
    validade DATETIME not null,
    nomeTitular varchar(60) not null,
    cpf VARCHAR(20) not NULL

    FOREIGN KEY (idCliente) REFERENCES Cliente
)

create table EnderecoCliente(
    id UNIQUEIDENTIFIER PRIMARY key not null,
    idCliente UNIQUEIDENTIFIER not null,
    linha1End varchar(100) not null,
    linha2End varchar(100) not null
)

insert into Restaurante(id,nomeRestaurante,nomeDonoRestaurante,cnpj,email,senha,tipo) values('3962dc97-01f6-471e-8b22-ee7f6ff1ed25','Thdfdf','fsfsf','83647283909384','fsdfsdf','@senha','fsfsfdsf')

create table Cobrancas(
    id UNIQUEIDENTIFIER PRIMARY key not null,
    idCartaoStripe varchar(40) not null,
    idCliente UNIQUEIDENTIFIER not null,
    idRestaurante UNIQUEIDENTIFIER not null,
    status varchar(20) not null,
    valor float not null,
    idPedido UNIQUEIDENTIFIER not null
)