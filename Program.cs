using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p = new Produto();
            p.Codigo = 123;
            p.Nome = "xboxone";
            p.Preco = 8000f;

            p.Cadastrar(p);
            p.Remover("xboxone");

            List<Produto> lista = new List<Produto>();
            lista = p.Ler();
            

            foreach(Produto item in lista){
                System.Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
            }


        }
    }
}
