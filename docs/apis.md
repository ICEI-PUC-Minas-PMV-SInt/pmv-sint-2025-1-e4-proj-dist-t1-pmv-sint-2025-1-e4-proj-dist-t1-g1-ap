# Especificação de APIs

> A especificação de APIs descreve os principais endpoints da API RESTful do produto
> de software, os métodos HTTP associados a cada endpoint, suas descrições, os formatos
> de respostas, os parâmetros de URL esperados e o mecanismo de autenticação e autorização 
> utilizado.

| **Endpoint**                  | **Método** | **Descrição**                           | **Parâmetros**                    | **Formato da Resposta** | **Autenticação e Autorização** | **** | **** | **** | **** |
|-------------------------------|------------|-----------------------------------------|-----------------------------------|-------------------------|--------------------------------|------|------|------|------|
| /api/users                    | POST       | Criar usuário                           | -                                 | JSON                    | -                              |      |      |      |      |
| /api/users/{user_id}          | GET        | Obter dados de um usuário específico    | user_id (string)                  | JSON                    | JWT Token                      |      |      |      |      |
| /api/users/{user_id}/{status} | POST       | Desabilita/habilita usuário específico  | user_id (string) status (string)  | JSON                    | JWT Token                      |      |      |      |      |
| /api/users/{user_id}          | PUT        | Atualiza dados de um usuário específico | user_id (string)                  | JSON                    | JWT Token                      |      |      |      |      |
| /api/posts                    | POST       | Cria um anúncio                         | -                                 | JSON                    | JWT Token                      |      |      |      |      |
| /api/posts/{post_id}          | GET        | Obter dados de um anúncio específico    | post_id (string)                  | JSON                    | JWT Token                      |      |      |      |      |
| /api/posts/{post_id}/{status} | POST       | Desabilita/habilita anúncio específico  | post_id (string)  status (string) | JSON                    | JWT Token                      |      |      |      |      |
| /api/posts/{post_id}          | PUT        | Atualiza dados de um anúncio específico | post_id (string)                  | JSON                    | JWT Token                      |      |      |      |      |
| /api/posts                    | GET        | Obter todos os anúncios                 | -                                 | JSON                    | JWT Token                      |      |      |      |      |
              |

[Retorna](../README.md)
