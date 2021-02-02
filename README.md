# Menor Preço
Uma API (REST/JSON), usando ASP.NET Core 3.1, para consulta e recuperação de produtos.  

<h4 align="center"> 
  <a href="#sobre-o-projeto">Sobre o projeto</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Tecnologias-e-ferramentas">Tecnologias e ferramentas</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp; 
  <a href="#Demonstração">Demonstração</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  </br>
  <a href="#Como-usar">Como usar</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a href="#Licença">Licença</a>
</h4>

<br/>

<p align="center">
  <a href="https://opensource.org/licenses/MIT">
    <img src="https://img.shields.io/badge/License-MIT-blue.svg" alt="License MIT">
  </a>
</p>


# Sobre o projeto

Este é um projeto básico de uma API (REST/JSON) para consulta e recuperação de produtos, filtrada pelo código GTIN (EAN) e ordenada de forma crescente pelo preço.
A base de dados será importada através de uma rota (GET /v1/produtos) e preenchida no banco de dados de forma automática.                                            
*`OBS: Assim que importar o novo arquivo, pela rota, os dados que constam anteriormente nas tabelas do banco de dados serão substituídos pelo novo arquivo!`*

A consulta para obter os produtos contidos no banco de dados, passando como parâmetro um valor GTIN, será realizada através da rota (GET /v1/produtos/).
Esta rota terá o seguinte comportamento, caso não seja passado o parâmetro:                                                                             
- Retornará resposta 400 (Bad Request);                                                                                                    
- Caso o produto não exista, retornará 404 (Not Found).   


Neste projeto, serão importadas planilhas de forma dinâmica para o banco de dados. Dessa forma, é importante que a planilha siga o seguinte padrão, antes de enviá-la pela rota:

- Será enviado na seguinte ordem, como ilustrado abaixo.                                                                                                                                                                                                                                                                                                        *`Obs: Evite preencher os campos com símbolos de potência, pois este código não interpretará os símbolos.`*

![Template](https://github.com/TesteReteste/testeMP/blob/main/GG/TemplateCSV.png)
                                              
                                              
Resumidamente, foram seguindos os seguintes passos:

1 - Criação do modelo de dados utilizando o banco de dados sqlite.
A criação do modelo se deu pelo uso do EF (Entity Framework) Core, um mapeador relacional de objeto (O/RM).
Foram baixados os pacotes que fazem parte, via Nuget:                                                                   
- Instalar Microsoft.EntityFrameworkCore (3.1.0)                                   
- Install Microsoft.EntityFrameworkCore.Relational (3.1.0)                                          
- Install Microsoft.EntityFrameworkCore.SqlServer (3.1.0) [Caso queira construir num banco sql server]                        
- Install Microsoft.EntityFrameworkCore.Tools (3.1.0) [Caso queira construir num banco sql server]                             
- Install Microsoft.EntityFrameworkCore.Sqlite (3.1.0) 
- Install Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer (4.2.0)

Ao rodar o projeto MenorPreco.Api, o banco de dados será criado automaticamento com o nome definido na string de conexão.                                     
Serão criados três arquivos na pasta raiz do projeto MenorPreco.Api (MenorPreco.db, MenorPreco.db-shm e MenorPreco.db-wal)

2 - O segundo passo foi criar uma rota (GET /v1/importar).                            
*`Lembrando de ajustar os dados na tabela antes de enviar o arquivo .csv pela rota, como descrito anteriormente.`*
                                              

# Tecnologias e ferramentas

O projeto foi desenvolvido com as seguintes tecnologias e ferramentas:

- [Visual Studio 2019](#Pré-requisitos)
- [.NET Core 3.1 LTS](#Pré-requisitos)
- [SQLite](#Pré-requisitos)


# Demonstração

PASSO 1

Para a visualização dos relacionamentos das tabelas do banco de dados sqlite, foi utilizado o software DBeaver (versão 7.3.3):

<img src="https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/BD_SqLite.jpg" alt="drawing" width="720"/>

E caso use a conexão de string para geração do banco e tabelas no Microsoft SQL Server Management Studio, abaixo está a imagem do relacionamento:

<img src="https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/BD_SqlServer.jpg" alt="drawing" width="720"/>


PASSO 2

O arquivo é enviado pela rota (POST /v1/importar) e os dados são inseridos automaticamente. Atentar-se para a ordem das colunas e pela formatação dos dados!
A primeira linha, que contém a descrição de cada coluna será ignorada quando os dados forem inseridos no banco.

![Example](https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/Gif_Post_.gif)


PASSO 3

A rota (GET /v1/produtos) receberá como parâmetro um código GTIN.


PASSO 4 

Incluindo a url do Google Maps no retorno dos produtos pesquisado pelo código GTIN, contendo o endereço do estabelecimento.

![Example](https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/Gif_GET.gif)

# Como usar

Após clonar o projeto, verificar o template de exemplo para atualizar o banco de dados, conforme está na pasta GitHub deste projeto (https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/dataset.csv)

<img src="https://github.com/renanegobbi/MenorPreco/blob/main/GitHub/Passos%20iniciais.jpg" alt="drawing" width="720"/>

# Licença
Este projeto está sob a licença do MIT. Consulte a [LICENÇA](https://github.com/TesteReteste/lim/blob/master/LICENSE) para obter mais informações.
Inicializar o projeto MenorPreco.Api.



