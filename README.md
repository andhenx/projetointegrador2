# Ferragem Tools — Projeto Integrador II

Sistema de controle de estoque para a Ferragem Tools. Desenvolvido para a disciplina de Projeto Integrador II — semestre 2026/1.

**Autores**

| Nome | E-mail institucional |
|------|----------------------|
| Anderson de Oliveira | anderson.202414773@unilasalle.edu.br |
| Andrew Henrique de Lara Girarde | andrew.202220811@unilasalle.edu.br |
| André Luiz dos Santos | andre.santos0230@unilasalle.edu.br |

---

## O que o sistema faz

- Cadastro de produtos (adicionar, editar e remover)
- Alertas automáticos de estoque baixo (≤ 5 unidades) e sem estoque
- Registro de movimentações de entrada e saída
- Dashboard com visão geral do estoque e valor total em estoque

---

## Como rodar

Você precisa ter o [Docker Desktop](https://www.docker.com/) instalado e aberto.

Na pasta raiz do projeto, rode:

```bash
docker-compose up -d --build
```

Depois é só abrir no navegador:

| O quê       | Endereço               |
|-------------|------------------------|
| Sistema     | http://localhost       |
| API         | http://localhost:8080  |
| Banco       | localhost:5433         |

Para parar tudo:

```bash
docker-compose down
```

> Se quiser resetar o banco e recarregar os dados iniciais, use `docker-compose down -v` (apaga o volume do PostgreSQL).

---

## Tecnologias usadas

**Backend**
- ASP.NET Core 8 (C#)
- Entity Framework Core + PostgreSQL (Npgsql)
- AutoMapper
- CsvHelper (para importar os dados iniciais do CSV)

**Frontend**
- HTML, CSS e JavaScript puro
- Sem frameworks — só jQuery e ícones do Tabler Icons

**Infraestrutura**
- Docker + Docker Compose (3 containers: frontend, backend, banco)

---

## Estrutura do projeto

```
projeto-integrador-2/
├── frontend/               # Páginas HTML servidas pelo Nginx
│   ├── index.html          # Dashboard
│   ├── produtos.html       # Estoque (cadastro de produtos)
│   └── movimentacoes.html  # Movimentações de entrada e saída
│
├── backend/
│   └── ProjetoIntegrador2/
│       └── ProjetoIntegrador2.API/
│           ├── Controllers/
│           │   ├── ProdutoController.cs    # Endpoints dos produtos
│           │   ├── MovimentoController.cs  # Endpoints das movimentações
│           │   └── HealthController.cs     # Verifica se a API está de pé
│           ├── Entities/
│           │   ├── Produto.cs              # Tabela de produtos
│           │   └── Movimento.cs            # Tabela de movimentações
│           ├── DTOs/                       # Objetos usados para entrada e saída da API
│           ├── Mappings/                   # Configuração do AutoMapper
│           ├── Data/
│           │   ├── AppDbContext.cs         # Configuração do banco e mapeamento das tabelas
│           │   └── Seeds/                  # CSVs com dados iniciais
│           └── Program.cs                  # Ponto de entrada da API
│
└── docker-compose.yml      # Define os 3 containers do projeto
```

---

## Endpoints da API

### Produtos

| Método | Rota                  | O que faz                    |
|--------|-----------------------|------------------------------|
| GET    | /api/produto          | Lista todos os produtos       |
| POST   | /api/produto          | Cadastra um novo produto      |
| PUT    | /api/produto/{id}     | Edita um produto existente    |
| DELETE | /api/produto/{id}     | Remove um produto             |

**Exemplo de corpo para POST/PUT:**
```json
{
  "descricao": "PREGO 18X27",
  "codigo": "FRG-001",
  "unid": "CX",
  "preco": 12.50,
  "custo": 8.00,
  "estoque": 10,
  "ativo": true
}
```

### Movimentações

| Método | Rota             | O que faz                          |
|--------|------------------|------------------------------------|
| GET    | /api/movimento   | Lista todas as movimentações        |
| POST   | /api/movimento   | Registra uma entrada ou saída       |

**Exemplo de corpo para POST:**
```json
{
  "produtoId": "uuid-do-produto",
  "tipo": "Entrada",
  "qtde": 5,
  "precoUnit": 12.50
}
```

> O tipo pode ser `"Entrada"` ou `"Saida"`. Em saídas, a API verifica se tem estoque suficiente antes de registrar.

### Health check

| Método | Rota        | O que faz                    |
|--------|-------------|------------------------------|
| GET    | /api/health | Verifica se a API está rodando |

---

## Variáveis de ambiente

As configurações padrão já funcionam sem precisar mudar nada. Se quiser customizar, crie um arquivo `.env` na raiz:

```env
POSTGRES_USER=postgres
POSTGRES_PASSWORD=postgres
POSTGRES_DB=projetointegrador
```
