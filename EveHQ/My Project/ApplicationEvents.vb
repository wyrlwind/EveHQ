'==============================================================================
'
' EveHQ - An Eve-Online� character assistance application
' Copyright � 2005-2015  EveHQ Development Team
'
' This file is part of EveHQ.
'
' The source code for EveHQ is free and you may redistribute 
' it and/or modify it under the terms of the MIT License. 
'
' Refer to the NOTICES file in the root folder of EVEHQ source
' project for details of 3rd party components that are covered
' under their own, separate licenses.
'
' EveHQ is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the MIT 
' license below for details.
'
' ------------------------------------------------------------------------------
'
' The MIT License (MIT)
'
' Copyright � 2005-2015  EveHQ Development Team
'
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in
' all copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
' THE SOFTWARE.
'
' ==============================================================================

Imports EveHQ.Forms

Namespace My

    'The following events are available for MyApplication
    '
    'Startup: Raised when the application starts, before the startup form is created.
    'Shutdown: Raised after all application forms are closed.  This event is not raised if the application is terminating abnormally.
    'UnhandledException: Raised if the application encounters an unhandled exception.
    'StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    'NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Class MyApplication

        Private Sub MyApplication_NetworkAvailabilityChanged(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.Devices.NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            Try
                If e.IsNetworkAvailable = False Then
                    frmEveHQ.EveStatusIcon.BalloonTipIcon = ToolTipIcon.Info
                    frmEveHQ.EveStatusIcon.BalloonTipTitle = "Network Status Notification"
                    frmEveHQ.EveStatusIcon.BalloonTipText = "EveHQ has detected that the connection to the network has been lost. This will affect the responses from the Eve Servers."
                    frmEveHQ.EveStatusIcon.ShowBalloonTip(3000)
                End If
            Catch ex As Exception
                ' Some form of error here so move on and see if things still work ok?
            End Try
        End Sub

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shutdown

        End Sub

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
            frmEveHQ.WindowState = FormWindowState.Minimized
        End Sub

        Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            'MessageBox.Show("You can only run one copy of EveHQ at a time!", "EveHQ Already Running", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Can we get /params?
            For Each param As String In e.CommandLine
            Next
            If frmEveHQ.WindowState = FormWindowState.Minimized Then
                frmEveHQ.WindowState = FormWindowState.Maximized
                frmEveHQ.Show()
                frmEveHQ.BringToFront()
            End If
            e.BringToForeground = True
        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Try
                Using myException As New FrmException
                    myException.lblVersion.Text = "Version: " & Application.Info.Version.ToString
                    myException.lblError.Text = e.Exception.Message
                    Dim trace As New System.Text.StringBuilder
                    trace.AppendLine(e.Exception.StackTrace.ToString)
                    trace.AppendLine("")
                    trace.AppendLine("========== Plug-ins ==========")
                    trace.AppendLine("")
                    For Each myPlugIn As Core.EveHQPlugIn In Core.HQ.Plugins.Values
                        If myPlugIn.ShortFileName IsNot Nothing Then
                            trace.AppendLine(myPlugIn.ShortFileName & " (" & myPlugIn.Version & ")")
                        End If
                    Next
                    trace.AppendLine("")
                    trace.AppendLine("")
                    trace.AppendLine("========= System Info =========")
                    trace.AppendLine("")
                    trace.AppendLine("Operating System: " & Environment.OSVersion.ToString)
                    trace.AppendLine(".Net Framework Version: " & Environment.Version.ToString)
                    trace.AppendLine("EveHQ Location: " & Core.HQ.AppFolder)
                    trace.AppendLine("EveHQ Cache Locations: " & Core.HQ.AppDataFolder)
                    myException.txtStackTrace.Text = trace.ToString
                    Dim result As Integer = myException.ShowDialog()
                    If result = DialogResult.Ignore Then
                        e.ExitApplication = False
                    Else
                        Call FrmEveHQ.ShutdownRoutine()
                        e.ExitApplication = True
                    End If
                End Using
            Catch ex As Exception
                Dim msg As New System.Text.StringBuilder
                msg.AppendLine("A critical error has occurred which has prevented the UI from displaying! The following message should have been copied to the clipboard but you may need to provide this message in a screenshot for any bug report.")
                msg.AppendLine("")
                msg.AppendLine("Original Error: " & e.Exception.Message)
                msg.AppendLine("")
                msg.AppendLine("Original Stacktrace: " & e.Exception.StackTrace)
                msg.AppendLine("")
                msg.AppendLine("Error Form Error: " & ex.Message)
                msg.AppendLine("")
                msg.AppendLine("Error Form Stacktrace: " & ex.StackTrace)
                msg.AppendLine("")
                Dim inner As Exception = ex.InnerException
                Dim count As Integer = 1
                While inner IsNot Nothing
                    msg.AppendLine("Error Form Inner Exception" & count & ": " & inner.Message)
                    inner = inner.InnerException
                    count += 1
                End While
                If ex.InnerException IsNot Nothing Then

                End If
                Try
                    Clipboard.SetText(msg.ToString)
                Catch ex2 As Exception
                    ' do nothing
                End Try
                MessageBox.Show(msg.ToString, "Farnsworth is Busy", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Try
        End Sub
    End Class

End Namespace
