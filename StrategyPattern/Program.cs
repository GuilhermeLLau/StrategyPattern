using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    internal class Program
    {
        interface TipoDeConta
        {
             float calculaMensalidade(float valor,int c);
        }

        public class ContaMaxiTotal : TipoDeConta {

            public float calculaMensalidade(float saldo,int c)
            {
                float mensalidade;
                float desconto = 50 * ((saldo - 200) / 1800);
                if (desconto > 50)
                {
                    mensalidade = 0;
                }
                else
                {
                    mensalidade = 50 - desconto;
                }
                return mensalidade;
            }
        }


        public class ContaMaxiSimples : TipoDeConta
        {
            public float calculaMensalidade(float saldo, int c)
            {
                float mensalidade;
                float desconto = 20 * ((saldo - 100) / 1400);
                if (desconto > 20)
                {
                    mensalidade = 0;
                }
                else
                {
                    if (c <= 5)
                    {
                        mensalidade = 20 - desconto;
                    }
                    else
                    {
                        desconto = (20 + c - 5) * ((saldo - 100) / 1400);
                        mensalidade = (20 + c - 5) - desconto;
                        if (desconto > 20 + c - 5)
                        {
                            mensalidade = 0;
                        }
                    }
                }
                return mensalidade;
            }
        }

        public class ContaMaxiEconomica : TipoDeConta
        {

            public float calculaMensalidade(float saldo,int c)
            {
                double mensalidade = c * 1.5 + 10;
                return (float)mensalidade;
            }
        }



        class Cliente
        {
            public string nome;

            public Cliente(string str)
            {
                this.nome = str;
            }
        }

        class MovimentoBancario
        {
            public string descricao;
            public float valor;

            public MovimentoBancario(string str, float num)
            {
                this.descricao = str;
                this.valor = num;
            }
        }

        class ContaCorrente
        {
            public TipoDeConta tipoDaConta;
            public Cliente conta;
            public MovimentoBancario[] movimentoBancarios;
            public int contadorDeMovimentos;

            public void realizaDeposito(string descricao, float valor)
            {
                MovimentoBancario movimento = new MovimentoBancario(descricao, valor);
                movimentoBancarios[contadorDeMovimentos] = movimento;
                contadorDeMovimentos++;

            }

            public void realizaSaque(string descricao, float valor)
            {
                MovimentoBancario movimento = new MovimentoBancario(descricao, valor);
                movimentoBancarios[contadorDeMovimentos] = movimento;
                contadorDeMovimentos++;
            }

            public float calculaMensalidade()
            {
                float saldo = obtemSaldo();
                return tipoDaConta.calculaMensalidade(saldo,contadorDeMovimentos);
            }

            public float obtemSaldo()
            {
                float valorTotal = 0;
                for (int i = 0; i < movimentoBancarios.Length; i++)
                {
                    if (movimentoBancarios[i] == null)
                    {
                        return valorTotal;
                    }
                    if (movimentoBancarios[i].descricao == "deposito")
                    {
                        valorTotal += movimentoBancarios[i].valor;
                    }
                    else if (movimentoBancarios[i].descricao == "saque")
                    {
                        valorTotal -= movimentoBancarios[i].valor;
                    }
                }
                return valorTotal;
            }
            public ContaCorrente(Cliente cliente)
            {
                this.conta = cliente;
                movimentoBancarios = new MovimentoBancario[15];
            }
        }

        static void Main(string[] args)
        {
            Cliente guilherme = new Cliente("Guilherme");
            ContaCorrente contaCorrente = new ContaCorrente(guilherme);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("deposito",1000);
            contaCorrente.realizaDeposito("saque",9800);
            contaCorrente.tipoDaConta = new ContaMaxiSimples();
            Console.WriteLine(contaCorrente.calculaMensalidade());
            contaCorrente.tipoDaConta = new ContaMaxiEconomica();
            Console.WriteLine(contaCorrente.calculaMensalidade());
            contaCorrente.tipoDaConta = new ContaMaxiTotal();
            Console.WriteLine(contaCorrente.calculaMensalidade());
            Console.ReadKey();
        }
    }
}
