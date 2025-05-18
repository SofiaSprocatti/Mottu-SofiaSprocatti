RM99208 - Sofia Sprocatti Silva

# patioAPI

## Descrição
API RESTful desenvolvida em ASP.NET Core para gerenciamento de motos, filiais e pátios, com integração ao banco de dados Oracle via Entity Framework Core. Permite operações CRUD completas, controle de posição das motos em plano cartesiano, visualização da ocupação do pátio, alertas de proximidade e segue boas práticas de documentação com Swagger/OpenAPI.

## Estrutura do Banco de Dados
- **Filial:** tabela `filiais` (BranchId, Branch, Address, Cidade, Estado)
- **Patio:** tabela `patios` (CourtId, CourtLocal, BranchId, Branch, AreaTotal, MaxMotos, GridRows, GridCols)
- **Moto:** tabela `fiapmottu` no schema `fleet` (VehicleId, Plate, Model, Localization, Branch, Court)
- **VeiculoPatio:** tabela `veiculopatio` no schema `fleet` (VehicleId, CourtId, BranchId, Position, X, Y)

## Como criar o repositório
1. Crie um repositório no GitHub com o nome desejado (ex: `patioAPI`).
2. Clone o repositório para sua máquina:
   ```sh
   git clone https://github.com/seu-usuario/seu-repositorio.git
   ```
3. Copie todos os arquivos do projeto para a pasta clonada.
4. Adicione, commite e envie os arquivos para o GitHub:
   ```sh
   git add .
   git commit -m "Projeto inicial da API do pátio"
   git push origin main
   ```

## Como rodar a aplicação
1. Configure a string de conexão Oracle no arquivo `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "OracleConnection": "User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL;"
   }
   ```
2. Restaure os pacotes:
   ```sh
   dotnet restore
   ```
3. Gere e aplique as migrations (criação das tabelas no Oracle):
   ```sh
   dotnet tool install --global dotnet-ef # (se necessário)
   dotnet ef migrations add InitialCreate --project patioAPI/patioAPI.csproj
   dotnet ef database update --project patioAPI/patioAPI.csproj
   ```
4. Rode a aplicação:
   ```sh
   dotnet run --project patioAPI/patioAPI.csproj
   ```
5. Acesse a documentação Swagger em: `https://localhost:5001/swagger` (ou porta configurada)

## Rotas da API

### Filiais (filiais)
- **GET /api/filiais?page=1&pageSize=20**: Lista todas as filiais (paginado)
- **GET /api/filiais/{branchId}**: Busca filial por ID
- **POST /api/filiais**: Cadastra nova filial
- **PUT /api/filiais/{branchId}**: Atualiza filial
- **PATCH /api/filiais/{branchId}**: Atualiza parcialmente uma filial
- **DELETE /api/filiais/{branchId}**: Remove filial

#### Exemplo de body para Filial
```json
{
  "branchId": 1,
  "branch": "Filial Centro",
  "address": "Rua Exemplo, 123",
  "cidade": "São Paulo",
  "estado": "SP"
}
```

### Pátios (patios)
- **GET /api/patios?page=1&pageSize=20**: Lista todos os pátios (paginado)
- **GET /api/patios/{courtId}**: Busca pátio por ID
- **POST /api/patios**: Cadastra novo pátio
- **PUT /api/patios/{courtId}**: Atualiza pátio
- **PATCH /api/patios/{courtId}**: Atualiza parcialmente um pátio
- **DELETE /api/patios/{courtId}**: Remove pátio
- **PUT /api/patios/{courtId}/area-grid**: Atualiza área, grid e capacidade máxima do pátio
- **GET /api/patios/{courtId}/ocupacao**: Visualiza a ocupação do pátio (motos e posições X/Y)

#### Exemplo de body para Patio (com grid)
```json
{
  "courtId": 1,
  "courtLocal": "Setor A",
  "branchId": 1,
  "branch": "Filial Centro",
  "areaTotal": 200.0,
  "maxMotos": 80,
  "gridRows": 10,
  "gridCols": 8
}
```

### Motos (fleet.fiapmottu)
- **GET /api/motos?page=1&pageSize=20**: Lista todas as motos (paginado)
- **GET /api/motos/{vehicleId}**: Busca moto por ID
- **GET /api/motos/branch/{branch}**: Busca motos por filial
- **POST /api/motos**: Cadastra nova moto
- **PUT /api/motos/{vehicleId}**: Atualiza moto
- **PATCH /api/motos/{vehicleId}**: Atualiza parcialmente uma moto
- **DELETE /api/motos/{vehicleId}**: Remove moto

#### Exemplo de body para Moto
```json
{
  "vehicleId": 1,
  "plate": "ABC1234",
  "model": "Honda CG",
  "localization": "Rua Exemplo, 123",
  "branch": "Filial Centro",
  "court": "Setor A"
}
```

### Veículo no Pátio (veiculopatio)
- **GET /api/veiculopatios?page=1&pageSize=20**: Lista todas as posições de motos nos pátios (paginado)
- **GET /api/veiculopatios/{vehicleId}/{courtId}/{branchId}**: Busca posição específica
- **POST /api/veiculopatios**: Registra posição de uma moto no pátio
- **PUT /api/veiculopatios/{vehicleId}/{courtId}/{branchId}**: Atualiza posição
- **PATCH /api/veiculopatios/{vehicleId}/{courtId}/{branchId}**: Atualiza parcialmente a posição
- **DELETE /api/veiculopatios/{vehicleId}/{courtId}/{branchId}**: Remove posição
- **GET /api/veiculopatios/alerta-proximidade?courtId=1&x=2&y=3&distancia=1**: Lista motos próximas a uma posição X/Y no pátio (alerta de proximidade)

#### Exemplo de body para VeiculoPatio (posição cartesiana)
```json
{
  "vehicleId": 1,
  "courtId": 1,
  "branchId": 1,
  "position": "A1",
  "x": 2,
  "y": 3
}
```

## Visualização e Alertas
- Use **GET /api/patios/{courtId}/ocupacao** para obter a ocupação do grid do pátio (motos e suas posições X/Y).
- Use **GET /api/veiculopatios/alerta-proximidade?courtId=1&x=2&y=3&distancia=1** para listar motos próximas a uma posição X/Y (alerta de proximidade).

## Códigos de Retorno
- 200 OK: Sucesso nas consultas
- 201 Created: Recurso criado
- 204 NoContent: Atualização ou remoção sem retorno
- 400 BadRequest: Dados inválidos
- 404 NotFound: Recurso não encontrado

## Observações
- Certifique-se de que o Oracle está acessível e as credenciais estão corretas.
- O projeto utiliza EF Core com migrations para versionamento do banco.
- A documentação OpenAPI está disponível via Swagger.

---

Desenvolvido para a disciplina Advanced Business Development with .NET - FIAP.
