using System.Net;
using Windows;
using DysonDesktop.Services;
using Gtk;

namespace Windows;

public class DashboardView : VBox
{
    private ApiService _apiService;

    public DashboardView()
    {
        _apiService = new ApiService();
        this.Spacing = 20;

        // 1. Título
        var lblTitle = new Label("Dashboard");
        lblTitle.Halign = Align.Start;
        lblTitle.StyleContext.AddClass("card-title");
        this.PackStart(lblTitle, false, false, 0);

        // 2. Cards (FlowBox ou HBox)
        var cardsBox = new HBox(true, 20); // Homogeneo = true para tamanhos iguais

        cardsBox.PackStart(CreateCard("13.00 Dtc", "Saldo em Carteira", "Gerenciar ->", "card-blue"), true, true, 0);
        cardsBox.PackStart(CreateCard("0.00 Dtc", "Total em Staking", "Ver Detalhes ->", "card-green"), true, true, 0);
        cardsBox.PackStart(CreateCard("5", "Contratos Inteligentes", "Administrar ->", "card-red"), true, true, 0);

        this.PackStart(cardsBox, false, false, 0);

        // 3. Título Transações
        var lblTrans = new Label("Últimas Transações");
        lblTrans.Halign = Align.Start;
        this.PackStart(lblTrans, false, false, 5);

        // 4. Tabela (TreeView)
        var treeView = CreateTransactionTable();
        this.PackStart(treeView, true, true, 10);

        // Carregar dados
        LoadData(treeView);
    }

    private Widget CreateCard(string title, string subtitle, string actionText, string cssClass)
    {
        var eventBox = new EventBox(); // Necessário para aplicar cor de fundo
        var vBox = new VBox(false, 5);
        vBox.StyleContext.AddClass("card-box");
        vBox.StyleContext.AddClass(cssClass);

        var lblValue = new Label(title) { Halign = Align.Start };
        lblValue.StyleContext.AddClass("card-title");

        var lblSub = new Label(subtitle) { Halign = Align.Start };

        var btnAction = new Label(actionText) { Halign = Align.End, MarginTop = 20 };
        btnAction.StyleContext.AddClass("card-subtitle");

        vBox.PackStart(lblValue, false, false, 0);
        vBox.PackStart(lblSub, false, false, 0);
        vBox.PackStart(btnAction, false, false, 0);

        eventBox.Add(vBox);
        return eventBox;
    }

    private TreeView CreateTransactionTable()
    {
        var tree = new TreeView();

        // Definir colunas
        AppendColumn(tree, "Hash ID", 0);
        AppendColumn(tree, "De", 1);
        AppendColumn(tree, "Para", 2);
        AppendColumn(tree, "Valor", 3);
        AppendColumn(tree, "Data", 4);

        return tree;
    }

    private void AppendColumn(TreeView tree, string title, int id)
    {
        var column = new TreeViewColumn { Title = title };
        var cell = new CellRendererText();
        column.PackStart(cell, true);
        column.AddAttribute(cell, "text", id);
        tree.AppendColumn(column);
    }

    private async void LoadData(TreeView tree)
    {
        // Modelo de dados para a TreeView (ListStore de strings)
        var listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

        var transactions = await _apiService.GetTransactionsAsync();

        foreach (var t in transactions)
        {
            listStore.AppendValues(t.HashId, t.From, t.To, t.Amount, t.Date);
        }

        tree.Model = listStore;
    }
}