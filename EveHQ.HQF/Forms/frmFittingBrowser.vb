﻿'==============================================================================
'
' EveHQ - An Eve-Online™ character assistance application
' Copyright © 2005-2015  EveHQ Development Team
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
' Copyright © 2005-2015  EveHQ Development Team
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
Imports EveHQ.Core

Namespace Forms

    Public Class FrmFittingBrowser

        Dim _currentShip As Ship
        ReadOnly _currentFit As New ArrayList
        Dim _currentFitting As Fitting
        Dim _cDNAFit As DNAFitting
        Dim _sourceURL As String = ""
        Dim _loadoutName As String = ""
        Dim _loadoutID As String = ""

        Public Sub New()

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add the current list of pilots to the combobox
            cboPilots.BeginUpdate()
            cboPilots.Items.Clear()
            For Each cPilot As EveHQPilot In HQ.Settings.Pilots.Values
                If cPilot.Active = True Then
                    cboPilots.Items.Add(cPilot.Name)
                End If
            Next
            cboPilots.EndUpdate()
            ' Look at the settings for default pilot
            If cboPilots.Items.Count > 0 Then
                If cboPilots.Items.Contains(PluginSettings.HQFSettings.DefaultPilot) = True Then
                    cboPilots.SelectedItem = PluginSettings.HQFSettings.DefaultPilot
                Else
                    cboPilots.SelectedIndex = 0
                End If
            End If

            ' Add the profiles
            cboProfiles.BeginUpdate()
            cboProfiles.Items.Clear()
            For Each newProfile As HQFDamageProfile In HQFDamageProfiles.ProfileList.Values
                cboProfiles.Items.Add(newProfile.Name)
            Next
            cboProfiles.EndUpdate()
            ' Select the default profile
            cboProfiles.SelectedItem = "<Omni-Damage>"

        End Sub

        Public Property DNAFit() As DNAFitting
            Get
                Return _cDNAFit
            End Get
            Set(ByVal value As DNAFitting)
                _cDNAFit = value
                _currentShip = ShipLists.ShipList(ShipLists.ShipListKeyID(value.ShipID)).Clone
                pbShip.ImageLocation = Core.ImageHandler.GetImageLocation(CInt(_currentShip.ID))
                lblShipType.Text = _currentShip.Name
                lblLoadoutName.Text = _loadoutName
            End Set
        End Property

#Region "Ship Fitting routines"

        Private Sub UpdateSlotColumns()
            ' Clear the columns
            lvwSlots.Columns.Clear()
            ' Add the module name column
            lvwSlots.Columns.Add("colName", "Module Name", 175, HorizontalAlignment.Left, "")
            lvwSlots.Columns.Add("Charge", "Charge Name", 175, HorizontalAlignment.Left, "")
        End Sub

        Private Sub UpdateSlotLayout()
            If _currentShip IsNot Nothing Then
                lvwSlots.BeginUpdate()
                lvwSlots.Items.Clear()
                ' Produce high slots
                For slot As Integer = 1 To _currentShip.HiSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "8_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.HiSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgHighSlots")
                    Call AddUserColumns(_currentShip.HiSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.MidSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "4_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.MidSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgMidSlots")
                    Call AddUserColumns(_currentShip.MidSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.LowSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "2_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.LowSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgLowSlots")
                    Call AddUserColumns(_currentShip.LowSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.RigSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "1_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.RigSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgRigSlots")
                    Call AddUserColumns(_currentShip.RigSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                For slot As Integer = 1 To _currentShip.SubSlots
                    Dim newSlot As New ListViewItem
                    newSlot.Name = "16_" & slot
                    newSlot.BackColor = Color.FromArgb(CInt(PluginSettings.HQFSettings.SubSlotColour))
                    newSlot.ForeColor = Color.Black
                    newSlot.Group = lvwSlots.Groups.Item("lvwgSubSlots")
                    Call AddUserColumns(_currentShip.SubSlot(slot), newSlot)
                    lvwSlots.Items.Add(newSlot)
                Next
                lvwSlots.EndUpdate()
            End If
        End Sub

        Private Sub AddUserColumns(ByVal shipMod As ShipModule, ByVal slotName As ListViewItem)
            ' Add subitems based on the user selected columns
            If shipMod IsNot Nothing Then
                ' Add in the module name
                slotName.Text = shipMod.Name
                If shipMod.LoadedCharge IsNot Nothing Then
                    slotName.SubItems.Add(shipMod.LoadedCharge.Name)
                Else
                    slotName.SubItems.Add("")
                End If
            Else
                slotName.Text = "<Empty>"
                slotName.SubItems.Add("")
            End If
        End Sub

        Private Sub GenerateFittingData()
            ' Let's try and generate a fitting and get some damage info
            If _currentShip IsNot Nothing Then
                If cboPilots.SelectedItem IsNot Nothing And cboProfiles.SelectedItem IsNot Nothing Then
                    If FittingPilots.HQFPilots.ContainsKey(cboPilots.SelectedItem.ToString) Then

                        Dim loadoutPilot As FittingPilot = FittingPilots.HQFPilots(cboPilots.SelectedItem.ToString)
                        Dim loadoutProfile As HQFDamageProfile = HQFDamageProfiles.ProfileList(cboProfiles.SelectedItem.ToString)

                        _currentFitting.PilotName = loadoutPilot.PilotName
                        _currentFitting.BaseShip.DamageProfile = loadoutProfile
                        _currentFitting.ApplyFitting()
                        Dim loadoutShip As Ship = _currentFitting.FittedShip

                        lblEHP.Text = loadoutShip.EffectiveHP.ToString("N0")
                        lblTank.Text = loadoutShip.Attributes(10062).ToString("N2") & " DPS"
                        lblVolley.Text = loadoutShip.Attributes(10028).ToString("N2")
                        lblDPS.Text = loadoutShip.Attributes(10029).ToString("N2")
                        lblShieldResists.Text = loadoutShip.ShieldEMResist.ToString("N0") & "/" & loadoutShip.ShieldExResist.ToString("N0") & "/" & loadoutShip.ShieldKiResist.ToString("N0") & "/" & loadoutShip.ShieldThResist.ToString("N0")
                        lblArmorResists.Text = loadoutShip.ArmorEMResist.ToString("N0") & "/" & loadoutShip.ArmorExResist.ToString("N0") & "/" & loadoutShip.ArmorKiResist.ToString("N0") & "/" & loadoutShip.ArmorThResist.ToString("N0")
                        Dim csr As CapSimResults = Capacitor.CalculateCapStatistics(loadoutShip, False)
                        If csr.CapIsDrained = False Then
                            lblCapacitor.Text = "Stable at " & (csr.MinimumCap / loadoutShip.CapCapacity * 100).ToString("N0") & "%"
                        Else
                            lblCapacitor.Text = "Lasts " & SkillFunctions.TimeToString(csr.TimeToDrain, False)
                        End If
                        lblVelocity.Text = loadoutShip.MaxVelocity.ToString("N2") & " m/s"
                        lblMaxRange.Text = loadoutShip.MaxTargetRange.ToString("N0") & "m"
                        Dim cpu As Double = loadoutShip.CPUUsed / loadoutShip.CPU * 100
                        lblCPU.Text = cpu.ToString("N2") & "%"
                        If cpu > 100 Then
                            lblCPU.ForeColor = Color.Red
                        Else
                            lblCPU.ForeColor = Color.Black
                        End If
                        Dim pg As Double = loadoutShip.PGUsed / loadoutShip.PG * 100
                        lblPG.Text = pg.ToString("N2") & "%"
                        If pg > 100 Then
                            lblPG.ForeColor = Color.Red
                        Else
                            lblPG.ForeColor = Color.Black
                        End If
                        Dim maxOpt As Double = 0
                        For slot As Integer = 1 To loadoutShip.HiSlots
                            Dim shipMod As ShipModule = loadoutShip.HiSlot(slot)
                            If shipMod IsNot Nothing Then
                                If shipMod.Attributes.ContainsKey(54) Then
                                    maxOpt = Math.Max(maxOpt, CDbl(shipMod.Attributes(54)))
                                End If
                            End If
                        Next
                        lblOptimalRange.Text = maxOpt.ToString("N0") & "m"
                    End If
                End If
            End If
        End Sub

        Private Sub cboPilots_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPilots.SelectedIndexChanged
            Call GenerateFittingData()
        End Sub

        Private Sub cboProfiles_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboProfiles.SelectedIndexChanged
            Call GenerateFittingData()
        End Sub

        Private Sub btnImport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImport.Click
            Dim shipName As String = lblShipType.Text
            Dim fittingName As String = _loadoutName
            ' If the fitting exists, add a number onto the end
            If fittingName.Trim <> "" Then
                If Fittings.FittingList.ContainsKey(shipName & ", " & fittingName) = True Then
                    Dim response As Integer = MessageBox.Show("Fitting name already exists. Are you sure you wish to import the fitting?", "Confirm Import for " & shipName, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If response = DialogResult.Yes Then
                        Dim newFittingName As String
                        Dim revision As Integer = 1
                        Do
                            revision += 1
                            newFittingName = fittingName & " " & revision.ToString
                        Loop Until Fittings.FittingList.ContainsKey(shipName & ", " & newFittingName) = False
                        fittingName = newFittingName
                        MessageBox.Show("New fitting name is '" & fittingName & "'.", "New Fitting Imported", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Exit Sub
                    End If
                End If
                ' Lets create the fitting
                Dim newFit As Fitting = Fittings.ConvertOldFitToNewFit(shipName & ", " & fittingName, _currentFit)
                Fittings.FittingList.Add(newFit.KeyName, newFit)
                HQFEvents.StartUpdateFittingList = True
            Else
                MessageBox.Show("Cannot import a fitting without a valid fitting name.", "Fitting Name Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End Sub

        Private Sub ReorderModules()
            Dim subs, mods As New ArrayList
            For Each cMod As String In _currentFit
                If ModuleLists.ModuleListName.ContainsKey(cMod) = True Then
                    If ModuleLists.ModuleList(ModuleLists.ModuleListName(cMod)).SlotType = 16 Then
                        subs.Add(cMod)
                    Else
                        mods.Add(cMod)
                    End If
                Else
                    mods.Add(cMod)
                End If
            Next
            ' Recreate the current fit
            _currentFit.Clear()
            For Each cmod As String In subs
                _currentFit.Add(cmod)
            Next
            For Each cmod As String In mods
                _currentFit.Add(cmod)
            Next
            subs.Clear()
            mods.Clear()
        End Sub

#End Region

#Region "UI Routines"

        Private Sub lblLoadoutTopic_LinkClicked(ByVal sender As Object, ByVal e As LinkLabelLinkClickedEventArgs) Handles lblLoadoutTopic.LinkClicked
            Try
                Process.Start(_sourceURL)
            Catch ex As Exception
                MessageBox.Show("Unable to start default web browser. Please check your browser PluginSettings.", "Error Starting Web Browser", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End Try
        End Sub

        Private Sub lblLoadoutTopic_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles lblLoadoutTopic.MouseEnter
            lblTopicAddress.Text = _sourceURL
        End Sub

        Private Sub lblLoadoutTopic_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles lblLoadoutTopic.MouseLeave
            lblTopicAddress.Text = ""
        End Sub

#End Region

    End Class
End NameSpace