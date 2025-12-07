using Gtk;
using Cairo; // Necessário para desenhar o gráfico
using System;

namespace Windows;

public class SmartWalletView : HBox
{
    public SmartWalletView()
    {
        this.Spacing = 20;

        // --- LADO ESQUERDO: Gráfico ---
        var leftPanel = new VBox(false, 10);

        var lblChartTitle = new Label("Flutuação do Token Dtc") { Halign = Align.Start };
        lblChartTitle.StyleContext.AddClass("card-title");
        leftPanel.PackStart(lblChartTitle, false, false, 0);

        // Área de desenho (Canvas)
        var drawingArea = new DrawingArea();
        drawingArea.SetSizeRequest(600, 400);
        drawingArea.Drawn += OnDrawChart;

        var chartFrame = new Frame();
        chartFrame.Add(drawingArea);
        chartFrame.StyleContext.AddClass("card-box");

        leftPanel.PackStart(chartFrame, true, true, 0);

        var lblPrice = new Label("R$ 1.00 PREÇO ATUAL") { Halign = Align.Center, MarginTop = 10 };
        lblPrice.StyleContext.AddClass("status-active");
        leftPanel.PackStart(lblPrice, false, false, 0);

        this.PackStart(leftPanel, true, true, 0);


        // --- LADO DIREITO: Detalhes da Carteira ---
        var rightPanel = new VBox(false, 15);
        rightPanel.WidthRequest = 300;
        rightPanel.StyleContext.AddClass("view-container");

        // Título
        rightPanel.PackStart(new Label("Sua Carteira") { Halign = Align.Start, MarginBottom = 10 }, false, false,
            0);

        // CORREÇÃO AQUI: Criar o label primeiro, depois adicionar a classe
        var lblAmount = new Label("13.0000 Dtc") { Halign = Align.Start };
        lblAmount.StyleContext.AddClass("label-big");
        rightPanel.PackStart(lblAmount, false, false, 0);

        var lblAvail = new Label("Disponível") { Halign = Align.Start };
        lblAvail.StyleContext.AddClass("label-dim");
        rightPanel.PackStart(lblAvail, false, false, 0);

        // Staking
        rightPanel.PackStart(new Label("0.0000 Dtc") { Halign = Align.Start, MarginTop = 10 }, false, false, 0);

        var lblLocked = new Label("Em Staking (Bloqueado)") { Halign = Align.Start };
        lblLocked.StyleContext.AddClass("label-dim");
        rightPanel.PackStart(lblLocked, false, false, 0);

        // Endereço e Botão Copiar
        rightPanel.PackStart(new Label("Endereço:") { Halign = Align.Start, MarginTop = 15 }, false, false, 0);

        var addressBox = new HBox(false, 5);
        var entryAddress = new Entry
            { Text = "MIIBCgKCAQEAry3qp73hQaLVGn5...", IsEditable = false, WidthRequest = 200 };

        var btnCopy = new Button("Copiar");
        btnCopy.StyleContext.AddClass("btn-blue");
        btnCopy.StyleContext.AddClass("btn-action");

        addressBox.PackStart(entryAddress, true, true, 0);
        addressBox.PackStart(btnCopy, false, false, 0);
        rightPanel.PackStart(addressBox, false, false, 0);

        // Ações Rápidas
        rightPanel.PackStart(new Label("Ações Rápidas") { Halign = Align.Start, MarginTop = 30 }, false, false, 0);

        var actionGrid = new HBox(true, 10);

        var btnBuy = new Button("COMPRAR");
        btnBuy.StyleContext.AddClass("btn-green");
        btnBuy.StyleContext.AddClass("btn-action");

        var btnSell = new Button("VENDER");
        btnSell.StyleContext.AddClass("btn-red");
        btnSell.StyleContext.AddClass("btn-action");

        var btnStake = new Button("STAKE");
        btnStake.StyleContext.AddClass("btn-orange");
        btnStake.StyleContext.AddClass("btn-action");

        actionGrid.PackStart(btnBuy, true, true, 0);
        actionGrid.PackStart(btnSell, true, true, 0);
        actionGrid.PackStart(btnStake, true, true, 0);

        rightPanel.PackStart(actionGrid, false, false, 0);

        // Descrição Final
        var lblDesc = new Label("O mercado está em Alta. Pode ser um bom momento para vender tokens excedentes.")
        {
            Wrap = true,
            Justify = Justification.Fill,
            MarginTop = 15
        };
        lblDesc.StyleContext.AddClass("label-dim");
        rightPanel.PackStart(lblDesc, false, false, 0);

        this.PackStart(rightPanel, false, false, 0);
    }

    private void OnDrawChart(object o, DrawnArgs args)
    {
        var cr = args.Cr;
        var da = (DrawingArea)o;
        int w = da.AllocatedWidth;
        int h = da.AllocatedHeight;

        cr.SetSourceRGB(0.15, 0.2, 0.25);
        cr.Paint();

        cr.SetSourceRGB(0.2, 0.6, 1.0);
        cr.LineWidth = 3;

        cr.MoveTo(0, h / 2);

        for (int i = 0; i < w; i += 5)
        {
            double y = (h / 2) + (Math.Sin(i * 0.05) * 50) + (Math.Cos(i * 0.02) * 30);
            cr.LineTo(i, y);
        }

        cr.Stroke();

        cr.SetSourceRGBA(1, 1, 1, 0.1);
        cr.LineWidth = 1;
        for (int j = 0; j < h; j += 40)
        {
            cr.MoveTo(0, j);
            cr.LineTo(w, j);
        }

        cr.Stroke();
    }
}