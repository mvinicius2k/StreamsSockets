using System;
using System.Collections.Generic;
using System.Text;

namespace StreamsSockets {
    [Serializable]
    class Pessoa {
        public string Nome { get; set; }

        public long Cpf { get; set; }

        public int Idade { get; set; }

        public string GetCpfFormatado() {
            string retorno = "";
            char[] cpfc = Cpf.ToString().ToCharArray();

            for (int i = 0; i < cpfc.Length; i++) {
                retorno += cpfc[i];
                if (i % 3 == 2)
                    if (i == 11)
                        retorno += "-";
                    else
                        retorno += ".";
            }
            return retorno;
        }

        public Pessoa(string nome, long cpf, int idade) {
            Nome = nome;
            Cpf = cpf;
            Idade = idade;
        }

        public Pessoa() {
        }

        public override string ToString() {
            return $"Nome: {Nome}\nCpf: {this.GetCpfFormatado()}\nIdade: {Idade}";
        }

        public static Pessoa Criar() {
            Pessoa p = new Pessoa();
            
            Console.Write("\nNome: ");
            p.Nome = Console.ReadLine();

            gCpf:
            Console.Write("\nCpf: ");
            try {
                p.Cpf = Convert.ToInt64(Console.ReadLine());
            } catch {
                Console.WriteLine("Digite somente os números");
                goto gCpf;
            }
            
            gIdade:
            Console.Write("Idade: ");
            try {
                p.Idade = Convert.ToInt32(Console.ReadLine());
            } catch {
                Console.WriteLine("Digite em números");
                goto gIdade;
            }

            return p;
        }
    }
}
