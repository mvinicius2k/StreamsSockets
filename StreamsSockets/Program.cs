using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace StreamsSockets {
    class Program {

        

        static void Main(string[] args) {
            string div = "-----------------------------------------";
            BancoContas bancoContas = new BancoContas();

            gMain:
            Console.Clear();
            Console.Write("1 - Cadastrar e enviar pessoas (Questão 1)\n2 - Iniciar servidor (Questão 2)\n3 - Banco (Questão 3)\n\n> ");


            string op = Console.ReadLine();


            if (op == "1") {
                File.Delete(PessoasOutputStream.caminho);

                gCadastrando:

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
                        goto gMain;



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

                goto gCadastrando;

            } else if (op == "2") {
                string caminho = Environment.CurrentDirectory + "\\StreamsSockets.exe";
                File.Delete(PessoasInputStream.caminho);




                Console.Clear();
                Console.Write("Porta: ");
                string porta = Console.ReadLine();

                try {
                    Process.Start("cmd.exe", "/c \"start cmd /c \"" + caminho + "\"");
                } catch {
                    //só funciona no windows
                }
                



                var pIn = new PessoasInputStream(Convert.ToInt32(porta));
                pIn.Iniciar();

                
            } else if(op == "3") {
                gBanco:
                Console.Clear();
                Console.Write("1 - Salvar\n2 - Carregar\n3 - Voltar\n\n> ");
                string op2 = Console.ReadLine();


                switch (op2) {
                    case "1":
                        Console.WriteLine("\n" + div);
                        Console.Write("Nome do arquivo: ");
                        try{
                            bancoContas.gravarContasArquivo(Console.ReadLine());
                            Console.WriteLine("Salvo com sucesso");
                        } catch (IOException e) {
                            Console.WriteLine("Erro de IO: " + e.Message);
                        } catch (Exception ex){
                            Console.WriteLine("Erro" + ex.Message);
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
                        } catch (Exception ex){
                            Console.WriteLine("Erro" + ex.Message);
                        }

                        Console.ReadLine();

                        break;
                       
                    case "3":
                        goto gMain;
                    

                }

                goto gBanco;

            }
        }
    }
}
