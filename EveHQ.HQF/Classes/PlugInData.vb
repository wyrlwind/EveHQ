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
Imports EveHQ.HQF.Classes
Imports EveHQ.Core
Imports System.Net
Imports System.Windows.Forms
Imports System.IO
Imports EveHQ.HQF.Forms
Imports EveHQ.Common.Extensions
Imports ProtoBuf

Public Class PlugInData
    Implements IEveHQPlugIn

    Public Shared ModuleChanges As New SortedList(Of String, String)
    Private _activeForm As FrmHQF

#Region "Plug-in Interface Properties and Functions"

    Public Function GetPlugInData(ByVal data As Object, ByVal dataType As Integer) As Object Implements IEveHQPlugIn.GetPlugInData
        Return Nothing
    End Function

    Public Function EveHQStartUp() As Boolean Implements IEveHQPlugIn.EveHQStartUp
        Try
            ' Check for existance of HQF folder & create if not existing
            If HQ.IsUsingLocalFolders = False Then
                PluginSettings.HQFFolder = Path.Combine(HQ.AppDataFolder, "HQF")
            Else
                PluginSettings.HQFFolder = Path.Combine(Application.StartupPath, "HQF")
            End If
            If My.Computer.FileSystem.DirectoryExists(PluginSettings.HQFFolder) = False Then
                My.Computer.FileSystem.CreateDirectory(PluginSettings.HQFFolder)
            End If

            PluginSettings.HQFCacheFolder = HQ.StaticDataFolder

            Dim mods() As String = My.Resources.ModuleChanges.Split(ControlChars.CrLf.ToCharArray)
            ModuleChanges.Clear()
            For Each modLine As String In mods
                If modLine <> "" Then
                    Dim modData() As String = modLine.Split(",".ToCharArray)
                    ModuleChanges.Add(modData(0).Trim(ControlChars.Quote), modData(1).Trim(ControlChars.Quote))
                End If
            Next
            Engine.BuildPirateImplants()
            Engine.BuildBoosterPenaltyList()
            Engine.BuildEffectsMap()
            Engine.BuildShipEffectsMap()
            Engine.BuildShipBonusesMap()
            Engine.BuildShipModeBonusesMap()
            Engine.BuildSubSystemBonusMap()

            ' Load the HQF cache data
            Dim s As FileStream

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "attributes.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "attributes.dat"), FileMode.Open)
                Try
                    Attributes.AttributeList = Serializer.Deserialize(Of SortedList(Of Integer, Attribute))(s)
                    s.Close()
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "ships.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "ships.dat"), FileMode.Open)
                Try
                    ShipLists.ShipList = Serializer.Deserialize(Of Dictionary(Of String, Ship))(s)
                    s.Close()
                    For Each cShip As Ship In ShipLists.ShipList.Values
                        ShipLists.ShipListKeyID.Add(cShip.ID, cShip.Name)
                        ShipLists.ShipListKeyName.Add(cShip.Name, cShip.ID)
                    Next
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "modules.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "modules.dat"), FileMode.Open)
                Try
                    ModuleLists.ModuleList = Serializer.Deserialize(Of SortedList(Of Integer, ShipModule))(s)
                    s.Close()
                    For Each cMod As ShipModule In ModuleLists.ModuleList.Values
                        ModuleLists.ModuleListName.Add(cMod.Name.Trim, cMod.ID)
                        If cMod.IsCharge = True Then
                            If Charges.ChargeGroups.ContainsKey(cMod.ID) = False Then
                                Charges.ChargeGroups.Add(cMod.ID, cMod.MarketGroup & "_" & cMod.DatabaseGroup & "_" & cMod.Name & "_" & cMod.ChargeSize)
                            End If
                        End If
                    Next
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "implants.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "implants.dat"), FileMode.Open)
                Try
                    Implants.ImplantList = Serializer.Deserialize(Of SortedList(Of String, ShipModule))(s)
                    s.Close()
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "boosters.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "boosters.dat"), FileMode.Open)
                Try
                    Boosters.BoosterList = Serializer.Deserialize(Of SortedList(Of String, ShipModule))(s)
                    s.Close()
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            If My.Computer.FileSystem.FileExists(Path.Combine(PluginSettings.HQFCacheFolder, "skills.dat")) = True Then
                s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "skills.dat"), FileMode.Open)
                Try
                    SkillLists.SkillList = Serializer.Deserialize(Of SortedList(Of Integer, Skill))(s)
                    s.Close()
                Catch sex As Exception
                    s.Close()
                End Try
                s.Dispose()
            End If

            ' Load Item Groups (from Core data)
            s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "ItemGroups.dat"), FileMode.Open)
            ModuleLists.TypeGroups = Serializer.Deserialize(Of SortedList(Of Integer, String))(s)
            s.Close()
            s.Dispose()

            ' Load Items Cats (from Core data)
            s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "ItemCats.dat"), FileMode.Open)
            ModuleLists.TypeCats = Serializer.Deserialize(Of SortedList(Of Integer, String))(s)
            s.Close()
            s.Dispose()

            ' Load Group Cats (from Core data)
            s = New FileStream(Path.Combine(PluginSettings.HQFCacheFolder, "GroupCats.dat"), FileMode.Open)
            ModuleLists.GroupCats = Serializer.Deserialize(Of SortedList(Of Integer, Integer))(s)
            s.Close()
            s.Dispose()

            Engine.BuildImplantEffectsMap()
            Call BuildAttributeQuickList()

            ' Create Image Cache
            Call GenerateIcons()

            ' Load the settings!
            Call PluginSettings.HQFSettings.LoadHQFSettings()

            ' Load the Profiles - stored separately from settings for distribution!
            Call HQFDamageProfiles.Load()
            Call HQFDefenceProfiles.Load()

            ' Load up a collection of pilots from the EveHQ Core
            Call FittingPilots.LoadHQFPilotData()

            ' Load saved setups into the fitting array
            Call SavedFittings.LoadFittings()

            Return True

        Catch ex As Exception
            Trace.TraceError(ex.FormatException())
            Throw
        End Try
    End Function

    Private Sub GenerateIcons()
        ' Create Image Cache
        ImageHandler.BaseIcons.Clear()
        For Each sMod As ShipModule In ModuleLists.ModuleList.Values
            ' Only get Tech 1 & 3 icons as base icons to avoid meta group indicators on the base icons
            If sMod.MetaType = MetaTypes.Tech1 Or sMod.MetaType = MetaTypes.Tech3 Then
                If sMod.Icon <> "" Then
                    If ImageHandler.BaseIcons.ContainsKey(sMod.Icon) = False Then
                        Dim oi As Bitmap = CType(Core.ImageHandler.GetImage(sMod.ID, 64, sMod.Icon), Bitmap)
                        If oi IsNot Nothing Then
                            ImageHandler.BaseIcons.Add(sMod.Icon, oi)
                        End If
                    End If
                End If
            End If
        Next
        ImageHandler.MetaIcons.Clear()
        For idx As Integer = 0 To 32
            Dim oi As Bitmap = CType(My.Resources.ResourceManager.GetObject("Meta" & (2 ^ idx).ToString), Bitmap)
            If oi IsNot Nothing Then
                ImageHandler.MetaIcons.Add((2 ^ idx).ToString, oi)
            End If
        Next
        ' Combine the images
        Call ImageHandler.CombineIcons24()
        Call ImageHandler.CombineIcons48()
    End Sub

    Private Sub BuildAttributeQuickList()
        Attributes.AttributeQuickList.Clear()
        Dim attData As Attribute
        For Each att As Integer In Attributes.AttributeList.Keys
            attData = Attributes.AttributeList(att)
            If attData.DisplayName <> "" Then
                Attributes.AttributeQuickList.Add(attData.ID, attData.DisplayName)
            Else
                Attributes.AttributeQuickList.Add(attData.ID, attData.Name)
            End If
        Next
    End Sub

    Public Function GetEveHQPlugInInfo() As EveHQPlugIn Implements IEveHQPlugIn.GetEveHQPlugInInfo
        ' Returns data to EveHQ to identify it as a plugin
        Dim eveHQPlugIn As New EveHQPlugIn
        eveHQPlugIn.Name = "EveHQ Fitter"
        eveHQPlugIn.Description = "Allows theoretical ship setup and simulation"
        eveHQPlugIn.Author = "EveHQ Team"
        eveHQPlugIn.MainMenuText = "EveHQ Fitter"
        eveHQPlugIn.RunAtStartup = True
        eveHQPlugIn.RunInIGB = True
        eveHQPlugIn.MenuImage = My.Resources.plugin_icon
        eveHQPlugIn.Version = My.Application.Info.Version.ToString
        Return eveHQPlugIn
    End Function

    Public Function RunEveHQPlugIn() As Form Implements IEveHQPlugIn.RunEveHQPlugIn
        _activeForm = New FrmHQF()
        Return _activeForm
    End Function

    Public Function SaveAll() As Boolean Implements IEveHQPlugIn.SaveAll
        If _activeForm IsNot Nothing Then
            _activeForm.SaveAll()
            Return True
        End If
        Return False
    End Function

#End Region

End Class
