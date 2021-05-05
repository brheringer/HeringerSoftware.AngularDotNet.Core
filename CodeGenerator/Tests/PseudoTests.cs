using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetalSoft.Core.CodeGenerator;
using MetalSoft.Core.CodeGenerator.Model;
using System.Collections.Generic;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class PseudoTests
	{
		[TestMethod]
		//[Ignore]
		public void PseudoTestCombo()
		{
			string inputs = @"
#Riscos Disciplina Disciplina Disciplinas Disciplinas
	string Codigo C�digo RE
	string Nome Nome RX
	string Definicao Definicao RX
	PeriodoRisco Periodo Per�odo XX
#Riscos EstruturaOrganizacional Estrutura_Organizacional EstruturasOrganizacionais Estruturas_Organizacionais
	EstruturaOrganizacional EstruturaSuperior Estrutura_Superior RX
	string Codigo C�digo RE
	string Descricao Descri��o RX
	string ResponsavelEstrutura Respons�vel_pela_Estrutura XX
#Riscos COMPOSICAO-ValorCriterio Valor ValoresCriterios Valores
	CONTAINER-Criterio CriterioValorizado Crit�rio RX
	string Descricao Descri��o RX
	decimal Valor Valor RX
#Riscos Criterio Crit�rio Criterios Crit�rios
	string Codigo C�digo RE
	string Nome Nome RX
	PeriodoRisco Periodo Per�odo RX
	int Peso Peso RX
	ENUM-TipoCriterio Tipo Tipo RX
	IList<ValorCriterio.CriterioValorizado> Valores Valores XX
";

			AppBuilder builder = new AppBuilder();
			builder.OutputBaseDir = @"c:\temp\_output-of-metalsoft-core-code-generator";
			//builder.TemplatesDir;
			List<Entity> entities = new List<Entity>();
			foreach (string input in inputs.Split('#'))
			{
				if (input.Trim().Length > 0)
				{
					var e = Parse(input, entities);
					builder.AddEntity(e);
					entities.Add(e);
				}
			}
			builder.Build();
		}

		[TestMethod]
		//[Ignore]
		public void PseudoTest_GeracaoMetalzilla_44643_44649_44652_44653()
		{
			string inputs = @"
#Riscos MacroFator Macro_Fator MacrosFatores Macros_Fatores
	string Codigo C�digo RE
	string Nome Nome RX
	StringClob Definicao Defini��o RX
#Riscos FatorRisco Fator_de_Risco FatoresRiscos Fatores_de_Riscos
	MacroFator MacroFator Macro_Fator RX
	int Codigo C�digo RE
	string Nome Nome RX
	StringClob Definicao Defini��o RX
	StringClob Observacao Observa��o XX
	ENUM-StatusAtividade Status Status RX
#Riscos Controle Controle Controles Controles
	int Codigo C�digo RE
	string Nome Nome RX
	StringClob Definicao Defini��o RX
	StringClob Objetivo Defini��o RX
	ENUM-TipoControle Tipo Tipo RX
	ENUM-CategoriaControle Categoria Categoria RX
	Periodicidade Periodicidade Periodicidade RX
	ENUM-StatusAtividade Status Status RX
#Riscos Risco Risco Riscos Riscos
	int Codigo C�digo RE
	string Nome Nome RX
	StringClob Definicao Defin��o RX
	CategoriaRisco Categoria Categoria RX
	AreaInteresse AreaInteresse �rea_de_Interesse RX
	ENUM-StatusAtividade Status Status RX
";

			AppBuilder builder = new AppBuilder();
			builder.OutputBaseDir = @"c:\temp\_output-of-metalsoft-core-code-generator";
			//builder.TemplatesDir;
			List<Entity> entities = new List<Entity>();
			foreach (string input in inputs.Split('#'))
			{
				if (input.Trim().Length > 0)
				{
					var e = Parse(input, entities);
					builder.AddEntity(e);
					entities.Add(e);
				}
			}
			builder.Build();
		}

		[TestMethod]
		//[Ignore]
		public void PseudoTest_ContratosServicos()
		{
			string inputs = @"
#ContratosServicos COMPOSICAO-ClienteContratoServico Cliente ClientesContratosServicos Clientes
	CONTAINER-ContratoServico Contrato Contrato RE
	Cliente ClienteContrato Cliente RE
#ContratosServicos COMPOSICAO-OcorrenciaContratoServico Ocorr�ncia OcorrenciasContratosServicos Ocorr�ncias
	CONTAINER-ContratoServico Contrato Contrato RE
	int Sequencial Sequencial RE
	DateTime Data Data RX
	string Ocorrencia Ocorr�ncia RX
	Responsavel ResponsavelResolucao Respons�vel_para_Resolver_Quest�o XX
	boolean Resolvido Resolvido RX
	string FollowUp Follow_Up XX
#ContratosServicos COMPOSICAO-ServicoComplementar Servi�o_Complementar ServicosComplementares Servi�os_Complementares
	CONTAINER-ContratoServico Contrato Contrato RX
	DateTime DataInicioVigencia Data_In�cio_Vig�ncia RX
	bool Vigente Vigente RX
	Cliente Cliente Cliente RX
	ServicoPadronizado Servico Servi�o RX
	ServicoPadronizado ServicoFaturamento Servi�o_Faturamento RX
	decimal Quantidade Quantidade RX
	decimal Valor Valor RX
	bool ReajusteJuntoContrato Reajuste_Junto_Contrato RX
	ENUM-FormaFaturamento FormaFaturamento Forma_Faturamento RX
	bool SubtrairTraslado Subtrair_Traslado RX
#ContratosServicos COMPOSICAO-AjusteContratoServico Ajuste_Contratual AjustesContratosServicos Ajustes_Contratuais
	CONTAINER-ContratoServico ContratoAjustado Contrato RE 
	int Sequencial Sequencial RE
	ENUM-TipoAjusteContratoServico Tipo Tipo RX
	DateTime DataInicial Data_Inicial RX
	DateTime DataFinal Data_Final XX
	string Observacao Observa��o XX
	IList<ServicoContratado.AjusteContratual> ServicosContratados Servi�os_Contratados XX
	IList<ImagemAjusteContratoServico.AjusteContrato> Imagens Imagens XX
#ContratosServicos COMPOSICAO-ServicoContratado Servi�o_Contratado ServicosContratados Servi�os_Contratados
	CONTAINER-AjusteContratoServico AjusteContratual Ajuste_Contratual RE
	int Sequencial Sequencial RE
	ServicoPadronizado Servico Servi�o RX
	Modulo Modulo M�dulo XX
	ServicoPadronizado ServicoNFS Servi�o_NFS RX
	decimal Quantidade Quantidade RX
	bool Perene Perene RX
	decimal ValorReajuste Valor_Reajuste RX
	DateTime DataUltimoReajuste Data_�ltimos_Reajuste RX
	decimal ValorAdicao Valor_Adi��o RX
	decimal PercentualAdicao Percentual_Adi��o RX
	decimal ValorReducao Valor_Redu��o RX
	decimal PercentualReducao Percentual_Redu��o RX
	decimal ValorUnitario Valor_Unit�rio RX
	decimal ValorTotal Valor_Total RX
	DateTime DataAlerta Data_Alerta XX
	DateTime DataInicialFaturamento DataInicialFaturamento RX
	int QuantidadeParcelas Quantas_Parcelas RX
	DateTime DataFinalFaturamento Data_Final_Faturamento XX
	decimal ValorParcela Valor_Parcela RX
	decimal EncargoFinanceiroParcela Encargo_Financeiro_Parcela RX
	Cliente ClienteFaturamento Cliente_Faturamento RX
	Moeda IndiceReajuste Indice_Reajuste XX
	decimal TaxaJuros Taxa_de_Juros RX
	bool Deflacao Defla��o RX
	DateTime DataProxReajOriginal Data_Prox_Reaj_Original XX
	DateTime DataProxReajPrevisto Data_Prox_Reaj_Previsto XX
	string Texto Aditivo XX
#ContratosServicos ContratoServico Contrato ContratosServicos Contratos
	Empresa EmpresaPrestadora Empresa_Prestadora RE
	int Numero N�mero RE
	Cliente ClienteContratante Cliente RX
	DateTime DataInicio Data_de_In�cio RX
	DateTime DataTermino Data_de_T�rmino XX
	DateTime DataAssinatura  Data_de_Assinatura XX
	int PrazoVigenciaEmMeses Prazo_de_Vig�ncia_em_Meses RX
	Moeda IndiceReajuste �ndice_de_Reajuste XX
	ENUM-Periodicidade PeriodicidadeReajuste Periodicidade_de_Reajuste XX
	decimal TaxaJuros Taxa_de_Juros RX
	DateTime DataBaseReajuste Data_Base_de_Reajuste XX
	DateTime DataInicioReajuste Data_In�cio_de_Reajuste XX
	bool Deflacao Defla��o RX
	TipoOpercaoNF TipoOperacaoFaturamento Tipo_de_Opera��o RX
	AgenteComercial AgenteComercial Agente_Comercial RX
	CondicaoPagamento CondicaoPagamento Condi��o_de_Pagamento RX
	TipoDocumento MeioPagamento Meio_de_Pagamento RX
	decimal AliquotaISS Al�quota_de_ISS RX
	ENUM-StatusContratoServico Status Status RX
	IList<ClienteContratoServico.Contrato> ClientesCoparticipantes Clientes XX
	IList<OcorrenciaContratoServico.Contrato> Ocorrencias Ocorr�ncias XX
	IList<ServicoComplementar.Contrato> OutrosServicos Outros_Servi�os XX
	IList<AjusteContratoServico.ContratoAjustado> AjustesContratuais Ajustes_Contratuais XX
#ContratosServicos COMPOSICAO-ItemOrdemServico Servi�o_Executado ItensOrdensServicos Servi�os_Executados
	CONTAINER-OrdemServico OrdemServico Ordem_de_Servi�o RE
	int Sequencial Sequencial RE
	ServicoPadronizado ServicoExecutado Servi�o RX
	Responsavel ResponsavelExecucao Respons�vel RX
	decimal Quantidade Quantidade RX
	decimal ValorUnitario Valor_Servi�o RX
	decimal ValorTotal Valor_Total RX
	ENUM-OpcaoLocalExecucaoServico Local Local RX
	DateTime Data Data RX
	DateTime DataInicioFaturamento Data_In�cio_Faturamento RX
	int QuantidadeParcelas Quantas_Parcelas RX
	DateTime DataFimFaturamento Data_Fim_Faturamento XX
	decimal ValorParcela Valor_Parcela RX
	decimal ValorFaturado Valor_Faturado RX
	IList<RateioServicoExecutadoPorModulo.ItemRateado> RateioPorModulo Rateio_por_M�dulo XX
#ContratosServicos COMPOSICAO-RateioServicoExecutadoPorModulo Por_M�dulo RateiosServicosExecutadosPorModulo Por_M�dulo
	CONTAINER-ItemOrdemServico ItemRateado Item_Rateado RE
	Modulo Modulo M�dulo RE
	decimal Quantidade Quantidade RX
#ContratosServicos OrdemServico Ordem_de_Servi�o OrdensServicos Ordens_de_Servi�os
	Empresa EmpresaPrestadora Empresa RE
	int Numero N�mero RE
	int Ano Ano RE
	DateTime DataSolicitacao Data_de_Solicita��o RX
	DateTime DataAtendimento Data_de_Atendimento RX
	ContratoServico Contrato Contrato RX
	Cliente ClienteAtendido Cliente RX
	string Solicitante Solicitante RX
	ENUM-TipoOrdemServico Tipo Tipo RX
	string DetalhamentoServico Detalhamento_do_Servi�o RX
	string Observacao Observa��o XX
	string HoraPartidaPrestadora Partida_da_Prestadora XX
	string HoraChegadaCliente Chegada_no_Cliente XX
	string HoraInicoAlmoco In�cio_Almo�o XX
	string HoraFimAlmoco Fim_Almo�o XX
	string HoraSaidaCliente Sa�da_Cliente XX
	string HoraChegadaPrestadora Chegada_na_Prestadora XX
	bool Verificado Verificado RX
	Responsavel VerificadoPor Verificador_Por XX
	IList<ItemOrdemServico.OrdemServico> Itens Servi�os_Executados XX
#ContratosServicos FaturamentoContratoServico Faturamento FaturamentosContratosServicos Faturamentos
	AjusteContratoServico AjusteContratualFaturado Ajuste_Contratual RX
	NotaFiscalSaida NotaFiscalContrato Nota_Fiscal_Contrato XX
	NotaFiscalSaida NotaFiscalServicosComplementares Nota_Fiscal_Servi�os_Complementares XX
	DocumentosReceber NotaDebito Nota_de_D�bito XX
#ContratosServicos FaturamentoOrdemServico Faturamento FaturamentosOrdensServicos Faturamentos
	ItemOrdemServico ItemFaturado Item_Faturado RX
	NotaFiscalSaida NotaFiscal Nota_Fiscal XX
	DocumentosReceber NotaDebito Nota_de_D�bito XX
#ContratosServicos NotaFiscalSaida NotaFiscalSaida NotasFiscaisSaida NotasFiscaisSaida
#ContratosServicos DocumentosReceber DocumentosReceber DocumentosRecebidos DocumentosRecebidos
#ContratosServicos ModuloContratoServico ModuloContratoServico ModulosContratoServico ModulosContratoServico
	string Codigo C�digo RE
	string Descricao Descri��o RX
";

			AppBuilder builder = new AppBuilder();
			builder.OutputBaseDir = @"c:\temp\_output-of-metalsoft-core-code-generator";
			//builder.TemplatesDir;
			List<Entity> entities = new List<Entity>();
			foreach (string input in inputs.Split('#'))
			{
				if (input.Trim().Length > 0)
				{
					var e = Parse(input, entities);
					builder.AddEntity(e);
					entities.Add(e);
				}
			}
			builder.Build();
		}

		private Entity Parse(string input, List<Entity> entities)
		{
			const string COMPOSICAO = "COMPOSICAO-";
			const string ENUM = "ENUM-";
			const string CONTAINER = "CONTAINER-";

			string[] tokens = input
				.Replace("\t", string.Empty)
				.Split(new string[] { System.Environment.NewLine, " " }, System.StringSplitOptions.RemoveEmptyEntries);

			var entity = new Entity();
			entity.Application = new Application() { Name = tokens[0] };
			entity.EntityName = tokens[1];
			entity.EntityLabel = tokens[2].Replace("_", " ");
			entity.CollectionName = tokens[3];
			entity.CollectionLabel = tokens[4].Replace("_", " ");
			if(entity.EntityName.StartsWith(COMPOSICAO))
			{
				entity.EntityName = entity.EntityName.Replace(COMPOSICAO, string.Empty);
				entity.IsComposition = true;
			}

			for (int i = 5; i < tokens.Length; i += 4)
			{
				var p = new Property(entity)
				{
					Type = tokens[i],
					Name = tokens[i + 1],
					Label = tokens[i + 2].Replace("_", " "),
					Tip = tokens[i + 2].Replace("_", " "),
					Required = tokens[i + 3].StartsWith("R"),
					CriterionForEquals = tokens[i + 3].EndsWith("E")
				};

				if (p.Type.StartsWith(ENUM))
				{
					p.Type = p.Type.Replace(ENUM, string.Empty);
					p.IsEnum = true;
				}
				else if (p.Type.StartsWith(CONTAINER))
				{
					p.Type = p.Type.Replace(CONTAINER, string.Empty);
					p.IsContainer = true;
				}
				else if (p.IsCollection)
				{
					string fk = p.Type.Substring(p.Type.IndexOf('.'));
					p.Type = p.Type.Replace(fk, string.Empty) + ">";
					p.ForeignKeyFieldName = fk.Substring(1, fk.Length - 2);
					p.CompositionInfo = entities.Find(x => x.EntityName == p.GetParameterizedType());
				}
				entity.Properties.Add(p);
			}
			return entity;
		}

	}
}
