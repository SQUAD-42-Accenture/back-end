# 📖 Sobre o Projeto
![Yuppies Collage General LinkdIn Banner](https://github.com/user-attachments/assets/bafca917-d9f6-4b58-8dfd-0b9649e87627)

O **Serve Pro** foi desenvolvido durante a Residência Tecnológica do Porto Digital, tem como objetivo desenvolver uma plataforma completa e fácil de usar para gerenciar os serviços de uma assistência técnica de informática. A plataforma visa auxiliar no controle de demandas, agendamento de serviços, acompanhamento de reparos, emissão de relatórios e muito mais, proporcionando uma experiência eficiente e organizada.


<br><br>
# 🎨 Equipe Atlas

Nossa equipe, a Atlas, responsável pelo desenvolvimento do back-end da plataforma, garante a performance e qualidade dos serviços e funcionalidades que permitem um gerenciamento eficiente.
<br><br>
| Tech Lead                          | SET/Dev                         | Dev                           | Dev                       |
|-----------------------------------|-----------------------------|-------------------------------|---------------------------|
| [![ViniciusRKX](https://github.com/user-attachments/assets/123e4c03-bb4c-4b3e-92e1-90a1e2a03580)](https://github.com/ViniciusRKX) | [![Beatriz-Rodriguesx](https://github.com/user-attachments/assets/ff129eeb-34f9-48d4-938c-1060fb29e76f)](https://github.com/Beatriz-Rodriguesx) | [![LuizSairaf](https://github.com/user-attachments/assets/7cffc2e7-4dd3-498c-b3ef-296fa133b12e)](https://github.com/LuizSairaf) | [![Rogerio-07](https://github.com/user-attachments/assets/dfb85649-ddc2-4414-8666-4c9b40fc2d61)](https://github.com/Rogerio-07) |
| [ViniciusRKX](https://github.com/ViniciusRKX)  | [Beatriz-Rodriguesx](https://github.com/Beatriz-Rodriguesx)  | [LuizSairaf](https://github.com/LuizSairaf)  | [Rogerio-07](https://github.com/gabrielnotty](https://github.com/Rogerio-07))  |

<br><br>
# 🚀 Tecnologias Utilizadas
| Back-end                          | Framework                         | Banco de dados                           |
|-----------------------------------|-----------------------------|-------------------------------|
<div align="left">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" height="60" width="90" alt="csharp logo"  /> 
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" height="60" width="100"  alt="dotnetcore logo"  /> 
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/postgresql/postgresql-original.svg" height="60" width="150" alt="postgresql logo"  />
</div><br><br>

Utilizamos o framework ASP.NET Core MVC desenvolvimento web de código aberto, poderoso e moderno, para construção da nossa aplicação.Ele permite a criação de aplicações web usando o padrão Model-View-Controller (MVC), que separa as responsabilidades da aplicação em três partes principais:

**🎲Model:** Representa os dados da aplicação e a lógica de negócios.<br>
**📟View:** Responsável pela interface do usuário, renderizando a saída para o navegador.<br>
**⚙️Controller:** Recebe as requisições do usuário, interage com o Model, decide qual View deve ser renderizada e envia a resposta para o navegador.<br><br>

# 💻 Funcionalidades

- **🙍Gerenciamento de Usuários:** Cadastro e autenticação de clientes e prestadores de serviços.

- **🧾Gerenciamento de Serviços:** Criação, atualização e acompanhamento de solicitações de serviços.

- **💵Sistema de Pagamento:** Integração com gateways de pagamento para processamento de pagamentos.

- **💬Comunicação:** Sistema de mensagens para comunicação entre clientes e prestadores de serviços.

- **📚Relatórios e Análise:** Gerar relatórios sobre serviços, pagamentos e desempenho.<br><br>


# **⚙️Api**

<img width="946" alt="apiservpro" src="https://github.com/user-attachments/assets/1357bf66-61de-46e8-b70c-d257ec271660">
<img width="941" alt="apiadministrador" src="https://github.com/user-attachments/assets/e014ac05-a3f6-465a-b32c-8ab863cae44d">
<img width="944" alt="apicliente" src="https://github.com/user-attachments/assets/3926c148-3ebf-436d-9208-40c7b4db2ee1">
<img width="945" alt="apiconta" src="https://github.com/user-attachments/assets/a8542bb5-66f7-4865-ba9f-560dfb96dd2b">
<img width="944" alt="apiequipamento" src="https://github.com/user-attachments/assets/0b793598-64a6-42ca-91bf-5afcac7bdf3d">
<img width="943" alt="apihistoricoos" src="https://github.com/user-attachments/assets/f1a7091e-9da0-47ea-be78-a92e5e9dd776">
<img width="945" alt="apiordemdeserviço" src="https://github.com/user-attachments/assets/b5578338-acb4-4d4d-97f7-69febca51fdb">
<img width="944" alt="apiproduto" src="https://github.com/user-attachments/assets/e602c316-4290-4c6f-b00d-877543ea0458">
<img width="947" alt="apitecnico" src="https://github.com/user-attachments/assets/d7e7220b-5edc-4e98-8a04-2b15c15d3c32">
<img width="943" alt="apiusuario" src="https://github.com/user-attachments/assets/08140abc-bc1e-417a-a7b3-0faa5eb971f4"><br><br>


# **🎲Schema banco de dados**

<img width="944" alt="apischema" src="https://github.com/user-attachments/assets/8d906182-914e-4420-b5c2-170e6d6a04c5">

# **🎲Testes Automatizados **SERVPRO** **

## Testes no .NET

Os testes do projeto foram implementados utilizando o framework de testes **xUnit**, uma das bibliotecas de testes mais populares e amplamente usadas no ecossistema .NET. Além disso, utilizamos outras ferramentas para garantir que os testes sejam eficientes e bem integrados ao processo de desenvolvimento.

### Ferramentas e Frameworks Utilizados

1. **xUnit**:
   - O xUnit é um framework de testes unitários que foi utilizado para escrever os testes para o projeto. Ele fornece uma maneira simples e eficiente de organizar e executar testes.
   - Pacote NuGet: `xunit`

2. **Microsoft.EntityFrameworkCore.InMemory**:
   - Para os testes que envolvem interações com o banco de dados, usamos o pacote `Microsoft.EntityFrameworkCore.InMemory`. Este pacote permite que criemos um banco de dados em memória, que é útil para testar o acesso ao banco de dados sem a necessidade de uma instância de banco de dados real.
   - Pacote NuGet: `Microsoft.EntityFrameworkCore.InMemory`

3. **Mocking com Moq**:
   - Utilizamos a biblioteca **Moq** para criar objetos simulados (mocks), permitindo testar partes isoladas do código sem a necessidade de depender de implementações reais de serviços ou repositórios.
   - Pacote NuGet: `Moq`

4. **FluentAssertions**:
   - Para melhorar a legibilidade dos testes e permitir uma sintaxe mais fluente nas asserções, usamos a biblioteca **FluentAssertions**. Com ela, é possível escrever verificações de maneira mais natural e expressiva.
   - Pacote NuGet: `FluentAssertions`

# Estrutura dos Testes

Os testes estão organizados na pasta `SERVPRO.Tests`, que contém o projeto de testes. Este projeto foi criado usando o modelo de projeto **xUnit**:


## Cenários de Teste para `AdministradorRepositorio`

Este arquivo descreve os cenários de teste para o repositório `AdministradorRepositorio`, utilizado para realizar operações no banco de dados com a entidade `Administrador`.

## Cenários de Teste

### 1. Adicionar Administrador
**Objetivo**: Verificar se um administrador é corretamente adicionado ao banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se o administrador é adicionado com todos os dados corretamente preenchidos.
- **Ação**: Chamar o método `Adicionar` passando um objeto `Administrador`.
- **Resultado Esperado**: O administrador deve ser adicionado ao banco e o CPF deve ser igual ao informado.

---

### 2. Buscar Todos os Administradores
**Objetivo**: Verificar se todos os administradores cadastrados são retornados corretamente.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `BuscarTodosAdministradores` todos os administradores armazenados no banco são retornados.
- **Ação**: Chamar o método `BuscarTodosAdministradores` após adicionar dois administradores ao banco de dados.
- **Resultado Esperado**: O número total de administradores retornados deve ser igual ao número de administradores cadastrados, e todos devem ser retornados corretamente.

---

### 3. Buscar Administrador por CPF
**Objetivo**: Verificar se um administrador pode ser encontrado corretamente pelo seu CPF.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar um administrador pelo CPF, o administrador correspondente é retornado.
- **Ação**: Chamar o método `BuscarPorCPF` passando um CPF válido de um administrador existente.
- **Resultado Esperado**: O administrador encontrado deve ter o CPF igual ao CPF informado na busca e seus dados devem ser correspondentes aos dados armazenados no banco de dados.

---

### 4. Atualizar Dados do Administrador
**Objetivo**: Verificar se os dados de um administrador podem ser atualizados corretamente no banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao atualizar os dados de um administrador, as mudanças são refletidas corretamente.
- **Ação**: Chamar o método `Atualizar` após modificar alguns dados de um administrador existente (como nome, departamento, etc.).
- **Resultado Esperado**: O administrador deve ser atualizado no banco de dados com os novos dados, e os dados alterados devem corresponder ao que foi informado na atualização.

---

### 5. Apagar Administrador
**Objetivo**: Verificar se um administrador pode ser removido corretamente do banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `Apagar` com o CPF de um administrador existente, o administrador é removido do banco.
- **Ação**: Chamar o método `Apagar` passando o CPF de um administrador já existente no banco.
- **Resultado Esperado**: O administrador deve ser removido com sucesso e ao tentar buscar esse administrador pelo CPF, o resultado deve ser `null` (administrador não encontrado).

---

## Cenários de Teste para `ClienteRepositorio`

Este arquivo descreve os cenários de teste para o repositório `ClienteRepositorio`, utilizado para realizar operações no banco de dados com a entidade `Cliente`.

## Cenários de Teste

### 1. Atualizar Dados do Cliente
**Objetivo**: Verificar se os dados de um cliente podem ser atualizados corretamente no banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao atualizar os dados de um cliente, as mudanças são refletidas corretamente.
- **Ação**: Chamar o método `Atualizar` após modificar alguns dados do cliente (como senha, tipo de usuário, e data de nascimento).
- **Resultado Esperado**: O cliente deve ser atualizado no banco de dados com os novos dados, e os dados alterados devem corresponder aos que foram informados na atualização.

---

### 2. Buscar Todos os Clientes
**Objetivo**: Verificar se todos os clientes cadastrados são retornados corretamente.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `BuscarTodosClientes` todos os clientes cadastrados são retornados.
- **Ação**: Chamar o método `BuscarTodosClientes` após adicionar dois clientes ao banco de dados.
- **Resultado Esperado**: O número total de clientes retornados deve ser igual ao número de clientes cadastrados, e todos devem ser retornados corretamente.

---

### 3. Buscar Cliente por CPF
**Objetivo**: Verificar se um cliente pode ser encontrado corretamente pelo seu CPF.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar um cliente pelo CPF, o cliente correspondente é retornado.
- **Ação**: Chamar o método `BuscarPorCPF` passando o CPF de um cliente existente.
- **Resultado Esperado**: O cliente encontrado deve ter o CPF igual ao CPF informado e seus dados devem ser correspondentes aos dados armazenados no banco de dados.

---

### 4. Adicionar Cliente
**Objetivo**: Verificar se um cliente é corretamente adicionado ao banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se o cliente é adicionado corretamente com todos os dados preenchidos.
- **Ação**: Chamar o método `Adicionar` passando um objeto `Cliente`.
- **Resultado Esperado**: O cliente deve ser adicionado ao banco de dados, e o CPF do cliente deve ser igual ao informado.
- 
---

### 5. Apagar Cliente
**Objetivo**: Verificar se um cliente pode ser removido corretamente do banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `Apagar` com o CPF de um cliente existente, o cliente é removido do banco.
- **Ação**: Chamar o método `Apagar` passando o CPF de um cliente já existente no banco.
- **Resultado Esperado**: O cliente deve ser removido com sucesso e ao tentar buscar esse cliente pelo CPF, o resultado deve ser `null` (cliente não encontrado).

---

## Cenários de Teste para `TecnicoRepositorio`

Este arquivo descreve os cenários de teste para o repositório `TecnicoRepositorio`, utilizado para realizar operações no banco de dados com a entidade `Tecnico`.

## Cenários de Teste

### 1. Buscar Técnico por CPF
**Objetivo**: Verificar se um técnico pode ser encontrado corretamente pelo seu CPF.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar um técnico pelo CPF, o técnico correspondente é retornado.
- **Ação**: Chamar o método `BuscarPorCPF` passando o CPF de um técnico existente.
- **Resultado Esperado**: O técnico encontrado deve ter o CPF igual ao CPF informado e seus dados (como nome e especialidade) devem ser correspondentes aos dados armazenados no banco de dados.
---

### 2. Buscar Todos os Técnicos
**Objetivo**: Verificar se todos os técnicos cadastrados são retornados corretamente.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `BuscarTodosTecnicos` todos os técnicos cadastrados são retornados.
- **Ação**: Chamar o método `BuscarTodosTecnicos` e verificar o número de técnicos no banco de dados.
- **Resultado Esperado**: O número de técnicos retornados deve ser igual ao número de técnicos cadastrados, e todos devem ser retornados corretamente.

---

### 3. Adicionar Técnico
**Objetivo**: Verificar se um técnico é corretamente adicionado ao banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se o técnico é adicionado corretamente com todos os dados preenchidos.
- **Ação**: Chamar o método `Adicionar` passando um objeto `Tecnico`.
- **Resultado Esperado**: O técnico deve ser adicionado ao banco de dados, e o CPF do técnico deve ser igual ao informado.

---

### 4. Apagar Técnico Existente
**Objetivo**: Verificar se um técnico pode ser removido corretamente do banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao chamar o método `Apagar` com o CPF de um técnico existente, o técnico é removido do banco de dados.
- **Ação**: Chamar o método `Apagar` passando o CPF de um técnico já existente no banco.
- **Resultado Esperado**: O técnico deve ser removido com sucesso e ao tentar buscar esse técnico pelo CPF, o resultado deve ser `null` (técnico não encontrado).

---

# Cenários de Teste para `UsuarioRepositorio`

Este arquivo descreve os cenários de teste para o repositório `UsuarioRepositorio`, que realiza operações no banco de dados com a entidade `Usuario`.

## Cenários de Teste

### 1. Buscar Usuário por CPF
**Objetivo**: Verificar se um usuário pode ser recuperado corretamente pelo seu CPF.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar um usuário pelo CPF, o usuário correspondente é retornado.
- **Ação**: Chamar o método `BuscarPorCpf` passando o CPF de um usuário.
- **Resultado Esperado**: O usuário retornado deve ter o CPF igual ao informado e os dados do usuário devem ser corretos.

---

### 2. Buscar Usuários por Tipo de Usuário
**Objetivo**: Verificar se os usuários de um tipo especificado são retornados corretamente.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar usuários por tipo, todos os usuários do tipo especificado são retornados.
- **Ação**: Chamar o método `BuscarPorTipoUsuario` passando um tipo de usuário (ex: "Tecnico").
- **Resultado Esperado**: Todos os usuários retornados devem ter o tipo de usuário correspondente ao informado.

---

### 3. Buscar Todos os Usuários
**Objetivo**: Verificar se todos os usuários são corretamente retornados do banco de dados.

#### Caso de Teste:
- **Descrição**: O teste valida se ao buscar todos os usuários, a lista de usuários é retornada corretamente.
- **Ação**: Chamar o método `BuscarTodosUsuarios`.
- **Resultado Esperado**: A lista de usuários não deve estar vazia e o número de usuários retornados deve ser o esperado (2 no caso de dados iniciais).


<br><br>
# **📱Como Rodar o Projeto**

1. Clone o repositório:
   ```bash
   git clone https://github.com/SQUAD-42-Accenture/back-end.git

2. Entrar na pasta SERVPRO:<br><br>
   <img width="227" alt="pastprojeto" src="https://github.com/user-attachments/assets/61da96bb-6eb5-4dae-ab41-1ed096e5fd4a">

3. Executar o arquivo SERVPRO.sln via visual studio:<br><br>
   <img width="228" alt="servpro_sln" src="https://github.com/user-attachments/assets/9c988cd9-9c9b-49f0-bd5c-8d04ccf8f6da">


