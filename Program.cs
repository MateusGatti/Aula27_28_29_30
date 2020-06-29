using System;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            Produto p = new Produto();
            p.Codigo = 123;
            p.Nome = "PS5";
            p.Preco = 8000f;

            p.Cadastrar(p);
        }
    }
}
