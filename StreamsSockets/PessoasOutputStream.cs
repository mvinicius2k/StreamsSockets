using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace StreamsSockets {
    class PessoasOutputStream{
        public List<Pessoa> PessoasList { get; set; }
        public string IP { get; set; }
        public int Porta { get; set; }

        public static string caminho = Environment.CurrentDirectory + "\\PessoasOutputStream.txt";


        public PessoasOutputStream(List<Pessoa> pessoasList, string iP, int porta) {
            PessoasList = pessoasList;
            IP = iP;
            Porta = porta;
            

        }

        public void Enviar() {
            int index = 0;
            byte[] dadosOut;


            using (Socket socket = SocketConectar()) {
                
               
                foreach (Pessoa p in PessoasList) {
                    
                    BinaryFormatter bf = new BinaryFormatter();
                    using (MemoryStream ms = new MemoryStream()) {
                         bf.Serialize(ms, p);
                         dadosOut = ms.ToArray();
                     }

                    socket.Send(dadosOut, dadosOut.Length, SocketFlags.None);
                    Console.WriteLine("Pessoa " + index++ + " enviada.");
                    try {
                        File.AppendAllText(caminho, "Pessoa " + index++ + " enviada." + p.ToString() + "\n", Encoding.UTF8);
                    } catch (Exception e) {
                        Console.WriteLine("Erro ao escrever arquivo " + e.Message);
                    }
                    

                }

                    
            }


                 
            
            
            
    }


        private Socket SocketConectar() {

            do {
                IPAddress ip = IPAddress.Parse(IP);
                IPEndPoint ipe = new IPEndPoint(ip, Porta);
                Socket socket = new Socket(ipe.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

                try {
                    socket.Connect(ipe);
                } catch (Exception e) {
                    Console.WriteLine("Não houve conexão: " + e.Message);
                }


                if (socket.Connected) {
                    Console.WriteLine("Conectado");
                    try {
                        File.AppendAllText(caminho, "Conectado\n", Encoding.UTF8);
                    } catch (Exception e) {
                        Console.WriteLine("Erro ao escrever arquivo " + e.Message);
                    }
                    
                    return socket;
                }


                Console.WriteLine("Servidor não iniciado, tentando novamente...");
                try {
                    File.AppendAllText(caminho, "Servidor não iniciado, tentando novamente...\n", Encoding.UTF8);
                } catch (Exception e) {
                    Console.WriteLine("Não foi possível escrever o arquivo. " + e.Message);
                }
                
                Thread.Sleep(1000);
                    

                 
            } while (true);
            
        }

        
    }
}
