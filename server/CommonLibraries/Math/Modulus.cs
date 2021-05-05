using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.CommonLibraries.Math
{
	/// <summary>
	/// https://en.wikipedia.org/wiki/Modular_arithmetic
	/// </summary>
	public static class Modulus
	{
		public static int CalculateDigit(string baseCalculo, int modulo)
		{
			return CalculateDigit(baseCalculo, modulo, int.MaxValue);
		}

		public static int CalculateDigit(string baseCalculo, int modulo, int maiorMultiplicador)
		{
			int[] multiplicadores = PrepareMultipliers(baseCalculo.Length, maiorMultiplicador);
			return CalculateDigit(baseCalculo, modulo, multiplicadores);
		}

		public static int CalculateDigit(string baseCalculo, int modulo, int[] multiplicadores)
		{
			if (baseCalculo == null)
				throw new ArgumentNullException();
			if (modulo <= 0)
				throw new ArgumentException("modulo tem que ser um número positivo");

			if (baseCalculo.Length != multiplicadores.Length)
				throw new InvalidOperationException("Base de cálculo com tamanho incorreto."); //em tese, nunca ocorre

			int somaProdutos = 0;
			for (int i = 0; i < baseCalculo.Length; i++)
			{
				int digito = Convert.ToInt32(baseCalculo[i].ToString());
				int multiplicador = multiplicadores[i];
				int produto = digito * multiplicador;
				somaProdutos += produto;
			}
			int resto = somaProdutos % modulo;
			int dv = modulo - resto;
			if (dv >= 10)
				dv = 0;
			return dv;
		}

		public static int[] PrepareMultipliers(int tamanho)
		{
			return PrepareMultipliers(tamanho, int.MaxValue);
		}

		public static int[] PrepareMultipliers(int tamanho, int maiorMultiplicador)
		{
			if (tamanho < 1)
				return new int[0];

			List<int> multiplicadores = new List<int>();
			for (int i = tamanho + 1; i >= 2; i--)
			{
				int m = i <= maiorMultiplicador
					? i
					: i - (maiorMultiplicador - 1);
				multiplicadores.Add(m);
			}
			return multiplicadores.ToArray();
		}

		public static int[] PrepareMultipliers(int tamanho, int[] multiplicadoresReferencia)
		{
			if (tamanho < 1)
				return new int[0];
			if (multiplicadoresReferencia == null || multiplicadoresReferencia.Length < 1)
				throw new ArgumentException("multiplicadoresReferencia");

			List<int> multiplicadores = new List<int>();
			for (int i = 0; i < tamanho; i++)
			{
				int m = multiplicadoresReferencia[i % multiplicadoresReferencia.Length];
				multiplicadores.Add(m);
			}
			return multiplicadores.ToArray();
		}

	}
}
