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


''' <summary>
''' Class to store an itemID together with a state
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class ModuleWithState
    ' ReSharper disable InconsistentNaming - For MS serialization compatability
    Dim cID As String
    Dim cChargeID As String
    Dim cState As ModuleStates
    ' ReSharper restore InconsistentNaming

    ''' <summary>
    ''' Gets or sets the ID of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the typeID of the module</returns>
    ''' <remarks></remarks>
    Public Property ID() As String
        Get
            Return cID
        End Get
        Set(ByVal value As String)
            cID = value
        End Set
    End Property

    Public Property ChargeID() As String
        Get
            Return cChargeID
        End Get
        Set(ByVal value As String)
            cChargeID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the module</returns>
    ''' <remarks></remarks>
    Public Property State() As ModuleStates
        Get
            Return cState
        End Get
        Set(ByVal value As ModuleStates)
            cState = value
        End Set
    End Property

    ''' <summary>
    ''' Creates a new ModuleWithState
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' Default constructor
    End Sub

    ''' <summary>
    ''' Creates a new ModuleWithState with the complete data
    ''' </summary>
    ''' <param name="moduleID">The ID of the module</param>
    ''' <param name="moduleChargeID">The ID of the loaded charge</param>
    ''' <param name="moduleState">The state of the module</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal moduleID As String, ByVal moduleChargeID As String, ByVal moduleState As ModuleStates)
        ' Parse the data
        cID = moduleID
        cChargeID = moduleChargeID
        cState = ModuleState
    End Sub

End Class

''' <summary>
''' Class to store an itemID together with a state and quantity
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class ModuleQWithState
    ' ReSharper disable InconsistentNaming - For MS serialization compatability
    Dim cID As String
    Dim cState As ModuleStates
    Dim cQuantity As Integer
    ' ReSharper restore InconsistentNaming

    ''' <summary>
    ''' Gets or sets the ID of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the typeID of the module</returns>
    ''' <remarks></remarks>
    Public Property ID() As String
        Get
            Return cID
        End Get
        Set(ByVal value As String)
            cID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the module</returns>
    ''' <remarks></remarks>
    Public Property State() As ModuleStates
        Get
            Return cState
        End Get
        Set(ByVal value As ModuleStates)
            cState = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the quantity of modules
    ''' </summary>
    ''' <value></value>
    ''' <returns>An integer containing the number of modules in the collection</returns>
    ''' <remarks></remarks>
    Public Property Quantity() As Integer
        Get
            Return cQuantity
        End Get
        Set(ByVal value As Integer)
            cQuantity = value
        End Set
    End Property

    ''' <summary>
    ''' Creates a new ModuleQWithState
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' Default constructor
    End Sub

    ''' <summary>
    ''' Creates a new ModuleQState with the complete data
    ''' </summary>
    ''' <param name="moduleID">The ID of the module</param>
    ''' <param name="moduleState">The state of the module</param>
    ''' <param name="moduleQuantity">The quantity of the modules in the group</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal moduleID As String, ByVal moduleState As ModuleStates, ByVal moduleQuantity As Integer)
        ' Parse the data
        cID = moduleID
        cState = moduleState
        cQuantity = ModuleQuantity
    End Sub

End Class

''' <summary>
''' Class to store an itemID together with a state and quantity and fighter settings
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class ModuleFighterWithState
    ' ReSharper disable InconsistentNaming - For MS serialization compatability
    Dim cID As String
    Dim cState As ModuleStates
    Dim cQuantity As Integer
    Dim cTurretActve As Boolean
    Dim cMissileActive As Boolean
    Dim cBombActive As Boolean
    ' ReSharper restore InconsistentNaming

    ''' <summary>
    ''' Gets or sets the ID of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the typeID of the module</returns>
    ''' <remarks></remarks>
    Public Property ID() As String
        Get
            Return cID
        End Get
        Set(ByVal value As String)
            cID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the module
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the module</returns>
    ''' <remarks></remarks>
    Public Property State() As ModuleStates
        Get
            Return cState
        End Get
        Set(ByVal value As ModuleStates)
            cState = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the quantity of modules
    ''' </summary>
    ''' <value></value>
    ''' <returns>An integer containing the number of modules in the collection</returns>
    ''' <remarks></remarks>
    Public Property Quantity() As Integer
        Get
            Return cQuantity
        End Get
        Set(ByVal value As Integer)
            cQuantity = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the turret ability
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the turret ability</returns>
    ''' <remarks></remarks>
    Public Property TurretState() As Boolean
        Get
            Return cTurretActve
        End Get
        Set(ByVal value As Boolean)
            cTurretActve = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the missile ability
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the missile ability</returns>
    ''' <remarks></remarks>
    Public Property MissileState() As Boolean
        Get
            Return cMissileActive
        End Get
        Set(ByVal value As Boolean)
            cMissileActive = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state of the bomb ability
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value containing the state of the bomb ability</returns>
    ''' <remarks></remarks>
    Public Property BombState() As Boolean
        Get
            Return cBombActive
        End Get
        Set(ByVal value As Boolean)
            cBombActive = value
        End Set
    End Property

    ''' <summary>
    ''' Creates a new ModuleFighterWithState
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        ' Default constructor
    End Sub

    ''' <summary>
    ''' Creates a new ModuleFighterWithState with the complete data
    ''' </summary>
    ''' <param name="moduleID">The ID of the module</param>
    ''' <param name="moduleState">The state of the module</param>
    ''' <param name="moduleQuantity">The quantity of the modules in the group</param>
    ''' <param name="moduleTurretActive">The state of the turret ability</param>
    ''' <param name="moduleMissileActive">The state of the missile ability</param>
    ''' <param name="moduleBombActive">The state of the bomb ability</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal moduleID As String, ByVal moduleState As ModuleStates, ByVal moduleQuantity As Integer, ByVal moduleTurretActive As Boolean, ByVal moduleMissileActive As Boolean, ByVal moduleBombActive As Boolean)
        ' Parse the data
        cID = moduleID
        cState = moduleState
        cQuantity = moduleQuantity
        cTurretActve = moduleTurretActive
        cMissileActive = moduleMissileActive
        cBombActive = moduleBombActive
    End Sub

End Class

''' <summary>
''' Enumeration containing the various states a ShipModule can exist in
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Enum ModuleStates
    Offline = 1
    Inactive = 2
    Active = 4
    Overloaded = 8
    Gang = 16
    Fleet = 32
End Enum

''' <summary>
''' Class to store details of a fleet effect applied to the fitting
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class FleetEffect
    ' ReSharper disable InconsistentNaming - For MS serialization compatability
    Dim cFleetPosition As FleetPositions
    Dim cFittingName As String
    Dim cPilotName As String
    Dim cImplants As List(Of ModuleWithState)
    Dim cIsActive As Boolean
    ' ReSharper restore InconsistentNaming

    ''' <summary>
    ''' Gets or sets the position in the fleet of the effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A value indicating the position in the fleet of the effect</returns>
    ''' <remarks></remarks>
    Public Property FleetPosition() As FleetPositions
        Get
            Return cFleetPosition
        End Get
        Set(ByVal value As FleetPositions)
            cFleetPosition = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the fitting for the fleet effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the name of the fitting for the fleet effect</returns>
    ''' <remarks></remarks>
    Public Property FittingName() As String
        Get
            Return cFittingName
        End Get
        Set(ByVal value As String)
            cFittingName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the pilot for the fleet effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the name of the pilot for the fleet effect</returns>
    ''' <remarks></remarks>
    Public Property PilotName() As String
        Get
            Return cPilotName
        End Get
        Set(ByVal value As String)
            cPilotName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a collection of implants to be used by the pilot for the fleet effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A collection of implants to be used by the pilot for the fleet effect</returns>
    ''' <remarks></remarks>
    Public Property Implants() As List(Of ModuleWithState)
        Get
            Return cImplants
        End Get
        Set(ByVal value As List(Of ModuleWithState))
            cImplants = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the fleet effect is active
    ''' </summary>
    ''' <value></value>
    ''' <returns>A boolean value indication whether the fleet effect is active</returns>
    ''' <remarks></remarks>
    Public Property IsActive() As Boolean
        Get
            Return cIsActive
        End Get
        Set(ByVal value As Boolean)
            cIsActive = value
        End Set
    End Property

End Class

''' <summary>
''' Enumeration containing the possible positions of gang boosters/commanders
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Enum FleetPositions As Integer
    Fleet = 1
    Wing = 2
    Squad = 3
End Enum

''' <summary>
''' Class to store details of a remote effect applied to the fitting
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class RemoteEffect
    ' ReSharper disable InconsistentNaming - For MS serialization compatability
    Dim cFittingName As String
    Dim cPilotName As String
    Dim cImplants As List(Of ModuleWithState)
    Dim cModules As List(Of ModuleWithState)
    Dim cIsActive As Boolean
    ' ReSharper restore InconsistentNaming

    ''' <summary>
    ''' Gets or sets the name of the fitting for the remote effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the name of the fitting for the remote effect</returns>
    ''' <remarks></remarks>
    Public Property FittingName() As String
        Get
            Return cFittingName
        End Get
        Set(ByVal value As String)
            cFittingName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the pilot for the remote effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A string containing the name of the pilot for the remote effect</returns>
    ''' <remarks></remarks>
    Public Property PilotName() As String
        Get
            Return cPilotName
        End Get
        Set(ByVal value As String)
            cPilotName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a collection of implants to be used by the pilot for the remote effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A collection of implants to be used by the pilot for the remote effect</returns>
    ''' <remarks></remarks>
    Public Property Implants() As List(Of ModuleWithState)
        Get
            Return cImplants
        End Get
        Set(ByVal value As List(Of ModuleWithState))
            cImplants = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a collection of modules to be used for the remote effect
    ''' </summary>
    ''' <value></value>
    ''' <returns>A collection of modules to be used for the remote effect</returns>
    ''' <remarks></remarks>
    Public Property Modules() As List(Of ModuleWithState)
        Get
            Return cModules
        End Get
        Set(ByVal value As List(Of ModuleWithState))
            cModules = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the remote effect is active
    ''' </summary>
    ''' <value></value>
    ''' <returns>A boolean value indication whether the remote effect is active</returns>
    ''' <remarks></remarks>
    Public Property IsActive() As Boolean
        Get
            Return cIsActive
        End Get
        Set(ByVal value As Boolean)
            cIsActive = value
        End Set
    End Property

End Class