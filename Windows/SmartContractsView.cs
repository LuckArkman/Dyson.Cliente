using Gtk;

namespace Windows;

public class SmartContractsView : VBox
{
    public SmartContractsView()
    {
        this.Spacing = 15;

        // Header com Botão Deploy
        var headerBox = new HBox(false, 0);
        var lblTitle = new Label("Gerenciar Contratos Inteligentes");
        lblTitle.StyleContext.AddClass("card-title");

        var btnDeploy = new Button("+ Deploy Novo Contrato");
        btnDeploy.StyleContext.AddClass("btn-green");
        btnDeploy.StyleContext.AddClass("btn-action");

        headerBox.PackStart(lblTitle, false, false, 0);
        headerBox.PackEnd(btnDeploy, false, false, 0);

        this.PackStart(headerBox, false, false, 10);

        // Tabela
        var tree = new TreeView();
        SetupColumns(tree);
        LoadData(tree);

        this.PackStart(tree, true, true, 0);
    }

    private void SetupColumns(TreeView tree)
    {
        AppendColumn(tree, "ID do Contrato", 0);
        AppendColumn(tree, "Nome", 1);
        AppendColumn(tree, "Tipo", 2);
        AppendColumn(tree, "Status", 3);
        AppendColumn(tree, "Gas Consumido", 4);
        AppendColumn(tree, "Ações", 5);
    }

    private void AppendColumn(TreeView tree, string title, int id)
    {
        var column = new TreeViewColumn { Title = title };
        var cell = new CellRendererText();

        // Customização simples de cor baseada no texto (simplificado para GTK)
        if (title == "Status")
        {
            // Lógica visual avançada requer CellDataFunc, vamos manter simples por enquanto
        }

        column.PackStart(cell, true);
        column.AddAttribute(cell, "text", id);
        tree.AppendColumn(column);
    }

    private void LoadData(TreeView tree)
    {
        var store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string));

        store.AppendValues("94e4bb89...", "Royalty Distribution", "Payment", "Ativo", "0.0042 ETH",
            "Detalhes | Pausar");
        store.AppendValues("c42b29ce...", "Content Copyright", "NFT", "Pendente", "0.0000 ETH", "Detalhes | Pausar");
        store.AppendValues("1eb26e25...", "DAO Vote", "Governance", "Executado", "0.0120 ETH", "Detalhes | Pausar");

        tree.Model = store;
    }
}