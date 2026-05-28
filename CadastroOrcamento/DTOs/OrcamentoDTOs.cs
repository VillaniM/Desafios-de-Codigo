namespace OficinaAPI.DTOs
{
    /// <summary>
    /// DTO para criar um novo orçamento
    /// </summary>
    public class CriarOrcamentoRequest
    {
        public int ClienteId { get; set; }
        
        public int VeiculoId { get; set; }
        
        public List<ItemOrcamentoRequest> Itens { get; set; } = new List<ItemOrcamentoRequest>();
    }

    /// <summary>
    /// DTO para um item do orçamento na requisição
    /// </summary>
    public class ItemOrcamentoRequest
    {
        public string Descricao { get; set; } = string.Empty;
        
        public int Quantidade { get; set; }
        
        public decimal ValorUnitario { get; set; }
    }

    /// <summary>
    /// DTO para resposta de sucesso ao criar orçamento
    /// </summary>
    public class OrcamentoResponse
    {
        public int Id { get; set; }
        
        public int ClienteId { get; set; }
        
        public int VeiculoId { get; set; }
        
        public List<ItemOrcamentoResponse> Itens { get; set; } = new List<ItemOrcamentoResponse>();
        
        public decimal Total { get; set; }
        
        public DateTime DataCriacao { get; set; }
        
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para um item na resposta
    /// </summary>
    public class ItemOrcamentoResponse
    {
        public int Id { get; set; }
        
        public string Descricao { get; set; } = string.Empty;
        
        public int Quantidade { get; set; }
        
        public decimal ValorUnitario { get; set; }
        
        public decimal Subtotal { get; set; }
    }

    /// <summary>
    /// DTO para resposta de erro
    /// </summary>
    public class ErrorResponse
    {
        public string Mensagem { get; set; } = string.Empty;
        
        public List<string> Erros { get; set; } = new List<string>();
        
        public int? CodigoErro { get; set; }
        
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
