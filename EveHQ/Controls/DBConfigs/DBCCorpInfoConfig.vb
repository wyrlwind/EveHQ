Imports EveHQ.Controls.DBControls

Namespace Controls.DBConfigs
    Public Class DBCCorpInfoConfig
#Region "Properties"

        Dim _dbWidget As New DBCCorpInfo
        Public Property DBWidget() As DBCCorpInfo
            Get
                Return _dbWidget
            End Get
            Set(ByVal value As DBCCorpInfo)
                _dbWidget = value
                Call SetControlInfo()
            End Set
        End Property

#End Region

        Private Sub SetControlInfo()
            If cboCorps.Items.Contains(_dbWidget.DefaultCorpName) = True Then
                cboCorps.SelectedItem = _dbWidget.DefaultCorpName
            Else
                If cboCorps.Items.Count > 0 Then
                    cboCorps.SelectedIndex = 0
                End If
            End If
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            ' Just close the form and do nothing
            Close()
        End Sub

        Private Sub btnAccept_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAccept.Click
            ' Update the control properties
            If cboCorps.SelectedItem Is Nothing Then
                MessageBox.Show("You must select a valid corporation before adding this widget.", "Corporation Required", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If cboCorps.SelectedItem IsNot Nothing Then
                _dbWidget.DefaultCorpName = cboCorps.SelectedItem.ToString
            End If
            ' Now close the form
            DialogResult = DialogResult.OK
            Close()
        End Sub

    End Class
End Namespace
