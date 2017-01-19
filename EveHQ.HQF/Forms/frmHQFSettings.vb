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
Imports System.IO
Imports System.Windows.Forms
Imports DevComponents.AdvTree
Imports EveHQ.Core
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.Win32

Namespace Forms

    Public Class FrmHQFSettings
        Dim _forceUpdate As Boolean = False
        Dim _redrawColumns As Boolean = True
        Dim _startUp As Boolean = True

#Region "Form Opening & Closing"

        Private Sub frmSettings_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing

            If e.CloseReason = CloseReason.None Then
                e.Cancel = True
            End If

            ' Save Widths
            Dim cols As New SortedList(Of String, UserSlotColumn)
            For Each userCol As UserSlotColumn In PluginSettings.HQFSettings.UserSlotColumns
                cols.Add(userCol.Name, userCol)
            Next

            ' Process the slot selection
            PluginSettings.HQFSettings.UserSlotColumns.Clear()
            For Each slotItem As ListViewItem In lvwColumns.Items
                Dim oldSlot As UserSlotColumn = cols(slotItem.Name)
                PluginSettings.HQFSettings.UserSlotColumns.Add(New UserSlotColumn(oldSlot.Name, oldSlot.Description, oldSlot.Width, slotItem.Checked))
            Next

            ' Save the profiles
            Call HQFDamageProfiles.Save()
            Call HQFDefenceProfiles.Save()

            ' Save the settings
            Call PluginSettings.HQFSettings.SaveHQFSettings()
            If _forceUpdate = True Then
                HQFEvents.StartUpdateFitting = True
            End If
        End Sub

        Private Sub frmSettings_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            _startUp = True
            Call UpdateGeneralOptions()
            Call UpdateSlotFormatOptions()
            Call UpdateConstantsOptions()
            Call UpdateDamageProfileOptions()
            Call UpdateDefenceProfileOptions()
            Call UpdateAttributeColumns()

            _forceUpdate = False
            _redrawColumns = True
            _startUp = False

            ' Switch to the right tab
            tvwSettings.Select()
            If Tag IsNot Nothing Then
                If Tag.ToString = "" Then
                    tvwSettings.SelectedNode = tvwSettings.Nodes("nodeGeneral")
                Else
                    If tvwSettings.Nodes.ContainsKey(Tag.ToString) = True Then
                        tvwSettings.SelectedNode = tvwSettings.Nodes(Tag.ToString)
                    Else
                        tvwSettings.SelectedNode = tvwSettings.Nodes("nodeGeneral")
                    End If
                End If
            Else
                tvwSettings.SelectedNode = tvwSettings.Nodes("nodeGeneral")
            End If

        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
            Close()
        End Sub

        Private Sub gbSlotFormat_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles gbSlotFormat.Paint
            _redrawColumns = False
        End Sub

#End Region

#Region "General Options"
        Private Sub UpdateGeneralOptions()
            cboStartupPilot.Items.Clear()
            For Each myPilot As EveHQPilot In HQ.Settings.Pilots.Values
                If myPilot.Active = True Then
                    cboStartupPilot.Items.Add(myPilot.Name)
                End If
            Next
            If HQ.Settings.Pilots.ContainsKey(PluginSettings.HQFSettings.DefaultPilot) = False Then
                If HQ.Settings.Pilots.Count > 0 Then
                    cboStartupPilot.SelectedIndex = 0
                End If
            Else
                cboStartupPilot.SelectedItem = PluginSettings.HQFSettings.DefaultPilot
            End If
            chkRestoreLastSession.Checked = PluginSettings.HQFSettings.RestoreLastSession
            chkAutoUpdateHQFSkills.Checked = PluginSettings.HQFSettings.AutoUpdateHQFSkills
            chkShowPerformance.Checked = PluginSettings.HQFSettings.ShowPerformanceData
            chkUseLastPilot.Checked = PluginSettings.HQFSettings.UseLastPilot
        End Sub
        Private Sub cboStartupPilot_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboStartupPilot.SelectedIndexChanged
            PluginSettings.HQFSettings.DefaultPilot = CStr(cboStartupPilot.SelectedItem)
        End Sub
        Private Sub chkRestoreLastSession_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRestoreLastSession.CheckedChanged
            PluginSettings.HQFSettings.RestoreLastSession = chkRestoreLastSession.Checked
        End Sub
        Private Sub chkAutoUpdateHQFSkills_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAutoUpdateHQFSkills.CheckedChanged
            PluginSettings.HQFSettings.AutoUpdateHQFSkills = chkAutoUpdateHQFSkills.Checked
        End Sub
        Private Sub chkShowPerformance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowPerformance.CheckedChanged
            PluginSettings.HQFSettings.ShowPerformanceData = chkShowPerformance.Checked
        End Sub
        Private Sub chkUseLastPilot_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUseLastPilot.CheckedChanged
            PluginSettings.HQFSettings.UseLastPilot = chkUseLastPilot.Checked
        End Sub
#End Region

#Region "Slot Format Options"

        Private Sub UpdateSlotFormatOptions()
            chkAutoResizeColumns.Checked = PluginSettings.HQFSettings.AutoResizeColumns
            Dim hColor As Color = Color.FromArgb(CInt(PluginSettings.HQFSettings.HiSlotColour))
            pbHiSlotColour.BackColor = hColor
            Dim mColor As Color = Color.FromArgb(CInt(PluginSettings.HQFSettings.MidSlotColour))
            pbMidSlotColour.BackColor = mColor
            Dim lColor As Color = Color.FromArgb(CInt(PluginSettings.HQFSettings.LowSlotColour))
            pbLowSlotColour.BackColor = lColor
            Dim rColor As Color = Color.FromArgb(CInt(PluginSettings.HQFSettings.RigSlotColour))
            pbRigSlotColour.BackColor = rColor
            Dim sColor As Color = Color.FromArgb(CInt(PluginSettings.HQFSettings.SubSlotColour))
            pbSubSlotColour.BackColor = sColor
            _redrawColumns = True
            Call RedrawSlotColumnList()
            _redrawColumns = False
        End Sub
        Private Sub RedrawSlotColumnList()
            ' Setup the listview
            Dim newCol As ListViewItem
            lvwColumns.BeginUpdate()
            lvwColumns.Items.Clear()
            For Each userSlot As UserSlotColumn In PluginSettings.HQFSettings.UserSlotColumns
                newCol = New ListViewItem(userSlot.Description)
                newCol.Name = userSlot.Name
                newCol.Checked = userSlot.Active
                lvwColumns.Items.Add(newCol)
            Next
            lvwColumns.EndUpdate()
        End Sub
        Private Sub btnMoveUp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMoveUp.Click
            ' Check we have something selected
            If lvwColumns.SelectedItems.Count = 0 Then
                MessageBox.Show("Please select an item before trying it move it!", "Item Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            ' Find the index in the user column list
            Dim idx As Integer = lvwColumns.SelectedItems(0).Index
            If idx > 0 Then
                Dim colToMove As UserSlotColumn = PluginSettings.HQFSettings.UserSlotColumns(idx)
                Dim colToSwitch As UserSlotColumn = PluginSettings.HQFSettings.UserSlotColumns(idx - 1)
                PluginSettings.HQFSettings.UserSlotColumns.Item(idx) = colToSwitch
                PluginSettings.HQFSettings.UserSlotColumns.Item(idx - 1) = colToMove
                ' Redraw the list
                _redrawColumns = True
                Call RedrawSlotColumnList()
                _redrawColumns = False
                lvwColumns.Items(idx - 1).Selected = True
                lvwColumns.Select()
                _forceUpdate = True
            End If

        End Sub
        Private Sub btnMoveDown_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMoveDown.Click
            ' Check we have something selected
            If lvwColumns.SelectedItems.Count = 0 Then
                MessageBox.Show("Please select an item before trying it move it!", "Item Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            ' Find the index in the user column list
            Dim idx As Integer = lvwColumns.SelectedItems(0).Index
            If idx < lvwColumns.Items.Count - 1 Then
                Dim colToMove As UserSlotColumn = PluginSettings.HQFSettings.UserSlotColumns(idx)
                Dim colToSwitch As UserSlotColumn = PluginSettings.HQFSettings.UserSlotColumns(idx + 1)
                PluginSettings.HQFSettings.UserSlotColumns.Item(idx) = colToSwitch
                PluginSettings.HQFSettings.UserSlotColumns.Item(idx + 1) = colToMove
                ' Redraw the list
                _redrawColumns = True
                Call RedrawSlotColumnList()
                _redrawColumns = False
                lvwColumns.Items(idx + 1).Selected = True
                lvwColumns.Select()
                _forceUpdate = True
            End If
        End Sub
        Private Sub lvwColumns_ItemChecked(ByVal sender As Object, ByVal e As ItemCheckedEventArgs) Handles lvwColumns.ItemChecked
            If _redrawColumns = False Then
                ' Find the index in the user column list
                Dim idx As Integer = e.Item.Index
                PluginSettings.HQFSettings.UserSlotColumns.Item(idx).Active = e.Item.Checked
                _forceUpdate = True
            End If
        End Sub

        Private Sub pbHiSlotColour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbHiSlotColour.Click
            Dim dlgResult As Integer
            With cd1
                .AllowFullOpen = True
                .AnyColor = True
                .FullOpen = True
                dlgResult = .ShowDialog()
            End With
            If dlgResult = DialogResult.Cancel Then
                Exit Sub
            Else
                pbHiSlotColour.BackColor = cd1.Color
                PluginSettings.HQFSettings.HiSlotColour = cd1.Color.ToArgb
            End If
        End Sub

        Private Sub pbMidSlotColour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbMidSlotColour.Click
            Dim dlgResult As Integer
            With cd1
                .AllowFullOpen = True
                .AnyColor = True
                .FullOpen = True
                dlgResult = .ShowDialog()
            End With
            If dlgResult = DialogResult.Cancel Then
                Exit Sub
            Else
                pbMidSlotColour.BackColor = cd1.Color
                PluginSettings.HQFSettings.MidSlotColour = cd1.Color.ToArgb
            End If
        End Sub

        Private Sub pbLowSlotColour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbLowSlotColour.Click
            Dim dlgResult As Integer
            With cd1
                .AllowFullOpen = True
                .AnyColor = True
                .FullOpen = True
                dlgResult = .ShowDialog()
            End With
            If dlgResult = DialogResult.Cancel Then
                Exit Sub
            Else
                pbLowSlotColour.BackColor = cd1.Color
                PluginSettings.HQFSettings.LowSlotColour = cd1.Color.ToArgb
            End If
        End Sub

        Private Sub pbRigSlotColour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbRigSlotColour.Click
            Dim dlgResult As Integer
            With cd1
                .AllowFullOpen = True
                .AnyColor = True
                .FullOpen = True
                dlgResult = .ShowDialog()
            End With
            If dlgResult = DialogResult.Cancel Then
                Exit Sub
            Else
                pbRigSlotColour.BackColor = cd1.Color
                PluginSettings.HQFSettings.RigSlotColour = cd1.Color.ToArgb
            End If
        End Sub

        Private Sub pbSubSlotColour_Click(ByVal sender As Object, ByVal e As EventArgs) Handles pbSubSlotColour.Click
            Dim dlgResult As Integer
            With cd1
                .AllowFullOpen = True
                .AnyColor = True
                .FullOpen = True
                dlgResult = .ShowDialog()
            End With
            If dlgResult = DialogResult.Cancel Then
                Exit Sub
            Else
                pbSubSlotColour.BackColor = cd1.Color
                PluginSettings.HQFSettings.SubSlotColour = cd1.Color.ToArgb
            End If
        End Sub

        Private Sub chkAutoResizeColumns_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoResizeColumns.CheckedChanged
            PluginSettings.HQFSettings.AutoResizeColumns = chkAutoResizeColumns.Checked
            _forceUpdate = True
        End Sub

#End Region

#Region "Treeview Routines"

        Private Sub tvwSettings_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvwSettings.AfterSelect
            Dim nodeName As String = e.Node.Name
            Dim gbName As String = nodeName.TrimStart("node".ToCharArray)
            gbName = "gb" & gbName
            For Each setControl As Control In pnlSettings.Controls
                If setControl.Name = "tvwSettings" Or setControl.Name = "btnClose" Or setControl.Name = gbName Then
                    pnlSettings.Controls(gbName).Top = 12
                    pnlSettings.Controls(gbName).Left = 195
                    pnlSettings.Controls(gbName).Width = 585
                    pnlSettings.Controls(gbName).Height = 500
                    pnlSettings.Controls(gbName).Visible = True
                Else
                    setControl.Visible = False
                End If
            Next
        End Sub

        Private Sub tvwSettings_NodeMouseClick(ByVal sender As Object, ByVal e As TreeNodeMouseClickEventArgs) Handles tvwSettings.NodeMouseClick
            tvwSettings.SelectedNode = e.Node
        End Sub

#End Region

#Region "Constants Options"
        Private Sub UpdateConstantsOptions()
            If PluginSettings.HQFSettings.CapRechargeConstant > nudCapRecharge.Maximum Then
                PluginSettings.HQFSettings.CapRechargeConstant = nudCapRecharge.Maximum
            ElseIf PluginSettings.HQFSettings.CapRechargeConstant < nudCapRecharge.Minimum Then
                PluginSettings.HQFSettings.CapRechargeConstant = nudCapRecharge.Minimum
            End If
            If PluginSettings.HQFSettings.ShieldRechargeConstant > nudShieldRecharge.Maximum Then
                PluginSettings.HQFSettings.ShieldRechargeConstant = nudShieldRecharge.Maximum
            ElseIf PluginSettings.HQFSettings.ShieldRechargeConstant < nudShieldRecharge.Minimum Then
                PluginSettings.HQFSettings.ShieldRechargeConstant = nudShieldRecharge.Minimum
            End If
            If PluginSettings.HQFSettings.MissileRangeConstant > nudMissileRange.Maximum Then
                PluginSettings.HQFSettings.MissileRangeConstant = nudMissileRange.Maximum
            ElseIf PluginSettings.HQFSettings.MissileRangeConstant < nudMissileRange.Minimum Then
                PluginSettings.HQFSettings.MissileRangeConstant = nudMissileRange.Minimum
            End If
            nudCapRecharge.Value = CDec(PluginSettings.HQFSettings.CapRechargeConstant)
            nudShieldRecharge.Value = CDec(PluginSettings.HQFSettings.ShieldRechargeConstant)
            nudMissileRange.Value = CDec(PluginSettings.HQFSettings.MissileRangeConstant)
            chkCapBoosterReloadTime.Checked = PluginSettings.HQFSettings.IncludeCapReloadTime
            chkAmmoLoadTime.Checked = PluginSettings.HQFSettings.IncludeAmmoReloadTime
        End Sub
        Private Sub nudCapRecharge_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles nudCapRecharge.HandleDestroyed
            PluginSettings.HQFSettings.CapRechargeConstant = nudCapRecharge.Value
        End Sub
        Private Sub nudShieldRecharge_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles nudShieldRecharge.HandleDestroyed
            PluginSettings.HQFSettings.ShieldRechargeConstant = nudShieldRecharge.Value
        End Sub
        Private Sub nudMissileRange_HandleDestroyed(ByVal sender As Object, ByVal e As EventArgs) Handles nudMissileRange.HandleDestroyed
            PluginSettings.HQFSettings.MissileRangeConstant = nudMissileRange.Value
        End Sub
        Private Sub nudCapRecharge_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudCapRecharge.ValueChanged
            If _startUp = False Then
                PluginSettings.HQFSettings.CapRechargeConstant = CDbl(nudCapRecharge.Value)
                _forceUpdate = True
            End If
        End Sub
        Private Sub nudShieldRecharge_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudShieldRecharge.ValueChanged
            If _startUp = False Then
                PluginSettings.HQFSettings.ShieldRechargeConstant = CDbl(nudShieldRecharge.Value)
                _forceUpdate = True
            End If
        End Sub
        Private Sub nudMissileRange_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles nudMissileRange.ValueChanged
            If _startUp = False Then
                PluginSettings.HQFSettings.MissileRangeConstant = CDbl(nudMissileRange.Value)
                _forceUpdate = True
            End If
        End Sub
        Private Sub chkCapBoosterReloadTime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCapBoosterReloadTime.CheckedChanged
            PluginSettings.HQFSettings.IncludeCapReloadTime = chkCapBoosterReloadTime.Checked
            _forceUpdate = True
        End Sub
        Private Sub chkAmmoLoadTime_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkAmmoLoadTime.CheckedChanged
            PluginSettings.HQFSettings.IncludeAmmoReloadTime = chkAmmoLoadTime.Checked
            _forceUpdate = True
        End Sub
#End Region

#Region "Damage Profile Options"

        Private Sub UpdateDamageProfileOptions()
            lvwProfiles.BeginUpdate()
            lvwProfiles.Items.Clear()
            Dim newItem As ListViewItem
            For Each newProfile As HQFDamageProfile In HQFDamageProfiles.ProfileList.Values
                newItem = New ListViewItem
                newItem.Name = newProfile.Name
                newItem.Text = newProfile.Name
                Select Case newProfile.Type
                    Case ProfileTypes.Manual
                        newItem.SubItems.Add("Manual")
                    Case ProfileTypes.Fitting
                        newItem.SubItems.Add("Fitting")
                End Select
                lvwProfiles.Items.Add(newItem)
            Next
            lvwProfiles.EndUpdate()
        End Sub

        Private Sub lvwProfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvwProfiles.SelectedIndexChanged
            If lvwProfiles.SelectedItems.Count > 0 Then
                Dim selProfile As HQFDamageProfile = HQFDamageProfiles.ProfileList(lvwProfiles.SelectedItems(0).Name)
                lblProfileName.Text = selProfile.Name
                Select Case selProfile.Type
                    Case ProfileTypes.Manual
                        lblProfileType.Text = "Manual"
                        lblFittingName.Text = "n/a"
                        lblPilotName.Text = "n/a"
                        lblNPCName.Text = "n/a"
                    Case ProfileTypes.Fitting
                        lblProfileType.Text = "Fitting"
                        lblFittingName.Text = selProfile.Fitting
                        lblPilotName.Text = selProfile.Pilot
                        lblNPCName.Text = "n/a"
                End Select
                lblEMDamageAmount.Text = selProfile.EM.ToString("N2")
                lblEXDamageAmount.Text = selProfile.Explosive.ToString("N2")
                lblKIDamageAmount.Text = selProfile.Kinetic.ToString("N2")
                lblTHDamageAmount.Text = selProfile.Thermal.ToString("N2")
                Dim total As Double = selProfile.EM + selProfile.Explosive + selProfile.Kinetic + selProfile.Thermal
                lblTotalDamageAmount.Text = total.ToString("N2")
                lblEMDamagePercentage.Text = "= " & (selProfile.EM / total * 100).ToString("N2") & "%"
                lblEXDamagePercentage.Text = "= " & (selProfile.Explosive / total * 100).ToString("N2") & "%"
                lblKIDamagePercentage.Text = "= " & (selProfile.Kinetic / total * 100).ToString("N2") & "%"
                lblTHDamagePercentage.Text = "= " & (selProfile.Thermal / total * 100).ToString("N2") & "%"
                lblDPS.Text = selProfile.DPS.ToString("N2")
                If gpProfile.Visible = False Then
                    gpProfile.Visible = True
                End If
            End If
        End Sub

        Private Sub btnDeleteProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteProfile.Click
            If lvwProfiles.SelectedItems.Count > 0 Then
                Dim profileName As String = lvwProfiles.SelectedItems(0).Name
                If profileName = "<Omni-Damage>" Then
                    MessageBox.Show("You cannot delete this profile!", "Error Deleting Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim reply As Integer = MessageBox.Show("Are you sure you want to delete the profile '" & profileName & "'?", "Confirm Profile Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If reply = DialogResult.No Then
                        Exit Sub
                    Else
                        ' Delete the profile
                        HQFDamageProfiles.ProfileList.Remove(profileName)
                        ' Save the profiles
                        HQFDamageProfiles.Save()
                        Call UpdateDamageProfileOptions()
                    End If
                End If
            Else
                MessageBox.Show("Please select a profile before trying to delete.", "Error Deleting Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub btnAddProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddProfile.Click
            Using profileForm As New FrmAddDamageProfile
                profileForm.Tag = "Add"
                profileForm.btnAccept.Text = "Add Profile"
                profileForm.ShowDialog()
            End Using
            Call UpdateDamageProfileOptions()
        End Sub

        Private Sub btnEditProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditProfile.Click
            If lvwProfiles.SelectedItems.Count > 0 Then
                Dim profileName As String = lvwProfiles.SelectedItems(0).Name
                If profileName = "<Omni-Damage>" Then
                    MessageBox.Show("You cannot edit this profile!", "Error Editing Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim editProfile As HQFDamageProfile = HQFDamageProfiles.ProfileList(profileName)
                    Using profileForm As New FrmAddDamageProfile
                        profileForm.Tag = editProfile
                        profileForm.btnAccept.Text = "Edit Profile"
                        profileForm.ShowDialog()
                    End Using
                    Call UpdateDamageProfileOptions()
                End If
            Else
                MessageBox.Show("Please select a profile before trying to delete.", "Error Deleting Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub btnResetProfiles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetProfiles.Click
            Dim response As Integer = MessageBox.Show("This will delete all your existing profiles and re-instate the defaults. Are you sure you wish to proceed?", "Confirm Reset ALL Profiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If response = DialogResult.Yes Then
                Dim cResponse As Integer = MessageBox.Show("Are you really sure you wish to proceed?", "Confirm Reset ALL Profiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If cResponse = DialogResult.Yes Then
                    Try
                        If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFFolder, "HQFProfiles.bin")) = True Then
                            My.Computer.FileSystem.DeleteFile(Path.Combine(PluginSettings.HQFFolder, "HQFProfiles.bin"), UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                        End If
                        HQFDamageProfiles.Reset()
                        Call UpdateDamageProfileOptions()
                        MessageBox.Show("Profiles successfully reset", "Profile Reset Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Unable to delete the Damage Profiles file", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End Sub

#End Region

#Region "Defence Profile Options"

        Private Sub UpdateDefenceProfileOptions()
            lvwDefenceProfiles.BeginUpdate()
            lvwDefenceProfiles.Items.Clear()
            Dim newItem As ListViewItem
            For Each newProfile As HQFDefenceProfile In HQFDefenceProfiles.ProfileList.Values
                newItem = New ListViewItem
                newItem.Name = newProfile.Name
                newItem.Text = newProfile.Name
                Select Case newProfile.Type
                    Case ProfileTypes.Manual
                        newItem.SubItems.Add("Manual")
                    Case ProfileTypes.Fitting
                        newItem.SubItems.Add("Fitting")
                End Select
                lvwDefenceProfiles.Items.Add(newItem)
            Next
            lvwDefenceProfiles.EndUpdate()
        End Sub

        Private Sub lvwDefenceProfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvwDefenceProfiles.SelectedIndexChanged
            If lvwDefenceProfiles.SelectedItems.Count > 0 Then
                Dim selProfile As HQFDefenceProfile = HQFDefenceProfiles.ProfileList(lvwDefenceProfiles.SelectedItems(0).Name)
                lblDefProfileName.Text = selProfile.Name
                Select Case selProfile.Type
                    Case ProfileTypes.Manual
                        lblDefProfileType.Text = "Manual"
                    Case ProfileTypes.Fitting
                        lblDefProfileType.Text = "Fitting"
                End Select
                lblDefSEM.Text = selProfile.SEm.ToString("N2")
                lblDefSEx.Text = selProfile.SExplosive.ToString("N2")
                lblDefSKi.Text = selProfile.SKinetic.ToString("N2")
                lblDefSTh.Text = selProfile.SThermal.ToString("N2")
                lblDefAEM.Text = selProfile.AEm.ToString("N2")
                lblDefAEx.Text = selProfile.AExplosive.ToString("N2")
                lblDefAKi.Text = selProfile.AKinetic.ToString("N2")
                lblDefATh.Text = selProfile.AThermal.ToString("N2")
                lblDefHEM.Text = selProfile.HEm.ToString("N2")
                lblDefHEx.Text = selProfile.HExplosive.ToString("N2")
                lblDefHKi.Text = selProfile.HKinetic.ToString("N2")
                lblDefHTh.Text = selProfile.HThermal.ToString("N2")
                If gpDefenceProfile.Visible = False Then
                    gpDefenceProfile.Visible = True
                End If
            End If
        End Sub

        Private Sub btnDeleteDefenceProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeleteDefenceProfile.Click
            If lvwDefenceProfiles.SelectedItems.Count > 0 Then
                Dim profileName As String = lvwDefenceProfiles.SelectedItems(0).Name
                Dim reply As Integer = MessageBox.Show("Are you sure you want to delete the profile '" & profileName & "'?", "Confirm Profile Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If reply = DialogResult.No Then
                    Exit Sub
                Else
                    ' Delete the profile
                    HQFDefenceProfiles.ProfileList.Remove(profileName)
                    ' Save the profiles
                    HQFDefenceProfiles.Save()
                    Call UpdateDefenceProfileOptions()
                End If
            Else
                MessageBox.Show("Please select a profile before trying to delete.", "Error Deleting Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub btnAddDefenceProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddDefenceProfile.Click
            Using profileForm As New FrmAddDefenceProfile
                profileForm.Tag = "Add"
                profileForm.btnAccept.Text = "Add Profile"
                profileForm.ShowDialog()
            End Using
            Call UpdateDefenceProfileOptions()
        End Sub

        Private Sub btnEditDefenceProfile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditDefenceProfile.Click
            If lvwDefenceProfiles.SelectedItems.Count > 0 Then
                Dim profileName As String = lvwDefenceProfiles.SelectedItems(0).Name
                Dim editProfile As HQFDefenceProfile = HQFDefenceProfiles.ProfileList(profileName)
                Using profileForm As New FrmAddDefenceProfile()
                    profileForm.Tag = editProfile
                    profileForm.btnAccept.Text = "Edit Profile"
                    profileForm.ShowDialog()
                End Using
                Call UpdateDefenceProfileOptions()
            Else
                MessageBox.Show("Please select a profile before trying to delete.", "Error Deleting Profile", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Sub

        Private Sub btnResetDefenceProfiles_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetDefenceProfiles.Click
            Dim response As Integer = MessageBox.Show("This will delete all your existing profiles and re-instate the defaults. Are you sure you wish to proceed?", "Confirm Reset ALL Profiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If response = DialogResult.Yes Then
                Dim cResponse As Integer = MessageBox.Show("Are you really sure you wish to proceed?", "Confirm Reset ALL Profiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If cResponse = DialogResult.Yes Then
                    Try
                        If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFFolder, "HQFDefenceProfiles.bin")) = True Then
                            My.Computer.FileSystem.DeleteFile(Path.Combine(PluginSettings.HQFFolder, "HQFDefenceProfiles.bin"), UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin)
                        End If
                        HQFDefenceProfiles.Reset()
                        Call UpdateDefenceProfileOptions()
                        MessageBox.Show("Profiles successfully reset", "Profile Reset Completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Unable to delete the Defence Profiles file", "Deletion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End Sub

#End Region

#Region "Attribute Column Options"

        Private Sub UpdateAttributeColumns()
            adtAttributeColumns.BeginUpdate()
            adtAttributeColumns.Nodes.Clear()
            For Each attId As String In PluginSettings.HQFSettings.IgnoredAttributeColumns
                Dim newNode As New Node
                newNode.Name = attId
                newNode.Text = attId
                newNode.Cells.Add(New Cell(CStr(Attributes.AttributeQuickList(CInt(attId)))))
                adtAttributeColumns.Nodes.Add(newNode)
            Next
            AdvTreeSorter.Sort(adtAttributeColumns, 1, False, True)
            adtAttributeColumns.EndUpdate()
        End Sub

        Private Sub adtAttributeColumns_SelectionChanged(sender As Object, e As EventArgs) Handles adtAttributeColumns.SelectionChanged
            If adtAttributeColumns.SelectedNodes.Count > 0 Then
                btnRemoveAttribute.Enabled = True
            Else
                btnRemoveAttribute.Enabled = False
            End If
        End Sub

        Private Sub btnRemoveAttribute_Click(sender As Object, e As EventArgs) Handles btnRemoveAttribute.Click
            If adtAttributeColumns.SelectedNodes.Count > 0 Then
                Dim attId As String = adtAttributeColumns.SelectedNodes(0).Name
                If PluginSettings.HQFSettings.IgnoredAttributeColumns.Contains(attId) = True Then
                    PluginSettings.HQFSettings.IgnoredAttributeColumns.Remove(attId)
                    Call UpdateAttributeColumns()
                End If
            End If
        End Sub

        Private Sub btnClearAttributes_Click(sender As Object, e As EventArgs) Handles btnClearAttributes.Click
            Dim reply As DialogResult = MessageBox.Show("Are you sure you wish to clear all the ignored attribute columns?", "Confirm Clear Columns", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If reply = DialogResult.Yes Then
                PluginSettings.HQFSettings.IgnoredAttributeColumns.Clear()
                Call UpdateAttributeColumns()
            Else
                Exit Sub
            End If
        End Sub
#End Region

    End Class
End NameSpace