#pragma checksum "..\..\AdminWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DCD806F2AC7ECE6E3D3CCB01EA3FE15C379E9158CF13172FC2D019E5A090A775"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignApp;
using MaterialDesignApp.ViewModels;
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


namespace MaterialDesignApp {
    
    
    /// <summary>
    /// AdminWindow
    /// </summary>
    public partial class AdminWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Wrapper;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Navbar;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PlayersButton;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ReportsButton;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image SettingsButton;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AccountButton;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CloseButton;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Content;
        
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
            System.Uri resourceLocater = new System.Uri("/MaterialDesignApp;component/adminwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdminWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.Wrapper = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.Navbar = ((System.Windows.Controls.Grid)(target));
            
            #line 39 "..\..\AdminWindow.xaml"
            this.Navbar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Navbar_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 62 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.PlayersButton_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 65 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NavbarButton_Click);
            
            #line default
            #line hidden
            
            #line 66 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 67 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PlayersButton = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            
            #line 78 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseUp);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 81 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NavbarButton_Click);
            
            #line default
            #line hidden
            
            #line 82 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 83 "..\..\AdminWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ReportsButton = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.SettingsButton = ((System.Windows.Controls.Image)(target));
            
            #line 107 "..\..\AdminWindow.xaml"
            this.SettingsButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SettingsButton_Click);
            
            #line default
            #line hidden
            
            #line 108 "..\..\AdminWindow.xaml"
            this.SettingsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 108 "..\..\AdminWindow.xaml"
            this.SettingsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 10:
            this.AccountButton = ((System.Windows.Controls.Image)(target));
            
            #line 117 "..\..\AdminWindow.xaml"
            this.AccountButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AccountButton_Click);
            
            #line default
            #line hidden
            
            #line 118 "..\..\AdminWindow.xaml"
            this.AccountButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 118 "..\..\AdminWindow.xaml"
            this.AccountButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 11:
            this.MinimizeButton = ((System.Windows.Controls.Image)(target));
            
            #line 129 "..\..\AdminWindow.xaml"
            this.MinimizeButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MinimizeButton_MouseUp);
            
            #line default
            #line hidden
            
            #line 130 "..\..\AdminWindow.xaml"
            this.MinimizeButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 130 "..\..\AdminWindow.xaml"
            this.MinimizeButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 12:
            this.CloseButton = ((System.Windows.Controls.Image)(target));
            
            #line 139 "..\..\AdminWindow.xaml"
            this.CloseButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.CloseButton_MouseUp);
            
            #line default
            #line hidden
            
            #line 140 "..\..\AdminWindow.xaml"
            this.CloseButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 140 "..\..\AdminWindow.xaml"
            this.CloseButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Content = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

