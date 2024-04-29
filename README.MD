# NLW UNITE - RocketSeat 🚀

Projeto desenvolvido durante o curso do NLW focado na criação de uma API para cadastro de eventos e participantes, bem como a leitura desses dados no banco de dados.

## 📕 Guia de Instalação

- [Instalação do Visual Studio Community](https://visualstudio.microsoft.com/pt-br/)
- [Instalação do .Net](https://dotnet.microsoft.com/pt-br/download)

Caso deseje utilizar algo mais leve na hora do desenvolvimento:

- [Instalação do Visual Studio Code](https://code.visualstudio.com/)
- [Comandos dotnet para o terminal](https://learn.microsoft.com/pt-br/dotnet/core/tools/dotnet)

## Passin.Api

### Controllers
- **`AttendeesController`**: Controller responsável pelo registro de participantes no evento e pela obtenção das informações de todos os participantes cadastrados.
- **`CheckingController`**: Controller responsável pelo registro do Checking do participante em determinado evento.
- **`EventsController`**: Controller responsável pelo registro do evento e pela obtenção dos dados de um evento específico.




### Filters
- **`ExceptionFilter`**: Filtros de Exception do projeto, retornando conforme a regra de negócio e removendo funcionalidades de try catch do controller.

## Passin.Application

### UseCases
- **Attendees**
    - `GetAllAttendeesByEventIdUseCase`: Classe responsável pela obtenção dos dados de todos os usuários cadastrados em um evento, conforme o ID fornecido.
- **Checkins**
    - `DoAttendeeCheckingUseCase`: Classe responsável pelo Checking do usuário em um evento.
- **Events**
    - `GetByIdUseCase`: Classe responsável pela obtenção de um evento conforme o ID fornecido.
    - `RegisterEventUseCase`: Classe responsável pelo cadastro de um evento.
    - `RegisterAttendeeUseCase`: Classe responsável pelo cadastro de um participante em um evento.

## Passin.Communication

### Requests
- `RequestEventJson`: Classe que define o modelo de cadastro de eventos.
- `RequestRegisterEventJson`: Classe que define o modelo de cadastro de participantes em um evento.

### Responses
- `ResponseAllAttendeesJson`: Classe de modelo para o retorno da lista dos participantes do evento.
- `ResponseAttendeeJson`: Classe de modelo para o retorno de um participante do evento
- `ResponseErrorJson`: Classe de modelo para retornar os erros da API.
- `ResponseEventJson`: Classe de modelo para retornar dados sobre o evento.
- `ResponseRegisteredJson`: Classe de modelo que retorna o Id depois de um cadastro no banco de dados

## Passin.Exceptions
### ConflictException
- **Código de Erro:**  `Conflict`
- **Status HTTP:**  409 Conflict
- **Descrição:**  Essa exceção é lançada quando o participante tenta cadastrar no mesmo evento.

### ErrorOnValidationException: 
- **Código de Erro:**  `Bad Request`
- **Status HTTP:**  400 Bad Request
- **Descrição:**  Exceção é lançada quando se tenta cadastrar o evento com carácter vazio e máximo de participantes igual ou menor que 0 ou quando um participante está sendo cadastrado com os carácteres vazios.

### NotFoundException
- **Código de Erro:**  `Not Found`
- **Status HTTP:**  404 Not Found
- **Descrição:**  Essa exceção é lançada quando algo não é encontrado no banco de dados.

### PassInException: 
- **Código de Erro:**  `PassinException`
- **Descrição:**  Exceção generica da API.

## Passin.Infrastructure
- `PassinDbContext`: Classe de configuração pra fazer a interação entre os objetos(Attendee, Checkin e Event) e o banco de dados. Ela define como esses objetos são mapeados para as tabelas correspondentes no banco de dados e facilita operações como cadastro e leitura de dados.

### Entities
- `Attendee`: Classe de modelo dos participantes 
- `Checkin`: Classe de modelo do checkin
- `Event`: Classe de modelo dos eventos.


# Endpoints da API: 
## Participantes 🧑‍🤝‍🧑
### Registro de Participante em um Evento

- **Método:** `POST`
- **Rota:** `/api/attendees/{eventId}/register`
- **Parâmetros da Rota:** `eventId` (Guid) - ID do evento no qual o participante será registrado.
- **Corpo da Requisição:** `RequestRegisterEventJson`
- **Respostas:**
  - **201 Created:** Participante registrado com sucesso no evento.
    - **Corpo da Resposta:** `ResponseRegisteredJson`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

  - **400 Bad Request:** Erro de validação no corpo da requisição.
    - **Corpo da Resposta:** `ResponseErrorJson`
  - **404 Not Found:** Evento não encontrado.
    - **Corpo da Resposta:** `ResponseErrorJson`
  - **409 Conflict:** O participante já está registrado no evento.
    - **Corpo da Resposta:** `ResponseErrorJson`
```json
{
  "message": "string"
}
```
### Obtenção de Todos os Participantes de um Evento

- **Método:** `GET`
- **Rota:** `/api/attendees/{eventId}`
- **Parâmetros da Rota:** `eventId` (Guid) - ID do evento do qual deseja-se obter os participantes.
- **Respostas:**
  - **200 OK:** Lista de todos os participantes do evento.
    - **Corpo da Resposta:** `ResponseAllAttendeesJson`
```json
{
  "attendees": [
    {
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      "name": "string",
      "email": "string",
      "createdAt": "2024-04-29T21:20:21.267Z",
      "checkedInAt": "2024-04-29T21:20:21.267Z"
    }
  ]
}
```
  - **404 Not Found:** Evento não encontrado.
    - **Corpo da Resposta:** `ResponseErrorJson`
```json
{
  "message": "string"
}
```

## Checkin ✔️

### Registro de Check-in de Participante

- **Método:** `POST`
- **Rota:** `/api/checkin/{attendeeId}`
- **Parâmetros da Rota:** `attendeeId` (Guid) - ID do participante que está fazendo o check-in.
- **Respostas:**
  - **201 Created:**
    - **Corpo da Resposta:** `ResponseRegisteredJson`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
  - **404 Not Found:** 
    - **Corpo da Resposta:** `ResponseErrorJson`
  - **409 Conflict:**
    - **Corpo da Resposta:** `ResponseErrorJson`
```json
{
  "message": "string"
}
```

## Eventos 📅

### Registro de Evento

- **Método:** `POST`
- **Rota:** `/api/events`
- **Corpo da Requisição:** `RequestEventJson`
- **Respostas:**
  - **201 Created:**
    - **Corpo da Resposta:** `ResponseRegisteredJson`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
  - **400 Bad Request:** Erro de validação no corpo da requisição.
    - **Corpo da Resposta:** `ResponseErrorJson`
```json
{
  "message": "string"
}
``` 


### Obtenção de Evento por ID

- **Método:** `GET`
- **Rota:** `/api/events/{id}`
- **Parâmetros da Rota:** `id` (Guid) - ID do evento desejado.
- **Respostas:**
  - **200 OK:** Dados do evento encontrados com sucesso.
    - **Corpo da Resposta:** `ResponseEventJson`
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "title": "string",
  "details": "string",
  "maximumAttendees": 0,
  "attendeesAmount": 0
}
```
  - **404 Not Found:** Evento não encontrado.
    - **Corpo da Resposta:** `ResponseErrorJson`
```json
{
  "message": "string"
}
```