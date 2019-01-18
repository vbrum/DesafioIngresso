# Desafio Ingresso.com
Nessa aplicação, fiz uso do MongoDB 4.0.5 e para poder visualizar os dados utilizei o Robo 3T 1.2

Após fazer o download e instalar o MongoDB, sugiro que configure a variável de ambiente PATH com o caminho onde o MongoDB foi instalado.
O caminho que coloquei em PATH foi esse: C:\Program Files\MongoDB\Server\4.0\bin 

Verificar se o MongoDB criou o diretório (C:\data\db). Crie se não existir, pois pode ocorrer algum erro quando tentar colocar o servidor no ar pela ausência desse diretório.

O comando para colocar o servidor no ar através do cmd é mongod.

Para facilitar o uso da aplicação, fiz um dump do Banco de Dados com as informações que usei nos meus testes. Ele se encontra na pasta dump aqui no repositório.

Para construir esses dados a partir do dump use o cmd da seguinte forma:

C:\Users\nome_usuario\Desktop\dump>mongorestore C:\Users\nome_usuario\Desktop\dump

