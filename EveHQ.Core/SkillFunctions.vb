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

Imports EveHQ.EveAPI
Imports EveHQ.EveData
Imports System.Xml

Public Class SkillFunctions

    ' Shared variables for repeated usage
    Shared sTimeSpan, eTimeSpan, tTimeSpan As TimeSpan

	Public Shared Function Roman(ByVal dec As Long) As String

        Roman = ""

        Dim intVal(12) As Long
        Dim strVal(12) As String, intLoopCounter As Integer

        intVal(0) = 1 : strVal(0) = "I"
        intVal(1) = 4 : strVal(1) = "IV"
        intVal(2) = 5 : strVal(2) = "V"
        intVal(3) = 9 : strVal(3) = "IX"
        intVal(4) = 10 : strVal(4) = "X"
        intVal(5) = 40 : strVal(5) = "XL"
        intVal(6) = 50 : strVal(6) = "L"
        intVal(7) = 90 : strVal(7) = "XC"
        intVal(8) = 100 : strVal(8) = "C"
        intVal(9) = 400 : strVal(9) = "CD"
        intVal(10) = 500 : strVal(10) = "D"
        intVal(11) = 900 : strVal(11) = "CM"
        intVal(12) = 1000 : strVal(12) = "M"

        If dec = 0 Then
            Roman = ""
            Exit Function
        End If

        For intLoopCounter = 12 To 0 Step -1
            Do
                If dec >= intVal(intLoopCounter) Then
                    Roman = Roman & "" & strVal(intLoopCounter)
                    dec = dec - intVal(intLoopCounter)
                End If
            Loop Until dec < intVal(intLoopCounter)
        Next intLoopCounter
    End Function

    Public Shared Function CalculateSPLevel(ByVal rank As Integer, ByVal level As Integer) As Double
        If level > 0 Then
            Return 250 * rank * (2 ^ (2.5 * (level - 1)))
        Else
            Return 0
        End If
    End Function         'CalculateSPLevel

    Public Shared Function CalculateSkillSPLevel(ByVal skillID As Integer, ByVal level As Integer) As Double
        Return CalculateSPLevel(HQ.SkillListID(skillID).Rank, level)
    End Function

    Public Shared Function TimeToString(ByVal sTime As Double, Optional ByVal skillTime As Boolean = True, Optional completedText As String = "") As String

        Dim days, hours, minutes, seconds As Long
        Dim strTime As String = ""


        If sTime <= 0 Then
            If skillTime = True Then
                strTime = "Training Complete"
            Else
                If completedText <> "" Then
                    strTime = completedText
                Else
                    strTime = "0s"
                End If
            End If
        Else
            days = CInt(Int(sTime / (60 * 60 * 24)))
            sTime = sTime - (days * 60 * 60 * 24)

            hours = CInt(Int(sTime / (60 * 60)))
            sTime = sTime - (hours * 60 * 60)

            minutes = CInt(Int(sTime / 60))
            sTime = sTime - (minutes * 60)

            seconds = CInt(Int(sTime))

            If days <> 0 Then
                strTime &= days & "d "
            End If
            If hours <> 0 Then
                strTime &= hours & "h "
            End If
            If minutes <> 0 Then
                strTime &= minutes & "m "
            End If
            If seconds <> 0 Then
                strTime &= seconds & "s"
            End If
        End If

        Return strTime.Trim(" ".ToCharArray)

    End Function             'TimeToString

    Public Shared Function TimeToStringAll(ByVal sTime As Double) As String
        Dim days, hours, minutes, seconds As Integer
        Dim strTime As String = ""
        Dim negative As Boolean = False

        If sTime < 0 Then
            sTime = -sTime
            negative = True
        End If

        days = CInt(Int(sTime / (60 * 60 * 24)))
        sTime = sTime - (days * 60 * 60 * 24)

        hours = CInt(Int(sTime / (60 * 60)))
        sTime = sTime - (hours * 60 * 60)

        minutes = CInt(Int(sTime / 60))
        sTime = sTime - (minutes * 60)

        seconds = CInt(Int(sTime))

        If days <> 0 Then
            strTime &= days & "d "
        End If
        If hours <> 0 Then
            strTime &= hours & "h "
        End If
        If minutes <> 0 Then
            strTime &= minutes & "m "
        End If

        ' Put the seconds in regardless
        strTime &= seconds & "s"

        If negative = True Then
            strTime = "- " & strTime
        End If

        Return strTime.Trim(" ".ToCharArray)
    End Function            'TimeToStringAll

    Public Shared Function CacheTimeToString(ByVal sTime As Double) As String

        Dim days, hours, minutes, seconds As Integer
        Dim strTime As String = ""


        If sTime <= 0 Then
            strTime = "Update Available"
        Else
            days = CInt(Int(sTime / (60 * 60 * 24)))
            sTime = sTime - (days * 60 * 60 * 24)

            hours = CInt(Int(sTime / (60 * 60)))
            sTime = sTime - (hours * 60 * 60)

            minutes = CInt(Int(sTime / 60))
            sTime = sTime - (minutes * 60)

            seconds = CInt(Int(sTime))

            If days <> 0 Then
                strTime &= days & "d "
            End If
            If hours <> 0 Then
                strTime &= hours & "h "
            End If
            If minutes <> 0 Then
                strTime &= minutes & "m "
            End If
            If seconds <> 0 Then
                strTime &= seconds & "s"
            End If
        End If

        Return strTime.Trim(" ".ToCharArray)

    End Function             'TimeToString

    Public Shared Function ConvertEveTimeToLocal(ByVal eveTime As Date) As Date
        ' Calculate the local time and UTC offset.
        Return TimeZone.CurrentTimeZone.ToLocalTime(eveTime)
    End Function    'ConvertEveTimeToLocal

    Public Shared Function ConvertLocalTimeToEve(ByVal localTime As Date) As Date
        ' Calculate the local time and UTC offset.
        Return TimeZone.CurrentTimeZone.ToUniversalTime(localTime)
    End Function

    Public Shared Function CalcCurrentSkillTime(ByRef myPilot As EveHQPilot) As Long
        If myPilot.Training = True Then
            eTimeSpan = ConvertEveTimeToLocal(myPilot.TrainingEndTime) - Now
            myPilot.TrainingCurrentTime = CLng(Math.Max(eTimeSpan.TotalSeconds, 0))
            Return myPilot.TrainingCurrentTime
        Else
            Return 0
        End If
    End Function     'CalcCurrentSkillTime

    Public Shared Function CalcCurrentSkillPoints(ByRef myPilot As EveHQPilot) As Long
        If myPilot.Training = True Then
            tTimeSpan = myPilot.TrainingEndTime - myPilot.TrainingStartTime
            If DateTime.Compare(Now, ConvertEveTimeToLocal(myPilot.TrainingEndTime)) < 0 Then
                sTimeSpan = Now - ConvertEveTimeToLocal(myPilot.TrainingStartTime)
            Else
                sTimeSpan = tTimeSpan
            End If
            myPilot.TrainingCurrentSP = CInt(sTimeSpan.TotalSeconds / tTimeSpan.TotalSeconds * (myPilot.TrainingEndSP - myPilot.TrainingStartSP))
            Return myPilot.TrainingCurrentSP
        Else
            Return 0
        End If
    End Function   'CalcCurrentSkillPoints

    Public Shared Function CalcProductionTime(ByRef myPilot As EveHQPilot, ByVal time As Double) As Double
        Try
            Return time * (1 - (0.04 * CDbl(myPilot.KeySkills(CType(Pilot.KeySkill.Industry, KeySkill)))))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function CalcResearchProdTime(ByRef myPilot As EveHQPilot, ByVal time As Double) As Double
        Try
            Return time * (1 - (0.05 * CDbl(myPilot.KeySkills(CType(Pilot.KeySkill.Research, KeySkill)))))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function CalcResearchMatTime(ByRef myPilot As EveHQPilot, ByVal time As Double) As Double
        Try
            Return time * (1 - (0.05 * CDbl(myPilot.KeySkills(CType(Pilot.KeySkill.Metallurgy, KeySkill)))))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function CalcResearchCopyTime(ByRef myPilot As EveHQPilot, ByVal time As Double) As Double
        Try
            Return time * (1 - (0.05 * CDbl(myPilot.KeySkills(CType(Pilot.KeySkill.Science, KeySkill)))))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Shared Function CalcWasteFactor(ByRef myPilot As EveHQPilot, ByVal wf As Double) As Double
        Try
            Return ((1 + wf) * (1.25 - (0.05 * CDbl(myPilot.KeySkills(CType(Pilot.KeySkill.ProductionEfficiency, KeySkill)))))) - 1
        Catch ex As Exception
            Return wf
        End Try
    End Function

    Public Shared Function LoadEveSkillData() As Boolean
        HQ.SkillListName.Clear()
        HQ.SkillListID.Clear()
        HQ.SkillGroups.Clear()

        Dim skillAttFilter As New List(Of Integer)

        ' Get details of skill groups from the database
        Dim groupIDs As IEnumerable(Of Integer) = StaticData.GetGroupsInCategory(16)
        For Each groupID As Integer In groupIDs
            If groupID <> 267 Then
                Dim newSkillGroup As New SkillGroup
                newSkillGroup.ID = groupID
                newSkillGroup.Name = StaticData.TypeGroups(groupID)
                HQ.SkillGroups.Add(newSkillGroup.Name, newSkillGroup)

                ' Get the items in this skill group
                Dim items As IEnumerable(Of EveType) = StaticData.GetItemsInGroup(CInt(groupID))
                For Each item As EveType In items
                    Dim newSkill As New EveSkill
                    newSkill.ID = item.Id
                    newSkill.Description = item.Description
                    newSkill.GroupID = item.Group
                    newSkill.Name = item.Name
                    newSkill.BasePrice = item.BasePrice
                    ' Check for salvage drone op skill in db!
                    If newSkill.ID = 3440 Then
                        newSkill.Published = True
                    Else
                        newSkill.Published = item.Published
                    End If
                    HQ.SkillListID.Add(newSkill.ID, newSkill)
                    skillAttFilter.Add(CInt(newSkill.ID))
                Next
            End If
        Next
        HQ.WriteLogEvent(" *** Parsed skill groups")

        ' Filter attributes to skills for quicker parsing in the loop
        Dim skillAtts As List(Of TypeAttrib) = (From ta In StaticData.TypeAttributes Where skillAttFilter.Contains(ta.TypeId)).ToList

        Const MaxPreReqs As Integer = 10
        For Each newSkill As EveSkill In HQ.SkillListID.Values
            Dim preReqSkills(MaxPreReqs) As Integer
            Dim preReqSkillLevels(MaxPreReqs) As Integer

            ' Fetch the attributes for the item
            Dim skillID As Integer = CInt(newSkill.ID)

            For Each att As TypeAttrib In From ta In skillAtts Where ta.TypeId = skillID
                Select Case att.AttributeId
                    Case 180
                        Select Case CInt(att.Value)
                            Case 164
                                newSkill.Pa = "Charisma"
                            Case 165
                                newSkill.Pa = "Intelligence"
                            Case 166
                                newSkill.Pa = "Memory"
                            Case 167
                                newSkill.Pa = "Perception"
                            Case 168
                                newSkill.Pa = "Willpower"
                        End Select
                    Case 181
                        Select Case CInt(att.Value)
                            Case 164
                                newSkill.Sa = "Charisma"
                            Case 165
                                newSkill.Sa = "Intelligence"
                            Case 166
                                newSkill.Sa = "Memory"
                            Case 167
                                newSkill.Sa = "Perception"
                            Case 168
                                newSkill.Sa = "Willpower"
                        End Select
                    Case 275
                        newSkill.Rank = CInt(att.Value)
                    Case 182
                        preReqSkills(1) = CInt(att.Value)
                    Case 183
                        preReqSkills(2) = CInt(att.Value)
                    Case 184
                        preReqSkills(3) = CInt(att.Value)
                    Case 1285
                        preReqSkills(4) = CInt(att.Value)
                    Case 1289
                        preReqSkills(5) = CInt(att.Value)
                    Case 1290
                        preReqSkills(6) = CInt(att.Value)
                    Case 277
                        preReqSkillLevels(1) = CInt(att.Value)
                    Case 278
                        preReqSkillLevels(2) = CInt(att.Value)
                    Case 279
                        preReqSkillLevels(3) = CInt(att.Value)
                    Case 1286
                        preReqSkillLevels(4) = CInt(att.Value)
                    Case 1287
                        preReqSkillLevels(5) = CInt(att.Value)
                    Case 1288
                        preReqSkillLevels(6) = CInt(att.Value)
                End Select

            Next

            ' Add the pre-reqs into the list
            For prereq As Integer = 1 To MaxPreReqs
                If preReqSkills(prereq) <> 0 Then
                    newSkill.PreReqSkills.Add(preReqSkills(prereq), preReqSkillLevels(prereq))
                End If
            Next
            ' Calculate the levels
            For a As Integer = 0 To 5
                newSkill.LevelUp(a) = CInt(Math.Ceiling(CalculateSPLevel(newSkill.Rank, a)))
            Next
            ' Add the currentskill to the name list
            HQ.SkillListName.Add(newSkill.Name, newSkill)
        Next

        ' All is Ok!
        Return True
    End Function              'LoadEveSkillData

    Public Shared Sub LoadEveSkillDataFromAPI()
        Try
            ' Get the XML data from the API
            Dim skillData = HQ.ApiProvider.Eve.SkillTree()
            ' Load the skills!
            If skillData.IsSuccess Then

                Dim skills = skillData.ResultData.SelectMany(Function(group) group.Skills)
                For Each skill In skills
                    If HQ.SkillListID.ContainsKey(skill.TypeId) = False Then
                        Dim newSkill As New EveSkill
                        newSkill.ID = skill.TypeId
                        newSkill.Name = skill.Name
                        newSkill.GroupID = skill.GroupId
                        newSkill.Published = skill.Published
                        newSkill.Description = skill.Description
                        newSkill.Rank = skill.Rank
                        If skill.RequiredSkills.Any() Then
                            For Each reqSkill In skill.RequiredSkills
                                newSkill.PreReqSkills.Add(reqSkill.TypeId, reqSkill.Level)
                            Next
                        End If
                        newSkill.Pa = StrConv(skill.PrimaryAttribute, VbStrConv.ProperCase)
                        newSkill.Sa = StrConv(skill.SecondaryAttribute, VbStrConv.ProperCase)
                        ' Calculate the levels
                        For a As Integer = 0 To 5
                            newSkill.LevelUp(a) = CInt(CalculateSPLevel(newSkill.Rank, a))
                        Next
                        ' Add the currentskill to the name list
                        HQ.SkillListID.Add(newSkill.ID, newSkill)
                        HQ.SkillListName.Add(newSkill.Name, newSkill)
                    Else
                        Dim newSkill As EveSkill = HQ.SkillListID(skill.TypeId)
                        If skill.Name <> newSkill.Name Then
                            ' Need to update the skill details because CCP changed them!
                             newSkill.ID = skill.TypeId
                            newSkill.Name = skill.Name
                            newSkill.GroupID = skill.GroupId
                            newSkill.Published = skill.Published
                            newSkill.Description = skill.Description
                            newSkill.Rank = skill.Rank
                            If skill.RequiredSkills.Any() Then
                                For Each reqSkill In skill.RequiredSkills
                                    If newSkill.PreReqSkills.ContainsKey(reqSkill.TypeId) = False Then
                                        newSkill.PreReqSkills.Add(reqSkill.TypeId, reqSkill.Level)
                                    End If
                                Next
                            End If
                            newSkill.Pa = StrConv(skill.PrimaryAttribute, VbStrConv.ProperCase)
                            newSkill.Sa = StrConv(skill.SecondaryAttribute, VbStrConv.ProperCase)
                            ' Calculate the levels
                            For a As Integer = 0 To 5
                                newSkill.LevelUp(a) = CInt(CalculateSPLevel(newSkill.Rank, a))
                            Next
                        End If
                    End If
                Next
            End If
        Catch e As Exception
            Exit Sub
        End Try
    End Sub

    Public Shared Function CalculateSPRate(ByVal cPilot As EveHQPilot, ByVal cSkill As EveSkill) As Integer
        Dim pa, sa As Double
        Dim rate As Integer

        ' Calculate the primary attribute
        Select Case cSkill.PA.Substring(0, 1)
            Case "C"
                pa = cPilot.CAttT
            Case "I"
                pa = cPilot.IntAttT
            Case "M"
                pa = cPilot.MAttT
            Case "P"
                pa = cPilot.PAttT
            Case "W"
                pa = cPilot.WAttT
        End Select

        ' Calculate the secondary attribute
        Select Case cSkill.SA.Substring(0, 1)
            Case "C"
                sa = cPilot.CAttT
            Case "I"
                sa = cPilot.IntAttT
            Case "M"
                sa = cPilot.MAttT
            Case "P"
                sa = cPilot.PAttT
            Case "W"
                sa = cPilot.WAttT
        End Select

        rate = CInt(((60 * pa) + (30 * sa)))

        ' Half the rate if Alpha Clone
        If (cPilot.AccountStatus = APIAccountStatuses.Alpha) Then
            rate = CInt(rate / 2)
        End If
        Return rate

    End Function          'CalculateSPRate

    Public Shared Function CalculateSP(ByVal cPilot As EveHQPilot, ByVal cSkill As EveSkill, ByVal toLevel As Integer, Optional ByVal fromLevel As Integer = -1) As Long

        Dim rank, startSP, endSP As Double
        Dim sp As Long

        ' Get the skill rank
        rank = cSkill.Rank

        ' Get starting skillpoints & level
        Dim startLevel As Integer
        If fromLevel = -1 Then
            If cPilot.PilotSkills.ContainsKey(cSkill.Name) = True Then
                Dim sSkill As EveHQPilotSkill = cPilot.PilotSkills(cSkill.Name)
                startSP = sSkill.SP
                If cPilot.Training = True And cPilot.TrainingSkillName = cSkill.Name Then
                    startSP += cPilot.TrainingCurrentSP
                End If
            Else
                startSP = 0
            End If
        Else
            startLevel = fromLevel
            startSP = CalculateSPLevel(CInt(rank), startLevel)
            If startSP < 0 Then MsgBox("eek!!")
        End If

        ' Calculate the SPs @ end of level required
        endSP = CalculateSPLevel(CInt(rank), toLevel)

        sp = CLng(endSP - startSP)
        Return sp

    End Function              'CalculateSP

    Public Shared Function CalcTimeToLevel(ByVal cPilot As EveHQPilot, ByVal cSkill As EveSkill, ByVal toLevel As Integer, Optional ByVal fromLevel As Integer = -1) As Long

        'NB Level = 0 indicates to train to the next available level

        Dim pa, sa As Double
        Dim rank, startSP, endSP As Double
        Dim sTime As Long

        ' Get the skill rank
        rank = cSkill.Rank

        ' Get starting skillpoints & level
        Dim startLevel As Integer
        If fromLevel = -1 Then
            If cPilot.PilotSkills.ContainsKey(cSkill.Name) = True Then
                Dim sSkill As EveHQPilotSkill = cPilot.PilotSkills(cSkill.Name)
                startLevel = sSkill.Level
                startSP = sSkill.SP
                If cPilot.Training = True And cPilot.TrainingSkillName = cSkill.Name Then
                    startSP += cPilot.TrainingCurrentSP
                End If
            Else
                startLevel = 0
                startSP = 0
            End If
        Else
            startLevel = fromLevel
            startSP = CalculateSPLevel(CInt(rank), startLevel)
            If startSP < 0 Then MsgBox("eek!!")
        End If

        ' Calculate the end of level required
        Dim sLevel As Integer = 0
        If toLevel = 0 Then
            Do
                sLevel += 1
            Loop Until CalculateSPLevel(CInt(rank), sLevel) > startSP
            If sLevel > 5 Then sLevel = 5
            'EndSP = CalculateSPLevel(CInt(Rank), sLevel)
        Else
            sLevel = toLevel
        End If

        For curLevel As Integer = startLevel + 1 To sLevel
            endSP = CalculateSPLevel(CInt(rank), curLevel)

            ' Calculate the primary attribute
            Select Case cSkill.PA.Substring(0, 1)
                Case "C"
                    pa = cPilot.CAttT
                Case "I"
                    pa = cPilot.IntAttT
                Case "M"
                    pa = cPilot.MAttT
                Case "P"
                    pa = cPilot.PAttT
                Case "W"
                    pa = cPilot.WAttT
            End Select

            ' Calculate the secondary attribute
            Select Case cSkill.SA.Substring(0, 1)
                Case "C"
                    sa = cPilot.CAttT
                Case "I"
                    sa = cPilot.IntAttT
                Case "M"
                    sa = cPilot.MAttT
                Case "P"
                    sa = cPilot.PAttT
                Case "W"
                    sa = cPilot.WAttT
            End Select

            If Math.Abs(pa - 0) < 0.0001 Or Math.Abs(sa - 0) < 0.0001 Then
                sTime = 0
            Else
                sTime = CLng(sTime + Int((endSP - startSP) / (pa + (sa / 2)) * 60))
                ' Double the time if alpha clone
                If cPilot.AccountStatus = APIAccountStatuses.Alpha Then
                    sTime = CLng(sTime * 2)
                End If
            End If

            startSP = endSP
        Next

        Return sTime

    End Function          'CalcTimeToLevel

    Public Shared Function SkillNameToID(ByVal name As String) As Integer
        If HQ.SkillListName.ContainsKey(name) = True Then
            Dim cSkill As EveSkill = HQ.SkillListName(name)
            Return cSkill.ID
        Else
            Return 0
        End If
    End Function            'SkillNameToID

    Public Shared Function SkillIDToName(ByVal id As Integer) As String
        If HQ.SkillListID.ContainsKey(id) = True Then
            Dim cSkill As EveSkill = HQ.SkillListID(id)
            Return cSkill.Name
        Else
            Return ""
        End If
    End Function            'SkillIDToName  

    Public Shared Function CalcNextLevel(ByVal cPilot As EveHQPilot, ByVal cSkill As EveSkill) As Integer

        Dim cLevel As Integer

        ' Get current skill level
        If cPilot.PilotSkills.ContainsKey(cSkill.Name) = True Then
            Dim sSkill As EveHQPilotSkill = cPilot.PilotSkills(cSkill.Name)
            cLevel = sSkill.Level + 1
            If cLevel = 6 Then cLevel = 0
        Else
            cLevel = 1
        End If

        Return cLevel

    End Function            'CalcTimeToLevel

    Public Shared Function CalcCurrentLevel(ByVal cpilot As EveHQPilot, ByVal cskill As EveSkill) As Integer
        Dim cLevel As Integer

        ' Get current skill level
        If cpilot.PilotSkills.ContainsKey(cskill.Name) = True Then
            Dim sSkill As EveHQPilotSkill = cpilot.PilotSkills(cskill.Name)
            cLevel = sSkill.Level
        Else
            cLevel = 0
        End If

        Return cLevel
    End Function

    Public Shared Function CalcLevelFromSP(ByVal skillID As Integer, ByVal sp As Integer) As Integer

        If HQ.SkillListID.ContainsKey(skillID) Then
            Dim cSkill As EveSkill = HQ.SkillListID(skillID)
            Dim curLevel As Integer = 0
            For level As Integer = 0 To 5
                If sp >= cSkill.LevelUp(level) Then
                    curLevel = level
                End If
            Next
            Return curLevel
        End If

        Return 1
    End Function

    Public Shared Function HavePreReqs(ByVal qpilot As EveHQPilot, ByVal skillID As Integer) As Boolean
        ' Sub to ensure we have all the prerequisite skills we require
        ' Skills are added if required

        ' Work out if Skill pre-requisites are needed and add them to the queue
        Dim myPreReqs As String = SkillQueueFunctions.GetSkillReqs(qpilot, skillID)
        Dim preReqs() As String = myPreReqs.Split(ControlChars.Cr)
        Dim preReq As String
        For Each preReq In preReqs
            If preReq.Length <> 0 Then
                Dim pilotLevel As String = preReq.Substring(preReq.Length - 1, 1)
                If pilotLevel <> "Y" Then
                    Return False
                End If
            End If
        Next
        Return True
    End Function

	Public Shared Function IsSkillTrained(ByVal cPilot As EveHQPilot, ByVal skillName As String, Optional ByVal toLevel As Long = 0) As Boolean
        If cPilot.PilotSkills.ContainsKey(skillName) Then
            Dim mySkill As EveHQPilotSkill = cPilot.PilotSkills(skillName)
            Dim myLevel As Integer = CInt(mySkill.Level)
            If myLevel >= toLevel Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Public Shared Function TimeBeforeCanTrain(ByVal rPilot As EveHQPilot, ByVal parentSkillID As Integer, toSkillLevel As Integer) As Double
        Dim usableTime As Double = 0
        Dim fromSkillLevel As Integer = -1
        If rPilot.PilotSkills.ContainsKey(SkillIDToName(parentSkillID)) = False Then
            usableTime = TimeBeforeCanTrain(rPilot, parentSkillID)
        Else
            fromSkillLevel = rPilot.PilotSkills(SkillIDToName(parentSkillID)).Level
        End If
        usableTime += CalcTimeToLevel(rPilot, HQ.SkillListID(parentSkillID), ToSkillLevel, fromSkillLevel)
        Return usableTime
    End Function

    Public Shared Function TimeBeforeCanTrain(ByVal rPilot As EveHQPilot, ByVal parentSkillID As Integer) As Double

        Dim parentSkill As EveSkill = HQ.SkillListID(parentSkillID)
        Dim skillsNeeded As New ArrayList
        Call PreReqTimeBeforeCanTrain(rPilot, parentSkill, 0, skillsNeeded, True)

        If skillsNeeded.Count > 0 Then
            If rPilot.Name <> "" Then
                Dim usableTime As Long = 0
                Dim skillNo As Integer = 0
                If skillsNeeded.Count > 1 Then
                    Do
                        Dim skill As String = CStr(skillsNeeded(skillNo))
                        Dim skillName As String = skill.Substring(0, skill.Length - 1)
                        Dim skillLvl As Integer = CInt(skill.Substring(skill.Length - 1, 1))
                        Dim skillno2 As Integer = skillNo + 1
                        Do
                            If skillno2 < skillsNeeded.Count Then
                                Dim skill2 As String = CStr(skillsNeeded(skillno2))
                                Dim skillName2 As String = skill2.Substring(0, skill2.Length - 1)
                                Dim skillLvl2 As Integer = CInt(skill2.Substring(skill2.Length - 1, 1))
                                If skillName = skillName2 Then
                                    If skillLvl >= skillLvl2 Then
                                        skillsNeeded.RemoveAt(skillno2)
                                    Else
                                        skillsNeeded.RemoveAt(skillNo)
                                        skillNo = -1
                                        Exit Do
                                    End If
                                Else
                                    skillno2 += 1
                                End If
                            End If
                        Loop Until skillno2 >= skillsNeeded.Count
                        skillNo += 1
                    Loop Until skillNo >= skillsNeeded.Count - 1
                    skillsNeeded.Reverse()
                End If
                For Each skill As String In skillsNeeded
                    Dim skillName As String = skill.Substring(0, skill.Length - 1)
                    Dim skillLvl As Integer = CInt(skill.Substring(skill.Length - 1, 1))
                    Dim cSkill As EveSkill = HQ.SkillListName(skillName)
                    usableTime = CLng(usableTime + CalcTimeToLevel(rPilot, cSkill, skillLvl))
                Next
                Return usableTime
            Else
                Return 0
            End If
        Else
            Return 0
        End If

    End Function

    Private Shared Sub PreReqTimeBeforeCanTrain(ByVal rPilot As EveHQPilot, ByVal cSkill As EveSkill, ByVal curLevel As Integer, ByRef skillsNeeded As ArrayList, ByVal rootSkill As Boolean)
        ' Write the skill we are querying as the first (parent) node
        Dim skillTrained As Boolean
        Dim myLevel As Integer
        skillTrained = False
        If HQ.Settings.Pilots.Count > 0 And rPilot.Updated = True Then
            If rPilot.PilotSkills.ContainsKey(cSkill.Name) = True Then
                Dim mySkill As EveHQPilotSkill
                mySkill = rPilot.PilotSkills(cSkill.Name)
                myLevel = CInt(mySkill.Level)
                If myLevel >= curLevel Then skillTrained = True
                If skillTrained = False Then
                    skillsNeeded.Add(cSkill.Name & curLevel)
                End If
            Else
                If rootSkill = False Then
                    skillsNeeded.Add(cSkill.Name & curLevel)
                End If
            End If
        End If
        If cSkill.PreReqSkills.Count > 0 Then
            Dim subSkill As EveSkill
            For Each subSkillID As Integer In cSkill.PreReqSkills.Keys
                subSkill = HQ.SkillListID(subSkillID)
                If subSkill.ID <> cSkill.ID Then
                    Call PreReqTimeBeforeCanTrain(rPilot, subSkill, cSkill.PreReqSkills(subSkillID), skillsNeeded, False)
                End If
            Next
        End If
    End Sub

End Class


<Serializable()> Public Class EveSkill
    Implements ICloneable
    Public ID As Integer
    Public Name As String
    Public Description As String
    Public GroupID As Integer
    Public Published As Boolean
    Public Rank As Integer
    Public SP As Integer
    Public Level As Integer
    Public LevelUp(5) As Integer
    Public Pa As String
    Public Sa As String
    Public PreReqSkills As New Dictionary(Of Integer, Integer) ' SkillID, SkillLevel
    Public BasePrice As Double
    Public Function Clone() As Object Implements ICloneable.Clone
        Return CType(MemberwiseClone(), EveSkill)
    End Function
End Class

<Serializable()> Public Class SkillGroup
    Public ID As Integer
    Public Name As String
End Class

