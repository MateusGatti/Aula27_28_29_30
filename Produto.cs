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

        private const string PATH = "Database/produto.csv";

        public Produto()
        {
            // ------------------------------------------------
            // Solução do desafio
            string pasta = PATH.Split('/')[0];

            if(!Directory.Exists(pasta)){
                Directory.CreateDirectory(pasta);
            }
            // ------------------------------------------------

            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        /// <summary>
        /// Cadastra um produto
        /// </summary>
        /// <param name="prod">Objeto Produto</param>
        public void Cadastrar(Produto prod)
        {
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Lê o csv 
        /// </summary>
        /// <returns>Lista de produtos</returns>
        public List<Produto> Ler()
        {
            // Criamos uma lista que servirá como nosso retorno
            List<Produto> produtos = new List<Produto>();

            // Lemos o arquivo e transformamos em um array de linhas
            string[] linhas = File.ReadAllLines(PATH);

            foreach(string linha in linhas){
                
                // Separamos os dados de cada linha com Split
                string[] dado = linha.Split(";");

                // Criamos instâncias de produtos para serem colocados na lista
                Produto p   = new Produto();
                p.Codigo    = Int32.Parse( Separar(dado[0]) );
                p.Nome      = Separar(dado[1]);
                p.Preco     = float.Parse( Separar(dado[2]) );

                produtos.Add(p);
            }

            produtos = produtos.OrderBy(y => y.Nome).ToList();
            return produtos; 
        }

        /// <summary>
        /// Remove uma ou mais linhas que contenham o termo
        /// </summary>
        /// <param name="_termo">termo para ser buscado</param>
        public void Remover(string _termo){

            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizamos a bliblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            // Removemos as linhas que tiverem o termo passado como argumento
            linhas.RemoveAll(l => l.Contains(_termo));

            // Reescrevemos nosso csv do zero
            ReescreverCSV(linhas);
        }
        
        /// <summary>
        /// Altera um produto
        /// </summary>
        /// <param name="_produtoAlterado">linha alterada</param>
        public void Alterar(Produto _produtoAlterado){

            // Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            // Utilizamos a bliblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader(PATH))
            {
                string linha;
                while((linha = arquivo.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            linhas.RemoveAll(z => z.Split(";")[0].Split("=")[1] == _produtoAlterado.Codigo.ToString());

            // Adicionamos a linha alterada na lista de backup
            linhas.Add( PrepararLinha(_produtoAlterado) );

            // Reescrevemos nosso csv do zero
            ReescreverCSV(linhas);         
        }

        /// <summary>
        /// Reescreve o csv
        /// </summary>
        /// <param name="lines"></param>
        private void ReescreverCSV(List<string> lines){
            // Reescrevemos nosso csv do zero
            using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach(string ln in lines)
                {
                    output.Write(ln + "\n");
                }
            }   
        }
        /// <summary>
        /// Filtra a linha em ordem alfabética
        /// </summary>
        /// <param name="_nome">nome produto</param>
        /// <returns>linha filtrada</returns>
        public List<Produto> Filtrar(string _nome)
        {
            return Ler().FindAll(x => x.Nome == _nome);
        }

        /// <summary>
        /// separa os argumentos
        /// </summary>
        /// <param name="_coluna">separador</param>
        /// <returns>lista separada</returns>
        private string Separar(string _coluna)
        {
            return _coluna.Split("=")[1];
        }

        /// <summary>
        /// Prepara a linha para ser "escrita"
        /// </summary>
        /// <param name="p">argumentos dos produtos</param>
        /// <returns>linha preparada</returns>
        private string PrepararLinha(Produto p)
        {
            return $"codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
        }

    }
}