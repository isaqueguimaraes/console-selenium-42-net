using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace ConsoleSelenium.Tests;

[TestClass]
public class BuscaDesafioTest
{
  private const string UrlBase = "https://duckduckgo.com";

  [TestMethod]
  public void Exercicio1_PesquisaBasica_ValidacaoTitulo()
  {
    IWebDriver driver = new ChromeDriver();
    try
    {
      // 1. Navegar até a URL base
      driver.Navigate().GoToUrl(UrlBase);

      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

      // 2. Localizar o campo de pesquisa (name="q")
      var searchBox = wait.Until(d => d.FindElement(By.Name("q")));

      // 3. Digitar o termo e 4. Pressionar Enter
      string termoPesquisa = "Selenium WebDriver C#";
      searchBox.SendKeys(termoPesquisa + Keys.Enter);

      // 5. Aguardar carregamento e validar título ou URL
      wait.Until(d => d.Title.Contains("Selenium", StringComparison.OrdinalIgnoreCase)
                   || d.Url.Contains("Selenium", StringComparison.OrdinalIgnoreCase));

      Assert.IsTrue(
          driver.Title.Contains("Selenium", StringComparison.OrdinalIgnoreCase) ||
          driver.Url.Contains("Selenium", StringComparison.OrdinalIgnoreCase),
          $"Título atual: {driver.Title} | URL atual: {driver.Url}"
      );
    }
    finally
    {
      driver.Quit();
    }
  }

  [TestMethod]
  public void Exercicio2_Autocompletar_Suggestions()
  {
    IWebDriver driver = new ChromeDriver();
    try
    {
      // 1. Acessar a página inicial do DuckDuckGo
      driver.Navigate().GoToUrl(UrlBase);

      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

      // 2. Digitar o termo sem pressionar Enter
      var searchBox = wait.Until(d => d.FindElement(By.Name("q")));
      searchBox.SendKeys("C# unit testing");

      // 3. Espera explícita pela listagem de sugestões de autocompletar no DOM
      var sugestoes = wait.Until(d =>
      {
        var itens = d.FindElements(By.CssSelector("[role='option'], .search-suggestions-list li, .sbox-suggestions li"));
        return itens.Count >= 3 ? itens : null;
      });

      // 4. Clicar na terceira opção sugerida (índice 2)
      string textoTerceiraOpcao = sugestoes[2].Text;
      sugestoes[2].Click();

      // 5. Garantir redirecionamento para os resultados da opção escolhida
      wait.Until(d => d.Url.Contains("q="));
      Assert.IsTrue(driver.Url.Contains("q="), "A URL não redirecionou para a página de resultados.");
    }
    finally
    {
      driver.Quit();
    }
  }

  [TestMethod]
  public void Exercicio3_NavegacaoHistorico_VoltarEAvancar()
  {
    IWebDriver driver = new ChromeDriver();
    try
    {
      // 1. Realizar busca inicial por "Automated Testing"
      driver.Navigate().GoToUrl(UrlBase);
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

      var searchBox = wait.Until(d => d.FindElement(By.Name("q")));
      searchBox.SendKeys("Automated Testing" + Keys.Enter);

      // Aguardar resultados orgânicos carregarem
      var primeiroResultado = wait.Until(d => d.FindElement(By.CssSelector("a[data-testid='result-title-a']")));
      string urlResultados = driver.Url;

      // 2. Clicar no primeiro link orgânico relevante
      primeiroResultado.Click();

      // 3. Validar que a página externa foi carregada verificando a URL atual
      wait.Until(d => !d.Url.Contains("duckduckgo.com"));
      Assert.IsFalse(driver.Url.Contains("duckduckgo.com"), "O navegador não saiu da página do DuckDuckGo.");

      // 4. Utilizar o comando Back() para retornar à página de resultados
      driver.Navigate().Back();

      // 5. Validar que a página de resultados foi restaurada corretamente
      wait.Until(d => d.Url.Contains("duckduckgo.com"));
      Assert.IsTrue(driver.Url.Contains("duckduckgo.com"), "A página de resultados do DuckDuckGo não foi restaurada.");
    }
    finally
    {
      driver.Quit();
    }
  }
}