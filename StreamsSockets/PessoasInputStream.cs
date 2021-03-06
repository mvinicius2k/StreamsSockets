﻿using System;
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

        private void Sair() {
            while (true) {
                string s = Console.ReadLine().ToLower();
               if (s == "n") {
                    Program.NovaJanela();
                }
            }
                
            
            
            
        }

        

        public void Iniciar() {


            IPEndPoint ipe = new IPEndPoint(IPAddress.Any, Porta);
            
            UdpClient servidor = new UdpClient(ipe);
            Console.WriteLine("N - Nova Janela\n" + Program.div);
            Console.WriteLine("Servidor iniciado, escutando... ");
            try {
                File.AppendAllText(caminho, "Servidor iniciado, escutando... \n", Encoding.UTF8);
            } catch(Exception e) {
                Console.WriteLine("Erro ao escrever no arquivo. " + e.Message);
            }



            new Thread(Sair).Start();
            while (true) {
                Thread.Sleep(1000);
                
                if (sair) {
                    Console.WriteLine("Q");
                    return;
                }
                    
                

                byte[] bytes = servidor.Receive(ref ipe);

                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(bytes)) {
                    
                    Pessoa p;
                    dynamic dPessoa = null;
                    try {
                        dPessoa = bf.Deserialize(ms);
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    

                    p = new Pessoa(dPessoa.Nome, dPessoa.Cpf, dPessoa.Idade);
                    PessoasList.Add(p);

                    if (primeiraMsg) {
                        Console.Clear();
                        primeiraMsg = false;

                        Console.WriteLine("N - Nova Janela\n" + Program.div);
                    }


                    Console.WriteLine($"[{ipe}]\n{p.ToString()}\n");
                    try {
                        File.AppendAllText(caminho, $"[]\n{p.ToString()}\n\n", Encoding.UTF8);
                    } catch (Exception e) {
                        Console.WriteLine("Erro ao escrever no arquivo. " + e.Message);
                    }
                    
                }

            }
            

            
            
        }
    }
}
