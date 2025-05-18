using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Any;
using System.Text.Json;

public class SwaggerDefaultResponseExamplesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Exemplo para Moto (GET /api/motos/{vehicleId})
        var motoExample = "{\n  \"vehicleId\": 1,\n  \"plate\": \"ABC1234\",\n  \"model\": \"Honda CG\",\n  \"localization\": \"Rua Exemplo, 123\",\n  \"branch\": \"Filial Centro\",\n  \"court\": \"Setor A\"\n}";
        // Exemplo para lista de Moto (GET /api/motos)
        var motoListExample = "[\n  {\n    \"vehicleId\": 1,\n    \"plate\": \"ABC1234\",\n    \"model\": \"Honda CG\",\n    \"localization\": \"Rua Exemplo, 123\",\n    \"branch\": \"Filial Centro\",\n    \"court\": \"Setor A\"\n  }\n]";
        // Exemplo para Patio (GET /api/patios/{courtId})
        var patioExample = "{\n  \"courtId\": 1,\n  \"courtLocal\": \"Setor A\",\n  \"branchId\": 1,\n  \"branch\": \"Filial Centro\",\n  \"areaTotal\": 200.0,\n  \"maxMotos\": 80,\n  \"gridRows\": 10,\n  \"gridCols\": 8\n}";
        // Exemplo para lista de Patio (GET /api/patios)
        var patioListExample = "[\n  {\n    \"courtId\": 1,\n    \"courtLocal\": \"Setor A\",\n    \"branchId\": 1,\n    \"branch\": \"Filial Centro\",\n    \"areaTotal\": 200.0,\n    \"maxMotos\": 80,\n    \"gridRows\": 10,\n    \"gridCols\": 8\n  }\n]";
        // Exemplo para Filial (GET /api/filiais/{branchId})
        var filialExample = "{\n  \"branchId\": 1,\n  \"branch\": \"Filial Centro\",\n  \"address\": \"Rua Exemplo, 123\",\n  \"cidade\": \"São Paulo\",\n  \"estado\": \"SP\"\n}";
        // Exemplo para lista de Filial (GET /api/filiais)
        var filialListExample = "[\n  {\n    \"branchId\": 1,\n    \"branch\": \"Filial Centro\",\n    \"address\": \"Rua Exemplo, 123\",\n    \"cidade\": \"São Paulo\",\n    \"estado\": \"SP\"\n  }\n]";
        // Exemplo para VeiculoPatio (GET /api/veiculopatios/{vehicleId}/{courtId}/{branchId})
        var veiculoPatioExample = "{\n  \"vehicleId\": 1,\n  \"courtId\": 1,\n  \"branchId\": 1,\n  \"position\": \"A1\",\n  \"x\": 2,\n  \"y\": 3\n}";
        // Exemplo para lista de VeiculoPatio (GET /api/veiculopatios)
        var veiculoPatioListExample = "[\n  {\n    \"vehicleId\": 1,\n    \"courtId\": 1,\n    \"branchId\": 1,\n    \"position\": \"A1\",\n    \"x\": 2,\n    \"y\": 3\n  }\n]";
        // Exemplo para ocupacao do patio (GET /api/patios/{courtId}/ocupacao)
        var ocupacaoExample = "{\n  \"patio\": {\n    \"courtId\": 1,\n    \"courtLocal\": \"Setor A\",\n    \"areaTotal\": 200.0,\n    \"maxMotos\": 80,\n    \"gridRows\": 10,\n    \"gridCols\": 8\n  },\n  \"ocupacao\": [\n    {\n      \"vehicleId\": 1,\n      \"x\": 2,\n      \"y\": 3,\n      \"position\": \"A1\"\n    }\n  ]\n}";
        // Exemplo para alerta de proximidade (GET /api/veiculopatios/alerta-proximidade)
        var alertaProximidadeExample = "[\n  {\n    \"vehicleId\": 1,\n    \"courtId\": 1,\n    \"branchId\": 1,\n    \"position\": \"A1\",\n    \"x\": 2,\n    \"y\": 3,\n    \"distancia\": 1,\n    \"veiculosProximos\": [\n      {\n        \"vehicleId\": 2,\n        \"plate\": \"DEF5678\",\n        \"model\": \"Yamaha YBR\",\n        \"x\": 2,\n        \"y\": 4,\n        \"position\": \"A2\"\n      },\n      {\n        \"vehicleId\": 3,\n        \"plate\": \"GHI9012\",\n        \"model\": \"Honda Biz\",\n        \"x\": 3,\n        \"y\": 3,\n        \"position\": \"B1\"\n      }\n    ]\n  }\n]";
        // Exemplo para erro
        var errorExample = "{\n  \"error\": \"Mensagem de erro detalhada.\"\n}";

        var path = context.ApiDescription.RelativePath?.ToLower() ?? "";
        if (operation.Responses.ContainsKey("200"))
        {
            var mediaType = operation.Responses["200"].Content.ContainsKey("application/json")
                ? operation.Responses["200"].Content["application/json"]
                : null;
            if (mediaType != null)
            {
                if (path == "api/motos") {
                    var example = ParseJsonExample(motoListExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploListaMoto", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/motos/{vehicleid}") {
                    var example = ParseJsonExample(motoExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploMoto", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/motos/branch/{branch}") {
                    var example = ParseJsonExample(motoListExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploListaMoto", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/patios") {
                    var example = ParseJsonExample(patioListExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploListaPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/patios/{courtid}") {
                    var example = ParseJsonExample(patioExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/patios/{courtid}/ocupacao") {
                    var example = ParseJsonExample(ocupacaoExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploOcupacao", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/filiais") {
                    var example = ParseJsonExample(filialListExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploListaFilial", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/filiais/{branchid}") {
                    var example = ParseJsonExample(filialExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploFilial", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/veiculopatios") {
                    var example = ParseJsonExample(veiculoPatioListExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploListaVeiculoPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/veiculopatios/{vehicleid}/{courtid}/{branchid}") {
                    var example = ParseJsonExample(veiculoPatioExample);
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploVeiculoPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
                else if (path == "api/veiculopatios/alerta-proximidade") {
                    var example = ParseJsonExample(alertaProximidadeExample);
                    // Força o tipo do schema para array de objetos, compatível com o exemplo
                    mediaType.Schema = new Microsoft.OpenApi.Models.OpenApiSchema {
                        Type = "array",
                        Items = new Microsoft.OpenApi.Models.OpenApiSchema {
                            Type = "object",
                            Properties = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiSchema> {
                                { "vehicleId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "courtId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "branchId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "position", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } },
                                { "x", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "y", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "distancia", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                { "veiculosProximos", new Microsoft.OpenApi.Models.OpenApiSchema {
                                    Type = "array",
                                    Items = new Microsoft.OpenApi.Models.OpenApiSchema {
                                        Type = "object",
                                        Properties = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiSchema> {
                                            { "vehicleId", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                            { "plate", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } },
                                            { "model", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } },
                                            { "x", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                            { "y", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "integer", Format = "int32" } },
                                            { "position", new Microsoft.OpenApi.Models.OpenApiSchema { Type = "string" } }
                                        }
                                    }
                                } }
                            }
                        }
                    };
                    mediaType.Example = example;
                    mediaType.Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                        { "ExemploAlertaProximidade", new Microsoft.OpenApi.Models.OpenApiExample { Value = example } }
                    };
                }
            }
        }
        if (operation.Responses.ContainsKey("201"))
        {
            if (path == "api/motos")
                operation.Responses["201"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                    { "ExemploMoto", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(motoExample) } }
                };
            else if (path == "api/patios")
                operation.Responses["201"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                    { "ExemploPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(patioExample) } }
                };
            else if (path == "api/filiais")
                operation.Responses["201"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                    { "ExemploFilial", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(filialExample) } }
                };
            else if (path == "api/veiculopatios")
                operation.Responses["201"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                    { "ExemploVeiculoPatio", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(veiculoPatioExample) } }
                };
        }
        if (operation.Responses.ContainsKey("204"))
        {
            operation.Responses["204"].Description = "Sem conteúdo (No Content).";
        }
        if (operation.Responses.ContainsKey("400"))
        {
            operation.Responses["400"].Description = "Requisição inválida (Bad Request).";
            operation.Responses["400"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                { "ExemploErro", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(errorExample) } }
            };
        }
        if (operation.Responses.ContainsKey("404"))
        {
            operation.Responses["404"].Description = "Recurso não encontrado (Not Found).";
            operation.Responses["404"].Content["application/json"].Examples = new Dictionary<string, Microsoft.OpenApi.Models.OpenApiExample> {
                { "ExemploErro", new Microsoft.OpenApi.Models.OpenApiExample { Value = ParseJsonExample(errorExample) } }
            };
        }
    }

    private IOpenApiAny ParseJsonExample(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            return ConvertJsonElement(doc.RootElement);
        }
        catch
        {
            return new OpenApiString(json);
        }
    }

    private IOpenApiAny ConvertJsonElement(JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var obj = new OpenApiObject();
                foreach (var prop in element.EnumerateObject())
                    obj.Add(prop.Name, ConvertJsonElement(prop.Value));
                return obj;
            case JsonValueKind.Array:
                var arr = new OpenApiArray();
                foreach (var item in element.EnumerateArray())
                    arr.Add(ConvertJsonElement(item));
                return arr;
            case JsonValueKind.String:
                return new OpenApiString(element.GetString());
            case JsonValueKind.Number:
                if (element.TryGetInt32(out int i))
                    return new OpenApiInteger(i);
                if (element.TryGetDouble(out double d))
                    return new OpenApiDouble(d);
                return new OpenApiString(element.GetRawText());
            case JsonValueKind.True:
            case JsonValueKind.False:
                return new OpenApiBoolean(element.GetBoolean());
            case JsonValueKind.Null:
            default:
                return new OpenApiNull();
        }
    }
}
