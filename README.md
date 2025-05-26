
# Sistema de Pedidos com Padrão Saga usando RabbitMQ

Este projeto é um exemplo de uma arquitetura de microsserviços orientada a eventos, utilizando .NET 8, RabbitMQ para mensageria, e o padrão Saga para gerenciamento de transações distribuídas.

## Estrutura do Projeto

A solução é composta por três serviços principais organizados da seguinte forma:

```
src/
├── MessageBus/           # Camada de infraestrutura para RabbitMQ (publisher e subscriber)
├── OrderService/         # API responsável pela criação e gerenciamento de pedidos (com CQRS)
├── PaymentService/
│   └── Worker/           # Worker responsável por processar pagamentos
```

Cada serviço tem sua própria `Solution (.sln)` e `Dockerfile`. Todos os containers são orquestrados com `docker-compose`.

---

## Tecnologias Utilizadas

- .NET 8
- RabbitMQ
- CQRS (na OrderService)
- MediatR
- Docker
- Docker Compose

---

## Fluxo de Eventos

### 1. Pedido Criado
- O `OrderService.API` cria um pedido e publica o evento `CreatedOrderEvent`.

### 2. Pagamento Processado
- O `PaymentService.Worker` escuta o `CreatedOrderEvent` e simula o processamento do pagamento.
- Ele publica de forma randômica:
  - `ApprovedPaymentEvent`
  - `PaymentRejectedEvent`

### 3. Pedido Aprovado ou Rejeitado
- O `OrderService` escuta os eventos `ApprovedPaymentEvent` e `PaymentRejectedEvent`.
  - Ao receber `ApprovedPaymentEvent`, marca o pedido como pago.
  - Ao receber `PaymentRejectedEvent`, utiliza o `Mediator` para disparar o comando `CancelOrderCommand`, que executa a lógica de cancelamento do pedido.

---

## Execução do Projeto

### Pré-requisitos

- Docker e Docker Compose instalados
- .NET 8 SDK instalado (para rodar localmente sem Docker, opcional)
- RabbitMQ Management UI disponível em http://localhost:15672 (user: `guest`, password: `guest`)

### Passo a Passo

1. Clone o repositório:
    ```bash
    git clone <seu-repo>.git
    cd <seu-repo>
    ```

2. Suba a infraestrutura:
    ```bash
    docker-compose up --build
    ```

3. Acesse os logs para visualizar o fluxo de eventos:
    - `OrderService`: responsável por criar pedidos e escutar os eventos de pagamento.
    - `Worker (PaymentService)`: processa os pagamentos de forma randômica.
    - Você verá mensagens como:
      ```
      Pedido 123 criado
      Pagamento aprovado para pedido 123
      Pedido 123 pago com sucesso
      ```

4. Testes manuais podem ser realizados criando pedidos via API do OrderService (porta 5000 por padrão) e acessando o swagger.

---

## Observações

- O Exchange do RabbitMQ está configurado como `direct` para garantir que cada fila só receba o evento para o qual foi configurada.
- O padrão Saga garante a consistência eventual entre os serviços.
- Filas utilizadas:
  - `queue_createdorderevent`
  - `queue_approvedpaymentevent`
  - `queue_paymentrejectedevent`

---

## Autor

Desenvolvido por Deividy Henrique Alves Pinheiro.
