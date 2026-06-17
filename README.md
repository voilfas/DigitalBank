# DigitalBank

DigitalBank — это работающая в реальных условиях микросервисная банковская система, созданная с использованием ASP.NET Core и предметно-ориентированного проектирования (DDD).

Проект демонстрирует современные методы разработки бэкенда, используемые в реальных финансовых системах, включая:

* Microservices Architecture
* Domain-Driven Design (DDD)
* Clean Architecture
* CQRS with MediatR
* Event-Driven Architecture
* RabbitMQ + MassTransit
* PostgreSQL
* Redis
* Docker & Docker Compose
* Unit and Integration Testing
* OpenTelemetry, Prometheus, Grafana

---

## Архитектура

```text
┌─────────────────────┐
│      API Client     │
└──────────┬──────────┘
           │
           ▼
┌─────────────────────┐
│   Account Service   │
│ Users, Accounts     │
│ Authentication      │
└──────────┬──────────┘
           │ Events
           ▼
┌─────────────────────┐
│ Transaction Service │
│ Transfers, Outbox   │
│ Saga Orchestration  │
└──────────┬──────────┘
           │
    ┌──────┴──────┐
    ▼             ▼
┌──────────┐ ┌──────────────┐
│NotifySvc │ │ ScoringSvc   │
│SMS/Email │ │ Credit Score │
└──────────┘ └──────────────┘
```

---

## Текущие Сервисы

### Account Service

Обязанности:

* User registration
* Authentication with JWT
* Account management
* Balance operations
* Card management

Domain Model:

* User
* Account
* Card

---

## Технологический Стек

### Backend

* ASP.NET Core 9
* C#
* MediatR
* FluentValidation

### Data

* PostgreSQL
* Redis
* Entity Framework Core
* Dapper

### Messaging

* RabbitMQ
* MassTransit

### Observability

* Serilog
* OpenTelemetry
* Prometheus
* Grafana
* Jaeger

### Testing

* xUnit
* FluentAssertions
* Testcontainers

### Infrastructure

* Docker
* Docker Compose
* GitHub Actions

---

## Project Structure

```text
DigitalBank
│
├── src
│   └── AccountService
│       ├── AccountService.API
│       ├── AccountService.Application
│       ├── AccountService.Domain
│       └── AccountService.Infrastructure
│
├── tests
│
├── docker
│
└── README.md
```

---

## Domain-Driven Design

Проект следует  DDD принципам:

* Entities
* Value Objects
* Aggregates
* Domain Events
* Bounded Contexts

Example Value Objects:

* Money
* AccountNumber
* Email
* CardNumber

---

## Планируемые Функции

* [x] Account Domain
* [ ] Domain Unit Tests
* [ ] Application Layer
* [ ] JWT Authentication
* [ ] PostgreSQL Integration
* [ ] Docker Compose
* [ ] GitHub Actions
* [ ] Transaction Service
* [ ] RabbitMQ Integration
* [ ] Outbox Pattern
* [ ] Saga Pattern
* [ ] Notification Service
* [ ] Scoring Service
* [ ] OpenTelemetry
* [ ] Prometheus + Grafana

---

## Локальный Запуск

```bash
docker compose up -d
```

Run API:

```bash
dotnet run
```

Run tests:

```bash
dotnet test
```

---

## Лицензия

Этот проект создан в образовательных целях и для демонстрации портфолио.
