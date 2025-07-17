# MiniMES.API

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-69217E?style=for-the-badge&logo=dot-net&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-8869A6?style=for-the-badge&logo=entity-framework&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-A0A0A0?style=for-the-badge&logo=github&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![XML](https://img.shields.io/badge/XML-00599C?style=for-the-badge&logo=xml&logoColor=white)

## Descrição do Projeto

O **MiniMES.API** é uma API RESTful desenvolvida em ASP.NET Core 8 que simula as funcionalidades essenciais de um **Sistema de Execução de Manufatura (MES - Manufacturing Execution System)**. Este projeto tem como objetivo demonstrar a capacidade de construir sistemas transacionais e de coleta de dados em tempo real para o chão de fábrica, integrando-se a diferentes fontes de informação e fornecendo uma base para o monitoramento e controle da produção.

### Contexto Industrial

Em ambientes de manufatura modernos, o MES desempenha um papel crucial na ponte entre o planejamento empresarial (ERP) e a execução física no chão de fábrica. Ele permite a coleta de dados precisos e em tempo real sobre máquinas, ordens de produção e eventos, facilitando a tomada de decisões, a otimização de processos e o aumento da eficiência operacional. O MiniMES.API foca na simulação da ingestão desses dados cruciais.

## Funcionalidades Implementadas

O projeto oferece endpoints para gerenciar e registrar informações fundamentais para o ambiente MES:

* **Gestão de Máquinas:**
    * Criação, leitura, atualização e exclusão de cadastros de máquinas.
    * As máquinas representam os ativos de produção no chão de fábrica.
* **Gestão de Ordens de Produção:**
    * Criação, leitura, atualização e exclusão de ordens de produção.
    * As ordens ditam o que deve ser produzido e seu status (`Pendente`, `Executando`, `Concluída`).
* **Registro de Eventos de Produção (Integração):**
    * Endpoint para o registro de eventos em tempo real associados a máquinas e ordens de produção.
    * Suporte para diferentes tipos de eventos como `Início`, `Parada`, `Alerta` e `Fim`.
    * **Destaque para Integração:** Implementação de um endpoint capaz de receber eventos tanto via **JSON** (padrão web) quanto via **XML** (`application/xml`), simulando a interoperabilidade com sistemas legados ou dispositivos de automação industrial (CLPs, SCADAs) que frequentemente utilizam este formato para troca de dados estruturados.

## Tecnologias Utilizadas

* **Backend:**
    * **C# / .NET 8 (ASP.NET Core):** Linguagem e framework robustos para construção de APIs escaláveis.
    * **Entity Framework Core (EF Core):** ORM para abstração e persistência de dados.
    * **SQL Server:** Banco de dados relacional para armazenamento das informações de manufatura.
    * **AutoMapper:** Para simplificar o mapeamento entre objetos (Models, DTOs, Commands).
    * **Swagger/OpenAPI:** Documentação interativa da API, facilitando o consumo e teste dos endpoints.
    * **Formatação XML:** Utilização de `Microsoft.AspNetCore.Mvc.Formatters.Xml` para suportar requisições e respostas em formato XML, essencial para cenários de integração industrial.

## Estrutura do Projeto

* **`Controllers/`:** Define os endpoints da API para Máquinas, Ordens de Produção e Eventos.
* **`Models/`:** Contém as entidades de domínio (Máquina, OrdemProducao, EventoProducao) que representam os dados do chão de fábrica.
* **`Data/`:** `MiniMESContext` (DbContext) e configurações do Entity Framework Core para acesso ao banco de dados.
* **`DTOs/`:** Data Transfer Objects para estruturar as respostas da API e dados de entrada/saída.
* **`Commands/`:** Objetos de comando para encapsular as requisições de criação e atualização, incluindo um `CreateEventoProducaoXmlCommand` dedicado à integração XML.
* **`Configurations/`:** Configurações do AutoMapper para mapeamento de objetos.
* **`Migrations/`:** Histórico de migrações do Entity Framework Core para gerenciamento do esquema do banco de dados.

## Como Executar o Projeto

1.  **Pré-requisitos:**
    * .NET SDK 8.0 ou superior.
    * SQL Server (instância local ou remota acessível).
    * Um editor de código como Visual Studio 2022 ou VS Code.

2.  **Configuração do Banco de Dados:**
    * Abra o arquivo `appsettings.json` e configure a string de conexão (`ConnectionStrings:DefaultConnection`) para apontar para sua instância do SQL Server. Exemplo:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=localhost;Database=MiniMESDb;Integrated Security=True;TrustServerCertificate=True"
        }
        ```
    * Abra o terminal na raiz do projeto (`MiniMES.API.sln`).
    * Execute os comandos para aplicar as migrações e criar o banco de dados:
        ```bash
        dotnet ef database update
        ```

3.  **Executar a Aplicação:**
    * Abra o Visual Studio e carregue a solução `MiniMES.API.sln`.
    * Pressione `F5` ou clique no botão `IIS Express` para iniciar a API.
    * A API será iniciada e o Swagger UI será aberto no navegador (ex: `https://localhost:7xxx/swagger`).

## Demonstração de Integração XML (Exemplo)

Você pode testar o endpoint de recebimento de XML usando o Swagger UI. Navegue até o endpoint `POST /api/eventos/xml`, selecione `application/xml` no `Content-Type` e utilize um corpo de requisição similar a este (ajuste os IDs conforme seus dados):

```xml
<EventoMaquina>
    <Tipo>Inicio</Tipo>
    <DataHora>2025-07-16T23:47:00</DataHora>
    <MaquinaId>1</MaquinaId>
    <OrdemProducaoId>1</OrdemProducaoId>
</EventoMaquina>
```
