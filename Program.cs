using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
        static Regex regexCpf, regexTelefone, regexAno, regexPlaca;

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Oficina Conserto Rápido");
                Console.WriteLine("\nSelecione uma das opções:");
                Console.WriteLine("1 - Consultar informações do cliente");
                Console.WriteLine("2 - Inserir novo cliente");
                Console.WriteLine("3 - Excluir informações de um cliente");
                Console.WriteLine("4 - Alterar informações de um cliente");
                Console.WriteLine("5 - Listar todos os clientes");
                Console.WriteLine("C - Carregar informações do banco de dados");
                Console.WriteLine("S - Salvar informações no banco de dados");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1': Console.Clear(); Consultar(); break;
                    case '2': Console.Clear(); Inserir(); break;
                    case '3': Console.Clear(); Excluir(); break;
                    case '4': Console.Clear(); Alterar(); break;
                    case '5': Console.Clear(); Listar(); break;
                    case 'c': Console.Clear(); CarregarInfo(); break;
                    case 's': Console.Clear(); SalvarInfo(); break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida!");
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
                arrayCliente[i] = $"{cliente[i].nome},{cliente[i].cpf},{cliente[i].email}," +
                    $"{cliente[i].telefone},{cliente[i].endereco},{cliente[i].tipo},{cliente[i].marca}," +
                    $"{cliente[i].modelo},{cliente[i].cor},{cliente[i].ano},{cliente[i].placa}";
            }
            File.WriteAllLines(localAqr, arrayCliente);
            Console.WriteLine("Informações salvas com sucesso!");
            Console.ReadKey();
        }

        private static void CarregarInfo()
        {
            if (File.Exists(localAqr))
            {
                string[] arrayCarregarCliente = File.ReadAllLines(localAqr);
                if (arrayCarregarCliente.Length > 0)
                {
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
                    Console.WriteLine("Informações carregadas com sucesso!");
                }
                else
                {
                    Console.WriteLine("Não há informações no banco de dados!");
                }
            }
            else
            {
                Console.WriteLine("Banco de dados inexistente!");
            }
            Console.ReadKey();
        }

        private static void Consultar()
        {
            Console.Write("Digite o nome do cliente que você está buscando: ");
            string nomeBusca = Console.ReadLine();
            for (int i = 0; i < cliente.Count; i++)
            {
                if (cliente[i].nome != null && cliente[i].nome.ToLower().Contains(nomeBusca.ToLower()))
                {
                    ListarInfoTotal(i);
                }
            }
        }

        private static void Excluir()
        {
            Consultar();
            Console.Write("\nDigite o número do ID do cliente que você deseja excluir as informações: ");
            cliente.RemoveAt(int.Parse(Console.ReadLine()));
            Console.WriteLine("\nInformações excluídas com sucesso!");
            Console.ReadKey();
        }

        private static void Alterar()
        {
            Consultar();
            Console.Write("\nDigite o número do ID do cliente que você deseja alterar as informações: ");
            int numCliente = int.Parse(Console.ReadLine());
            Cliente clienteAlterar = cliente[numCliente];
            CarregarRegex(out regexCpf, out regexTelefone, out regexAno, out regexPlaca);
            Console.Clear();
            Console.WriteLine($"Qual informação do cliente {numCliente} você deseja alterar?");
            Console.WriteLine("\n1 - Informações pessoais");
            Console.WriteLine("2 - Informações do veículo");
            if (Console.ReadKey().KeyChar == '1')
            {
                Console.Clear();
                Console.WriteLine($"Qual dos dados do cliente {numCliente} você deseja alterar?");
                Console.WriteLine("1 - Nome");
                Console.WriteLine("2 - CPF");
                Console.WriteLine("3 - Email");
                Console.WriteLine("4 - Telefone");
                Console.WriteLine("5 - Endereço");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Novo nome: ");
                        clienteAlterar.nome = Console.ReadLine();
                        Console.WriteLine("\nNome alterado com sucesso!");
                        break;
                    case '2':
                        Console.Clear();
                        do
                        {
                            Console.Write("Novo CPF (modelo - 00000000000 - somente números): ");
                            clienteAlterar.cpf = Console.ReadLine();
                        } while (!regexCpf.IsMatch(clienteAlterar.cpf) || clienteAlterar.cpf.Length != 11);
                        Console.WriteLine("\nCPF alterado com sucesso!");
                        break;
                    case '3':
                        Console.Clear();
                        do
                        {
                            Console.Write("Novo email (modelo - xxxxx@xxx.xxx): ");
                            clienteAlterar.email = Console.ReadLine();
                        } while (!clienteAlterar.email.Contains("@") || !clienteAlterar.email.Contains("."));
                        Console.WriteLine("\nEmail alterado com sucesso!");
                        break;
                    case '4':
                        Console.Clear();
                        do
                        {
                            Console.Write("Novo telefone (modelo - 81999999999): ");
                            clienteAlterar.telefone = Console.ReadLine();
                        } while (!regexTelefone.IsMatch(clienteAlterar.telefone) || clienteAlterar.telefone.Length != 11);
                        Console.WriteLine("\nTelefone alterado com sucesso!");
                        break;
                    case '5':
                        Console.Clear();
                        Console.Write("Novo endereço: ");
                        clienteAlterar.endereco = Console.ReadLine();
                        Console.WriteLine("\nEndereço alterado com sucesso!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida...");
                        Console.ReadKey();
                        break;
                }
            } 
            else if (Console.ReadKey().KeyChar == '2')
            {
                Console.Clear();
                Console.WriteLine($"Qual dos dados do veículo do cliente {numCliente} você deseja alterar?");
                Console.WriteLine("1 - Tipo");
                Console.WriteLine("2 - Marca");
                Console.WriteLine("3 - Modelo");
                Console.WriteLine("4 - Cor");
                Console.WriteLine("5 - Ano");
                Console.WriteLine("6 - Placa");
                switch (Console.ReadKey().KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.Write("Novo tipo do veículo: ");
                        clienteAlterar.tipo = Console.ReadLine();
                        Console.WriteLine("\nTipo do veículo alterado com sucesso!");
                        break;
                    case '2':
                        Console.Clear();
                        Console.Write("Nova marca do veículo: ");
                        clienteAlterar.marca = Console.ReadLine();
                        Console.WriteLine("\nMarca do veículo alterada com sucesso!");
                        break;
                    case '3':
                        Console.Clear();
                        Console.Write("Novo modelo do veículo: ");
                        clienteAlterar.modelo = Console.ReadLine();
                        Console.WriteLine("\nModelo do veículo alterado com sucesso!");
                        break;
                    case '4':
                        Console.Clear();
                        Console.Write("Nova cor do veículo: ");
                        clienteAlterar.cor = Console.ReadLine();
                        Console.WriteLine("\nCor do veículo alterado com sucesso!");
                        break;
                    case '5':
                        Console.Clear();
                        do
                        {
                            Console.Write("Novo ano do veículo: ");
                            clienteAlterar.ano = Console.ReadLine();
                        } while (!regexAno.IsMatch(clienteAlterar.ano) || clienteAlterar.ano.Length != 4);
                        Console.WriteLine("\nAno do veículo alterado com sucesso!");
                        break;
                    case '6':
                        Console.Clear();
                        do
                        {
                            Console.Write("Nova placa do veículo (modelo - AAA1111 ou AAA1A11): ");
                            clienteAlterar.placa = Console.ReadLine();
                        } while (!regexPlaca.IsMatch(clienteAlterar.placa) || clienteAlterar.placa.Length != 7);
                        Console.WriteLine("\nAno do veículo alterado com sucesso!");
                        break;
                    default:
                        Console.WriteLine("\nOpção inválida...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nOpção inválida...");
            }
            cliente[numCliente] = clienteAlterar;
            Console.ReadKey();
        }

        private static void Listar()
        {
            for (int i = 0; i < cliente.Count; i++)
            {
                ListarInfoTotal(i);
            }
            Console.ReadKey();
        }

        private static void Inserir()
        {
            Cliente c = new Cliente();
            CarregarRegex(out regexCpf, out regexTelefone, out regexAno, out regexPlaca);
            Console.WriteLine($"Digite as informações do Cliente:\n");
            Console.Write("Nome: ");
            c.nome = Console.ReadLine();
            do
            {
                Console.Write("CPF (modelo - 00000000000 - somente números): ");
                c.cpf = Console.ReadLine();
            } while (!regexCpf.IsMatch(c.cpf) || c.cpf.Length != 11);
            do
            {
                Console.Write("Email (modelo - xxxxx@xxx.xxx): ");
                c.email = Console.ReadLine();
            } while (!c.email.Contains("@") || !c.email.Contains("."));
            do
            {
                Console.Write("Telefone (modelo - 81999999999): ");
                c.telefone = Console.ReadLine();
            } while (!regexTelefone.IsMatch(c.telefone) || c.telefone.Length != 11);
            Console.Write("Endereço: ");
            c.endereco = Console.ReadLine();
            Console.WriteLine("\nDigite as informações do Veículo:");
            Console.Write("\nTipo: ");
            c.tipo = Console.ReadLine();
            Console.Write("Marca: ");
            c.marca = Console.ReadLine();
            Console.Write("Modelo: ");
            c.modelo = Console.ReadLine();
            do
            {
                Console.Write("Ano: ");
                c.ano = Console.ReadLine();
            } while (!regexAno.IsMatch(c.ano) || c.ano.Length != 4);
            Console.Write("Cor: ");
            c.cor = Console.ReadLine();
            do
            {
                Console.Write("Placa (modelo: AAA1111 ou AAA1A11): ");
                c.placa = Console.ReadLine();
            } while (!regexPlaca.IsMatch(c.placa.ToUpper()) || c.placa.Length != 7);
            cliente.Add(c);
            Console.WriteLine("\nInformações salvas!");
            Console.ReadKey();
        }

        private static void CarregarRegex(out Regex regexCpf, out Regex regexTelefone, out Regex regexAno, out Regex regexPlaca)
        {
            regexCpf = new Regex("[0-9]{11}");
            regexTelefone = new Regex("[1-9]{2}[9]{1}[1-9]{1}[0-9]{7}");
            regexAno = new Regex("[1-2][0-9]{3}");
            regexPlaca = new Regex("[A-Z]{3}[0-9][0-9A-Z][0-9]{2}");
        }

        private static void ListarInfoTotal(int i)
        {
            Console.WriteLine($"ID Cliente ---> {i}");
            Console.WriteLine();
            Console.WriteLine("Informações do Cliente:");
            Console.WriteLine();
            Console.WriteLine($"Nome: {cliente[i].nome}");
            Console.WriteLine($"CPF: {cliente[i].cpf}");
            Console.WriteLine($"Email: {cliente[i].email}");
            Console.WriteLine($"Telefone: {cliente[i].telefone}");
            Console.WriteLine($"Endereco (sem vígulas!): {cliente[i].endereco}");
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