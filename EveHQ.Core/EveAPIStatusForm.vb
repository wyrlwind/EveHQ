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

Imports System.Drawing
Imports System.Windows.Forms
Imports EveHQ.EveAPI
Imports System.Text

Public Class EveAPIStatusForm

    Dim _results() As String
    ReadOnly _errors As New SortedList

    Private Sub EveAPIStatusForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        If HQ.Settings.UseApirs = True Then
            Text = "Eve API Status - " & HQ.Settings.ApirsAddress
        Else
            Text = "Eve API Status - " & HQ.Settings.CcpapiServerAddress
        End If

        Dim accountName As String
        _errors.Clear()
        If HQ.APIResults.Count > 0 Then
            For Each result As String In HQ.APIResults.Keys
                Dim itemResult As New ListViewItem
                itemResult.UseItemStyleForSubItems = False
                _results = result.Split("_".ToCharArray)
                If _results.Length = 3 Then
                    If lvwStatus.Items.ContainsKey(_results(0) & "_" & _results(1)) Then
                        ' Use this listview
                        itemResult = lvwStatus.Items(_results(0) & "_" & _results(1))
                    Else
                        ' Create a new one
                        itemResult.Name = _results(0) & "_" & _results(1)
                        accountName = HQ.Settings.Accounts(_results(0)).FriendlyName
                        If accountName <> "" Then
                            itemResult.Text = accountName
                        Else
                            itemResult.Text = _results(0)
                        End If
                        ' ReSharper disable once RedundantAssignment - Incorrect warning by R#
                        For si As Integer = 1 To 4
                            itemResult.SubItems.Add("n/a")
                        Next
                    End If
                Else
                    itemResult.Name = _results(0)
                    accountName = HQ.Settings.Accounts(_results(0)).FriendlyName
                    If accountName <> "" Then
                        itemResult.Text = accountName
                    Else
                        itemResult.Text = _results(0)
                    End If
                    ' ReSharper disable once RedundantAssignment - Incorrect warning by R#
                    For si As Integer = 1 To 4
                        itemResult.SubItems.Add("n/a")
                    Next
                End If

                If _results.Length = 1 Then
                    ' Add Result for account XML here
                    Call DisplayAPIResult(2, CInt(HQ.APIResults(result)), itemResult)
                Else
                    ' Set pilot
                    itemResult.SubItems(1).Text = _results(1)
                    ' Add Result for character XML
                    If CInt(_results(2)) = APITypes.CharacterSheet Then
                        Call DisplayAPIResult(3, CInt(HQ.APIResults(result)), itemResult)
                    End If
                    ' Add Result for training XML
                    If CInt(_results(2)) = APITypes.SkillQueue Then
                        Call DisplayAPIResult(4, CInt(HQ.APIResults(result)), itemResult)
                    End If
                End If
                If lvwStatus.Items.ContainsKey(itemResult.Name) = False Then
                    lvwStatus.Items.Add(itemResult)
                End If
            Next

            ' Display any error codes
            If _errors.Count > 0 Then
                Dim strError As New StringBuilder
                For Each errCode As String In _errors.Keys
                    strError.AppendLine("Code: " & errCode & " - " & CStr(_errors(errCode)))
                Next
                lblErrorDetails.Text = strError.ToString
            End If
        End If
    End Sub

    Private Sub DisplayAPIResult(ByVal idx As Integer, ByVal result As Integer, ByRef lvItem As ListViewItem)
        LVItem.SubItems(idx).Tag = result
        Select Case result
            Case APIResults.ReturnedNew
                LVItem.SubItems(idx).ForeColor = Color.Green
                LVItem.SubItems(idx).Text = "New"
            Case APIResults.ReturnedCached
                LVItem.SubItems(idx).ForeColor = Color.Blue
                LVItem.SubItems(idx).Text = "Cached"
            Case APIResults.ReturnedActual
                LVItem.SubItems(idx).ForeColor = Color.Green
                LVItem.SubItems(idx).Text = "Actual"
            Case APIResults.APIServerDownReturnedCached
                LVItem.SubItems(idx).ForeColor = Color.Blue
                LVItem.SubItems(idx).Text = "Down - Cached"
            Case APIResults.APIServerDownReturnedNull
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "Server Down"
            Case APIResults.PageNotFound
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "Page Not Found"
            Case APIResults.TimedOut
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "Timed Out"
            Case APIResults.CCPError
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "API Error"
            Case APIResults.UnknownError
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "Unknown"
            Case Is < 0
                LVItem.SubItems(idx).ForeColor = Color.Red
                LVItem.SubItems(idx).Text = "API Error " & Math.Abs(result).ToString
                Dim errorCode As String = Math.Abs(result).ToString
                If _errors.Contains(errorCode) = False Then
                    _errors.Add(errorCode, CStr(HQ.APIErrors(CStr(errorCode))))
                End If
        End Select
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class