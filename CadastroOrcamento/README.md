# API de Orçamentos - Oficina Mecânica

## Visão Geral

Esta API .NET fornece um endpoint para cadastro de orçamentos em um sistema de oficina mecânica. A API valida todos os dados de entrada, calcula automaticamente o total e retorna respostas estruturadas com mensagens de erro claras.

## Arquitetura

```
Controllers/
├── OrcamentosController        # Endpoints HTTP
├── Services/
│   └── OrcamentoService        # Lógica de negócio
├── Validators/
│   └── OrcamentoValidator      # Validações
├── Models/
│   └── Orcamento               # Entidades de domínio
└── DTOs/
    └── OrcamentoDTOs           # Dados de entrada/saída
```

## Endpoint: POST /api/orcamentos

### Descrição
Cria um novo orçamento para uma oficina mecânica.

### Headers Requeridos
```
Content-Type: application/json
```

### Corpo da Requisição
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00
    },
    {
      "descricao": "Filtro de óleo",
      "quantidade": 1,
      "valorUnitario": 45.00
    }
  ]
}
```

### Regras de Validação

#### ClienteId
- ✅ Obrigatório
- ✅ Deve ser maior que zero

#### VeiculoId
- ✅ Obrigatório
- ✅ Deve ser maior que zero

#### Itens
- ✅ Deve ter pelo menos 1 item
- ✅ Máximo de itens: sem limite (escalável)

#### Cada Item
- **Descrição**: 
  - Obrigatória
  - Máximo 500 caracteres
- **Quantidade**:
  - Obrigatória
  - Deve ser maior que zero
  - Tipo: inteiro
- **Valor Unitário**:
  - Obrigatório
  - Deve ser maior que zero
  - Tipo: decimal (até 2 casas decimais)

### Respostas

#### ✅ Sucesso (201 Created)
```json
{
  "id": 1,
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {
      "id": 1,
      "descricao": "Troca de óleo",
      "quantidade": 1,
      "valorUnitario": 120.00,
      "subtotal": 120.00
    },
    {
      "id": 2,
      "descricao": "Filtro de óleo",
      "quantidade": 1,
      "valorUnitario": 45.00,
      "subtotal": 45.00
    }
  ],
  "total": 165.00,
  "dataCriacao": "2024-01-15T10:30:00Z",
  "status": "Pendente"
}
```

#### ❌ Erro de Validação (400 Bad Request)
```json
{
  "mensagem": "Falha ao criar orçamento. Verifique os dados informados.",
  "erros": [
    "ClienteId é obrigatório e deve ser maior que zero.",
    "Item 1: A quantidade deve ser maior que zero."
  ],
  "codigoErro": 400,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

#### ❌ Erro Interno (500 Internal Server Error)
```json
{
  "mensagem": "Erro interno do servidor",
  "erros": [
    "Ocorreu um erro ao processar sua requisição. Tente novamente mais tarde."
  ],
  "codigoErro": 500,
  "timestamp": "2024-01-15T10:30:00Z"
}
```

## Exemplos de Requisições

### cURL
```bash
curl -X POST http://localhost:5000/api/orcamentos \
  -H "Content-Type: application/json" \
  -d '{
    "clienteId": 10,
    "veiculoId": 25,
    "itens": [
      {
        "descricao": "Troca de óleo",
        "quantidade": 1,
        "valorUnitario": 120.00
      },
      {
        "descricao": "Filtro de óleo",
        "quantidade": 1,
        "valorUnitario": 45.00
      }
    ]
  }'
```

## Casos de Teste

### ✅ Caso 1: Dados Válidos
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {"descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00},
    {"descricao": "Filtro de óleo", "quantidade": 1, "valorUnitario": 45.00}
  ]
}
```
**Resultado**: 201 Created com orçamento ID 1, Total: 165.00

### ❌ Caso 2: ClienteId Faltando
```json
{
  "clienteId": 0,
  "veiculoId": 25,
  "itens": [{"descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": 120.00}]
}
```
**Resultado**: 400 Bad Request - "ClienteId é obrigatório"

### ❌ Caso 3: Sem Itens
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": []
}
```
**Resultado**: 400 Bad Request - "Deve existir pelo menos 1 item"

### ❌ Caso 4: Quantidade Inválida
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {"descricao": "Troca de óleo", "quantidade": 0, "valorUnitario": 120.00}
  ]
}
```
**Resultado**: 400 Bad Request - "A quantidade deve ser maior que zero"

### ❌ Caso 5: Valor Negativo
```json
{
  "clienteId": 10,
  "veiculoId": 25,
  "itens": [
    {"descricao": "Troca de óleo", "quantidade": 1, "valorUnitario": -120.00}
  ]
}
```
**Resultado**: 400 Bad Request - "O valor unitário deve ser maior que zero"

### ✅ Caso 6: Múltiplos Itens com Cálculo de Total
```json
{
  "clienteId": 5,
  "veiculoId": 15,
  "itens": [
    {"descricao": "Pintura", "quantidade": 2, "valorUnitario": 500.00},
    {"descricao": "Polimento", "quantidade": 1, "valorUnitario": 300.00},
    {"descricao": "Envernizamento", "quantidade": 3, "valorUnitario": 150.00}
  ]
}
```
**Resultado**: 201 Created
- Item 1: 2 × 500 = 1000.00
- Item 2: 1 × 300 = 300.00
- Item 3: 3 × 150 = 450.00
- **Total: 1750.00**

## Como Executar

### Pré-requisitos
- .NET 10 SDK
- Visual Studio 2022 (ou VS Code)

### Passos

1. **Clonar/Extrair projeto**
```bash
cd CadastroOrcamento
```

2. **Restaurar dependências**
```bash
dotnet restore
```

3. **Compilar**
```bash
dotnet build
```

4. **Executar**
```bash
dotnet run
```

A API estará disponível em: `https://localhost:5001`

### Acessar Swagger UI
```
https://localhost:5001
```

## Tratamento de Erros

A API segue padrão REST com códigos HTTP apropriados:

| Código | Significado | Causa |
|--------|-------------|-------|
| **201** | Created | Orçamento criado com sucesso |
| **400** | Bad Request | Dados de entrada inválidos |
| **500** | Internal Server Error | Erro no servidor |

Cada resposta de erro contém:
- `mensagem`: Descrição geral do erro
- `erros`: Array com detalhes específicos de cada validação
- `codigoErro`: Código HTTP
- `timestamp`: Data/hora do erro

## Calculando o Total

O total é calculado automaticamente:

```
Total = Σ (item.quantidade × item.valorUnitario)
```

**Exemplo:**
- Troca de óleo: 1 × 120.00 = 120.00
- Filtro de óleo: 1 × 45.00 = 45.00
- **Total: 165.00**

## Melhorias Futuras

- [ ] Integração com banco de dados (Entity Framework)
- [ ] Autenticação e autorização
- [ ] Validação de ClienteId e VeiculoId contra banco
- [ ] Buscar orçamento por ID (GET)
- [ ] Listar todos os orçamentos (GET)
- [ ] Atualizar orçamento (PUT/PATCH)
- [ ] Deletar orçamento (DELETE)
- [ ] Aprovar/Rejeitar orçamento
- [ ] Gerar PDF do orçamento
- [ ] Enviar orçamento por email
