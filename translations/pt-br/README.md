# dotnet-api-boilerplate
<p align="center">
  <a href="https://github.com/yanpitangui/dotnet-api-boilerplate/tree/main/README.md">English</a> |
  <span>Português</span>
</p>

Um boilerplate de API ``.Net 8.0`` / projeto de template. MediatR, Swagger, ~~AutoMapper~~ Mapster, Serilog, entre outros, implementados.

[![Build](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml/badge.svg)](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=coverage)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=yanpitangui_dotnet-api-boilerplate&metric=alert_status)](https://sonarcloud.io/dashboard?id=yanpitangui_dotnet-api-boilerplate)

O objetivo deste projeto é ser um ponto de partida para a sua WebApi .Net, implementando os padrões e tecnologias mais comuns para uma API .Net Restful, tornando o seu trabalho mais fácil.

# Como executar
- Utilize esse template (github) ou clone/download em seu repositório local.
- Realize o download do .Net SDK mais novo e Visual Studio/Code/Rider.

## Execução Independente
1. Você vai precisar de uma instância do Postgres em execução, com as migrações apropriadas inicializadas.
- Você pode executar apenas o banco de dados no docker. Para isso, execute o seguinte comando: ``docker-compose up -d db-server``. Fazendo isso, a aplicação poderá chegar ao container do postgres.
2. Vá para a pasta src/Boilerplate.Api e execute ``dotnet run``, ou, no visual studio, defina o projeto da API como inicialização e execute como console/docker/IIS.
3. Visite http://localhost:7122/api-docs ou https://localhost:7123/api-docs para acessar o swagger do aplicativo.

## Docker
1. Execute ``docker-compose up -d`` no diretório raiz ou, no visual studio, defina o projeto docker-compose project como "startup" e execute. Isso iniciará a aplicação e o banco de dados.
 - 1. Para o docker-compose, você deve executar esse comando na pasta raiz: ``dotnet dev-certs https -ep https/aspnetapp.pfx -p yourpassword``
		Substitua "yourpassword" por outra coisa nesse comando e o arquivo docker-compose.override.yml.
Isso criará o certificado https.
2. Visite http://localhost:7122/api-docs ou https://localhost:7123/api-docs para acessar o swagger da aplicação.

## Executando testes

**Importante**: É necessário ter o docker instalado e rodando. Os testes de integração vão criar um container Postgres para testar juntamente com a Api.

Na pasta raiz, execute ``dotnet test``. Este comando tentará encontrar todos os porjetos associados ao arquivo da solução.
Se você estiver utilizando o Visual Studio, você também pode acessar o menu "Test" e abrir o "Test Explorer", onde é possível executar todos os testes ou algum específico.

## Autenticação
Neste projeto, algumas rotas requerem autenticação/autorização. Para isso, você terá que usar a rota ``api/identity/register`` para criar uma conta.
Depois disso, você pode fazer login usando ``/api/identity/login`` sem usar cookies e então usar o accessToken recebido no cadeado (se estiver usando swagger) ou através do cabeçalho Authorization em uma solicitação http.
Para obter mais informações, dê uma olhada na documentação do swagger.

# Este projeto contém:
- SwaggerUI
- EntityFramework
- Postgres
- Minimal apis
- Ids fortemente tipados
- ~~AutoMapper~~ Mapster
- MediatR
- Feature Slicing (divisão das porções lógicas da api em pastas organizadas)
- Serilog com logs de requisição e tipos de saída facilmente configuráveis
- Injeção de Dependência
- Filtro de recursos
- Compressão de Resposta
- Paginação de resposta
- CI (Github Actions)
- Testes Unitários
- Testes de Integração com testcontainers
- Suporte a containers com [docker](src/Boilerplate.Api/dockerfile) e [docker-compose](docker-compose.yml)
- Suporte a OpenTelemetry (com OLTP como o exportador padrão)
- Gerenciamento centralizado de pacotes do NuGet

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
	1. Boilerplate.Domain
		- Contém modelos de negócio e enums.
4. Infra
	- Esta pasta contém todas as configurações de acesso à dados, contexto de banco de dados, tudo o que se conecta com dados externos.
	1. Boilerplate.Infrastructure
		- Este projeto contém o dbcontext, configurações das entidades do banco e migrations.


# Adotando ao seu projeto
1. Remova/Renomeie todas as coisas relacionados ao "Hero" de acordo com a sua necessidade.
2. Renomeie a solução, projetos, namespaces, e ruleset de acordo com a sua necessidade.
3. Altere o dockerfile e docker-compose.yml de acordo com os novos nomes de csproj/pasta.
3. Dê uma estrela a este repositório!

# Migrations
Para executar migrações neste projeto, você precisa da ferramenta dotnet-ef.
- Execute ``dotnet tool install --global dotnet-ef``
- Agora, dependendo do seu sistema operacional, você tem comandos diferentes:
	1. Para Windows: ``dotnet ef migrações add InitialCreate --startup-project .\src\Boilerplate.Api\ --project .\src\Boilerplate.Infrastructure\``
	2. Para linux/mac: ``dotnet ef migrações add InitialCreate --startup-project ./src/Boilerplate.Api/ --project ./src/Boilerplate.Infrastructure/``

# Caso tenha gostado deste repositório, dê uma estrela!
Se este template foi útil para você ou se você aprendeu algo, por favor dê uma estrela! :star:

# Obrigado!
Este projeto tem grande influência de https://github.com/lkurzyniec/netcore-boilerplate e https://github.com/EduardoPires/EquinoxProject. Se você tiver tempo, por favor, visite estes repositórios e dê estrelas a eles também!

# Sobre
Este boilerplate/template foi desenvolvido por Yan Pitangui sob [Licença MIT](LICENSE).
