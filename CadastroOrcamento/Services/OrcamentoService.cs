using OficinaAPI.DTOs;
using OficinaAPI.Models;
using OficinaAPI.Validators;

namespace OficinaAPI.Services
{
    /// <summary>
    /// Serviço que contém a lógica de negócio para orçamentos
    /// </summary>
    public interface IOrcamentoService
    {
        Task<(bool Sucesso, OrcamentoResponse? Dados, List<string> Erros)> 
            CriarOrcamentoAsync(CriarOrcamentoRequest request);
    }

    /// <summary>
    /// Implementação do serviço de orçamentos
    /// </summary>
    public class OrcamentoService : IOrcamentoService
    {
        private static int _proximoId = 1;
        private static List<Orcamento> _orcamentos = new List<Orcamento>();

        /// <summary>
        /// Cria um novo orçamento após validações
        /// </summary>
        public async Task<(bool Sucesso, OrcamentoResponse? Dados, List<string> Erros)> 
            CriarOrcamentoAsync(CriarOrcamentoRequest request)
        {
            // Validar dados
            var errosValidacao = OrcamentoValidator.Validar(request);
            if (errosValidacao.Count > 0)
            {
                return (false, null, errosValidacao);
            }

            // Simular operação assíncrona (em produção, seria uma chamada ao BD)
            await Task.Delay(50);

            try
            {
                // Criar orçamento
                var orcamento = new Orcamento
                {
                    Id = _proximoId++,
                    ClienteId = request.ClienteId,
                    VeiculoId = request.VeiculoId,
                    DataCriacao = DateTime.UtcNow
                };

                // Adicionar itens
                foreach (var itemRequest in request.Itens)
                {
                    var item = new ItemOrcamento
                    {
                        Id = _proximoId,
                        OrcamentoId = orcamento.Id,
                        Descricao = itemRequest.Descricao.Trim(),
                        Quantidade = itemRequest.Quantidade,
                        ValorUnitario = itemRequest.ValorUnitario
                    };

                    orcamento.Itens.Add(item);
                    _proximoId++;
                }

                // Calcular total
                orcamento.Total = CalcularTotal(orcamento);

                // Simular persistência (em produção seria salvo no BD)
                _orcamentos.Add(orcamento);

                // Mapear para response
                var response = MapearParaResponse(orcamento);

                return (true, response, new List<string>());
            }
            catch (Exception ex)
            {
                return (false, null, new List<string> { $"Erro ao criar orçamento: {ex.Message}" });
            }
        }

        /// <summary>
        /// Calcula o valor total do orçamento
        /// </summary>
        private decimal CalcularTotal(Orcamento orcamento)
        {
            return orcamento.Itens.Sum(item => item.Subtotal);
        }

        /// <summary>
        /// Mapeia um orçamento para o DTO de resposta
        /// </summary>
        private OrcamentoResponse MapearParaResponse(Orcamento orcamento)
        {
            return new OrcamentoResponse
            {
                Id = orcamento.Id,
                ClienteId = orcamento.ClienteId,
                VeiculoId = orcamento.VeiculoId,
                DataCriacao = orcamento.DataCriacao,
                Total = orcamento.Total,
                Status = orcamento.Status.ToString(),
                Itens = orcamento.Itens.Select(item => new ItemOrcamentoResponse
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    Subtotal = item.Subtotal
                }).ToList()
            };
        }
    }
}
