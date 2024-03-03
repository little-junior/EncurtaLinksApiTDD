# Encurta Links API

API em ASP.NET que visa implementar um encurtador de links

## Requisitos do Projeto

- Receber uma URL qualquer e retornar um objeto que contém:
    - O Id da requisição;
    - A URL curta que foi gerada;
    - O tempo em segundos pelo qual a url curta será válida.
- Retornar a URL original ao consultar uma url curta válida.
- Retornar um erro caso não exista uma URL original válida para a URL curta consultada.
- A última parte da URL encurtada deve ter no máximo 7 caracteres.

## Endpoints

- `GET /api/encurta?link` : Retorna o link original vinculado ao encurtado e sua validade
- `POST /api/encurta` : Cria um novo link encurtado e retorna suas informações 
