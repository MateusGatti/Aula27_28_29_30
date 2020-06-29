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
            //Cria o diretório caso ele não exista
            string pasta = PATH.Split('/')[0];
            if(!Directory.Exists(pasta)){
                Directory.CreateDirectory(pasta);
            }
            
            //Cria o arquivo caso ele não exista
            if(!File.Exists(PATH)){
                File.Create(PATH).Close();
            }

        }

        
        
    
        public void Cadastrar(Produto p){
            /// <summary>
            /// Método
            /// </summary>
            /// <returns>Acrescenta linhas em um arquivo</returns>
            
            var linha = new string[] { p.PrepararLinha(p) };
            File.AppendAllLines(PATH, linha);

        }

        private string PrepararLinha(Produto produto){
            /// <summary>
            /// Método
            /// </summary>
            /// <value>Escreve no arquivo</value>
            return $"codigo={produto.Codigo};nome={produto.Nome};preco={produto.Preco}";

        }

    }
}