using Gtk;

namespace Windows;

public class OrdersView : VBox
{
    public OrdersView()
    {
        this.Spacing = 15;

        var lblTitle = new Label("Histórico de Compras");
        lblTitle.Halign = Align.Start;
        lblTitle.StyleContext.AddClass("card-title");
        this.PackStart(lblTitle, false, false, 0);

        // Barra de Filtro
        var filterBox = new HBox(false, 5);

        var lblSearch = new Label("Pesquisar Pedido:");
        var entrySearch = new Entry { PlaceholderText = "ID do Pedido, Data ou Item..." };
        var btnSearch =
            new Button("Buscar"); // GTK padrão vem com ícone de lupa se usar Stock icons, mas usaremos texto
        btnSearch.StyleContext.AddClass("nav-button"); // Reusa estilo simples

        filterBox.PackStart(lblSearch, false, false, 0);
        filterBox.PackStart(entrySearch, true, true, 0);
        filterBox.PackStart(btnSearch, false, false, 0);

        this.PackStart(filterBox, false, false, 10);

        // Tabela
        var tree = new TreeView();

        // Colunas
        CreateColumn(tree, "ID Pedido", 0);
        CreateColumn(tree, "Data", 1);
        CreateColumn(tree, "Itens", 2);
        CreateColumn(tree, "Total", 3);
        CreateColumn(tree, "Método", 4);
        CreateColumn(tree, "Status", 5);
        CreateColumn(tree, "Ações", 6);

        // Dados (Simulando "Nenhuma compra encontrada" ou dados vazios)
        var store = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string),
            typeof(string), typeof(string));

        // Adicionar linha vazia ou mensagem
        store.AppendValues("-", "-", "Nenhuma compra encontrada.", "-", "-", "-", "-");

        tree.Model = store;
        this.PackStart(tree, true, true, 0);
    }

    private void CreateColumn(TreeView tree, string title, int id)
    {
        var col = new TreeViewColumn { Title = title };
        var cell = new CellRendererText();
        col.PackStart(cell, true);
        col.AddAttribute(cell, "text", id);
        tree.AppendColumn(col);
    }
}