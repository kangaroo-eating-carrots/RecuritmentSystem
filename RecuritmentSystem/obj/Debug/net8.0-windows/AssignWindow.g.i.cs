﻿#pragma checksum "..\..\..\AssignWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "266EBB4FBE20C7C73C98219739E44354F14F4918"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RecuritmentSystem20122791;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace RecuritmentSystem20122791 {
    
    
    /// <summary>
    /// AssignWindow
    /// </summary>
    public partial class AssignWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\AssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListboxContractors;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\AssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAssign;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\AssignWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAuto;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RecuritmentSystem20122791;component/assignwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AssignWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ListboxContractors = ((System.Windows.Controls.ListBox)(target));
            
            #line 23 "..\..\..\AssignWindow.xaml"
            this.ListboxContractors.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListboxContractors_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonAssign = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\AssignWindow.xaml"
            this.ButtonAssign.Click += new System.Windows.RoutedEventHandler(this.ButtonAssign_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonAuto = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\AssignWindow.xaml"
            this.ButtonAuto.Click += new System.Windows.RoutedEventHandler(this.ButtonAuto_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

