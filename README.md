
![Sep_Banner](https://github.com/GianLost/SEP_Web/assets/107321084/f463b39f-10e5-4a2b-9e54-2db91d44b1a1)

# SEP - Sistema de Estágio Probatório

Inicialmente o pojeto precisa contar com um sistema de Login e cadastro de usuários. Para isso usaremos um sistma em Asp.Net Core MVC com base no .Net 6.0. Para persistência dos dados iremos utilizar um banco de dados Mysql que será gerenciado atrvés da ferramenta do EntityFramework 6.0.



## Contribuindo

Contribuições são sempre bem-vindas!

Veja `contribuindo.md` para saber como começar.

Por favor, siga o `código de conduta` desse projeto.



## Instalação

crie um projeto com

```bash
dotnet new mvc --no-https --framework net6.0
```
para instalar o EfCore 6.0

```bash
dotnet tool install --global dotnet-ef --version 6.0.0
```
adicione também os pacotes usados pelo EfCore para realizar as migrações

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0
```

```bash
dotnet add Pomelo.EntityFrameworkCore.MySql --version 6.0.0
```
    
## Licença

[MIT](https://github.com/GianLost/SEP_Web/blob/main/LICENSE.txt)


