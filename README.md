## Desafio Técnico - Backend

Este repositório contém o desafio técnico do processo seletivo para professor c# da let's code

### Usage

0. Vá para a pasta 'BACK'
1. Preencha o arquivo com os segredos da aplicação. Um arquivo exemplo (secrets.json) foi disponibilizado. Você pode usar o VS para gerenciar os segredos de usuário, adicionar os valores direto no arquivo de appsettings.json ou usar a linha de comando
   ```sh
   dotnet user-secrets set "TokenKey" "very long and secret token with some numbers in it"
   dotnet user-secrets set "Login" "letscode"
   dotnet user-secrets set "Password" "lets@123"
   ```
2. Subir o container da aplicação
   ```sh
   docker-compose up
   ```
