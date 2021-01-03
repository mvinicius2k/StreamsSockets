using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace StreamsSockets {
    class PessoasInputStream {
        public int Porta { get; set; }
        public List<Pessoa> PessoasList = new List<Pessoa>();
        public static string caminho = Environment.CurrentDirectory + "\\PessoasInputStream.txt";
        private bool primeiraMsg = true;
        private bool sair = false;

        public PessoasInputStream(int porta) {
            Porta = porta;
            
            
        }

        

        public void Iniciar() {


            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, Porta);
            
            UdpClient servidor = new UdpClient(ipe);
            Console.WriteLine("\n[Enter] Voltar\n" + Program.div);
            Console.WriteLine("Servidor iniciado, escutando... ");
            File.AppendAllText(caminho,"Servidor iniciado, escutando... \n", Encoding.UTF8);


            
            while (true) {
                Thread.Sleep(1000);

                

                byte[] bytes = servidor.Receive(ref ipe);

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(bytes)) {

                    Pessoa p;
                    dynamic dPessoa = bf.Deserialize(ms);

                    p = new Pessoa(dPessoa.Nome, dPessoa.Cpf, dPessoa.Idade);
                    PessoasList.Add(p);

                    if (primeiraMsg) {
                        Console.Clear();
                        primeiraMsg = false;

                        Console.WriteLine("[Enter] Voltar\n" + Program.div);
                    }


                    Console.WriteLine($"[{ipe}]\n{p.ToString()}\n");
                    File.AppendAllText(caminho, $"[]\n{p.ToString()}\n\n", Encoding.UTF8);
                }

            }
            

            
            
        }
    }
}
