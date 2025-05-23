# Especificação de APIs

> A especificação de APIs descreve os principais endpoints da API RESTful do produto
> de software, os métodos HTTP associados a cada endpoint, suas descrições, os formatos
> de respostas, os parâmetros de URL esperados e o mecanismo de autenticação e autorização 
> utilizado.

| **Endpoint**                  | **Método** | **Descrição**                           | **Parâmetros**                    | **Formato da Resposta** | **Autenticação e Autorização** |
|-------------------------------|------------|-----------------------------------------|-----------------------------------|-------------------------|--------------------------------|
| /api/Usuarios/authenticate | POST       | Loga um usuário   | - | JSON                    | -                   |
| /api/Usuarios                    | POST       | Criar usuário                           | -                                 | JSON                    | -                              |
| /api/Usuarios/{id}          | GET        | Obter dados de um usuário específico    | id (string)                  | JSON                    | JWT Token                      |
| /api/Usuarios/{id}/{status} | POST       | Desabilita/habilita usuário específico  | id (string) status (string)  | JSON                    | JWT Token                      |
| /api/Usuarios/{id}          | PUT        | Atualiza dados de um usuário específico | id (string)                  | JSON                    | JWT Token                      |
| /api/Usuarios/admin/{id}/{status} | POST       | Desabilita/habilita usuário específico  | id (string) status (string)  | JSON                    | JWT Token, Usuário Admin                      |
| /api/Anuncios                    | POST       | Cria um anúncio                         | -                                 | JSON                    | JWT Token                      |
| /api/Anuncios/{id}          | GET        | Obter dados de um anúncio específico    | id (string)                  | JSON                    | JWT Token                      |
| /api/Anuncios/{id}/{status} | POST       | Desabilita/habilita anúncio específico  | id (string)  status (string) | JSON                    | JWT Token                      |
| /api/Anuncios/{id}          | PUT        | Atualiza dados de um anúncio específico | id (string)                  | JSON                    | JWT Token                      |
| /api/Anuncios                    | GET        | Obter todos os anúncios                 | -                                 | JSON                    | JWT Token                      |
| /api/Anuncios/admin/{id}/{status} | POST       | Desabilita/habilita anúncio específico  | id (string) status (string)  | JSON                    | JWT Token, Usuário Admin                      |
| /api/Anuncios/like/{id} | POST | Favorita um anúncio específico | id (string) | JSON | JWT Token |
| /api/Anuncios/like/{id} | DELETE | Desfavorita um anúncio específico | id (string) | JSON | JWT Token |
| /api/Anuncios/like | GET | Obtém todos os favoritos do usuário | - | JSON | JWT Token |
| /api/Anuncios/report/{id} | POST | Denuncia um anúncio específico | id (string) | JSON | JWT Token |
| /api/Anuncios/report | GET | Obtém todos os anúncios denunciados | - | JSON | JWT Token, Usuário Admin |




[Retorna](../README.md)
