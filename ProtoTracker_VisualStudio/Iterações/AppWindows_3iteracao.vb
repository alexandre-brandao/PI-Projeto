Imports MySql.Data.MySqlClient
Public Class ProtoTracker

    Dim CurWidth As Integer = Me.Width
    Dim CurHeight As Integer = Me.Height
    Dim connection As New MySqlConnection("server=localhost;Port=3306;database=altice_labs_db;username=root;password=team11altice")
	
	Private Sub btnCadastro_Click(sender As Object, e As EventArgs) Handles btnCadastro.Click
        currentOption.Height = btnCadastro.Height
        currentOption.Top = btnCadastro.Top
        PanelCadastro.Visible = True
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelRemove.Visible = False

        TagBox.Text = ""
        NomeBox.Text = ""
        IdBox.Text = ""
        ProjetoBox.Text = ""
        Andar.SelectedIndex = -1
        Edif.SelectedIndex = -1


    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        currentOption.Height = btnUpdate.Height
        currentOption.Top = btnUpdate.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelRemove.Visible = False

        IDAtualizarBox.Text = ""
        CodTagBox.Text = ""
        Edif2.SelectedIndex = -1
        Andar2.SelectedIndex = -1

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        currentOption.Height = btnSearch.Height
        currentOption.Top = btnSearch.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = True
        PanelRemove.Visible = False

        CodTagSearchBox.Text = ""
        IDSearchTextBox.Text = ""
        Edif1.SelectedIndex = -1
        Andar1.SelectedIndex = -1

    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        currentOption.Height = btnRemove.Height
        currentOption.Top = btnRemove.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = True

        TextBoxRemoveID.Text = ""
        TextBoxRemoveTag.Text = ""

    End Sub
	
	Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        Dim RatioHeight As Double = (Me.Height - CurHeight) / CurHeight
        Dim RatioWidth As Double = (Me.Width - CurWidth) / CurWidth

        For Each Ctrl As Control In Controls
            Ctrl.Width += Ctrl.Width * RatioWidth
            Ctrl.Left += Ctrl.Left * RatioWidth
            Ctrl.Top += Ctrl.Top * RatioHeight
            Ctrl.Height += Ctrl.Height * RatioHeight
        Next

        CurHeight = Me.Height
        CurWidth = Me.Width

    End Sub
	
	Private Sub btnRegUtil_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        PanelLogin.Visible = False
        PanelRegisto.Visible = True
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False

        NomeUtilBox.Text = ""
        PassBox.Text = ""
        EmailUtilBox.Text = ""
        TelemovelBox.Text = ""

    End Sub

    Private Sub btnRegistUtil_click(sender As Object, e As EventArgs) Handles Registar_btn.Click

        Dim command As New MySqlCommand("SELECT * FROM User where email = @mail", connection)

        command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = EmailUtilBox.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If NomeUtilBox.Text = "" Or PassBox.Text = "" Or EmailUtilBox.Text = "" Then

            MessageBox.Show("Por favor preencha todos os campos necessários!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows.Count() > 0 Then

            MessageBox.Show("Email já registado!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        Else

            Dim command2 As New MySqlCommand("INSERT INTO `User`(`name`,`password`,`email`,`phone`,`access`) VALUES (@nm,@pass,@mail,@tel,@acc)", connection)

            command2.Parameters.Add("nm", MySqlDbType.VarChar).Value = NomeUtilBox.Text
            command2.Parameters.Add("pass", MySqlDbType.VarChar).Value = PassBox.Text
            command2.Parameters.Add("mail", MySqlDbType.VarChar).Value = EmailUtilBox.Text
            command2.Parameters.Add("tel", MySqlDbType.VarChar).Value = TelemovelBox.Text
            If admin.Checked Then
                command2.Parameters.Add("acc", MySqlDbType.VarChar).Value = "1"
            Else
                command2.Parameters.Add("acc", MySqlDbType.VarChar).Value = "0"
            End If

            connection.Open()

            If command2.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Utilizador registado com sucesso!")
                TextBoxEmail.Text = EmailUtilBox.Text
                TextBoxPassword.Text = ""
            Else
                MessageBox.Show("Erro no registo")
            End If

            connection.Close()

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        End If

        NomeUtilBox.Text = ""
        PassBox.Text = ""
        EmailUtilBox.Text = ""
        TelemovelBox.Text = ""

    End Sub
	
	Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click

        Dim command As New MySqlCommand("SELECT * FROM User where email = @mail", connection)

        command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = TextBoxEmail.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If TextBoxEmail.Text = "" And TextBoxPassword.Text = "" Then

            Dim iExit As DialogResult

            iExit = MessageBox.Show("Introduza Email e Password!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False


        ElseIf table.Rows.Count = 0 Then

            MessageBox.Show("Utilizador não Registado!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows(0)(1).ToString() = TextBoxPassword.Text Then

            If table.Rows(0)(4) = "1" Then
                PanelLogin.Visible = False
                PanelRegisto.Visible = False
                PanelCadastro.Visible = True
                PanelUpdate.Visible = False
                PanelSearch.Visible = False
                PanelMostrarLoc.Visible = False
                PanelRemove.Visible = False
                PanelLeftBlock.Visible = False
                PanelLeft.Visible = True

            Else

                PanelLogin.Visible = False
                PanelRegisto.Visible = False
                PanelCadastro.Visible = False
                PanelUpdate.Visible = True
                PanelSearch.Visible = False
                PanelMostrarLoc.Visible = False
                PanelRemove.Visible = False
                PanelLeftBlock.Visible = True
                PanelLeft.Visible = False

            End If

        Else

            MessageBox.Show("Password Incorreta!")

            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub VoltarRegisto(sender As Object, e As EventArgs) Handles PictureBox2.Click
        PanelLogin.Visible = True
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = False
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False
    End Sub
	
	Private Sub FecharAPP(sender As Object, e As EventArgs) Handles PictureBox9.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub
	
	Private Sub FecharAppReg(sender As Object, e As EventArgs) Handles PictureBox10.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = True
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub

    Private Sub FecharAppCad(sender As Object, e As EventArgs) Handles PictureBox11.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub

    Private Sub FecharAppUpdt(sender As Object, e As EventArgs) Handles PictureBox12.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = True
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub

    Private Sub FecharAppProc(sender As Object, e As EventArgs) Handles PictureBox13.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = True
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub

    Private Sub FecharAppRem(sender As Object, e As EventArgs) Handles PictureBox14.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = True
        End If

    End Sub
	
	Private Sub Close_btn_hist(sender As Object, e As EventArgs) Handles PictureBox18.Click

        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelMostrarProt.Visible = True
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub close_app_Loc(sender As Object, e As EventArgs) Handles PictureBox15.Click
        Dim SairAPP As New DialogResult
        SairAPP = MessageBox.Show("Deseja fechar a aplicação?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairAPP = DialogResult.Yes Then
            Application.Exit()
        End If

        If SairAPP = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
            PanelMostrarLoc.Visible = True
            PanelMostrarProt.Visible = False
        End If
    End Sub
	
	Private Sub btnBack_click(sender As Object, e As EventArgs) Handles PictureBox7.Click

        Dim SairSessão As New DialogResult
        SairSessão = MessageBox.Show("Deseja terminar sessao?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairSessão = DialogResult.Yes Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

            TextBoxEmail.Text = ""
            TextBoxPassword.Text = ""

        End If

        If SairSessão = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If
    End Sub
	
	Private Sub btnCadastrar_click(sender As Object, e As EventArgs) Handles btnConfirmRegister.Click

        Dim Location As String
        Location = "Building " & Edif.SelectedItem & ", Floor " & Andar.SelectedItem

        Dim command1 As New MySqlCommand("SELECT * FROM Prototype where tag_code = @cod_tag", connection)

        command1.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = TagBox.Text

        Dim adapter As New MySqlDataAdapter(command1)
        Dim table As New DataTable()
        adapter.Fill(table)

        Dim command3 As New MySqlCommand("SELECT * FROM Prototype where prototype_id = @id", connection)

        command3.Parameters.Add("@id", MySqlDbType.VarChar).Value = IdBox.Text

        Dim adapter1 As New MySqlDataAdapter(command3)
        Dim table1 As New DataTable()
        adapter1.Fill(table1)

        If TagBox.Text = "" Or NomeBox.Text = "" Or IdBox.Text = "" Or Location = "" Or (Edif.SelectedIndex < 0 Or Andar.SelectedIndex < 0) Then

            MessageBox.Show("Por favor preencha todos os campos necessários!")
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table.Rows.Count() > 0 Then

            MessageBox.Show("Código Tag já associado a um protótipo!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

        ElseIf table1.Rows.Count() > 0 Then

            MessageBox.Show("Id já associado a um protótipo!")

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False


        Else

            Dim Command As New MySqlCommand("INSERT INTO `Prototype`(`tag_code`,`name`,`prototype_id`,`project`,`location`,`name_reg`,`date_reg`,`device`) VALUES (@cod,@nome,@id,@proj,@loc,@Rg_mail,@Rg_data,@dev)", connection)

            Command.Parameters.Add("cod", MySqlDbType.VarChar).Value = TagBox.Text
            Command.Parameters.Add("nome", MySqlDbType.VarChar).Value = NomeBox.Text
            Command.Parameters.Add("id", MySqlDbType.VarChar).Value = IdBox.Text
            Command.Parameters.Add("proj", MySqlDbType.VarChar).Value = ProjetoBox.Text
            Command.Parameters.Add("loc", MySqlDbType.VarChar).Value = Location

            Command.Parameters.Add("Rg_mail", MySqlDbType.VarChar).Value = TextBoxEmail.Text
            Command.Parameters.Add("Rg_data", MySqlDbType.DateTime).Value = Now
            Command.Parameters.Add("dev", MySqlDbType.VarChar).Value = "Aplicação Windows"

            Dim command2 As New MySqlCommand("INSERT INTO `History`(`date`,`tag_code`,`location`) VALUES (@data,@tag,@loc2)", connection)

            command2.Parameters.Add("data", MySqlDbType.DateTime).Value = Now
            command2.Parameters.Add("tag", MySqlDbType.VarChar).Value = TagBox.Text
            command2.Parameters.Add("loc2", MySqlDbType.VarChar).Value = Location

            connection.Open()

            If Command.ExecuteNonQuery() = 1 And command2.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Protótipo cadastrado com sucesso!")

                TagBox.Text = ""
                NomeBox.Text = ""
                IdBox.Text = ""
                ProjetoBox.Text = ""
                Andar.SelectedIndex = -1
                Edif.SelectedIndex = -1
            Else
                MessageBox.Show("Erro no cadastro!")
            End If

            connection.Close()

            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = True
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub
	
	Private Sub btnAtualizar(sender As Object, e As EventArgs) Handles btnConfirmUpdate.Click


        If CodTagBox.Text = "" Or IDAtualizarBox.Text = "" Then

            MessageBox.Show("Por favor preencha os campos Código Tag e ID!")

        ElseIf Edif2.SelectedIndex < 0 Or Andar2.SelectedIndex < 0 Then

            MessageBox.Show("Preencha a Localização!")

        Else

            Dim command1 As New MySqlCommand("SELECT * FROM Prototype where tag_code = @cod_tag And prototype_id = @id", connection)

            command1.Parameters.Add("@cod_tag", MySqlDbType.VarChar).Value = CodTagBox.Text
            command1.Parameters.Add("@id", MySqlDbType.VarChar).Value = IDAtualizarBox.Text

            Dim adapter1 As New MySqlDataAdapter(command1)
            Dim table1 As New DataTable()
            adapter1.Fill(table1)

            Dim Location2 As String
            Location2 = "Building " & Edif2.SelectedItem & ", Floor " & Andar2.SelectedItem

            If table1.Rows.Count() = 0 Then

                MessageBox.Show("Nenhum protótipo encontrado com o Código Tag e ID introduzido!")

            Else

                Dim command3 As New MySqlCommand("UPDATE `prototype` SET `location`=@loc, `device`=@dev WHERE `tag_code`=@tag", connection)

                command3.Parameters.Add("@tag", MySqlDbType.VarChar).Value = CodTagBox.Text
                command3.Parameters.Add("@loc", MySqlDbType.VarChar).Value = Location2
                command3.Parameters.Add("dev", MySqlDbType.VarChar).Value = "Aplicação Windows"

                connection.Open()
                If command3.ExecuteNonQuery() Then

                    MessageBox.Show("Localização atualizada com sucesso!")

                End If
                connection.Close()

                Dim command4 As New MySqlCommand("INSERT INTO `History`(`date`,`tag_code`,`location`) VALUES (@data,@tag2,@loc2)", connection)

                command4.Parameters.Add("data", MySqlDbType.DateTime).Value = Now
                command4.Parameters.Add("tag2", MySqlDbType.VarChar).Value = CodTagBox.Text
                command4.Parameters.Add("loc2", MySqlDbType.VarChar).Value = Location2

                connection.Open()
                If command4.ExecuteNonQuery() Then
                    CodTagBox.Text = ""
                    IDAtualizarBox.Text = ""
                    Edif2.SelectedIndex = -1
                    Andar2.SelectedIndex = -1
                End If
                connection.Close()

            End If

        End If

        PanelLogin.Visible = False
        PanelRegisto.Visible = False
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelMostrarLoc.Visible = False
        PanelRemove.Visible = False

    End Sub
	
	Private Sub btn_update_block(sender As Object, e As EventArgs) Handles Button1.Click
        Panel2.Height = Button1.Height
        Panel2.Top = Button1.Top
        PanelCadastro.Visible = False
        PanelUpdate.Visible = True
        PanelSearch.Visible = False
        PanelRemove.Visible = False
    End Sub
	
	Private Sub btn_backblock(sender As Object, e As EventArgs) Handles PictureBox17.Click

        Dim SairSessão As New DialogResult
        SairSessão = MessageBox.Show("Deseja terminar sessao?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If SairSessão = DialogResult.Yes Then
            PanelLogin.Visible = True
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = False
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False

            TextBoxEmail.Text = ""
            TextBoxPassword.Text = ""

        End If

        If SairSessão = DialogResult.No Then
            PanelLogin.Visible = False
            PanelRegisto.Visible = False
            PanelCadastro.Visible = False
            PanelUpdate.Visible = True
            PanelSearch.Visible = False
            PanelMostrarLoc.Visible = False
            PanelRemove.Visible = False
        End If

    End Sub

End Class
	
	
	
	