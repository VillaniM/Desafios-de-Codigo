using Microsoft.AspNetCore.Mvc;
using OficinaAPI.DTOs;
using OficinaAPI.Services;

namespace OficinaAPI.Controllers
{
    /// <summary>
    /// Controller para gerenciar orçamentos
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrcamentosController : ControllerBase
    {
        private readonly IOrcamentoService _orcamentoService;
        private readonly ILogger<OrcamentosController> _logger;

        public OrcamentosController(
            IOrcamentoService orcamentoService,
            ILogger<OrcamentosController> logger)
        {
            _orcamentoService = orcamentoService;
            _logger = logger;
        }

        /// <summary>
        /// Cria um novo orçamento
        /// </summary>
        /// <param name="request">Dados do orçamento</param>
        /// <returns>Orçamento criado com sucesso</returns>
        /// <response code="201">Orçamento criado com sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrcamentoResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarOrcamento(
            [FromBody] CriarOrcamentoRequest request)
        {
            try
            {
                _logger.LogInformation(
                    "Iniciando criação de orçamento para cliente {ClienteId}, veículo {VeiculoId}",
                    request?.ClienteId,
                    request?.VeiculoId);

                // Validar se request é nulo
                if (request == null)
                {
                    var erroNull = new ErrorResponse
                    {
                        Mensagem = "Dados do orçamento são obrigatórios",
                        Erros = new List<string> { "O corpo da requisição não pode estar vazio" },
                        CodigoErro = 400
                    };

                    _logger.LogWarning("Requisição nula recebida");
                    return BadRequest(erroNull);
                }

                // Chamar serviço
                var (sucesso, dados, erros) = 
                    await _orcamentoService.CriarOrcamentoAsync(request);

                if (!sucesso)
                {
                    var errorResponse = new ErrorResponse
                    {
                        Mensagem = "Falha ao criar orçamento. Verifique os dados informados.",
                        Erros = erros,
                        CodigoErro = 400
                    };

                    _logger.LogWarning(
                        "Erro ao criar orçamento: {@Erros}",
                        erros);

                    return BadRequest(errorResponse);
                }

                _logger.LogInformation(
                    "Orçamento criado com sucesso. ID: {OrcamentoId}, Total: {Total}",
                    dados?.Id,
                    dados?.Total);

                // Retornar 201 Created com location header
                return CreatedAtAction(
                    nameof(CriarOrcamento),
                    new { id = dados!.Id },
                    dados);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Erro interno ao criar orçamento");

                var errorResponse = new ErrorResponse
                {
                    Mensagem = "Erro interno do servidor",
                    Erros = new List<string> { "Ocorreu um erro ao processar sua requisição. Tente novamente mais tarde." },
                    CodigoErro = 500
                };

                return StatusCode(500, errorResponse);
            }
        }
    }
}
