using System.Net;
using System.IO;
using Windows;
using Gtk;


namespace DysonDesktop;

public class MainWindow : Window
{
    public MainWindow() : base("Dyson.AI Desktop Client")
    {
        SetDefaultSize(1200, 700);
        SetPosition(WindowPosition.Center);

        // Carregar CSS
        var cssProvider = new CssProvider();
        string cssPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"Styles", "Style.css");
        if (File.Exists(cssPath))
        {
            
            cssProvider.LoadFromPath(cssPath);
            StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, 800);
        }

        DeleteEvent += delegate { Application.Quit(); };

        // Layout Principal: HBox (Sidebar | Conteúdo)
        var mainLayout = new HBox(false, 0);

        // --- Sidebar Esquerda ---
        var sidebar = new VBox(false, 0);
        sidebar.WidthRequest = 250;
        sidebar.StyleContext.AddClass("sidebar");

        // Perfil
        var lblProfile = new Label("Mauricio Paixao") { MarginTop = 20, MarginBottom = 20 };
        sidebar.PackStart(lblProfile, false, false, 0);

        // Botões de Navegação
        sidebar.PackStart(CreateNavButton("Dashboard", true), false, false, 0);
        sidebar.PackStart(CreateNavButton("Smart Wallet & Trade", false), false, false, 0);
        sidebar.PackStart(CreateNavButton("Smart Contracts", false), false, false, 0);
        sidebar.PackStart(CreateNavButton("Orders", false), false, false, 0);

        mainLayout.PackStart(sidebar, false, false, 0);

        // --- Área de Conteúdo (Direita) ---
        var contentArea = new VBox(false, 0);
        contentArea.StyleContext.AddClass("view-container");

        // Top Bar simples
        var topBar = new HBox(false, 10);
        topBar.PackEnd(new Label("Sair") { MarginRight = 20 }, false, false, 0);
        contentArea.PackStart(topBar, false, false, 10);

        // Injetar a View do Dashboard
        var dashboard = new DashboardView();
        contentArea.PackStart(dashboard, true, true, 0);

        mainLayout.PackStart(contentArea, true, true, 0);

        this.Add(mainLayout);
    }

    private Button CreateNavButton(string label, bool isActive)
    {
        var btn = new Button(label);
        btn.Relief = ReliefStyle.None; // Remove bordas padrão
        btn.StyleContext.AddClass("nav-button");
        // Se quiser ícones, pode adicionar um HBox dentro do botão com Image + Label
        return btn;
    }
}