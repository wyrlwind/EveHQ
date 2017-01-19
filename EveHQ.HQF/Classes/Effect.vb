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

Public Class Effect
    Public AffectingAtt As Integer
    Public AffectingType As HQFEffectType
    Public AffectingID As Integer
    Public AffectedAtt As Integer
    Public AffectedType As HQFEffectType
    Public AffectedID As New List(Of Integer)
    Public StackNerf As EffectStackType
    Public IsPerLevel As Boolean
    Public CalcType As EffectCalcType
    Public Status As Integer
End Class

<Serializable()> Public Class ShipEffect
    Public ShipID As Integer
    Public AffectingType As HQFEffectType
    Public AffectingID As Integer
    Public AffectedAtt As Integer
    Public AffectedType As HQFEffectType
    Public AffectedID As New List(Of Integer)
    Public StackNerf As EffectStackType
    Public IsPerLevel As Boolean
    Public CalcType As EffectCalcType
    Public Status As Integer
    Public Value As Double

    Public Function Clone() As ShipEffect
        Dim newEffect As New ShipEffect
        newEffect.ShipID = ShipID
        newEffect.AffectingType = AffectingType
        newEffect.AffectingID = AffectingID
        newEffect.AffectedAtt = AffectedAtt
        newEffect.AffectedType = AffectedType
        For Each id As Integer In AffectedID
            newEffect.AffectedID.Add(id)
        Next
        newEffect.StackNerf = StackNerf
        newEffect.IsPerLevel = IsPerLevel
        newEffect.CalcType = CalcType
        newEffect.Status = Status
        newEffect.Value = Value
        Return newEffect
    End Function

End Class

Public Class ImplantEffect
    Public ImplantName As String
    Public AffectingAtt As Integer
    Public AffectedAtt As Integer
    Public AffectedType As HQFEffectType
    Public AffectedID As New List(Of Integer)
    Public CalcType As EffectCalcType
    Public Status As Integer
    Public Value As Double
    Public IsGang As Boolean
    Public Groups As New ArrayList
End Class

<Serializable()> Public Class FinalEffect
    Public AffectedAtt As Integer
    Public AffectedType As HQFEffectType
    Public AffectedID As New List(Of Integer)
    Public AffectedValue As Double
    Public StackNerf As EffectStackType
    Public Cause As String
    Public CalcType As EffectCalcType
    Public Status As Integer
    Public CauseModule As ShipModule
End Class

Public Enum HQFEffectType As Integer
    All = 0 ' Role bonus for ship affecting type
    Item = 1 ' Skill bonus for ship affecting type
    Group = 2 ' Misc. bonus for ship affecting type
    Category = 3
    MarketGroup = 4
    Skill = 5
    Slot = 6
    Attribute = 7
End Enum

Public Enum EffectCalcType As Integer
    Percentage = 0 ' Simply percentage variation (+/-)
    Addition = 1 ' For adding values
    Difference = 2 ' For resistances
    Velocity = 3 ' For AB/MWD calculations
    Absolute = 4 ' For setting values
    Multiplier = 5 ' Damage multiplier
    AddPositive = 6 ' Adding positive values only
    AddNegative = 7 ' Adding negative values only
    Subtraction = 8 ' Subtracting positive values
    CloakedVelocity = 9 ' Bonus for dealing with cloaked velocity
    SkillLevel = 10 ' Add one to the existing value
    SkillLevelxAtt = 11 ' Multiply the attribute by the skill level
    AbsoluteMax = 12 ' Set value only if higher than the existing value
    AbsoluteMin = 13 ' Set value only if lower than the existing value
    CapBoosters = 14 ' For cap and fueled shield boosters
    ResistanceKiller = 15 ' For resistance killing 
    HullResistanceKiller = 16 ' For hull resistance killing 
End Enum

Public Enum EffectStackType As Integer
    None = 0
    Standard = 1
    Group = 2
    Group2 = 3
End Enum

