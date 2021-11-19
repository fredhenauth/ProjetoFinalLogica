using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetoFinalLogica
{
    public struct Cliente
    {
        public string nome;
        public string cpf;
        public string email;
        public string telefone;
        public string endereco;
        public string tipo;
        public string marca;
        public string modelo;
        public string ano;
        public string cor;
        public string placa;
    }
    
    public class Program
    {
        public static List<Cliente> cliente = new List<Cliente>();
        static string localAqr = @"C:\BD_Clientes.txt";

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Oficina Conserto Rápido");
                Console.WriteLine();
                Console.WriteLine("Selecione uma das opções:");
                Console.WriteLine("1 - Consultar informações do cliente");
                Console.WriteLine("2 - Inserir novo cliente");
                Console.WriteLine("3 - Excluir informações de um cliente");
                Console.WriteLine("4 - Alterar informações de um cliente");
                Console.WriteLine("5 - Listar todos os clientes");
                Console.WriteLine("C - Carregar informações do banco de dados");
                Console.WriteLine("S - Salva informações no banco de dados");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1": Consultar(); break;
                    case "2": Inserir(); break;
                    case "3": Excluir(); break;
                    case "4": AlterarCliente(); break;
                    case "5": Listar(); break;
                    case "c": CarregarInfo(); break;
                    case "s": SalvarInfo(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida! Selecione uma opção de 1 a 5");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
                Console.WriteLine("Tecle ENTER para voltar ao menu inicial");
                Console.WriteLine("Ou aperte qualquer tecla para sair do programa");
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        private static void SalvarInfo()
        {
            string[] arrayCliente = new string[cliente.Count];
            for (int i = 0; i < cliente.Count; i++)
            {
                arrayCliente[i] = $"{cliente[i].nome}, {cliente[i].cpf}, {cliente[i].email}, " +
                    $"{cliente[i].telefone}, {cliente[i].endereco}, {cliente[i].tipo}, {cliente[i].marca}, " +
                    $"{cliente[i].modelo}, {cliente[i].cor}, {cliente[i].ano}, {cliente[i].placa}";
            }
            File.WriteAllLines(localAqr, arrayCliente);
        }

        private static void CarregarInfo()
        {
            string[] arrayCarregarCliente = File.ReadAllLines(localAqr);
            for (int i = 0; i < arrayCarregarCliente.Length; i++)
            {
                string[] linha = arrayCarregarCliente[i].Split(",");
                Cliente c = new Cliente();
                c.nome = linha[0];
                c.cpf = linha[1];
                c.email = linha[2];
                c.telefone = linha[3];
                c.endereco = linha[4];
                c.tipo = linha[5];
                c.marca = linha[6];
                c.modelo = linha[7];
                c.cor = linha[8];
                c.ano = linha[9];
                c.placa = linha[10];
                cliente.Add(c);
            }
        }

        private static void Consultar()
        {
            Console.Write("Digite o nome do cliente para consultar as informações: ");
            string nomeConsulta = Console.ReadLine();
            Console.Clear();
            for (int i = 0; i < cliente.Count; i++)
            {
                if (cliente[i].nome != null && cliente[i].nome.ToLower().Contains(nomeConsulta.ToLower()))
                {
                    ListarInfoTotal(i);
                }
            }
        }

        private static void Inserir()
        {
            Console.Clear();
            SolicitarInfoCliente();
        }

        private static void Excluir()
        {
            Console.Write("Digite o nome do cliente que você está buscado: ");
            string nomeExcluir = Console.ReadLine();
            for (int i = 0; i < cliente.Count; i++)
            {
                if (cliente[i].nome != null && cliente[i].nome.ToLower().Contains(nomeExcluir.ToLower()))
                {
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Número ---> {i}");
                    ListarInfoTotal(i);
                }
            }
            Console.WriteLine();
            Console.Write("Digite o número do cliente que você deseja excluir as informações: ");
            cliente.RemoveAt(int.Parse(Console.ReadLine()));
        }

        private static void AlterarCliente()
        {
            Console.Clear();
            Console.WriteLine("Digite o nome do cliente que você deseja alterar informações");
            string nomeAlterar = Console.ReadLine();
            Console.WriteLine("Qual dos dados você deseja alterar?");
            Console.WriteLine("1 - Nome");
            Console.WriteLine("2 - CPF");
            Console.WriteLine("3 - Email");
            Console.WriteLine("4 - Telefone");
            Console.WriteLine("5 - Endereco");
            string opcaoAlterar = Console.ReadLine();
            for (int i = 0; i < cliente.Count; i++)
            {
                if (cliente[i].nome != null && cliente[i].nome.ToLower().Contains(nomeAlterar.ToLower()))
                {
                    switch (opcaoAlterar)
                    {
                        case "1":
                            Console.Clear();
                            Console.Write($"Deseja alterar '{cliente[i].nome}' para qual nome? ");
                            cliente[i].nome = Console.ReadLine();
                            break;
                        default: Console.WriteLine("Opção inválida!"); break;
                    }
                }
            }
        }

        private static void AlterarVeiculo()
        {

        }

        private static void Listar()
        {
            for (int i = 0; i < cliente.Count; i++)
            {
                ListarInfoTotal(i);
            }
        }
        
        private static void SolicitarInfoCliente()
        {
            Cliente c = new Cliente();
            Console.WriteLine($"Digite as informações do Cliente:");
            Console.WriteLine();
            Console.Write("Nome: ");
            c.nome = Console.ReadLine();
            Console.Write("CPF: ");
            c.cpf = Console.ReadLine();
            Console.Write("Email: ");
            c.email = Console.ReadLine();
            Console.Write("Telefone: ");
            c.telefone = Console.ReadLine();
            Console.Write("Endereço: ");
            c.endereco = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Digite as informações do Veículo:");
            Console.WriteLine();
            Console.Write("Tipo: ");
            c.tipo = Console.ReadLine();
            Console.Write("Marca: ");
            c.marca = Console.ReadLine();
            Console.Write("Modelo: ");
            c.modelo = Console.ReadLine();
            Console.Write("Ano: ");
            c.ano = Console.ReadLine();
            Console.Write("Cor: ");
            c.cor = Console.ReadLine();
            Console.Write("Placa: ");
            c.placa = Console.ReadLine();
            cliente.Add(c);
        }

        private static void ListarInfoTotal(int i)
        {
            Console.WriteLine("Informações do Cliente:");
            Console.WriteLine($"Nome: {cliente[i].nome}");
            Console.WriteLine($"CPF: {cliente[i].cpf}");
            Console.WriteLine($"Email: {cliente[i].email}");
            Console.WriteLine($"Telefone: {cliente[i].telefone}");
            Console.WriteLine($"Endereco: {cliente[i].endereco}");
            Console.WriteLine();
            Console.WriteLine("Informações do Veículo:");
            Console.WriteLine();
            Console.WriteLine($"Tipo: {cliente[i].tipo}");
            Console.WriteLine($"Marca: {cliente[i].marca}");
            Console.WriteLine($"Modelo: {cliente[i].modelo}");
            Console.WriteLine($"Cor: {cliente[i].cor}");
            Console.WriteLine($"Ano: {cliente[i].ano}");
            Console.WriteLine($"Placa: {cliente[i].placa}");
            Console.WriteLine("--------------------------------------");
        }
    }
}