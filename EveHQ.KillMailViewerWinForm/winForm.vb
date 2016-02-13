﻿''Wyrlwind : wwyrlwind8@gmail.net

Imports System.Globalization
Imports EveHQ.EveData
Imports EveHQ.EveAPI
Imports EveHQ.Core
Imports System.Xml
Imports System.Windows.Forms
Imports EveHQ.Common.Extensions
Imports System.Text
Imports System.Net
Imports System.IO

Public Class winForm
    Private Const KillTimeFormat As String = "yyyy-MM-dd HH:mm:ss"
    ReadOnly _culture As CultureInfo = New CultureInfo("en-GB")
    Dim _kmAccount As New EveHQAccount
    Dim _charName As String = ""
    Dim _charID As String = ""
    ReadOnly _kms As New SortedList(Of Long, KillMail)

    Private Sub winfrmKMV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call UpdateAccounts()

    End Sub

    Private Sub UpdateAccounts()
        cb_UseExistingAccount.BeginUpdate()
        cb_UseExistingAccount.Items.Clear()
        For Each cAccount As EveHQAccount In HQ.Settings.Accounts.Values
            If cAccount.FriendlyName <> "" Then
                cb_UseExistingAccount.Items.Add(cAccount.FriendlyName)
            Else
                cb_UseExistingAccount.Items.Add(cAccount.UserID)
            End If
        Next
        cb_UseExistingAccount.EndUpdate()
    End Sub

    Private Sub rb_UseExistingAccount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles rb_UseExistingAccount.CheckedChanged
        rb_UseExistingAccount.Enabled = rb_UseExistingAccount.Checked
    End Sub

    Private Sub rb_UseSpecificAPI_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rb_UseSpecificAPI.CheckedChanged
        lb_UserID.Enabled = rb_UseSpecificAPI.Checked
        tb_UserID.Enabled = rb_UseSpecificAPI.Checked
        lb_APIKey.Enabled = rb_UseSpecificAPI.Checked
        tb_APIKey.Enabled = rb_UseSpecificAPI.Checked
        lb_APIStatus.Enabled = rb_UseSpecificAPI.Checked
        bt_GetCharacters.Enabled = rb_UseSpecificAPI.Checked
    End Sub

    Private Sub cb_UseExistingAccount_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles cb_UseExistingAccount.SelectedIndexChanged
        Dim itm As ListViewItem
        ' Get the accountID and pilots
        lv_AvaibleCharacters.BeginUpdate()
        lv_AvaibleCharacters.Items.Clear()
        For Each cAccount As EveHQAccount In HQ.Settings.Accounts.Values
            If _
                cAccount.FriendlyName = cb_UseExistingAccount.SelectedItem.ToString Or
                cAccount.UserID = cb_UseExistingAccount.SelectedItem.ToString Then
                _kmAccount = cAccount
                ' Get the list of characters and the character IDs
                If cAccount.Characters IsNot Nothing Then

                    For Each character As String In cAccount.Characters
                        Dim newPilot As New ListViewItem
                        newPilot.Text = character
                        If HQ.Settings.Pilots.ContainsKey(character) Then
                            newPilot.Name = HQ.Settings.Pilots(character).ID
                        Else
                            If HQ.Settings.Corporations.ContainsKey(character) Then
                                newPilot.Name = CStr(HQ.Settings.Corporations(character).ID)
                            Else
                                newPilot.Name = character
                            End If
                        End If
                        lv_AvaibleCharacters.Items.Add(newPilot)

                    Next


                    ' Enable the Fetch Killmails button
                    bt_FetchKillmails.Enabled = True
                    ' Check the account - disable the Get Corp Killmails box for v2
                    Exit For
                Else
                    MessageBox.Show(
                        "The list of characaters in this account is blank. Please connect to the API to get the latest character information.",
                        "Characters required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Next
        lv_AvaibleCharacters.Sort()
        lv_AvaibleCharacters.EndUpdate()
    End Sub

    Private Sub bt_GetCharacters_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bt_GetCharacters.Click

        ' Check for blank UserID and APIKey
        If tb_UserID.Text = "" Or tb_APIKey.Text = "" Then
            MessageBox.Show("UserID and APIKey cannot be blank. Please enter the required information.",
                            "API Information Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Create a dummy account for the information
        _kmAccount = New EveHQAccount
        _kmAccount.UserID = tb_UserID.Text
        _kmAccount.APIKey = tb_APIKey.Text
        _kmAccount.FriendlyName = "Killmail Viewing Account"

        Dim accountCharacters As EveServiceResponse(Of IEnumerable(Of AccountCharacter)) =
                HQ.ApiProvider.Account.Characters(_kmAccount.UserID, _kmAccount.APIKey)


        ' Check for errors
        If _
            accountCharacters.IsFaulted Or accountCharacters.IsSuccessfulHttpStatus = False Or
            accountCharacters.EveErrorCode <> 0 Then

            MessageBox.Show("There was an error retrieving character information from the API.", "API Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            lb_APIStatus.Text = "API Status: Character retrieval failed."
            lb_APIStatus.Refresh()
            Exit Sub

        End If

        If accountCharacters.ResultData IsNot Nothing Then
            ' Seems ok so let's add the characters to the list

            ' Get the list of characters and the character IDs
            lv_AvaibleCharacters.BeginUpdate()
            lv_AvaibleCharacters.Items.Clear()
            For Each character As AccountCharacter In accountCharacters.ResultData
                Dim newPilot As New ListViewItem
                newPilot.Text = character.Name
                newPilot.Name = character.CharacterId.ToInvariantString()
                lv_AvaibleCharacters.Items.Add(newPilot)
            Next
            lv_AvaibleCharacters.Sort()
            lv_AvaibleCharacters.EndUpdate()

            ' Enable the Fetch Killmails button
            bt_FetchKillmails.Enabled = True
            lb_APIStatus.Text = "API Status: Successfully retrieved characters."
            lb_APIStatus.Refresh()
        End If
    End Sub

    Private Sub bt_FetchKillmails_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bt_FetchKillmails.Click

        ' Check we have a character selected
        If lv_AvaibleCharacters.SelectedItems.Count = 0 Then
            MessageBox.Show("You must select a character before fetching the killmails", "Character required",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Get the name and ID of the character
        _charName = lv_AvaibleCharacters.SelectedItems(0).Text
        _charID = lv_AvaibleCharacters.SelectedItems(0).Name

        ' Clear the list of Killmails
        _kms.Clear()
        Dim allKMsDownloaded As Boolean = False
        Dim lastKillID As String = ""
        Dim kmxml As XmlDocument

        Do
            ' Let's try and get the killmail details (use the standard caching method for this)
            Dim _
                apiReq As _
                    New EveAPIRequest(HQ.EveHqapiServerInfo, HQ.RemoteProxy, HQ.Settings.APIFileExtension,
                                      HQ.ApiCacheFolder)
            If _kmAccount.APIKeyType = APIKeyTypes.Corporation Then
                kmxml = apiReq.GetAPIXML(APITypes.KillLogCorp, _kmAccount.ToAPIAccount, _charID, lastKillID,
                                         APIReturnMethods.ReturnStandard)
            Else
                kmxml = apiReq.GetAPIXML(APITypes.KillLogChar, _kmAccount.ToAPIAccount, _charID, lastKillID,
                                         APIReturnMethods.ReturnStandard)
            End If

            ' Check for any Errors
            If kmxml IsNot Nothing Then
                If kmxml.InnerText <> "" Then
                    ' Check XML for any error codes (do we have the full API key?)
                    Dim errlist As XmlNodeList = kmxml.SelectNodes("/eveapi/error")
                    If errlist.Count <> 0 Then
                        allKMsDownloaded = True
                    Else
                        ' Parse the current killmail XML
                        Call ParseKillmailXML(kmxml)
                        ' Get the last killmailID
                        Dim kmList As XmlNodeList = kmxml.SelectNodes("/eveapi/result/rowset/row")
                        If kmList.Count > 0 Then
                            lastKillID = kmList(kmList.Count - 1).Attributes.GetNamedItem("killID").Value
                        Else
                            allKMsDownloaded = True
                        End If
                    End If
                Else
                    If (String.IsNullOrEmpty(lastKillID)) Then
                        MessageBox.Show("A null XML document was returned. Check the API Server and internet connection.",
                                        "API Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Exit Do 'Work around for bug EVEHQ-165: KillLog API only returns up to the last month of data now.
                    End If
                End If
            Else
                allKMsDownloaded = True
            End If
        Loop Until allKMsDownloaded = True

        ' Do a summary of the killmails
        Call DrawKillmailSummary()
    End Sub

    Private Sub ParseKillmailXML(ByVal km As XmlDocument)

        Dim kmList As XmlNodeList
        Dim kmInfo As XmlNode

        ' Get the list of characters and the character IDs
        kmList = km.SelectNodes("/eveapi/result/rowset/row")

        For Each kmInfo In kmList
            ' Start a new KM class
            Dim newKillmail As New KillMail
            newKillmail.KillID = CInt(kmInfo.Attributes.GetNamedItem("killID").Value)
            newKillmail.SystemID = CInt(kmInfo.Attributes.GetNamedItem("solarSystemID").Value)
            newKillmail.KillTime = DateTime.ParseExact(kmInfo.Attributes.GetNamedItem("killTime").Value, KillTimeFormat,
                                                       _culture, DateTimeStyles.None)
            newKillmail.MoonID = kmInfo.Attributes.GetNamedItem("moonID").Value

            ' Parse the victim data
            Dim newVictim As New KillmailVictim
            With kmInfo.ChildNodes(0)
                newVictim.CharID = .Attributes.GetNamedItem("characterID").Value
                newVictim.CharName = .Attributes.GetNamedItem("characterName").Value
                newVictim.CorpID = .Attributes.GetNamedItem("corporationID").Value
                newVictim.CorpName = .Attributes.GetNamedItem("corporationName").Value
                newVictim.AllianceID = .Attributes.GetNamedItem("allianceID").Value
                newVictim.AllianceName = .Attributes.GetNamedItem("allianceName").Value
                newVictim.FactionID = .Attributes.GetNamedItem("factionID").Value
                newVictim.FactionName = .Attributes.GetNamedItem("factionName").Value
                newVictim.DamageTaken = Double.Parse(.Attributes.GetNamedItem("damageTaken").Value, NumberStyles.Any,
                                                     _culture)
                newVictim.ShipTypeID = CInt(.Attributes.GetNamedItem("shipTypeID").Value)
            End With
            newKillmail.Victim = newVictim

            ' Parse the attackers data
            For Each attackerNode As XmlNode In kmInfo.ChildNodes(1).ChildNodes
                Dim newAttacker As New KillmailAttacker
                newAttacker.CharID = attackerNode.Attributes.GetNamedItem("characterID").Value
                newAttacker.CharName = attackerNode.Attributes.GetNamedItem("characterName").Value
                newAttacker.CorpID = attackerNode.Attributes.GetNamedItem("corporationID").Value
                newAttacker.CorpName = attackerNode.Attributes.GetNamedItem("corporationName").Value
                newAttacker.AllianceID = attackerNode.Attributes.GetNamedItem("allianceID").Value
                newAttacker.AllianceName = attackerNode.Attributes.GetNamedItem("allianceName").Value
                newAttacker.FactionID = attackerNode.Attributes.GetNamedItem("factionID").Value
                newAttacker.FactionName = attackerNode.Attributes.GetNamedItem("factionName").Value
                newAttacker.SecStatus = Double.Parse(attackerNode.Attributes.GetNamedItem("securityStatus").Value,
                                                     NumberStyles.Any, _culture)
                newAttacker.DamageDone = Double.Parse(attackerNode.Attributes.GetNamedItem("damageDone").Value,
                                                      NumberStyles.Any, _culture)
                newAttacker.FinalBlow = CBool(attackerNode.Attributes.GetNamedItem("finalBlow").Value)
                newAttacker.WeaponTypeID = CInt(attackerNode.Attributes.GetNamedItem("weaponTypeID").Value)
                newAttacker.ShipTypeID = CInt(attackerNode.Attributes.GetNamedItem("shipTypeID").Value)
                ' Setup the key for sorting
                Dim key As String = Format(newAttacker.DamageDone, "0000000000") & newAttacker.CharName &
                                    newAttacker.CorpID
                newKillmail.Attackers.Add(key, newAttacker)
            Next

            ' Parse the item data
            For Each itemNode As XmlNode In kmInfo.ChildNodes(2).ChildNodes
                Dim newItem As New KillmailItem
                newItem.TypeID = CInt(itemNode.Attributes.GetNamedItem("typeID").Value)
                newItem.Flag = Integer.Parse(itemNode.Attributes.GetNamedItem("flag").Value, NumberStyles.Any, _culture)
                newItem.QtyDropped = Integer.Parse(itemNode.Attributes.GetNamedItem("qtyDropped").Value,
                                                   NumberStyles.Any, _culture)
                newItem.QtyDestroyed = Integer.Parse(itemNode.Attributes.GetNamedItem("qtyDestroyed").Value,
                                                     NumberStyles.Any, _culture)
                newKillmail.Items.Add(newItem)
            Next

            ' Add the killmail to the collection
            Dim temp As New KillMail
            If _kms.TryGetValue(newKillmail.KillID, temp) = False Then
                _kms.Add(newKillmail.KillID, newKillmail)
            End If

        Next
    End Sub

    Private Sub DrawKillmailSummary()
        ' Update the list of killmails
        Dim itm As ListViewItem
        Dim str(4) As String
        ' str(0) = ID
        ' str(1) = Name
        ' str(2) = Kill type
        ' str(3) = Kill date





        lv_KillmailSummary.BeginUpdate()
        'lv_KillmailSummary.Clear()
        For Each charKillmail As KillMail In _kms.Values

            str(0) = CType(charKillmail.KillID, String)
            If charKillmail.Victim.CharName = "" Then
                str(1) = charKillmail.Victim.CorpName
            Else
                str(1) = charKillmail.Victim.CharName
            End If

            str(2) = StaticData.Types(charKillmail.Victim.ShipTypeID).Name
            str(3) = charKillmail.KillTime.ToString

            itm = New ListViewItem(str)
            lv_KillmailSummary.Items.Add(itm)



        Next
        lv_KillmailSummary.EndUpdate()
        ' Update the summary label
        lb_KillmailSummary.Text = "Killmail Summary for " & _charName & " (" & lv_KillmailSummary.Items.Count & " items)"
        ' Clear the killmail detail text
        rtb_KillmailDetails.Text = ""
        'AdvTreeSorter.Sort(adtKillmails, 1, True, True)
    End Sub


    'Private Sub adtKillmails_ColumnHeaderMouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) _
    '    Handles adtKillmails.ColumnHeaderMouseDown
    '    Dim ch As DevComponents.AdvTree.ColumnHeader = CType(sender, DevComponents.AdvTree.ColumnHeader)
    '    AdvTreeSorter.Sort(ch, True, False)
    'End Sub

    'Private Sub lv_KillmailSummary_SelectionChanged(ByVal sender As Object, ByVal e As EventArgs) _
    '    Handles lv_KillmailSummary.SelectionChanged

    '    If adtKillmails.SelectedNodes.Count > 0 Then
    '        ' Get the killID of the selected Killmail
    '        Dim killID As Long = CInt(adtKillmails.SelectedNodes(0).Tag)
    '        Dim selKillmail As New KillMail
    '        If _kms.TryGetValue(killID, selKillmail) Then
    '            ' Write the killmail detail
    '            Call DrawKillmailDetail(selKillmail)
    '            btnCopyKillmail.Enabled = True
    '            btnExportToHQF.Enabled = True
    '        End If

    '    End If
    'End Sub
    Private Sub lv_KillmailSummary_MouseClick(sender As Object, e As MouseEventArgs) Handles lv_KillmailSummary.MouseClick
        If lv_KillmailSummary.SelectedItems.Count = 1 Then
            Dim killID As Long = CType(lv_KillmailSummary.SelectedItems(0).Text, Long)
            Dim selKillmail As New KillMail
            If _kms.TryGetValue(killID, selKillmail) Then
                ' Write the killmail detail
                Call DrawKillmailDetail(selKillmail)
                bt_CopyKillmail.Enabled = True
                bt_ExportToHQF.Enabled = True
            End If
        End If
    End Sub


    Private Sub DrawKillmailDetail(ByVal selKillmail As KillMail)

        ' Write the killmail text to the label
        rtb_KillmailDetails.Text = BuildKillmailDetails(selKillmail)
    End Sub

    Private Function BuildKillmailDetails(ByVal selKillmail As KillMail) As String
        Dim killmailText As New StringBuilder

        ' Write the time
        killmailText.AppendLine(Format(selKillmail.KillTime, "yyyy.MM.dd HH:mm:ss"))
        killmailText.AppendLine("")

        ' Get the solar system details
        Dim ss As SolarSystem = StaticData.SolarSystems(selKillmail.SystemID)

        ' Write the victim details
        If selKillmail.Victim.CharName = "" Then
            killmailText.AppendLine("Victim: " & selKillmail.Victim.CorpName)
        Else
            killmailText.AppendLine("Victim: " & selKillmail.Victim.CharName)
        End If
        killmailText.AppendLine("Corp: " & selKillmail.Victim.CorpName)
        If selKillmail.Victim.AllianceName = "" Then
            killmailText.AppendLine("Alliance: NONE")
        Else
            killmailText.AppendLine("Alliance: " & selKillmail.Victim.AllianceName)
        End If
        If selKillmail.Victim.FactionName = "" Then
            killmailText.AppendLine("Faction: NONE")
        Else
            killmailText.AppendLine("Faction: " & selKillmail.Victim.FactionName)
        End If
        killmailText.AppendLine("Destroyed: " & StaticData.Types(selKillmail.Victim.ShipTypeID).Name)
        killmailText.AppendLine("System: " & ss.Name)
        killmailText.AppendLine("Security: " & Math.Max(ss.Security, 0).ToString("N1"))
        killmailText.AppendLine("Damage Taken: " & selKillmail.Victim.DamageTaken.ToString)

        ' Put the attackers into the correct order
        Dim attackers As New List(Of KillmailAttacker)
        For Each attacker As KillmailAttacker In selKillmail.Attackers.Values
            attackers.Add(attacker)
        Next
        attackers.Reverse()

        ' Write the Attackers details
        killmailText.AppendLine("")
        killmailText.AppendLine("Involved parties:")
        killmailText.AppendLine("")
        For Each attacker As KillmailAttacker In attackers
            If attacker.CharName <> "" Then
                killmailText.Append("Name: " & attacker.CharName)
                If attacker.FinalBlow = True Then
                    killmailText.AppendLine(" (laid the final blow)")
                Else
                    killmailText.AppendLine("")
                End If
                killmailText.AppendLine("Security: " & attacker.SecStatus.ToString("N1"))
                killmailText.AppendLine("Corp: " & attacker.CorpName)
                If attacker.AllianceName = "" Then
                    killmailText.AppendLine("Alliance: NONE")
                Else
                    killmailText.AppendLine("Alliance: " & attacker.AllianceName)
                End If
                If attacker.FactionName = "" Then
                    killmailText.AppendLine("Faction: NONE")
                Else
                    killmailText.AppendLine("Faction: " & attacker.FactionName)
                End If
                If attacker.ShipTypeID = 0 Then
                    killmailText.AppendLine("Ship: Unknown")
                Else
                    killmailText.AppendLine("Ship: " & StaticData.Types(attacker.ShipTypeID).Name)
                End If
                killmailText.AppendLine("Weapon: " & StaticData.Types(attacker.WeaponTypeID).Name)
            Else
                If attacker.CorpName = "" Then
                    killmailText.Append("Name: " & StaticData.Types(attacker.ShipTypeID).Name & " / Unknown")
                Else
                    killmailText.Append(
                        "Name: " & StaticData.Types(attacker.ShipTypeID).Name & " / " & attacker.CorpName)
                End If
                If attacker.FinalBlow = True Then
                    killmailText.AppendLine(" (laid the final blow)")
                Else
                    killmailText.AppendLine("")
                End If
            End If
            killmailText.AppendLine("Damage Done: " & attacker.DamageDone.ToString)
            killmailText.AppendLine("")
        Next

        ' Make a list of dropped and destroyed items
        Dim droppedItems, destroyedItems As New List(Of String)
        Dim itemName As String
        For Each item As KillmailItem In selKillmail.Items
            itemName = StaticData.Types(item.TypeID).Name
            If item.QtyDestroyed > 0 Then

                destroyedItems.Add(itemName & ", Qty: " & item.QtyDestroyed.ToString & " (" & StaticData.ItemMarkers(item.Flag) & ")")

                'If item.QtyDestroyed = 1 Then
                '    If item.Flag = 0 Then
                '        destroyedItems.Add(itemName)
                '    Else
                '        destroyedItems.Add(itemName & " (Cargo)")
                '    End If
                'Else
                '    If item.Flag = 0 Then
                '        destroyedItems.Add(itemName & ", Qty: " & item.QtyDestroyed.ToString)
                '    Else
                '        destroyedItems.Add(itemName & ", Qty: " & item.QtyDestroyed.ToString & " (Cargo)")
                '    End If
                'End If

            End If
            If item.QtyDropped > 0 Then

                droppedItems.Add(itemName & ", Qty: " & item.QtyDropped.ToString & " (" & StaticData.ItemMarkers(item.Flag) & ")")

                'If item.QtyDropped = 1 Then
                '    If item.Flag = 0 Then
                '        droppedItems.Add(itemName)
                '    Else
                '        droppedItems.Add(itemName & " (Cargo)")
                '    End If
                'Else
                '    If item.Flag = 0 Then
                '        droppedItems.Add(itemName & ", Qty: " & item.QtyDropped.ToString)
                '    Else
                '        droppedItems.Add(itemName & ", Qty: " & item.QtyDropped.ToString & " (Cargo)")
                '    End If
                'End If

            End If
        Next

        ' Write the Destroyed items if applicable
        If destroyedItems.Count > 0 Then
            killmailText.AppendLine("Destroyed items:")
            killmailText.AppendLine("")
            For Each dItem As String In destroyedItems
                killmailText.AppendLine(dItem)
            Next
            killmailText.AppendLine("")
        End If

        ' Write the Dropped items if applicable
        If droppedItems.Count > 0 Then
            killmailText.AppendLine("Dropped items:")
            killmailText.AppendLine("")
            For Each dItem As String In droppedItems
                killmailText.AppendLine(dItem)
            Next
            killmailText.AppendLine("")
        End If

        Return killmailText.ToString
    End Function



    'Private Sub bt_UploadToBattleClinic_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bt_UploadToBattleClinic.Click
    '    ' Only do selected KM for now for testing purposes
    '    Const Uri As String = "http://eve.battleclinic.com/killboard/index.php"

    '    If adtKillmails.SelectedNodes.Count > 0 Then
    '        ' Get the killID of the selected Killmail
    '        Dim killID As Long = CLng(adtKillmails.SelectedNodes(0).Tag)
    '        Dim selKillmail As New KillMail
    '        If _kms.TryGetValue(killID, selKillmail) Then
    '            ' Check for valid attackers (i.e. not all NPC ones)
    '            Dim validKillmail As Boolean =
    '                    selKillmail.Attackers.Values.Cast(Of KillmailAttacker)().Any(
    '                        Function(attacker) attacker.CharName <> "")
    '            If validKillmail = False Then
    '                MessageBox.Show(
    '                    "There does not appear to be any valid Attackers on this killmail other than NPCs. The killmail will therefore not be uploaded.",
    '                    "Non-NPC Attackers Required.", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                ' Write the killmail detail
    '                Call UploadKillmail(Uri, BuildKillmailDetails(selKillmail))
    '            End If
    '        End If
    '    End If
    'End Sub

    'Private Sub UploadKillmail(ByVal remoteURL As String, ByVal killmailText As String)

    '    ' Build the POST data
    '    Dim postData As String = ""
    '    postData &= "mail=" & killmailText
    '    postData &= "&option=return"
    '    postData &= "&Submit=Submit"
    '    Try
    '        ' Create the requester
    '        ServicePointManager.DefaultConnectionLimit = 20
    '        ServicePointManager.Expect100Continue = False
    '        ServicePointManager.FindServicePoint(New Uri(remoteURL))
    '        Dim request As HttpWebRequest = CType(WebRequest.Create(remoteURL), HttpWebRequest)
    '        ' Setup proxy server (if required)
    '        Call ProxyServerFunctions.SetupWebProxy(request)
    '        ' Setup request parameters
    '        request.Method = "POST"
    '        request.ContentLength = postData.Length
    '        request.ContentType = "application/x-www-form-urlencoded"
    '        request.Headers.Set(HttpRequestHeader.AcceptEncoding, "identity")
    '        ' Setup a stream to write the HTTP "POST" data
    '        Dim webEncoding As New ASCIIEncoding()
    '        Dim byte1 As Byte() = webEncoding.GetBytes(postData)
    '        Dim newStream As Stream = request.GetRequestStream()
    '        newStream.Write(byte1, 0, byte1.Length)
    '        newStream.Close()
    '        ' Prepare for a response from the server
    '        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
    '        ' Get the stream associated with the response.
    '        Dim receiveStream As Stream = response.GetResponseStream()
    '        ' Pipes the stream to a higher level stream reader with the required encoding format. 
    '        Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)
    '        readStream.ReadToEnd()
    '        ' Need to check here for bad responses!
    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub bt_CopyKillmail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bt_CopyKillmail.Click

    '    If txtKillMailDetails.Text <> "" Then
    '        Try
    '            Clipboard.SetText(txtKillMailDetails.Text)
    '        Catch ex As Exception
    '            MessageBox.Show(
    '                "There was an error copying the killmail details to the clipboard. The error was: " & ex.Message,
    '                "Killmail Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    Else
    '        MessageBox.Show("Please select a killmail before copying to the clipboard", "Killmail Required",
    '                        MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End If
    'End Sub

    'Private Sub bt_ExportToHQFF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles bt_ExportToHQF.Click

    '    If adtKillmails.SelectedNodes.Count > 0 Then

    '        Dim fittedItems As New SortedList(Of Integer, Integer)

    '        ' Get the killID of the selected Killmail
    '        Dim killID As Long = CLng(adtKillmails.SelectedNodes(0).Tag)
    '        Dim selKillmail As New KillMail
    '        If _kms.TryGetValue(killID, selKillmail) Then

    '            ' Make a list of dropped and destroyed items

    '            For Each item As KillmailItem In selKillmail.Items
    '                If (item.QtyDestroyed + item.QtyDropped) > 0 Then
    '                    Select Case item.Flag
    '                        Case 5, 87, 90, 133 To 143 ' Cargo, drone bay & specialised storage
    '                            'fittedItems.Add(item.TypeID, item.QtyDestroyed + item.QtyDropped)
    '                        Case 11 To 18, 19 To 26, 27 To 34, 92 To 99, 125 To 132 ' Low, Mid, High, Rig and Sub slots
    '                            If fittedItems.ContainsKey(item.TypeID) Then
    '                                fittedItems(item.TypeID) += item.QtyDestroyed + item.QtyDropped
    '                            Else
    '                                fittedItems.Add(item.TypeID, item.QtyDestroyed + item.QtyDropped)
    '                            End If
    '                    End Select
    '                End If
    '            Next

    '            ' Create Fitting DNA string
    '            Dim dna As New StringBuilder
    '            dna.Append("fitting://evehq/")
    '            dna.Append(selKillmail.Victim.ShipTypeID.ToString)
    '            For Each item As Integer In fittedItems.Keys
    '                dna.Append(":" & item & "*" & fittedItems(item).ToString)
    '            Next
    '            ' Add a basic loadout name
    '            dna.Append(
    '                "?LoadoutName=" & selKillmail.Victim.CharName & "'s " &
    '                StaticData.Types(selKillmail.Victim.ShipTypeID).Name)

    '            ' Start the HQF plug-in if it's active
    '            Const PluginName As String = "EveHQ Fitter"
    '            Dim myPlugIn As EveHQPlugIn = HQ.Plugins(PluginName)
    '            If myPlugIn.Status = EveHQPlugInStatus.Active Then
    '                Dim mainTab As TabStrip = CType(HQ.MainForm.Controls("tabEveHQMDI"), TabStrip)
    '                Dim tp As TabItem = HQ.GetMdiTab(PluginName)
    '                If tp IsNot Nothing Then
    '                    mainTab.SelectedTab = tp
    '                Else
    '                    Dim plugInForm As Form = myPlugIn.Instance.RunEveHQPlugIn
    '                    plugInForm.MdiParent = HQ.MainForm
    '                    plugInForm.Show()
    '                End If
    '                myPlugIn.Instance.GetPlugInData(dna.ToString, 0)
    '            Else
    '                ' Plug-in is not loaded so best not try to access it!
    '                Dim msg As String = ""
    '                msg &= "The " & myPlugIn.MainMenuText & " Plug-in is not currently active." & ControlChars.CrLf &
    '                       ControlChars.CrLf
    '                msg &= "Please load the plug-in before proceeding."
    '                MessageBox.Show(msg, "Error Starting " & myPlugIn.MainMenuText & " Plug-in!", MessageBoxButtons.OK,
    '                                MessageBoxIcon.Information)
    '            End If
    '        End If

    '    End If
    'End Sub

End Class