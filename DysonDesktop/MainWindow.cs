using Gtk;
using System;
using System.IO;
using Windows;
using Action = System.Action;

namespace DysonDesktop;

public class MainWindow : Window
{
    private Box _contentArea; // Área onde as views serão trocadas

    public MainWindow() : base("Dyson.AI Desktop Client")
    {
        SetDefaultSize(1280, 720);
        SetPosition(WindowPosition.Center);

        // Carregar CSS
        var cssProvider = new CssProvider();
        string cssPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Styles","Style.css");
        Console.WriteLine(cssPath);
        if (File.Exists(cssPath))
        {
            cssProvider.LoadFromPath(cssPath);
            StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, 800);
        }

        DeleteEvent += delegate { Application.Quit(); };

        var mainLayout = new HBox(false, 0);

        // --- Sidebar ---
        var sidebar = new VBox(false, 0);
        sidebar.WidthRequest = 250;
        sidebar.StyleContext.AddClass("sidebar");

        var lblProfile = new Label("Mauricio Paixao") { MarginTop = 20, MarginBottom = 20 };
        sidebar.PackStart(lblProfile, false, false, 0);

        // Botões de Navegação (Agora com eventos de clique)
        sidebar.PackStart(CreateNavButton("Dashboard", () => NavigateTo(new DashboardView())), false, false, 0);
        sidebar.PackStart(CreateNavButton("Smart Node", () => NavigateTo(new SmartNodeView())), false, false,
            
            0);
        sidebar.PackStart(CreateNavButton("AI.Agents", () => NavigateTo(new AIagent())), false, false,
            
            0);
        sidebar.PackStart(CreateNavButton("Orders", () => NavigateTo(new OrdersView())), false, false, 0);

        mainLayout.PackStart(sidebar, false, false, 0);

        // --- Área de Conteúdo ---
        // Usamos um VBox para o container principal da direita
        var rightColumn = new VBox(false, 0);
        rightColumn.StyleContext.AddClass("view-container");

        // Top Bar
        var topBar = new HBox(false, 10);
        topBar.PackEnd(new Label("Sair") { MarginRight = 20 }, false, false, 0);
        rightColumn.PackStart(topBar, false, false, 10);

        // Placeholder para o conteúdo dinâmico
        _contentArea = new VBox(false, 0);
        rightColumn.PackStart(_contentArea, true, true, 0);

        mainLayout.PackStart(rightColumn, true, true, 0);
        this.Add(mainLayout);

        // Iniciar na Dashboard
        NavigateTo(new DashboardView());
    }

    // Método para criar botões com ação de clique
    private Button CreateNavButton(string labelText, Action onClickAction)
    {
        var btn = new Button();
        btn.Relief = ReliefStyle.None;
        btn.StyleContext.AddClass("nav-button");

        var lbl = new Label(labelText) { Halign = Align.Start };
        btn.Add(lbl);

        // Conectar evento Click
        btn.Clicked += (sender, e) => onClickAction();

        return btn;
    }

    // Método que troca a tela
    private void NavigateTo(Widget newView)
    {
        // 1. Remove todos os widgets atuais da área de conteúdo
        foreach (Widget child in _contentArea.Children)
        {
            _contentArea.Remove(child);
        }

        // 2. Adiciona a nova View
        _contentArea.PackStart(newView, true, true, 0);

        // 3. Força o GTK a redesenhar os widgets novos
        _contentArea.ShowAll();
    }
}