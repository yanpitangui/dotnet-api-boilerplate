# dotnet-api-boilerplate
<p align="center">
  <a href="https://github.com/yanpitangui/dotnet-api-boilerplate/tree/main/README.md">English</a> |
  <span>Português</span>
</p>

Um boilerplate de API ``.Net 6.0`` / projeto de template. Repositórios, Swagger, Mapper, Serilog, entre outros, implementados.

[![Build](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml/badge.svg)](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=coverage)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=alert_status)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)

O objetivo deste projeto é ser um ponto de partida para a sua WebApi .Net, implementando os padrões e tecnologias mais comuns para uma API .Net Restful, tornando o seu trabalho mais fácil.

# Como executar
- Utilize esse template (github) ou clone/download em seu repositório local.
- Realize o download do .Net SDK mais novo e Visual Studio/Code.

## Execução Independente
1. Você vai precisar de uma instância do MsSQL rodando, com as migrations propriamente inicializadas.
	- Você pode simplesmente rodar o banco de dados no docker. Para isso, você precisa alterar a connection string para "Server=127.0.0.1;Database=master;User=sa;Password=Yourpassword123” e rodar o comandao a seguir: ``docker-compose up -d db-server``. Fazendo isso, a aplicação estará apta para se conectar ao container do servidor de banco de dados.
	- Caso você prefira, você pode alterar o arquivo DatabaseExtension para utilizar banco de dados em memória (UseInMemoryDatabase), ao invés do Mssql.
2. Vá para a pasta src/Boilerplate.Api e execute ``dotnet run``, ou no visual studio, defina o projeto api como "startup" e execute como console ou docker (não como IIS).
3. Visite http://localhost:5000/api-docs ou https://localhost:5001/api-docs para acessar o swagger da aplicação.

## Docker
1. Execute ``docker-compose up -d`` no diretório raiz ou, no visual studio, defina o projeto docker-compose project como "startup" e execute. Isso iniciará a aplicação e o banco de dados.
 - 1. Para o docker-compose, você deve executar esse comando na pasta raiz: ``dotnet dev-certs https -ep https/aspnetapp.pfx -p suasenha``
		Substitua "suasenha" por outra coisa nesse comando e o arquivo docker-compose.override.yml.
Isso criará o certificado https.
2. Visite http://localhost:5000/api-docs oru https://localhost:5001/api-docs para acessar o swagger da aplicação.

## Executando testes
Na pasta raiz, execute ``dotnet test``. Este comando tentará encontrar todos os porjetos associados ao arquivo da solução.
Se você estiver utilizando o Visual Studio, você também pode acessar o menu "Test" e abrir o "Test Explorer", onde é possível executar todos os testes ou algum específico.

## Autenticação
Neste projeto, algumas rotas precisam de autenticação/autorização. Para isso, você terá que utilizar a rota ``api/user/authenticate`` para obter o JWT.
Por padrão, você tem dois usuários disponíveis para login:
- Usuário normal: 
	- email: user@boilerplate.com
	- senha: userpassword
- Admin:
	- email: admin@boilerplate.com
	- senha: adminpassword

Depois disso, você pode passar o JWT clicando no cadeado (se estiver usando swagger) ou via o cabeçalho `Authorization` se tiver realizando uma requisição http.

# Este projeto contém:
- SwaggerUI
- EntityFramework
- AutoMapper
- Repositório genérico (Para facilmente criar um repositório CRUD)
- Serilog com logs de requisição e tipos de saída facilmente configuráveis
- Injeção de Dependência
- Filtro de recursos
- Compressão de Resposta
- Paginação de resposta
- CI (Github Actions)
- Testes Unitários
- Testes de Integração
- Suporte de Container com [docker](src/Boilerplate.Api/dockerfile) e [docker-compose](docker-compose.yml)


# Estrutura do Projeto
1. Services
	- Esta pasta guarda suas apis e qualquer projeto que envie dados aos seus usuários.
	1. Boilerplate.Api
		- Este é o projeto principal. Aqui estão todos os controllers e inicialização para a api que será utilizada.
	2. docker-compose
		- Este projeto existe para permitir que você execute o docker-compose com o Visual Studio. Ele contém uma referência para o arquivo docker-compose e irá construir todas as dependências do projeto e executar.
2. Application
	- Esta pasta guarda todas as transformações de dados entre sua api e sua camada de domínio. Ela também contém lógica de negócio.
3. Domain
	- Esta pasta contém seus modelos de negócio, enums e interfaces comuns.
	1. Boilerplate.Domain.Core
		- Contém a entidade base para todas as outras entidades de domínio, bem como a interface para a implementação do repositório.
	1. Boilerplate.Domain
		- Contém modelos de negócio e enums.
4. Infra
	- Esta pasta contém todos os repositórios de acesso à dados, contexto de banco de dados, tudo o que se conecta com dados externos.
	1. Boilerplate.Infrastructure
		- Este projeto contém o dbcontext, uma implementação genérica do padrão de repositório e um repositório da classe de domínio Hero.


# Adotando ao seu projeto
1. Remova/Renomeie todas as coisas relacionados ao "Hero" de acordo com a sua necessidade.
2. Renomeie a solução, projetos, namespaces, e ruleset de acordo com a sua necessidade.
3. Altere o dockerfile e docker-compose.yml de acordo com os novos nomes de csproj/pasta.
3. Dê uma estrela a este repositório!

# Migrations
1. Para executar migrations neste projeto, execute o comando a seguir na pasta raiz: 
	- ``dotnet ef migrations add InitialCreate --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\``

2. Este comando definirá o entrypoint dessa migration (o responsável por selecionar o provedor de banco { sqlserver, mysql, etc } e a connection string) e o próprio projeto selecionado será o "Boilerplate.Infrastructure", que é onde fica o dbcontext.

# Caso tenha gostado deste repositório, dê uma estrela!
Se este template foi útil para você ou se você aprendeu algo, por favor dê uma estrela! :star:

# Obrigado!
Este projeto tem grande influência de https://github.com/lkurzyniec/netcore-boilerplate e https://github.com/EduardoPires/EquinoxProject. Se você tiver tempo, por favor, visite estes repositórios e dê estrelas a eles também!

# Sobre
Este boilerplate/template foi desenvolvido por Yan Pitangui sob [Licença MIT](LICENSE).
