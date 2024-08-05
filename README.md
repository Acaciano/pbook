# Sistema de Livraria

Bem-vindo ao projeto de Sistema de Livraria! Este é um sistema completo para gerenciar o cadastro de livros, autores e assuntos, desenvolvido com as seguintes tecnologias:

## Tecnologias Utilizadas

- **.NET 8.0**: Framework utilizado para o desenvolvimento da aplicação.
- **ASP.NET MVC**: Framework para criar aplicações web baseadas em Model-View-Controller.
- **Entity Framework**: ORM (Object-Relational Mapper) utilizado para interagir com o banco de dados SQL Server.
- **Dapper**: Micro-ORM utilizado para consultas rápidas e eficientes.
- **SQL Server**: Banco de dados relacional utilizado para armazenar as informações da aplicação.

## Funcionalidades

1. **Cadastro de Livros**:
   - Permite adicionar, editar e excluir livros.
   - Vinculação de livros a autores e assuntos.

2. **Cadastro de Autores**:
   - Permite adicionar, editar e excluir autores.

3. **Cadastro de Assuntos**:
   - Permite adicionar, editar e excluir assuntos.

## Estrutura do Projeto

### Camadas do Projeto

1. **Camada de Apresentação**:
   - Contém as Views e Controllers que definem a interface do usuário e a lógica de apresentação.
   
2. **Camada de Serviço**:
   - Contém as classes de serviço que encapsulam a lógica de negócios e interagem com os repositórios.
   
3. **Camada de Repositório**:
   - Contém os repositórios que utilizam Entity Framework e Dapper para acessar e manipular dados no banco de dados.
   
4. **Camada de Dados**:
   - Contém as entidades do Entity Framework e a configuração do banco de dados.

## Configuração do Ambiente

1. **Clone o Repositório**:
   ```bash
   git clone https://github.com/Acaciano/pbook
