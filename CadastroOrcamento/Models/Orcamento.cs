namespace OficinaAPI.Models
{
    /// <summary>
    /// Representa um orçamento de serviços da oficina
    /// </summary>
    public class Orcamento
    {
        public int Id { get; set; }
        
        public int ClienteId { get; set; }
        
        public int VeiculoId { get; set; }
        
        public List<ItemOrcamento> Itens { get; set; } = new List<ItemOrcamento>();
        
        public decimal Total { get; set; }
        
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        
        public OrcamentoStatus Status { get; set; } = OrcamentoStatus.Pendente;
    }

    /// <summary>
    /// Representa um item dentro do orçamento
    /// </summary>
    public class ItemOrcamento
    {
        public int Id { get; set; }
        
        public int OrcamentoId { get; set; }
        
        public string Descricao { get; set; } = string.Empty;
        
        public int Quantidade { get; set; }
        
        public decimal ValorUnitario { get; set; }
        
        /// <summary>
        /// Subtotal do item (Quantidade * ValorUnitario)
        /// </summary>
        public decimal Subtotal => Quantidade * ValorUnitario;
    }

    /// <summary>
    /// Estados possíveis de um orçamento
    /// </summary>
    public enum OrcamentoStatus
    {
        Pendente = 0,
        Aprovado = 1,
        Rejeitado = 2,
        Cancelado = 3
    }
}
