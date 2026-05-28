using OficinaAPI.DTOs;

namespace OficinaAPI.Validators
{
    /// <summary>
    /// Validador para dados de orçamento
    /// </summary>
    public class OrcamentoValidator
    {
        /// <summary>
        /// Valida os dados da requisição de criar orçamento
        /// </summary>
        /// <param name="request">Dados do orçamento</param>
        /// <returns>Lista de erros encontrados (vazia se válido)</returns>
        public static List<string> Validar(CriarOrcamentoRequest request)
        {
            var erros = new List<string>();

            // Validação de ClienteId
            if (request.ClienteId <= 0)
            {
                erros.Add("ClienteId é obrigatório e deve ser maior que zero.");
            }

            // Validação de VeiculoId
            if (request.VeiculoId <= 0)
            {
                erros.Add("VeiculoId é obrigatório e deve ser maior que zero.");
            }

            // Validação de Itens
            if (request.Itens == null || request.Itens.Count == 0)
            {
                erros.Add("Deve existir pelo menos 1 item no orçamento.");
            }
            else
            {
                // Validar cada item
                for (int i = 0; i < request.Itens.Count; i++)
                {
                    var erro = ValidarItem(request.Itens[i], i + 1);
                    erros.AddRange(erro);
                }
            }

            return erros;
        }

        /// <summary>
        /// Valida um item individual do orçamento
        /// </summary>
        private static List<string> ValidarItem(ItemOrcamentoRequest item, int indice)
        {
            var erros = new List<string>();

            // Descrição obrigatória
            if (string.IsNullOrWhiteSpace(item.Descricao))
            {
                erros.Add($"Item {indice}: A descrição é obrigatória.");
            }
            else if (item.Descricao.Length > 500)
            {
                erros.Add($"Item {indice}: A descrição não pode exceder 500 caracteres.");
            }

            // Quantidade obrigatória e maior que zero
            if (item.Quantidade <= 0)
            {
                erros.Add($"Item {indice}: A quantidade deve ser maior que zero.");
            }

            // Valor unitário obrigatório e maior que zero
            if (item.ValorUnitario <= 0)
            {
                erros.Add($"Item {indice}: O valor unitário deve ser maior que zero.");
            }

            return erros;
        }
    }
}
