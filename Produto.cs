using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        /// <summary>
        /// Lê o csv
        /// </summary>
        /// <returns>Lista de produtos</returns>
        public List<Produto> Ler(){
            //Criando uma lista de produtos
            List<Produto> produtos = new List<Produto>();

            //Transformando as linhas disponiveis em uma array de strings
            string[] linhas = File.ReadAllLines(PATH);

            foreach(var linha in linhas){

                //Separando os dados em cada linha com o Split
                string[] dados = linha.Split(";");

                Produto p = new Produto();
                p.Codigo = Int32.Parse( Separar(dados[0]) );
                p.Nome = (Separar(dados[1]));
                p.Preco = float.Parse(Separar(dados[2]));

                produtos.Add(p);

            }

            produtos = produtos.OrderBy(y => y.Nome).ToList();
            return produtos;

        }
        /// <summary>
        /// Remove uma ou mais linhas que tenham o termo
        /// </summary>
        /// <param name="_termo">termo para ser buscado</param>
        public void Remover (string _termo){
            //Criando uma lista para servir como um backup para as linhas do csv
            List<string> linhas = new List<string>();
            //Usando a biblioteca StreamReader para ler nosso csv
            using(StreamReader arquivo = new StreamReader(PATH)){

                string linha;
                while ((linha = arquivo.ReadLine()) != null ){

                    linhas.Add(linha);
                
                }

            }
            //Removemos as linhas que tiverem o termo passado como argumento, irá ser passado pelo program
            linhas.RemoveAll(l => l.Contains(_termo));
            // Reescrevemos o csv do zero
            using(StreamWriter output = new StreamWriter(PATH)){

                foreach(string ln in linhas){
                    output.Write(ln + "\n");
                }

            }

        }
        /// <summary>
        /// Filtra as linhas do csv em ordem alfabética
        /// </summary>
        /// <param name="_nome">nomes a serem filtrados</param>
        /// <returns>Linha filtrada</returns>
        public List<Produto> Filtrar(string _nome){
            return Ler().FindAll(x => x.Nome == _nome);
        }

        /// <summary>
        /// Adiciona itens ao csv
        /// </summary>
        /// <param name="p">Coloca o codigo, nome e preço no csv</param>
        public void Cadastrar(Produto p){
            var linha = new string[] { p.PrepararLinha(p) };
            File.AppendAllLines(PATH, linha);
        }
        
        /// <summary>
        /// Separa a lista do csv para aparecer no console
        /// </summary>
        /// <param name="_coluna">Deixa só o valor depois do =</param>
        /// <returns>Serapação</returns>
        private string Separar(string _coluna){
            return _coluna.Split("=")[1];
        }

        /// <summary>
        /// Escreve no csv
        /// </summary>
        /// <param name="produto">nome do produto, codigo do produto e preço do produto</param>
        /// <returns>Linha com dados</returns>
        private string PrepararLinha(Produto produto){
            
            return $"codigo={produto.Codigo};nome={produto.Nome};preco={produto.Preco}";

        }

    }
}