﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace StreamsSockets {
    [Serializable]
    class Pessoa{
        public string Nome { get; set; }

        public long Cpf { get; set; }

        public int Idade { get; set; }

        public string GetCpfFormatado() {
            string retorno = "";
            char[] cpfc = Cpf.ToString().ToCharArray();
            for (int i = 0; i < cpfc.Length; i++) {
                retorno += cpfc[i];
                if (i % 3 == 2)
                    if (i == 8)
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

            int tamanho = Encoding.UTF8.GetBytes(Nome).Length;

            return $"Nome: {Nome} ({tamanho} bytes)\nCpf: {this.GetCpfFormatado()}\nIdade: {Idade}";
        }

        public static Pessoa Criar() {
            Pessoa p = new Pessoa();
            
            Console.Write("\nNome: ");
            p.Nome = Console.ReadLine();

            bool loop = true;
            do {                
                Console.Write("\nCpf: ");
                try {
                    p.Cpf = Convert.ToInt64(Console.ReadLine());
                    loop = false;
                } catch {
                    Console.WriteLine("Digite somente os números");
                    
                } 
            } while (loop);

            loop = true;
            do {
                Console.Write("Idade: ");
                try {
                    p.Idade = Convert.ToInt32(Console.ReadLine());
                    loop = false;
                } catch {
                    Console.WriteLine("Digite em números");
                } 
            } while (loop);

            return p;
        }
       
    }
}
