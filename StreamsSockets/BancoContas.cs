using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreamsSockets {
    class BancoContas {


        public List<Pessoa> listPessoas = new List<Pessoa>();

        /// <exception cref="IOException"></exception>
        public void gravarContasArquivo(string nomeArquivo) {
            
            string txt = "";

            listPessoas.ForEach(delegate (Pessoa p) {
                txt += $"Nome={p.Nome};Cpf={p.Cpf};Idade={p.Idade}\n";
            });

            try {
                File.WriteAllText(Environment.CurrentDirectory + "\\" + nomeArquivo, txt, Encoding.UTF8);
            } catch (IOException) {
                throw;
            } catch (Exception e) {
                Console.WriteLine("Erro ao escrever arquivo. " + e.Message);
            }


        }

        /// <exception cref="IOException"></exception>
        public void carregarContasArquivo(string nomeArquivo) {
            string txt;

            try {
                txt = File.ReadAllText(Environment.CurrentDirectory + "\\" + nomeArquivo, Encoding.UTF8);
            } catch (Exception) {
                throw;
            }

            foreach (string strPessoa in txt.TrimEnd('\n').Split('\n')) {
                Pessoa p = new Pessoa();
                foreach (string atributo in strPessoa.Split(';')) {

                    if (atributo.StartsWith("Nome="))
                        p.Nome = atributo.Substring("Nome=".Length);
                    else if (atributo.StartsWith("Cpf="))
                        p.Cpf = Convert.ToInt64(atributo.Substring("Cpf=".Length));
                    else if (atributo.StartsWith("Idade="))
                        p.Idade = Convert.ToInt32(atributo.Substring("Idade=".Length));
                }

                listPessoas.Add(p);
            }
        }

        
    }
}
