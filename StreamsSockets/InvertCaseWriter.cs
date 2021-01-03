using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StreamsSockets {
    class InvertCaseWriter {
        public string Ip { get; private set; }
        public int Porta { get; private set; }


        public static string caminho = Environment.CurrentDirectory + "\\InvertCaseWriter.txt";

        public InvertCaseWriter(string ip, int porta) {
            Ip = ip;
            Porta = porta;
        }

        public void Iniciar() {

            string saida = "";

            using (TcpClient cliente = new TcpClient(Ip, Porta))
            using (NetworkStream ns = cliente.GetStream())
            using (StreamWriter sw = new StreamWriter(ns, Encoding.UTF8)) {

                Print("Conexão feita");

                do {

                    Console.Write("\n> ");

                    saida = Console.ReadLine();
                    File.AppendAllText(caminho, saida + "\n");
                    sw.WriteLine(InverterStr(saida));
                    sw.Flush();
                } while (saida.Length != 0);
            }


        }

        


        private string InverterStr(string entrada) {
            string novaStr = "";
            foreach(char c in entrada) {
                if(c.ToString() == c.ToString().ToUpper()) {
                    novaStr += c.ToString().ToLower();
                } else {
                    novaStr += c.ToString().ToUpper();
                }

            }

            return novaStr;
        }

        private void Print(string str) {
            Console.WriteLine(str);
            File.AppendAllText(caminho, str + "\n");
        }
    }
}
