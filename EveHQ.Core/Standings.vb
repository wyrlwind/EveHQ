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

Imports System.Globalization
Imports EveHQ.EveApi
Imports EveHQ.Common.Extensions
Imports System.Xml

Public Class Standings
    Public Shared Sub GetStandings(ByVal pilotName As String)

        If HQ.Settings.Pilots.ContainsKey(pilotName) = True Then
            Dim pilot As EveHQPilot = HQ.Settings.Pilots(pilotName)

            ' Clear the existing standings
            pilot.Standings.Clear()

            ' Set culture info
            Dim culture As CultureInfo = New CultureInfo("en-GB")

            ' Get Account info for the API
            Dim accountName As String = pilot.Account
            If HQ.Settings.Accounts.ContainsKey(accountName) = True Then
                Dim pilotAccount As EveHQAccount = HQ.Settings.Accounts.Item(accountName)

                ' Stage 1 - Get the NPC Standings
                Try
                    Dim standingsList As IEnumerable(Of NewEveApi.Entities.NpcStanding)
                    Dim standingsNode As NewEveApi.Entities.NpcStanding
                    Dim standingsResponse As NewEveApi.EveServiceResponse(Of IEnumerable(Of NewEveApi.Entities.NpcStanding)) =
                            HQ.ApiProvider.Character.NPCStandings(pilotAccount.UserID, pilotAccount.APIKey, CLng(pilot.ID))

                    'Dim apiReq As New EveAPIRequest(HQ.EveHQAPIServerInfo, HQ.RemoteProxy, HQ.Settings.APIFileExtension, HQ.ApiCacheFolder)
                    'Dim standingsXML As XmlDocument = apiReq.GetAPIXML(APITypes.StandingsChar, pilotAccount.ToAPIAccount, pilot.ID, APIReturnMethods.ReturnStandard)
                    If standingsResponse IsNot Nothing Then


                        If standingsResponse.IsSuccess Then
                            standingsList = standingsResponse.ResultData
                            If standingsList IsNot Nothing Then
                                If standingsList.Count > 0 Then

                                    For Each standingsNode In standingsList

                                        Dim currentStandingsType As StandingType = StandingType.Unknown

                                        Select Case standingsNode.Kind
                                            Case NewEveApi.Entities.NpcType.agents
                                                currentStandingsType = StandingType.Agent
                                            Case NewEveApi.Entities.NpcType.NPCCorporations
                                                currentStandingsType = StandingType.NPCCorporation
                                            Case NewEveApi.Entities.NpcType.factions
                                                currentStandingsType = StandingType.Faction
                                        End Select


                                        Dim newStanding As New PilotStanding
                                        newStanding.ID = standingsNode.FromId
                                        newStanding.Name = standingsNode.FromName
                                        newStanding.Type = currentStandingsType
                                        newStanding.Standing = standingsNode.Standing
                                        If pilot.Standings.ContainsKey(newStanding.ID) = False Then
                                            pilot.Standings.Add(newStanding.ID, newStanding)
                                        End If


                                    Next

                                End If
                            End If
                        End If
                    End If
                Catch e As Exception
                    ' Just skip and try the next stage
                End Try

                ' Stage 2 - Get the player and corp standings
                Try
                    Dim standingsList As IEnumerable(Of NewEveApi.Entities.Contact)
                    Dim contactResponse As NewEveApi.EveServiceResponse(Of IEnumerable(Of NewEveApi.Entities.Contact)) = HQ.ApiProvider.Character.ContactList(pilotAccount.UserID, pilotAccount.APIKey, pilot.ID.ToInt32())
                    'Dim _
                    '    apiReq As _
                    '        New EveAPIRequest(HQ.EveHqapiServerInfo, HQ.RemoteProxy, HQ.Settings.APIFileExtension,
                    '                          HQ.ApiCacheFolder)
                    'Dim standingsXML As XmlDocument = apiReq.GetAPIXML(APITypes.ContactListChar,
                    '                                                   pilotAccount.ToAPIAccount, pilot.ID,
                    '                                                   APIReturnMethods.ReturnStandard)
                    If contactResponse IsNot Nothing Then


                        If contactResponse.IsSuccess Then
                            standingsList = contactResponse.ResultData
                            If standingsList IsNot Nothing Then


                                Const currentStandingsType As StandingType = StandingType.PlayerCorp

                                For Each entity As NewEveApi.Entities.Contact In standingsList

                                    Dim newStanding As New PilotStanding
                                    newStanding.ID = entity.ContactId
                                    newStanding.Name = entity.ContactName
                                    newStanding.Type = currentStandingsType
                                    newStanding.Standing = entity.Standing
                                    If pilot.Standings.ContainsKey(newStanding.ID) = False Then
                                        pilot.Standings.Add(newStanding.ID, newStanding)
                                    End If

                                Next
                            End If
                        End If
                    End If
                Catch e As Exception
                ' Just skip and exit
            End Try

            End If

        End If
    End Sub

    Public Shared Function GetStanding(ByVal pilotName As String, ByVal entityID As String,
                                       ByVal returnEffectiveStanding As Boolean) As Double
        ' Try and get the standings data
        If HQ.Settings.Pilots.ContainsKey(pilotName) = True Then

            Dim sPilot As EveHQPilot = HQ.Settings.Pilots(pilotName)

            ' Get the Connections and Diplomacy skills
            Dim diplomacyLevel As Integer = sPilot.KeySkills(KeySkill.Diplomacy)
            Dim connectionsLevel As Integer = sPilot.KeySkills(KeySkill.Connections)

            If sPilot.Standings.ContainsKey(CLng(entityID)) = True Then
                If returnEffectiveStanding = True Then
                    Dim rawStanding As Double = sPilot.Standings(CLng(entityID)).Standing
                    If rawStanding < 0 Then
                        Return rawStanding + ((10 - rawStanding)*(diplomacyLevel*4/100))
                    Else
                        Return rawStanding + ((10 - rawStanding)*(connectionsLevel*4/100))
                    End If
                Else
                    Return sPilot.Standings(CLng(entityID)).Standing
                End If
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function
End Class
