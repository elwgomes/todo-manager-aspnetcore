<div align="center">

![](https://img.shields.io/badge/Status-Concluded-green)
</div>

<div align="center">

# Todo Manager - ASP.NET CORE + EF
Trata-se de: Prova Prática de Desenvolvimento em C#/.NET: API de TODO/Tarefas

![](https://img.shields.io/badge/Autor-Leonardo%20Gomes-brightgreen)
![](https://img.shields.io/badge/Language-CSHARP-brightgreen)
![](https://img.shields.io/badge/Framework-ENTITY%20FRAMEWORK-brightgreen)
![](https://img.shields.io/badge/docs-swagger-brightgreen)

## Sobre o projeto
### Arquitetura
Desenvolvido utilizando os princípios da Arquitetura Limpa, e design pattern CQRS (Command Query Responsibility Segregation), seguindo à risca a inversão de dependências, a divisão de responsabilidades e o desacoplamento.
![Arquitetura](clean-architecture.jpg "Architecture")
</div> 

### Conceitos
Esta aplicação também apresenta os princípios SOLID e padrões de design, como o Unit of Work.

### Considerações e melhorias
Testes unitários.<br>
A aplicação não é construída pelo Docker, somente o banco de dados, para facilitar aos avaliadores o teste dos endpoints.

###### DEFAULT DB CONFIG
<p>default_schema: todo_db</p>
<p>user_db: havira</p>
<p>password_db: havira</p>

## License
![](https://img.shields.io/badge/license-MIT-brightgreen)


## Tecnologias
- ASP.NET 6
- Entity Framework 6.0.0
  - Design
  - Tools
- MySQL
- Swagger
  - swagger-annotations
- Git

## Execução

- Scripts
  ### Navegue até a camada de Presentation
  - 1° comando: ``` cd Presentation```
  - 2° comando: ```docker-compose up -d```
  - 3° Adicione e faça update das Migrations
  ### Ainda na camada de Presentation
  -  ``` dotnet watch run ```

# Utilização

## Autenticação

POST
api/users/register
```
{
  "username": "seu_username",
  "password": "sua_senha",
}
```

POST
api/auth/login
```
{
  "username": "seu_username",
  "password": "sua_senha",
}
```

A resposta do endpoint de Login retornará um Token JWT. Utilize-o para navegar nos demais endpoints, que são protegidos por [Authenticate].

PATCH
api/todos/{id}
```
PATCH /api/todos/{id}: Marca o Todo como Concluded, se pertencer ao usuário autenticado.
```

Endpoints Esperados:
```
POST /api/todos: Cria uma nova tarefa, associando-a ao usuário autenticado.
GET /api/todos: Retorna todas as tarefas do usuário autenticado.
GET /api/todos/{id}: Retorna uma única tarefa com o ID correspondente, se pertencer ao usuário autenticado.
PUT /api/todos/{id}: Atualiza uma tarefa existente com o ID correspondente, se pertencer ao usuário autenticado.
DELETE /api/todos/{id}: Exclui uma tarefa existente com o ID correspondente, se pertencer ao usuário autenticado.
```

## Swagger
/swagger/index.html
