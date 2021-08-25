﻿#pragma checksum "..\..\UserWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DF9A2AF77BE8B0F723DFC4865A990BC0FB88FE4D39B23E829D6B38A5A0041CE5"
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
    /// UserWindow
    /// </summary>
    public partial class UserWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Wrapper;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Navbar;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MyCharactersButton;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label GameButton;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ShopWrapper;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label ShopButton;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image SettingsButton;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image AccountButton;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MinimizeButton;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\UserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image CloseButton;
        
        #line default
        #line hidden
        
        
        #line 172 "..\..\UserWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/MaterialDesignApp;component/userwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserWindow.xaml"
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
            
            #line 48 "..\..\UserWindow.xaml"
            this.Navbar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Navbar_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 72 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MyCharactersButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 75 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NavbarButton_Click);
            
            #line default
            #line hidden
            
            #line 76 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 77 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MyCharactersButton = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            
            #line 87 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.GameButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 90 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NavbarButton_Click);
            
            #line default
            #line hidden
            
            #line 91 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 92 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 8:
            this.GameButton = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            
            #line 102 "..\..\UserWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.ShopButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ShopWrapper = ((System.Windows.Controls.Border)(target));
            
            #line 106 "..\..\UserWindow.xaml"
            this.ShopWrapper.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.NavbarButton_Click);
            
            #line default
            #line hidden
            
            #line 107 "..\..\UserWindow.xaml"
            this.ShopWrapper.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 108 "..\..\UserWindow.xaml"
            this.ShopWrapper.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ShopButton = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.SettingsButton = ((System.Windows.Controls.Image)(target));
            
            #line 131 "..\..\UserWindow.xaml"
            this.SettingsButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.SettingsButton_Click);
            
            #line default
            #line hidden
            
            #line 132 "..\..\UserWindow.xaml"
            this.SettingsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 132 "..\..\UserWindow.xaml"
            this.SettingsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 13:
            this.AccountButton = ((System.Windows.Controls.Image)(target));
            
            #line 141 "..\..\UserWindow.xaml"
            this.AccountButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.AccountButton_Click);
            
            #line default
            #line hidden
            
            #line 142 "..\..\UserWindow.xaml"
            this.AccountButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 142 "..\..\UserWindow.xaml"
            this.AccountButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 14:
            this.MinimizeButton = ((System.Windows.Controls.Image)(target));
            
            #line 153 "..\..\UserWindow.xaml"
            this.MinimizeButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MinimizeButton_MouseUp);
            
            #line default
            #line hidden
            
            #line 154 "..\..\UserWindow.xaml"
            this.MinimizeButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 154 "..\..\UserWindow.xaml"
            this.MinimizeButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 15:
            this.CloseButton = ((System.Windows.Controls.Image)(target));
            
            #line 163 "..\..\UserWindow.xaml"
            this.CloseButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.CloseButton_MouseUp);
            
            #line default
            #line hidden
            
            #line 164 "..\..\UserWindow.xaml"
            this.CloseButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 164 "..\..\UserWindow.xaml"
            this.CloseButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.NavbarButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 16:
            this.Content = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

