# 🦆 ConsoleSelenium42

Solução em **C# / .NET 10** para **automação de testes web com Selenium WebDriver e MSTest**, utilizando cenários reais de busca no **[DuckDuckGo](https://duckduckgo.com)**. Os testes cobrem pesquisa básica, autocompletar dinâmico (com espera explícita) e navegação por histórico do navegador.

Projeto desenvolvido individualmente para a disciplina **Garantia da Qualidade de Software** (Gestão e Qualidade de Software, Ânima Educação), com foco em testes automatizados de interface.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Selenium](https://img.shields.io/badge/Selenium-WebDriver-43B02A?logo=selenium&logoColor=white)
![MSTest](https://img.shields.io/badge/tests-MSTest-5E2D91)

---

## 📑 Sumário

- [Visão geral](#-visão-geral)
- [Requisitos técnicos](#-requisitos-técnicos)
- [Estrutura do projeto](#-estrutura-do-projeto)
- [Como o projeto foi criado](#-como-o-projeto-foi-criado)
- [Como clonar e compilar](#-como-clonar-e-compilar)
- [Como executar os testes](#-como-executar-os-testes)
- [Cenários de teste automatizados](#-cenários-de-teste-automatizados)
- [Boas práticas adotadas](#-boas-práticas-adotadas)
- [Observações importantes](#-observações-importantes)
- [Tecnologias utilizadas](#-tecnologias-utilizadas)
- [Autor](#-autor)

---

## 📖 Visão geral

A solução é composta por dois projetos:

| Projeto | Tipo | Responsabilidade |
|---------|------|------------------|
| `ConsoleSelenium` | Console Application (.NET 10) | Projeto principal, responsável por centralizar scripts e classes auxiliares de automação. |
| `ConsoleSelenium.Tests` | MSTest | Projeto de testes que utiliza **Selenium.WebDriver** e **Selenium.Support** para executar os cenários de automação, referenciando o projeto principal. |

Os testes abrem um navegador real, acessam o DuckDuckGo, realizam buscas e validam os resultados com `Assert`.

---

## 🛠 Requisitos técnicos

- **[.NET SDK 10.0](https://dotnet.microsoft.com/download)** (ou superior)
- **[Git](https://git-scm.com/downloads)**
- **Google Chrome** instalado (os testes utilizam o `ChromeDriver`)
- **Conexão com a internet** (os cenários acessam o site real do DuckDuckGo e o Selenium baixa automaticamente o driver do navegador, se necessário)
- Terminal (PowerShell, CMD, Bash, Zsh etc.)
- IDE opcional: Visual Studio 2022+, VS Code (com extensão C# Dev Kit) ou JetBrains Rider

Para verificar a versão do .NET instalada:

```bash
dotnet --version
```

---

## 📂 Estrutura do projeto

```text
console-selenium-42-net/
├── ConsoleSelenium42.slnx
├── ConsoleSelenium/
│   ├── ConsoleSelenium.csproj
│   └── Program.cs
├── ConsoleSelenium.Tests/
│   ├── ConsoleSelenium.Tests.csproj
│   └── BuscaDesafioTest.cs
├── .gitignore
└── README.md
```

- **`ConsoleSelenium`**: projeto de console (`net10.0`) com o ponto de entrada e as classes auxiliares de automação.
- **`ConsoleSelenium.Tests`**: projeto de testes MSTest (`net10.0`) com a classe `BuscaDesafioTest`, que reúne os três cenários automatizados.

---

## 🏗 Como o projeto foi criado

A solução foi criada do zero via .NET CLI, seguindo os passos abaixo:

```bash
# 1. Criar a solution e entrar no diretório
dotnet new sln -n ConsoleSelenium42
cd ConsoleSelenium42

# 2. Criar o projeto principal (Console Application, .NET 10)
dotnet new console -n ConsoleSelenium

# 3. Criar o projeto de testes (MSTest)
dotnet new mstest -n ConsoleSelenium.Tests

# 4. Adicionar os projetos à solution
dotnet sln add ConsoleSelenium/ConsoleSelenium.csproj
dotnet sln add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj

# 5. Referenciar o projeto principal no projeto de testes
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj reference ConsoleSelenium/ConsoleSelenium.csproj

# 6. Instalar os pacotes NuGet de automação no projeto de testes
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj package Selenium.WebDriver
dotnet add ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj package Selenium.Support
```

> Esses comandos **não precisam ser executados** para rodar o projeto. Eles documentam como a solução foi montada.

---

## 🚀 Como clonar e compilar

### 1. Clonar o repositório

```bash
git clone https://github.com/isaqueguimaraes/console-selenium-42-net.git
```

### 2. Acessar a pasta do projeto

```bash
cd console-selenium-42-net
```

### 3. Restaurar as dependências

```bash
dotnet restore
```

### 4. Compilar a solução

```bash
dotnet build
```

Se tudo estiver correto, a saída final indicará **Build succeeded** (compilação bem-sucedida), com 0 erros.

---

## ✅ Como executar os testes

Na raiz do repositório, execute:

```bash
dotnet test
```

Para uma saída mais detalhada, com o nome de cada teste executado:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Para executar apenas o projeto de testes:

```bash
dotnet test ConsoleSelenium.Tests/ConsoleSelenium.Tests.csproj
```

Durante a execução, o **Chrome será aberto automaticamente** e controlado pelo Selenium. Ao final, os **3 testes** devem ser aprovados, com uma saída semelhante a:

```text
Resumo do teste: total: 3; falhou: 0; bem-sucedido: 3; ignorado: 0
```

> 💡 Também é possível executar os testes pela extensão **C# Dev Kit** do VS Code, utilizando o **Test Explorer**.

---

## 🎯 Cenários de teste automatizados

Os testes estão na classe `BuscaDesafioTest` e utilizam o atributo `[TestMethod]` do MSTest. O site testado é: <https://duckduckgo.com>

### Exercício 1: Pesquisa básica e validação de título

**Objetivo:** automatizar a digitação na barra de pesquisa principal, acionar a submissão e validar o carregamento da página de resultados.

| Passo | Ação |
|:-----:|------|
| 1 | Navegar até a URL base `https://duckduckgo.com`. |
| 2 | Localizar o campo de pesquisa (atributo `name="q"`). |
| 3 | Digitar o termo **"Selenium WebDriver C#"** com `SendKeys`. |
| 4 | Enviar o formulário pressionando `Keys.Enter`. |
| 5 | Validar via `Assert` que o título ou a URL da página de resultados contém o termo pesquisado. |

### Exercício 2: Autocompletar (Suggestions)

**Objetivo:** lidar com menus suspensos dinâmicos gerados via JavaScript enquanto o usuário digita.

| Passo | Ação |
|:-----:|------|
| 1 | Acessar a página inicial do DuckDuckGo. |
| 2 | Digitar o termo **"C# unit testing"** no campo de pesquisa, **sem** pressionar Enter. |
| 3 | Usar **espera explícita** (`WebDriverWait`) para aguardar a lista de sugestões aparecer no DOM (seletores como `.search-suggestions-list li`, `.sbox-suggestions li` e `[role='option']`). |
| 4 | Clicar na **terceira opção** sugerida pela lista dinâmica. |
| 5 | Garantir via `Assert` que a navegação redirecionou corretamente para os resultados da opção escolhida. |

### Exercício 3: Navegação por histórico

**Objetivo:** simular interações complexas de navegação, usando o botão "Voltar" do navegador integrado ao fluxo de teste.

| Passo | Ação |
|:-----:|------|
| 1 | Realizar uma busca inicial por **"Automated Testing"**. |
| 2 | Clicar no primeiro link orgânico relevante da página de resultados, saindo da página de busca. |
| 3 | Validar que a página externa foi carregada, verificando a URL atual (`driver.Url`). |
| 4 | Utilizar `driver.Navigation.Back()` para retornar à página de resultados. |
| 5 | Validar que a página de resultados foi restaurada corretamente. |

---

## 🧹 Boas práticas adotadas

- **Clean Code e modularização**: solução organizada em projetos separados (automação e testes), com nomes claros para classes e métodos.
- **Asserções**: cada cenário valida o resultado esperado com `Assert` do MSTest.
- **Espera explícita**: uso de `WebDriverWait` para aguardar elementos dinâmicos (como as sugestões do autocompletar), tornando os testes mais estáveis.
- **Encerramento do navegador**: o driver é finalizado ao término de cada teste, evitando processos abertos.
- **Commits semânticos**: o histórico segue o padrão de mensagens semânticas, por exemplo:
  - `feat(setup): cria estrutura inicial da solution console-selenium-42`
  - `test(google): implementa cenario de autocompletar`

---

## ⚠️ Observações importantes

- Os testes dependem de um **site real e dinâmico**. Alterações no layout do DuckDuckGo, nos seletores ou no conteúdo dos resultados podem afetar a execução.
- O primeiro link orgânico e as sugestões do autocompletar **podem variar** com o tempo e com a região, por isso as asserções validam o comportamento da navegação, e não textos fixos.
- Se o site exibir uma verificação anti-robô ou limitar as requisições, reexecute os testes após alguns instantes.

---

## 🧰 Tecnologias utilizadas

- [C#](https://learn.microsoft.com/dotnet/csharp/)
- [.NET 10](https://dotnet.microsoft.com/)
- [Selenium WebDriver](https://www.selenium.dev/documentation/webdriver/)
- [MSTest](https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-mstest)
- [DuckDuckGo](https://duckduckgo.com)
- [Git](https://git-scm.com/) e [GitHub](https://github.com/)

---

## 👤 Autor

Desenvolvido por [Isaque Guimarães](https://github.com/isaqueguimaraes)
