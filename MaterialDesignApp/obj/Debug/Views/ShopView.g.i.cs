﻿#pragma checksum "..\..\..\Views\ShopView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "26F621C8A4DA218267533012E8D4CF9D466270406B674898511A82B50C9E03EA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignApp.Views;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace MaterialDesignApp.Views {
    
    
    /// <summary>
    /// ShopView
    /// </summary>
    public partial class ShopView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost DialogHost;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ItemInfoDialog;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush DialogItemImage;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DialogItemName;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DialogItemQuantity;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBoxItem ComboBoxItem1;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DialogItemPrice;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DialogPurchaseCancelButton;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DialogPurchaseConfirmButton;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotEnoughMoneyError;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SuccessfulPurchaseDialog;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DialogCloseButton;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Wrapper;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ItemCategories;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ItemsGridContainer;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\Views\ShopView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignApp.Views.OutlinedTextBlock GoldBalance;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MaterialDesignApp;component/views/shopview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ShopView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 12 "..\..\..\Views\ShopView.xaml"
            ((MaterialDesignApp.Views.ShopView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DialogHost = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 3:
            this.ItemInfoDialog = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.DialogItemImage = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 5:
            this.DialogItemName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.DialogItemQuantity = ((System.Windows.Controls.ComboBox)(target));
            
            #line 40 "..\..\..\Views\ShopView.xaml"
            this.DialogItemQuantity.DropDownClosed += new System.EventHandler(this.DialogItemQuantity_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ComboBoxItem1 = ((System.Windows.Controls.ComboBoxItem)(target));
            return;
            case 8:
            this.DialogItemPrice = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.DialogPurchaseCancelButton = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.DialogPurchaseConfirmButton = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\Views\ShopView.xaml"
            this.DialogPurchaseConfirmButton.Click += new System.Windows.RoutedEventHandler(this.DialogPurchaseConfirmButton_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.NotEnoughMoneyError = ((System.Windows.Controls.TextBlock)(target));
            
            #line 76 "..\..\..\Views\ShopView.xaml"
            this.NotEnoughMoneyError.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NotEnoughMoneyError_MouseUp);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SuccessfulPurchaseDialog = ((System.Windows.Controls.Grid)(target));
            return;
            case 13:
            
            #line 86 "..\..\..\Views\ShopView.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).Click += new System.Windows.RoutedEventHandler(this.InventoryHyperlink_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 87 "..\..\..\Views\ShopView.xaml"
            ((System.Windows.Documents.Run)(target)).AddHandler(System.Windows.Documents.Hyperlink.ClickEvent, new System.Windows.RoutedEventHandler(this.InventoryHyperlink_Click));
            
            #line default
            #line hidden
            return;
            case 15:
            this.DialogCloseButton = ((System.Windows.Controls.Button)(target));
            return;
            case 16:
            this.Wrapper = ((System.Windows.Controls.Grid)(target));
            return;
            case 17:
            this.ItemCategories = ((System.Windows.Controls.ListView)(target));
            return;
            case 18:
            this.ItemsGridContainer = ((System.Windows.Controls.Grid)(target));
            return;
            case 19:
            this.GoldBalance = ((MaterialDesignApp.Views.OutlinedTextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
