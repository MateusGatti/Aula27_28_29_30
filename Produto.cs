using System.IO;

namespace Aula27_28_29_30
{
    public class Produto
    {
        
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }

        private const string PATH = "Database/Produto.csv";

        public Produto(){

            string pasta = PATH.Split('/')[0];
            if(!Directory.Exists(pasta)){
                Directory.CreateDirectory(pasta);
            }
            
            using (var output = File.Create(PATH)) {
            }

        }

        

        public void Cadastrar(Produto p){

            var linha = new string[] { p.PrepararLinha(p) };
            File.AppendAllLines(PATH, linha);

        }

        private string PrepararLinha(Produto produto){

            return $"codigo={produto.Codigo};nome={produto.Nome};preco={produto.Preco}";

        }

    }
}