/*
 *
 * Link do git: https://github.com/mvinicius2k/StreamsSockets.git
 *
 */


using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace StreamsSockets {
    class Program {
        public static string div = "-----------------------------------------";
        public static string caminho = Environment.CurrentDirectory + "\\StreamsSockets.exe";

        static bool novaJanela = false;


        public static void NovaJanela() {
            
            try {
                Process.Start("cmd.exe", "/c \"start cmd /c \"" + caminho + "\" -n");
            } catch {
                //só funciona no windows
            }
        }

        









        static void Main(string[] args) {
            
            BancoContas bancoContas = new BancoContas();

            while (true) {
                Console.Clear();
                Console.Write("1 - Cadastrar e enviar pessoas (Questão 1)\n2 - Iniciar servidor (Questão 2)\n3 - Banco (Questão 3)\n4 - OutputStreamWriter (Questão 4)\n5 - InputStreamReader (Questão 5)\n6 - Abrir uma nova janela\n0 - Sair\n\n> ");


                string op = Console.ReadLine();

                switch (op) {
                    case "1":
                        Q1();
                        break;
                    case "2":
                        Q2();
                        break;
                    case "3":
                        Q3();
                        break;
                    case "4":
                        Q4();
                        break;
                    case "5":
                        Q5();
                        break;
                    case "6":
                        NovaJanela();
                        break;
                    case "0":
                        return;

                } 
            }


            //Questões

            void Q1() {
                File.Delete(PessoasOutputStream.caminho);


                bool cadastrando = true;
                do {
                    Console.Clear();
                    Console.WriteLine("Pessoas inseridas\n" + div);

                    foreach (Pessoa pessoa in bancoContas.listPessoas) {
                        Console.WriteLine(pessoa.ToString() + "\n");
                    }

                    Console.WriteLine("\n1 - Inserir pessoas\n2 - Enviar pessoas\n3 - Limpar\n4 - Voltar");
                    string op2 = Console.ReadLine();
                    if (op2 == "1")
                        bancoContas.listPessoas.Add(Pessoa.Criar());
                    else if (op2 == "2")
                        cadastrando = false;
                    else if (op2 == "3")
                        bancoContas.listPessoas.Clear();
                    else if (op2 == "4")
                        return;



                } while (cadastrando);

                Console.WriteLine("\n" + div + "\n");
                Console.Write($"IP do destino: ");
                string ip = Console.ReadLine();

                Console.Write("Porta: ");

                string porta = Console.ReadLine();
                PessoasOutputStream pOut = null;

                pOut = new PessoasOutputStream(bancoContas.listPessoas, ip, Convert.ToInt32(porta));


                pOut.Enviar();


                Console.ReadLine();
            }

            void Q2() {
                File.Delete(PessoasInputStream.caminho);




                Console.Clear();
                Console.Write("Porta: ");
                string porta = Console.ReadLine();

                var pIn = new PessoasInputStream(Convert.ToInt32(porta));
                pIn.Iniciar();
            }

            void Q3() {
                Console.Clear();
                try {
                    var arquivos = Directory.EnumerateFiles(Environment.CurrentDirectory);
                    foreach (string s in arquivos) {
                        Console.WriteLine(s.Split('\\').Last());
                    }
                } catch {
                    Console.WriteLine("Não foi possível imprimir os arquivos da pasta");
                }

                Console.WriteLine(div);

                Console.Write("1 - Salvar\n2 - Carregar\n3 - Voltar\n\n> ");
                string op2 = Console.ReadLine();


                switch (op2) {
                    case "1":
                        Console.WriteLine("\n" + div);
                        Console.Write("Nome do arquivo: ");
                        try {
                            bancoContas.gravarContasArquivo(Console.ReadLine());
                            Console.WriteLine("Salvo com sucesso");
                        } catch (IOException e) {
                            Console.WriteLine("Erro de IO: " + e.Message);
                        }

                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("\n" + div);
                        Console.Write("Nome do arquivo: ");
                        try {
                            bancoContas.carregarContasArquivo(Console.ReadLine());
                            Console.WriteLine("Carregado com sucesso");

                        } catch (IOException e) {
                            Console.WriteLine("Erro de IO: " + e.Message);
                        }

                        Console.ReadLine();

                        break;

                    case "3":
                        return;


                }
            }
            
            void Q4() {

                try {
                    File.Delete(InvertCaseWriter.caminho);
                } catch {
                }

                Console.Clear();
                Console.Write("\nIP: ");
                string ip = Console.ReadLine();
                Console.Write("\nPorta: ");
                int porta = Convert.ToInt32(Console.ReadLine());

                InvertCaseWriter invertCaseWriter = new InvertCaseWriter(ip, porta);
                invertCaseWriter.Iniciar();



            }

            void Q5() {
                try {
                    File.Delete(InvertCaseReader.caminho);
                } catch (Exception) {

                }
                Console.Clear();
                Console.Write("Porta: ");
                int porta = Convert.ToInt32(Console.ReadLine());
                


                InvertCaseReader invertCaseReader = new InvertCaseReader(porta);
                invertCaseReader.Ler();
            }
        }


    }
}

    

