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

            //p.Cadastrar(p);
            //p.Remover("ps4");

            Produto alterado = new Produto();
            alterado.Codigo = 3;
            alterado.Nome = "polystation";
            alterado.Preco = 300f;

            p.Alterar(alterado);

            List<Produto> lista = p.Ler();

            foreach(Produto item in lista)
            {
                Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
            }

        }
    }
}