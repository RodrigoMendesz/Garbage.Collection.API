# GarbageCollection

## Descrição

Essa API permite gerenciar recursos de coleta inteligente com operações CRUD (Criar, Ler, Atualizar e Deletar). Desenvolvida em .NET 8, com suporte a autenticação JWT e usando Entity Framework Core para persistência de dados.

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Swagger](https://swagger.io/) para documentação da API
- [JWT](https://jwt.io/) para autenticação
- [SQL Server](https://www.microsoft.com/pt-br/sql-server) ou outro banco de dados

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server) ou outro banco de dados configurado
- [Postman](https://www.postman.com/) ou similar para testar os endpoints

## Instalação

  1. Clone este repositório:

    git clone https://github.com/seu-usuario/sua-api-dotnet.

  2. Navegue até a pasta do projeto:

    cd /GarbageCollection

  3. Instale as dependências:

    dotnet restore

  4.Configure o banco de dados no arquivo appsettings.json:

    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Database=SeuBancoDeDados;Trusted_Connection=True;"
    }

  5. Atualize o banco de dados com as migrações do Entity Framework Core:
   
    dotnet ef database update


  ##Executando a Aplicação
    Execute o projeto localmente com o seguinte comando:

    dotnet run

  ##Documentação da API
    A documentação da API está disponível no Swagger. Para acessar, inicie o servidor e acesse:

    https://localhost:5001/swagger

  ##Autenticação
    Esta API usa autenticação baseada em JWT. Para acessar alguns endpoints protegidos, é necessário incluir um token de autorização no cabeçalho da requisição:

    Authorization: Bearer {seu_token_jwt}
    Gerando Token de Autenticação
    Você pode gerar um token autenticando um usuário via o endpoint /api/auth/login com as credenciais:
    {
      "username": "adm",
      "password": "123"
    }

  ##Testes
    Para rodar os testes unitários, utilize o seguinte comando:

    dotnet test

  ##Deploy no Azure
    Configure seu ambiente no Azure App Service.
    Publique sua API no Azure usando o comando:

    dotnet publish --configuration Release
    Siga as instruções do Azure para fazer o deploy da aplicação.

  ##Contribuições
    Contribuições são bem-vindas! Por favor, abra uma issue antes de enviar um pull request.

  ##Licença
    Este projeto está licenciado sob a MIT License.




