﻿#pragma checksum "..\..\Administrator.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EF0F5C428D82B8F3AC4A4F298FA2FDC46E907B73"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PLWPF;
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


namespace PLWPF {
    
    
    /// <summary>
    /// Administrator
    /// </summary>
    public partial class Administrator : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\Administrator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tests_list;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Administrator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid testers_list;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\Administrator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid trainees_list;
        
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
            System.Uri resourceLocater = new System.Uri("/PLWPF;component/administrator.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Administrator.xaml"
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
            
            #line 16 "..\..\Administrator.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.add_test);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 17 "..\..\Administrator.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.add_tester);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 18 "..\..\Administrator.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.add_trainee);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 21 "..\..\Administrator.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Remove_tester);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 22 "..\..\Administrator.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Remove_trainee);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 27 "..\..\Administrator.xaml"
            ((System.Windows.Controls.TabItem)(target)).PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.refresh_tests);
            
            #line default
            #line hidden
            return;
            case 7:
            this.tests_list = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            
            #line 42 "..\..\Administrator.xaml"
            ((System.Windows.Controls.TabItem)(target)).PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.refresh_testers);
            
            #line default
            #line hidden
            return;
            case 9:
            this.testers_list = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 65 "..\..\Administrator.xaml"
            ((System.Windows.Controls.TabItem)(target)).PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(this.refresh_trainees);
            
            #line default
            #line hidden
            return;
            case 11:
            this.trainees_list = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

