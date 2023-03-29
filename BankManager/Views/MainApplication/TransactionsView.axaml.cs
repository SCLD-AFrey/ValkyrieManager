﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BankManager.ViewModels.MainApplication;

namespace BankManager.Views.MainApplication;

public partial class TransactionsView : UserControl
{
#pragma warning disable CS8618
    public TransactionsView() {  }
#pragma warning restore CS8618
    public TransactionsView(TransactionsViewModel p_viewModel)
    {
        DataContext = p_viewModel;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}