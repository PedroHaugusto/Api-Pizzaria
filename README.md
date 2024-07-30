# Pizzaria API

Esta é uma API simples para gerenciar pizzas. A API permite criar, ler, atualizar e excluir (CRUD) pizzas.

## Endpoints
### Obter todas as pizzas

Get /PizzaController

#### Resposta

```json
[
  {
    "id": 1,
    "name": "Italiana",
    "conGluten": false
  },
  {
    "id": 2,
    "name": "Calabresa",
    "conGluten": true
  }
]
```

### Obter uma Pizza pelo ID

Get /PizzaController/{id}

#### Resposta

```json
{
  "id": 1,
  "name": "Italiana",
  "conGluten": false
}
```

### Criar uma nova Pizza

Post /PizzaController

#### Corpo da requisição

```json
{
  "name": "Margherita",
  "conGluten": true
}
```

#### Resposta

```json
{
  "id": 3,
  "name": "Margherita",
  "conGluten": true
}
```

### Atualizar uma Pizza

Put /PizzaController/{id}

#### Corpo da requisição

```json
{
  "id": 1,
  "name": "Margherita Updated",
  "conGluten": false
}
```

#### Resposta

```json
204 No Content
```

### Excluir uma Pizza

Delete /PizzaController/{id}

#### Resposta

```json
204 No Content
```











