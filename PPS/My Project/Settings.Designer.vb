﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(ByVal sender As Global.System.Object, ByVal e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("5")>  _
        Public Property version_id() As Integer
            Get
                Return CType(Me("version_id"),Integer)
            End Get
            Set
                Me("version_id") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("4242")>  _
        Public Property port_number() As String
            Get
                Return CType(Me("port_number"),String)
            End Get
            Set
                Me("port_number") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("ppsbi_engie")>  _
        Public Property database() As String
            Get
                Return CType(Me("database"),String)
            End Get
            Set
                Me("database") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8")>  _
        Public Property tablesFontSize() As UInteger
            Get
                Return CType(Me("tablesFontSize"),UInteger)
            End Get
            Set
                Me("tablesFontSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3")>  _
        Public Property mainCurrency() As UInteger
            Get
                Return CType(Me("mainCurrency"),UInteger)
            End Get
            Set
                Me("mainCurrency") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("8.5")>  _
        Public Property dgvFontSize() As Single
            Get
                Return CType(Me("dgvFontSize"),Single)
            End Get
            Set
                Me("dgvFontSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("52.18.194.135")>  _
        Public Property serverIp() As String
            Get
                Return CType(Me("serverIp"),String)
            End Get
            Set
                Me("serverIp") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("root")>  _
        Public Property user() As String
            Get
                Return CType(Me("user"),String)
            End Get
            Set
                Me("user") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property controllingUIResizeTofitGrid() As Boolean
            Get
                Return CType(Me("controllingUIResizeTofitGrid"),Boolean)
            End Get
            Set
                Me("controllingUIResizeTofitGrid") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property titleFontColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("titleFontColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("titleFontColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("DarkSlateBlue")>  _
        Public Property titleBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("titleBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("titleBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property titleFontBold() As Boolean
            Get
                Return CType(Me("titleFontBold"),Boolean)
            End Get
            Set
                Me("titleFontBold") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property titleFontItalic() As Boolean
            Get
                Return CType(Me("titleFontItalic"),Boolean)
            End Get
            Set
                Me("titleFontItalic") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property titleBordersPresent() As Boolean
            Get
                Return CType(Me("titleBordersPresent"),Boolean)
            End Get
            Set
                Me("titleBordersPresent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property titleIndent() As Integer
            Get
                Return CType(Me("titleIndent"),Integer)
            End Get
            Set
                Me("titleIndent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property importantFontColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("importantFontColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("importantFontColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property importantBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("importantBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("importantBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property importantFontBold() As Boolean
            Get
                Return CType(Me("importantFontBold"),Boolean)
            End Get
            Set
                Me("importantFontBold") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property importantFontItalic() As Boolean
            Get
                Return CType(Me("importantFontItalic"),Boolean)
            End Get
            Set
                Me("importantFontItalic") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property importantBordersPresent() As Boolean
            Get
                Return CType(Me("importantBordersPresent"),Boolean)
            End Get
            Set
                Me("importantBordersPresent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property importantBordersColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("importantBordersColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("importantBordersColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property normalFontColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("normalFontColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("normalFontColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property normalBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("normalBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("normalBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property normalFontBold() As Boolean
            Get
                Return CType(Me("normalFontBold"),Boolean)
            End Get
            Set
                Me("normalFontBold") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property normalFontItalic() As Boolean
            Get
                Return CType(Me("normalFontItalic"),Boolean)
            End Get
            Set
                Me("normalFontItalic") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property normalBordersPresent() As Boolean
            Get
                Return CType(Me("normalBordersPresent"),Boolean)
            End Get
            Set
                Me("normalBordersPresent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("2")>  _
        Public Property normalIndent() As Integer
            Get
                Return CType(Me("normalIndent"),Integer)
            End Get
            Set
                Me("normalIndent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property normalBordersColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("normalBordersColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("normalBordersColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property detailFontBold() As Boolean
            Get
                Return CType(Me("detailFontBold"),Boolean)
            End Get
            Set
                Me("detailFontBold") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property detailFontItalic() As Boolean
            Get
                Return CType(Me("detailFontItalic"),Boolean)
            End Get
            Set
                Me("detailFontItalic") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property detailBordersPresent() As Boolean
            Get
                Return CType(Me("detailBordersPresent"),Boolean)
            End Get
            Set
                Me("detailBordersPresent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("3")>  _
        Public Property detailIndent() As Integer
            Get
                Return CType(Me("detailIndent"),Integer)
            End Get
            Set
                Me("detailIndent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property detailBordersColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("detailBordersColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("detailBordersColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property importantIndent() As Integer
            Get
                Return CType(Me("importantIndent"),Integer)
            End Get
            Set
                Me("importantIndent") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Green")>  _
        Public Property detailFontColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("detailFontColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("detailFontColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("PaleGreen")>  _
        Public Property detailBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("detailBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("detailBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property titleBordersColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("titleBordersColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("titleBordersColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("PowderBlue")>  _
        Public Property snapshotInputsBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("snapshotInputsBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("snapshotInputsBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Navy")>  _
        Public Property snapshotInputsTextColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("snapshotInputsTextColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("snapshotInputsTextColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
        Public Property snapshotOutputsBackColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("snapshotOutputsBackColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("snapshotOutputsBackColor") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("Black")>  _
        Public Property snapshotOutputsTextColor() As Global.System.Drawing.Color
            Get
                Return CType(Me("snapshotOutputsTextColor"),Global.System.Drawing.Color)
            End Get
            Set
                Me("snapshotOutputsTextColor") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.FinancialBI.My.MySettings
            Get
                Return Global.FinancialBI.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
