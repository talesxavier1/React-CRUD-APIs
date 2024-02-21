<h1>React CRUD APIs</h1>

<h3>Objetivo:</h3>
Criar uma camada de API em .NET aplicando o padrão de design Repository.

------------
<h3>O que está sendo desenvolvido?:</h3>
Esse projeto tem o objetivo de fornecer uma camada de API para uma aplicação desenvolvida em React.
Mais detalher sobre essa aplicação podem ser conferidos {{AQUI}}

------------
<h3>Problemas:</h3>
Durante o desenvolvimento desse projeto dois principais problemas causaram lentidão na evolução.

1 - <b>Falta de abstração</b>: Durante o desenvolvimenro, pude notar que a maioria das interfaces do meu Repository possuiam as mesmas funcionalidades. A criação de uma interface genérica atenderia a maioria das classes de implementação do repository.<br>
2 - <b>Autenticação</b>: A forma de autenticação poderia ser mais simples com a implementação de um middleware. Da forma que foi construído, cada método do meu controller tinha um bloco de código voltado a validação do token passado pelo cliente.<br>
3 - <b>Quantidade de Collections</b>: Para cada cadastro foi criado uma colection. O volume de dados desses cadastros não é grande, uma única collection poderia comportar os dados de todos os cadastros.

------------
<h3>Principais aprendizados:</h3>

1 - Melhoria nos conhecimentos .NET.<br>
2 - Repository Pattern.<br>
3 - C# + MongoDb.<br>
4 - Criação de APIs.<br>
5 - Melhoria nos conhecimento de C#.<br>
6 - Melhoria nos conhecimentos de Docker.<br>

------------
<h3>Ferramentas utilizadas:</h3>

-  [Visual Studio Community 2022](https://visualstudio.microsoft.com/pt-br/vs/community/ "Visual Studio Community")
-  [.NET 6](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0 ".NET 6")
-  [MongoDB Driver 2.17.0](https://www.nuget.org/packages/MongoDB.Driver/2.17.0 "MongoDB Driver 2.17.0")
-  [MongoDB Compass](https://www.mongodb.com/products/tools/compass "MongoDB Compass")

------------
<h3>Funcionalidades:</h3>

- Autenticação:
    - Todas as requisições de manipuplação de dados contam com a validação do user token passado pelo usuário.
- Contole de alteração:
    - Os registros alterados recebem dados de criação, alteração e exclusão. As datas e usuário são registrados no documento.
- Token de Administrador:
    - Um GUID é armazenado no appsettings com a chave 'Admin_AccessToken' e toda requisição feita com esse token é permitida.
- Exclusão lógica:
    - Todas as APIs de exclusão efetuam a exclusão lógica do registro, portanto, os dados permanecem no banco de dados, mas não ficam acessíveis nas APIs de consulta.
 - APIs:
    - A aplicação disponibiliza 17 APIs que foram desenvolvidas para a aplicação feita em React. Cada API permite a manipulação do dado que a representa.

------------
<h3>APIs:</h3>

<div style="background-color:blue">
    <span style="font-family: monospace; font-size: 16px; font-weight: 600;">Texto com fonte Arial</span>
<div>