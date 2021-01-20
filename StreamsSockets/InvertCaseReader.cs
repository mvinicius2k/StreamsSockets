using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StreamsSockets {
    class InvertCaseReader {

        public int Porta { get; set; }
        public static string caminho = Environment.CurrentDirectory + "\\InvertCaseReader.txt";

        public InvertCaseReader(int porta) {
            Porta = porta;
        }

        private void Sair() {
            while (true) {
                string s = Console.ReadLine().ToLower();
                if (s == "n") {
                    Program.NovaJanela();
                }
            }




        }

        public void Ler() {
            new Thread(Sair).Start();
            Print("N - Nova Janela\n\nEsperando conexão");
            TcpListener escutador = new TcpListener(IPAddress.Any, Porta);
            string entrada = "";
            escutador.Start();
            
            using (Socket socket = escutador.AcceptSocket())
            using (NetworkStream ns = new NetworkStream(socket))
            using (StreamReader sr = new StreamReader(ns, Encoding.UTF8)) {
                Print("Conexão feita, escutando...\n" + Program.div + "\n");
                do {

                    Thread.Sleep(200);

                    byte[] resposta = Encoding.UTF8.GetBytes(entrada);
                    try {
                        entrada = sr.ReadLine();
                        Print(entrada);
                        ns.Write(resposta, 0, resposta.Length);
                    } catch (IOException) {

                        Print(Program.div + "\nConexão fechada.");
                        Console.ReadLine();
                        
                        return;
                    }
                    
                } while (entrada.Length != 0);
            }

            
        }

        private void Print(string str) {
            Console.WriteLine(str);
            File.AppendAllText(caminho, str + "\n");
        }
    }
}
